using System.Collections.Specialized;
using System.Text;
using System;
using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Seguro.Model;
using System.Linq;

namespace Seguro.Controllers;


public class requestAsegurado
{
  public string cedulaAsegurado {set;get;}

  public string nombreAsegurado {set;get;}

  public int telefonoasegurado {set;get;}
  public DateOnly fechaNacimientoAsegurado {set;get;}
}


[Microsoft.AspNetCore.Cors.EnableCors("_myAllowSpecificOrigins")]
[ApiController]
[Route("[controller]")]
public class AseguradoController : Controller
{
    private readonly ConsultorioSeguroContext _context;

    public AseguradoController(ConsultorioSeguroContext logger)
    {
        this._context = logger;
    }

    [HttpGet("/GetAsegurados")]
    public IActionResult Get()
    {
        return Ok(_context.Asegurados.ToList());
    }


    [HttpPost("/CreateAsegurados")]
    public IActionResult CrearSeguro([FromBody] requestAsegurado asegurado)
    {
        try{
            Asegurado aux = new Asegurado();
            var busqueda = from b in _context.Seguros
                   where b.CodigoSeguro.Equals(asegurado.cedulaAsegurado)
                   select b;
        if(busqueda.Count() == 0){
            aux.IdAsegurado = _context.Asegurados.OrderBy(x=>x.IdAsegurado).Last().IdAsegurado +1;
            aux.CedulaAsegurado = asegurado.cedulaAsegurado;
            aux.NombreAsegurado = asegurado.nombreAsegurado;
            aux.TelefonoAsegurado = asegurado.telefonoasegurado;
            aux.FechaNacimiento = asegurado.fechaNacimientoAsegurado;
            _context.Asegurados.Add(aux);
            _context.SaveChanges();
            return Ok(aux);
        }else{
            return BadRequest("Cedula duplicada, intente con otro");
        }
        }catch(Exception ex){
            return BadRequest("Error: "+ex);
        }
        
    }


    [HttpPost("/ModifyAsegurado")]
    public IActionResult ModificarSeguro([FromBody] Asegurado asegurado)
    {
        try{
            _context.Asegurados.Update(asegurado);
            _context.SaveChanges();
            return Ok(new Dictionary<string,string>{{"message", "Actualizado con exito"}});
        }catch(Exception ex){
            return BadRequest("Error al actualizar "+ex.ToString());
        }
        
    }

    [HttpPost("/DeleteAsegurado")]
    public IActionResult EliminarSeguro(int id)
    {
        try{
            var seguro = _context.Asegurados.Where(x=>x.IdAsegurado == id).FirstOrDefault();
            _context.Asegurados.Remove(seguro);
            _context.SaveChanges();
            return Ok(new Dictionary<string,string>{{"message", "Eliminado con exito"}});
        }catch(Exception ex){
            return BadRequest("Error al eliminar "+ex.ToString());
        }
    }


}
