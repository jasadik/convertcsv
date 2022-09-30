using System.ComponentModel.DataAnnotations;

public class RequestInfoFile
{
    [Required]
    public string csvUri { get; set; }
    public string type { get; set; } = "json";
    public char separator { get; set; } = '|';

}