using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.Repositories
{
    internal sealed class InventoryRepository : RepositoryBase, IInventoryrepository
    {
        public InventoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Inventory?> GetInventoryByEmail(string email)
        {
            var inventory = await _dbContext.Inventory.AsNoTracking().FirstOrDefaultAsync(i => i.UserEmail == email);
            if(inventory == null)
            {
                inventory = new Inventory()
                {
                    UserEmail = email,
                    Items = [],
                    Funds = 0
                };

                await _dbContext.Inventory.AddAsync(inventory);

                await _dbContext.SaveChangesAsync();
            }

            return inventory;
        }

        public async Task<bool> UpdateInventoryByEmail(Inventory inventory)
        {
            // Check if the entity is already tracked locally or exists in DB
            var existing = await _dbContext.Inventory
                .FirstOrDefaultAsync(i => i.UserEmail == inventory.UserEmail);

            if (existing == null)
            {
                // Insert brand new document
                await _dbContext.Inventory.AddAsync(inventory);
            }
            else
            {
                // Copy updated properties onto the tracked entity
                existing.Funds = inventory.Funds;
                existing.Items = inventory.Items;
                // EF Core automatically detects changes on tracked 'existing' — no .Update() needed
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
