using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SprintGroopWebApplication.Data;
using SprintGroopWebApplication.Helpers;
using SprintGroopWebApplication.Interfaces;
using SprintGroopWebApplication.Models;
using SprintGroopWebApplication.Repository;
using SprintGroopWebApplication.ViewModels;

namespace SprintGroopWebApplication.Controllers
{
    public class RaceController : Controller
    {
        
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;
        public readonly IHttpContextAccessor _httpContextAccessor;

        public RaceController(IRaceRepository raceRepository, IPhotoService photoService,IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _raceRepository = raceRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var RaceVM = new CreateRaceViewModel { AppUserId = curUser };
            return View(RaceVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Title = raceVM.Title,
                    Image = result.Url.ToString(),
                    AppUserId = raceVM.AppUserId,   
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        Country = raceVM.Address.Country,
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
               
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");

            }
            return View(raceVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Race currentRace = await _raceRepository.GetByIdAsync(id);
            if (currentRace == null) return View("Error");

            EditRaceViewModel editVM = new EditRaceViewModel()
            {
                Id = currentRace.Id,
                Title = currentRace.Title,
                Address = currentRace.Address,
                URL = currentRace.Image,
                AddressId = currentRace.AddressId,
                RaceCategory = currentRace.RaceCategory,

            };
            return View(editVM);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Some mistakes with validation");
                return View("Edit",raceVM);
            }
            Race existingrace = await _raceRepository.GetByIdAsyncNoTracking(id);
            if (existingrace != null)
            {
                try
                {
                    _photoService.DeletePhotoAsync(existingrace.Image);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Photo wasn't deleted");
                    return View(raceVM);

                }
                var photoFinal = await _photoService.AddPhotoAsync(raceVM.Image);

                Race race = new Race()
                {
                    Id = id,
                    Title = raceVM.Title,
                    Image = photoFinal.Url.ToString(),
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address,
                };
                _raceRepository.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit");
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            var raceMV = await _raceRepository.GetByIdAsync(id);
            if (raceMV == null) return View("Error");
            return View(raceMV);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var raceMV = await _raceRepository.GetByIdAsync(id);
            if (raceMV == null) return View("Error");
            _raceRepository.Delete(raceMV);
            return RedirectToAction("Index");

        }
    }
}

