namespace media.hub.Models;
public class AdModel
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [Required]
    [MaxLength(255)]
    public string Description { get; set; }

    [Required]
    [MaxLength(255)]
    public List<string> Tags { get; set; }

    [Required]
    public IEnumerable<IFormFile> Files { get; set; }
}