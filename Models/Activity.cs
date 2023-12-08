using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacramentMeetingPlanner.Models
{
    public class Activity
    {

        // Key.
        public int ActivityID { get; set; }

        // Foreign Key.
        public int MeetingID { get; set; }

        // Specific to Model.
        [Required]
        [StringLength(150)]
        [Display(Name = "Event Name")]
        public string EventName { get; set; } = "";

        [StringLength(100)]
        [Display(Name = "Event Info")]
        public string? EventInfo { get; set; }

        [StringLength(150)]
        [Display(Name = "Event Footer")]
        public string? EventFooter { get; set; }

        [Required]
        [Display(Name = "Display Order")]
        public int Order { get; set; }
    }
}
