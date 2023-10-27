namespace api_programacion_iii.Entities.Resources;

public class Resource 
{
    public long Id { get; set; } = 0;

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public string UrlImg { get; set; } = "";

    public string Link { get; set; } = "";

    public string Notes { get; set; } = "";

    public ResourceType? ResourceType { get; set; }
}