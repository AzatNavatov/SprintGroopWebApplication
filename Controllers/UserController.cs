using Microsoft.AspNetCore.Mvc;
using SprintGroopWebApplication.Interfaces;
using SprintGroopWebApplication.Models;
using SprintGroopWebApplication.Repository;
using SprintGroopWebApplication.ViewModels;
using System.Collections.Generic;

namespace SprintGroopWebApplication.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository )
        {
           _userRepository = userRepository;
        }
  
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();

            List<UserViewModel> result = new List<UserViewModel>();
            
            foreach (var user in users)
            {
                var userVM = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Mileage = user.Mileage,
                    Pace = user.Pace,
                };
                result.Add(userVM);
            }

            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDVM = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Mileage = user.Mileage,
                Pace = user.Pace,
            };
            return View(userDVM);
        }
    }
}
