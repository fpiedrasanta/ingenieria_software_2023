using System.Diagnostics.CodeAnalysis;
using System.Net;
using api_programacion_iii.Data;
using api_programacion_iii.DTO.Resource;
using api_programacion_iii.Entities.Resources;
using api_programacion_iii.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_programacion_iii.Controllers;

[ApiController]
[Route("[controller]")]
public class ResourceController : ControllerBase
{
    [NotNull]
    private readonly DataContext dataContext;

    public ResourceController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Resource>> GetResource(long id)
    {
        Resource? dbResource = await this.dataContext.Resources.FindAsync(id);
        if(dbResource == null)
        {
            return NotFound("Recurso no encontrado");
        }

        return Ok(dbResource);
    }

    [HttpGet("type/{id}")]
    public async Task<ActionResult<List<DTOResource>>> Get(
        [FromRoute] long id, 
        [FromQuery] DTOList dtoList)
    {
        ResourceType? resourceType = await this.dataContext.ResourceTypes.FindAsync(id);
        
        //Primer cambio.
        var query = this.dataContext.Resources.AsQueryable();

        if(!string.IsNullOrEmpty(dtoList.Query))
        {
            query = query.Where(bibliografia => bibliografia.Title.Contains(dtoList.Query));
        }

        if(!string.IsNullOrEmpty(dtoList.OrderBy))
        {
            query = query.OrderBy(bibliografia => bibliografia.Title);
        }

        int page = dtoList.Page != null ? dtoList.Page.Value : 1;
        int pageSize = dtoList.PageSize != null ? dtoList.PageSize.Value : 10;

        var resources = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        List<DTOResource> dtos = new List<DTOResource>();

        foreach(Resource resource in resources)
        {
            dtos.Add(new DTOResource
            {
                Description = resource.Description,
                Id = resource.Id,
                Link = "/Image/" + (resource.Image != null ? resource.Image.Id.ToString() : ""),
                Title = resource.Title
            });
        }

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult> Post(
        [FromForm] DTOResource dtoResource)
    {
        if (dtoResource.File == null)
        {
            return BadRequest("Archivo no válido");
        }

        string path = await SaveFile(dtoResource);
        Resource resource = await CreateResource(dtoResource, path);
        await SaveResourceDB(resource);

        return Ok();
    }

    private async Task SaveResourceDB(Resource resource)
    {
        await this.dataContext.Resources.AddAsync(resource);

        await this.dataContext.SaveChangesAsync();
    }

    private async Task<Resource> CreateResource(DTOResource dtoResource, string path)
    {
        ResourceType? resourceType = 
            await this.dataContext
                .ResourceTypes
                .FindAsync(dtoResource.IdResourceType);

        return new Resource
        {
            Description = dtoResource.Description,
            Image = new Entities.Common.Image
            {
                Path = path,
                UploadDate = DateTime.Now,
                Url = ""
            },
            Link = "",
            Notes = dtoResource.Notes,
            Title = dtoResource.Title,
            ResourceType = resourceType
        };
    }

    private static async Task<string> SaveFile(DTOResource dtoResource)
    {
        string path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "Archivos",
                    dtoResource.File.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await dtoResource.File.CopyToAsync(stream);
        }

        return path;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Resource>> Put(
        [FromRoute]long id, 
        [FromBody] Resource resource)
    {
        Resource? dbResource = await this.dataContext.Resources.FindAsync(id);
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

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        Resource? dbResource = await this.dataContext.Resources.FindAsync(id);
        if(dbResource == null)
        {
            return NotFound("Recurso no encontrado");
        }

        dataContext.Resources.Remove(dbResource);

        await this.dataContext.SaveChangesAsync();
                
        return Ok();
    }
}