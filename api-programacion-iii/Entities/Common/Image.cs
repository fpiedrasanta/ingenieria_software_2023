namespace api_programacion_iii.Entities.Common;

public class Image 
{
    public long Id { get; set; } = 0;

    public string Path { get; set; } = "";

    public string Url { get; set; } = "";

    public DateTime? UploadDate { get; set; } = null;
}