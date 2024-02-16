using SprintGroopWebApplication.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprintGroopWebApplication.Models
{
    public class Race
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } 

        public string? Image { get; set;}
        public int? EntryFee { get; set; }
        public string? Contact { get; set; }
        public DateTime? StartTime { get; set; }

        public RaceCategory RaceCategory { get; set; }
        [ForeignKey ("Address")]
        public int AddressId  { get; set; }
        public Address Address { get; set; }
        [ForeignKey ("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

       


    }
}
