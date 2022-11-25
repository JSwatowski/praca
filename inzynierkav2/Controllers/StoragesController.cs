using inzynierka.Data.Base;
using inzynierka.Data.Services;
using inzynierka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracainz.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace inzynierka.Controllers
{
    public class StoragesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMoviesService _moviesService;
        private readonly StorageCard _storageCard;
        private readonly IStorageService _storageService;
        public StoragesController(IMoviesService moviesService, StorageCard storageCard, IStorageService storageService, AppDbContext context)
        {
            _context = context;
            _moviesService = moviesService;
            _storageCard = storageCard;
            _storageService = storageService;
        }

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            var items = _storageCard.GetStorageCardItems();
            _storageCard.StorageCardItems = items;

            var response = new StorageCardVM()
            {
                StorageCard = _storageCard
            };

            return View(response);
        }

        [Authorize(Roles = "User")]
        public async Task<RedirectToActionResult> AddToStorageCard(int id)
        {
            var item = await _moviesService.GetByIdAsync(id);

            if (item != null)
            {
                _storageCard.AddItemToStorageCard(item);
            }

            var allItems = _context.StorageItems.ToList();

            var allStores = _context.Storages.ToList();


            var items = _storageCard.GetStorageCardItems();
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string userAddressEmail = User.FindFirst(ClaimTypes.Email).Value;

            foreach (var p in allItems)
            {
                if (p.MovieId == item.Id)
                {
                    foreach (var x in allStores)
                    {
                        if (p.StorageId == x.Id)
                        {
                            if(x.UserId == userId)
                            {

                            }else
                            {
                                await _storageService.StoreStorageAsync(items, userId, userAddressEmail);
                            }
                        }
                    }
                }
            }


           // await _storageService.StoreStorageAsync(items, userId, userAddressEmail);

            await _storageCard.ClearStorageCardAsync();

            return RedirectToAction(nameof(StorageView));
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> StorageView()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var storages = await _storageService.GetStorageByUserIdAsync(userId);
            return View(storages);
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int storageId)
        {
            var storage = _context.Storages.FirstOrDefault(n => n.Id == storageId);
            var storageItems = _context.StorageItems.Where(n => n.StorageId == storageId).FirstOrDefault();
            return View(storageItems);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int Id)
        {
            var storage = _context.Storages.FirstOrDefault(n => n.Id == Id);
            _context.Storages.Remove(storage);
            var storageItems = _context.StorageItems.Where(n => n.StorageId == Id).FirstOrDefault();
            _context.StorageItems.Remove(storageItems);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(StorageView));
        }
        [Authorize(Roles = "User")]
        public async Task<ActionResult> CheckWatched(int id)
        {
            var storageItem = _context.StorageItems.FirstOrDefault(n => n.Id == id);
            storageItem.IsWatched = "Watched";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(WatchedView));
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> WatchedView()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var storages = await _storageService.GetStorageByUserIdAsync(userId);
            return View(storages);
        }

    }

}
