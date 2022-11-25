using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using pracainz.Data;
using pracainz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Models
{
    public class StorageCard
    {
        private AppDbContext _context { get; set; }

        public string StorageCardId { get; set; }
        public List<StorageCardItem> StorageCardItems { get; set; }

        public StorageCard(AppDbContext context)
        {
            _context = context;
        }

        public static StorageCard GetStorageCard(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            string storageId = session.GetString("StorageId") ?? Guid.NewGuid().ToString();
            session.SetString("StorageId", storageId);
            return new StorageCard(context) { StorageCardId = storageId };

        }

        public void AddItemToStorageCard(Movie movie)
        {
            var storageCardItem = _context.StorageCardItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.StorageCardId == StorageCardId);

            if (storageCardItem == null)
            {
                storageCardItem = new StorageCardItem()
                {
                    StorageCardId = StorageCardId,
                    Movie = movie,
                };
                _context.StorageCardItems.Add(storageCardItem);
            }

            _context.SaveChanges();
        }

        public void RemoveItemFromCard(Movie movie)
        {
            var storageCardItem = _context.StorageCardItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.StorageCardId == StorageCardId);

            if (storageCardItem != null)
            {
                _context.StorageCardItems.Remove(storageCardItem);
            }

            _context.SaveChanges();
        }


        public List<StorageCardItem> GetStorageCardItems()
        {
            return StorageCardItems ?? (StorageCardItems = _context.StorageCardItems.Where(n => n.StorageCardId == StorageCardId).Include(n => n.Movie).ToList());
        }


        public async Task ClearStorageCardAsync()
        {
            var items = await _context.StorageCardItems.Where(n => n.StorageCardId == StorageCardId).Include(n => n.Movie).ToListAsync();

            _context.StorageCardItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
