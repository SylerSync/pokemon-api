using System.Diagnostics;
using System.Globalization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Core.Infrastructure;

public sealed class MongoSeedOptions
{
    /// <summary>
    /// Folder containing *.jsonl / *.ndjson seed files. A relative path is resolved against the
    /// working directory (project root during `dotnet run`) and then the build output directory.
    /// </summary>
    public string Folder { get; set; } = "MongoSeedData";

    /// <summary>
    /// false (default): a collection is only seeded when it has no documents, so restarts don't duplicate data.
    /// true: matching collections are dropped and re-seeded on every run — handy while iterating on seed files.
    /// </summary>
    public bool DropExistingCollections { get; set; }

    /// <summary>
    /// true: rewrite every field name in the seed data to camelCase before inserting, so PascalCase seed
    /// files line up with EF's UseCamelCaseNamingConvention() / the driver's CamelCaseElementNameConvention.
    /// "_id" and $-prefixed keys are left alone. Only the first character is lowered ("ID" -> "iD"),
    /// which matches the driver convention; set element names explicitly for all-caps property names.
    /// </summary>
    public bool CamelCaseFieldNames { get; set; }

    public int BatchSize { get; set; } = 1000;
}

/// <summary>
/// Seeds MongoDB from newline-delimited JSON files. Each file maps to the collection named after it:
/// MongoSeedData/pokemon.jsonl -> "pokemon" collection. Lines are parsed as MongoDB Extended JSON,
/// so {"$oid": "..."} and friends deserialize to their proper BSON types.
/// </summary>
public sealed class MongoSeeder
{
    private static readonly string[] Extensions = { ".jsonl", ".ndjson" };

    private readonly IMongoDatabase _database;
    private readonly MongoSeedOptions _options;
    private readonly ILogger<MongoSeeder> _logger;

    public MongoSeeder(IMongoDatabase database, IOptions<MongoSeedOptions> options, ILogger<MongoSeeder> logger)
    {
        _database = database;
        _options = options.Value;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken ct = default)
    {
        var folder = ResolveFolder(_options.Folder, out var searched);
        if (folder is null)
        {
            _logger.LogWarning("Mongo seed folder '{Folder}' not found — nothing to seed. Looked in: {Searched}",
                _options.Folder, string.Join(" | ", searched));
            return;
        }

        var collections = Directory.EnumerateFiles(folder)
            .Where(f => Extensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
            .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
            .GroupBy(f => Path.GetFileNameWithoutExtension(f)!, StringComparer.Ordinal)
            .ToList();

        if (collections.Count == 0)
        {
            _logger.LogWarning("No .jsonl/.ndjson files found in '{Folder}' — nothing to seed.", folder);
            return;
        }

        _logger.LogInformation("Seeding database '{Database}' from '{Folder}'.",
            _database.DatabaseNamespace.DatabaseName, folder);

        foreach (var group in collections)
            await SeedCollectionAsync(group.Key, group.ToList(), ct);
    }

    private async Task SeedCollectionAsync(string collectionName, IReadOnlyList<string> files, CancellationToken ct)
    {
        var collection = _database.GetCollection<BsonDocument>(collectionName);

        if (_options.DropExistingCollections)
        {
            await _database.DropCollectionAsync(collectionName, ct);
        }
        else if (await collection.EstimatedDocumentCountAsync(cancellationToken: ct) > 0)
        {
            _logger.LogInformation("Collection '{Collection}' already has documents — skipping seed.", collectionName);
            return;
        }

        var stopwatch = Stopwatch.StartNew();
        var total = 0;

        foreach (var file in files)
            total += await InsertFileAsync(collection, file, ct);

        _logger.LogInformation("Seeded {Count} documents into '{Collection}' in {Elapsed:0.0}s.",
            total, collectionName, stopwatch.Elapsed.TotalSeconds);
    }

    private async Task<int> InsertFileAsync(IMongoCollection<BsonDocument> collection, string file, CancellationToken ct)
    {
        var batch = new List<BsonDocument>(_options.BatchSize);
        var inserted = 0;
        var lineNumber = 0;

        using var reader = new StreamReader(file);
        while (await reader.ReadLineAsync() is { } line)
        {
            ct.ThrowIfCancellationRequested();
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line))
                continue;

            BsonDocument document;
            try
            {
                document = BsonDocument.Parse(line);
            }
            catch (Exception ex)
            {
                throw new FormatException($"{Path.GetFileName(file)}, line {lineNumber}: not a valid JSON document.", ex);
            }

            batch.Add(_options.CamelCaseFieldNames ? (BsonDocument)ToCamelCase(document) : document);

            if (batch.Count == _options.BatchSize)
            {
                await collection.InsertManyAsync(batch, cancellationToken: ct);
                inserted += batch.Count;
                batch.Clear();
            }
        }

        if (batch.Count > 0)
        {
            await collection.InsertManyAsync(batch, cancellationToken: ct);
            inserted += batch.Count;
        }

        return inserted;
    }

    /// <summary>Recursively camel-cases every field name, including those in nested documents and arrays.</summary>
    private static BsonValue ToCamelCase(BsonValue value)
    {
        switch (value)
        {
            case BsonDocument document:
                var result = new BsonDocument();
                foreach (var element in document)
                    result.Add(CamelCaseName(element.Name), ToCamelCase(element.Value));
                return result;

            case BsonArray array:
                return new BsonArray(array.Select(ToCamelCase));

            default:
                return value;
        }
    }

    private static string CamelCaseName(string name)
    {
        if (name.Length == 0 || name[0] == '_' || name[0] == '$' || !char.IsUpper(name[0]))
            return name;
        return char.ToLower(name[0], CultureInfo.InvariantCulture) + name[1..];
    }

    private static string? ResolveFolder(string folder, out IReadOnlyList<string> searched)
    {
        if (Path.IsPathRooted(folder))
        {
            searched = new[] { folder };
            return Directory.Exists(folder) ? folder : null;
        }

        var candidates = new[]
        {
            Path.Combine(Directory.GetCurrentDirectory(), folder),
            Path.Combine(AppContext.BaseDirectory, folder)
        };
        searched = candidates;
        return candidates.FirstOrDefault(Directory.Exists);
    }
}