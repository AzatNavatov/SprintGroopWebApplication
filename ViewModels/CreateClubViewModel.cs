using SprintGroopWebApplication.Data.Enums;
using SprintGroopWebApplication.Models;

namespace SprintGroopWebApplication.ViewModels
{
    public class CreateClubViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public ClubCategory ClubCategory { get; set; }

        public string AppUserId { get; set; }
       
    }
}
