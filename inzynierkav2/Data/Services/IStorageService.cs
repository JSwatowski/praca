using inzynierka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Data.Services
{
    public interface IStorageService
    {
        Task StoreStorageAsync(List<StorageCardItem> items, string userId, string userAddressEmail);
        Task<List<Storage>> GetStorageByUserIdAsync(string userId);
    }
}
