using System.Diagnostics.CodeAnalysis;
using System.Net;
using api_programacion_iii.Data;
using api_programacion_iii.DTO.Resource;
using api_programacion_iii.Entities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_programacion_iii.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    [NotNull]
    private readonly DataContext dataContext;

    public ImageController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("{idImagen}")]
    public IActionResult GetImage(long idImagen)
    {
        Image? imageDB = this.dataContext.Images.Find(idImagen);
        if(imageDB == null)
        {
            return NotFound("Imagen no encontrada");
        }

        if(!System.IO.File.Exists(imageDB.Path))
        {
            return NotFound("Imagen no encontrada");
        }

        var image = System.IO.File.OpenRead(imageDB.Path);

        return File(image, "image/jpg");
    }
}
