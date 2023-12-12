namespace SacramentMeetingPlanner.Models;

using System.ComponentModel.DataAnnotations;

public class Talk
{
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    [Display(Name = "Meeting Id")]
    public int MeetingId { get; set; }

    [Required]
    [StringLength(250)]
    [Display(Name = "Speaker")]
    public string Speaker { get; set; }

    [Required]
    [StringLength(250)]
    [Display(Name = "Topic")]
    public string Topic { get; set; }

    public Meeting Meeting { get; set; }
}
