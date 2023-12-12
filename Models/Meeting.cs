using System.ComponentModel.DataAnnotations;

namespace SacramentMeetingPlanner.Models
{
    public class Meeting
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Leader")]
        public string Leader { get; set; } = "";

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Meeting Date")]
        public DateTime Date { get; set; }

        public string DateFormatted => Date.ToString("dddd, dd MMMM yyyy");

        [Required]
        [StringLength(250)]
        [Display(Name = "Opening Song")]
        public string OpeningSong { get; set; } = "";

        [Required]
        [StringLength(250)]
        [Display(Name = "Opening Prayer")]
        public string OpeningPrayer { get; set; } = "";

        [Required]
        [StringLength(250)]
        [Display(Name = "Sacrament Hymn")]
        public string SacramentHymn { get; set; } = "";

        [StringLength(250)]
        [Display(Name = "Intermediate Hymn - Music Number")]
        public string? MusicNumber { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Closing Song")]
        public string ClosingSong { get; set; } = "";

        [Required]
        [StringLength(250)]
        [Display(Name = "Closing Prayer")]
        public string ClosingPrayer { get; set; } = "";

        public List<Talk>? Talks { get; set; }
    }
}
