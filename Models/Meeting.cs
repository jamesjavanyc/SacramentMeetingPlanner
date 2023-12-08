using System.ComponentModel.DataAnnotations;

namespace SacramentMeetingPlanner.Models
{
    public class Meeting
    {
        public int Id { get; set; }

        // Things specific to the meeting.

        [Required]
        [StringLength(250)]
        [Display(Name = "Ward")]
        public string Ward { get; set; } = "";

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Meeting Date")]
        public DateTime Date { get; set; }

        [StringLength(500)]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        // OPTIONAL MEETING TYPE
        // public int Type { get; set; }
    }
}
