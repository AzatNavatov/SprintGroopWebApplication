using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

namespace SprintGroopWebApplication.Models
{
    public class AppUser : IdentityUser
    {
        
        public int? Mileage { get; set; }
        
        public int? Pace { get; set; }
 
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }
    }
}
