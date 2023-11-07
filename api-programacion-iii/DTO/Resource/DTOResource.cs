namespace api_programacion_iii.DTO.Resource;

public class DTOResource 
{
    public long Id { get; set; } = 0;

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public string Link { get; set; } = "";

    public string Notes { get; set; } = "";

    public long IdResourceType { get; set; }

    public IFormFile? File { get; set; }
}