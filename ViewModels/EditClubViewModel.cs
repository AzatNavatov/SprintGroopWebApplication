using SprintGroopWebApplication.Data.Enums;
using SprintGroopWebApplication.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprintGroopWebApplication.ViewModels
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public IFormFile Image { get; set; }
      
        public int AddressId { get; set; }
        public Address Address { get; set; }

        public string? URL { get; set; }
        public ClubCategory ClubCategory { get; set; }
      
      
    }
}
