﻿using Microsoft.AspNetCore.Mvc;
using SprintGroopWebApplication.Data;
using SprintGroopWebApplication.Interfaces;
using SprintGroopWebApplication.ViewModels;

namespace SprintGroopWebApplication.Controllers
{
    public class DashboardController : Controller
    {
        
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository )
        {
          
            _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardRepository.GetAllUserRaces();
            var userClubs = await _dashboardRepository.GetAllUserClubs();

            var dashboardVM = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs

            };
            return View(dashboardVM);
        }
    }
}
