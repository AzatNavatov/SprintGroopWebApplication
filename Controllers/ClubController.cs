using Microsoft.AspNetCore.Mvc;
using SprintGroopWebApplication.Helpers;
using SprintGroopWebApplication.Interfaces;
using SprintGroopWebApplication.Models;
using SprintGroopWebApplication.ViewModels;
using System.Security.Claims;
using SprintGroopWebApplication.wwwroot;


namespace SprintGroopWebApplication.Controllers
{
    public class ClubController : Controller
    {
        
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
     

        public ClubController(IClubRepository clubRepository,IPhotoService photoService,IHttpContextAccessor httpContextAccessor)
        {
            
            _clubRepository = clubRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        public IActionResult Create()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var ClubVM = new CreateClubViewModel { AppUserId = curUser };
            return View(ClubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    Image = result.Url.ToString(),
                    AppUserId = clubVM.AppUserId,
                    Address = new Address
                    {
                        Street =clubVM.Address.Street,
                        City = clubVM.Address.City,
                        Country = clubVM.Address.Country,
                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }

            else
            {
                ModelState.AddModelError("", "Photo upload failed");

            }
            return View(clubVM);
            
        }

        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve existing club details based on id
            Club existingClub = await _clubRepository.GetByIdAsync(id);
            if(existingClub ==null) return View("Error");
            // Populate the edit view with the existing club data
            EditClubViewModel editViewModel = new EditClubViewModel
            {
                Id = existingClub.Id,
                Title = existingClub.Title,
                AddressId = existingClub.AddressId,
                Address = existingClub.Address,
                URL = existingClub.Image,
                ClubCategory = existingClub.ClubCategory,
                // Other properties...
            };

            return View(editViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Some mistachio with validation");
                return View("Edit",clubVM);
            }
            Club existingClub = await _clubRepository.GetByIdAsyncNoTracking(id);

            if (existingClub != null)
            {
                try
                {
                    _photoService.DeletePhotoAsync(existingClub.Image);

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "Photo wasn't deleted");
                    return View(clubVM); //*
                }
                var photoFinal = await _photoService.AddPhotoAsync(clubVM.Image);

                Club club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Image = photoFinal.Url.ToString(),
                    AddressId = clubVM.AddressId,
                    Address = clubVM.Address,
                };
                _clubRepository.Update(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clubVM = await _clubRepository.GetByIdAsync(id);
            if (clubVM == null) return View("Error");
            return View(clubVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubMV = await _clubRepository.GetByIdAsync(id);
            if(clubMV == null) return View("Error");
            _clubRepository.Delete(clubMV);
            return RedirectToAction("Index");

        }

    }

    
}
