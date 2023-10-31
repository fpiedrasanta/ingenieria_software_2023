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

    [HttpGet("{id}")]
    public async Task<ActionResult<Resource>> GetResource(long id)
    {
        if(this.dataContext != null && this.dataContext.Resources != null)
        {
            Resource dbResource = await this.dataContext.Resources.FindAsync(id);
            if(dbResource == null)
            {
                return NotFound("Recurso no encontrado");
            }

            return Ok(dbResource);
        }

        return NotFound();
    }

    [HttpGet("type/{id}")]
    public async Task<ActionResult<List<Resource>>> Get(long id)
    {
        List<Resource> resources = new List<Resource>();
        
        if(this.dataContext != null && this.dataContext.Resources != null && this.dataContext.ResourceTypes != null)
        {
            ResourceType? resourceType = await this.dataContext.ResourceTypes.FindAsync(id);
            resources = 
                await this.dataContext.Resources
                    .Where(resource => resource.ResourceType == resourceType)
                    .ToListAsync();
        }
        
        return Ok(resources);
    }

    [HttpPost]
    public async Task<ActionResult<Resource>> Post([FromBody] Resource resource)
    {
        if(this.dataContext != null && this.dataContext.Resources != null)
        {
            await this.dataContext.Resources.AddAsync(resource);

            await this.dataContext.SaveChangesAsync();
        }

        return Ok(resource);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Resource>> Put(
        [FromRoute]long id, 
        [FromBody] Resource resource)
    {
        if(this.dataContext != null && this.dataContext.Resources != null)
        {
            Resource dbResource = await this.dataContext.Resources.FindAsync(id);
            if(dbResource == null)
            {
                return NotFound("Recurso no encontrado");
            }

            dbResource.Description = resource.Description;
            dbResource.Title = resource.Title;
            dbResource.Link = resource.Link;
            dbResource.Notes = resource.Notes;

            await this.dataContext.SaveChangesAsync();

            return Ok(dbResource);
        }

        return BadRequest("Error");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        if(this.dataContext != null && this.dataContext.Resources != null)
        {
            Resource dbResource = await this.dataContext.Resources.FindAsync(id);
            if(dbResource == null)
            {
                return NotFound("Recurso no encontrado");
            }

            dataContext.Resources.Remove(dbResource);

            await this.dataContext.SaveChangesAsync();
        }

        return Ok();
    }
}