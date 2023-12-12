namespace SacramentMeetingPlanner.Models;

using System.ComponentModel.DataAnnotations;

public class Talk
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Meeting")]
    public int MeetingId { get; set; }

    [Required]
    [StringLength(250)]
    [Display(Name = "Speaker")]
    public string Speaker { get; set; } = string.Empty;

    [Required]
    [StringLength(250)]
    [Display(Name = "Topic")]
    public string Topic { get; set; } = string.Empty;

    public Meeting? Meeting { get; set; }
}
