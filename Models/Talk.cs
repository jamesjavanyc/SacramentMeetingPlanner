namespace SacramentMeetingPlanner.Models;

using System.ComponentModel.DataAnnotations;

public class Talk
{
    public int Id { get; set; }

    public int MeetingId { get; set; }

    [Required]
    public string Speaker { get; set; }

    [Required]
    public string Topic { get; set; }

    public Meeting Meeting { get; set; }
}
