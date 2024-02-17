using SprintGroopWebApplication.Data.Enums;
using SprintGroopWebApplication.Models;

namespace SprintGroopWebApplication.ViewModels
{
    public class CreateRaceViewModel3
    {
        public int Id { get; set; }
        public int I { get; set; }


        public string Title { get; set; }

        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public RaceCategory RaceCategory { get; set; }
        public string AppUserId { get; set; }
   
    }
}
