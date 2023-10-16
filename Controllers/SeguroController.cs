using System.Collections.Specialized;
using System.Text;
using System;
using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Seguro.Model;
using System.Linq;

namespace Seguro.Controllers;


public class requestSeguro
{
  public string nombreSeguro {set;get;}

  public string codigoSeguro {set;get;}

  public decimal sumaAseguradaSeguro {set;get;}
  public decimal primaSeguro {set;get;}
}


[Microsoft.AspNetCore.Cors.EnableCors("_myAllowSpecificOrigins")]
[ApiController]
[Route("[controller]")]
public class SeguroController : Controller
{
    private readonly ConsultorioSeguroContext _context;

    public SeguroController(ConsultorioSeguroContext logger)
    {
        this._context = logger;
    }

    [HttpGet("/GetSeguros")]
    public IActionResult Get()
    {
        return Ok(_context.Seguros.ToList());
    }


    [HttpPost("/CreateSeguros")]
    public IActionResult CrearSeguro([FromBody] requestSeguro seguro)
    {
        try{
            Seguros aux = new Seguros();
            var busqueda = from b in _context.Seguros
                   where b.CodigoSeguro.Equals(seguro.codigoSeguro)
                   select b;
        if(busqueda.Count() == 0){
            aux.IdSeguro = _context.Seguros.OrderBy(x=>x.IdSeguro).Last().IdSeguro +1;
            aux.NombreSeguro = seguro.nombreSeguro;
            aux.CodigoSeguro = seguro.codigoSeguro;
            aux.SumaAsegurada = seguro.sumaAseguradaSeguro;
            aux.Prima = seguro.primaSeguro;
            _context.Seguros.Add(aux);
            _context.SaveChanges();
            return Ok(aux);
        }else{
            return BadRequest("Codigo de Seguro duplicado, intente con otro");
        }
        }catch(Exception ex){
            return BadRequest("Error: "+ex);
        }
        
    }


    [HttpPost("/ModifySeguros")]
    public IActionResult ModificarSeguro([FromBody] Seguros seguro)
    {
        try{
            _context.Seguros.Update(seguro);
            _context.SaveChanges();
            return Ok("Actualizado con exito");
        }catch(Exception ex){
            return BadRequest("Error al actualizar "+ex.ToString());
        }
        
    }

    [HttpPost("/DeleteSeguros")]
    public IActionResult EliminarSeguro(int id)
    {
        try{
            var seguro = _context.Seguros.Where(x=>x.IdSeguro == id).FirstOrDefault();
            _context.Seguros.Remove(seguro);
            _context.SaveChanges();
            return Ok("Eliminado con exito");
        }catch(Exception ex){
            return BadRequest("Error al eliminar "+ex.ToString());
        }
    }


}
