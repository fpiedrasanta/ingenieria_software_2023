using api_programacion_iii.Data;
using api_programacion_iii.Entities.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_programacion_iii.Controllers;

[ApiController]
[Route("[controller]")]
public class ResourceController : ControllerBase
{
    private readonly DataContext dataContext;

    public ResourceController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("{idType}")]
    public async Task<ActionResult<List<Resource>>> Get(long idType)
    {
        List<Resource> resources = new List<Resource>();
        
        if(this.dataContext != null && this.dataContext.Resources != null && this.dataContext.ResourceTypes != null)
        {
            ResourceType? resourceType = await this.dataContext.ResourceTypes.FindAsync(idType);
            resources = 
                await this.dataContext.Resources
                    .Where(resource => resource.ResourceType == resourceType)
                    .ToListAsync();
        }

        return Ok(resources);
    }
}