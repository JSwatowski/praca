using inzynierka.Models;
using Microsoft.EntityFrameworkCore;
using pracainz.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Data.Services
{
    public class StorageService : IStorageService
    {
        private readonly AppDbContext _context;

        public StorageService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Storage>> GetStorageByUserIdAsync(string userId)
        {
            var storages = await _context.Storages.Include(n => n.StorageItems).ThenInclude(n => n.Movie).Where(n => n.UserId == userId).ToListAsync();
            return storages;
        }

        public async Task StoreStorageAsync(List<StorageCardItem> items, string userId, string userAddressEmail)
        {
            var storage = new Storage()
            {
                UserId = userId,
                Email = userAddressEmail
            };

            await _context.Storages.AddAsync(storage);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var storageItem = new StorageItem()
                {
                    StorageId = storage.Id,
                    MovieId = item.Movie.Id
                };
                await _context.StorageItems.AddAsync(storageItem);
            }
            await _context.SaveChangesAsync();
        }


    }
}
