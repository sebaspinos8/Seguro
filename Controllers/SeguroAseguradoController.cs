using System.Collections.Specialized;
using System.Text;
using System;
using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Seguro.Model;
using System.Linq;

namespace Seguro.Controllers;




[Microsoft.AspNetCore.Cors.EnableCors("_myAllowSpecificOrigins")]
[ApiController]
[Route("[controller]")]
public class SeguroAseguradoController : Controller
{
    private readonly ConsultorioSeguroContext _context;

    public SeguroAseguradoController(ConsultorioSeguroContext logger)
    {
        this._context = logger;
    }

    [HttpGet("/GetSeguroAsegurados")]
    public IActionResult Get()
    {
        return Ok(_context.Seguroasegurados.ToList());
    }

    [HttpGet("/GetSegurosxClienteDisponibles")]
    public IActionResult GetCliente(int idAsegurado)
    {
        var listaSeguros = _context.Seguros.Where(p=>!_context.Seguroasegurados.Any(p2=>p2.IdSeguro == p.IdSeguro)).ToList();
        
        return Ok(listaSeguros);
    }

    [HttpGet("/GetSegurosxClienteNoDisponibles")]
    public IActionResult GetClienteAsegurados(int idAsegurado)
    {
        var listaSeguros = _context.Seguros.Where(p=>_context.Seguroasegurados.Any(p2=>p2.IdSeguro == p.IdSeguro)).ToList();
        
        return Ok(listaSeguros);
    }


    [HttpPost("/CreateSeguroAsegurados")]
    public IActionResult CrearSeguro(int idSeguro, int idAsegurado)
    {
        try{
            Seguroasegurado aux = new Seguroasegurado();
            var busqueda = from b in _context.Seguroasegurados
                   where b.IdAsegurado.Equals(idAsegurado) && b.IdSeguro.Equals(idSeguro)
                   select b;
        if(busqueda.Count() == 0){
            aux.IdseguroAsegurado = _context.Seguroasegurados.OrderBy(x=>x.IdseguroAsegurado).Last().IdseguroAsegurado +1;
            aux.IdAsegurado = idAsegurado;
            aux.IdSeguro = idSeguro;
            _context.Seguroasegurados.Add(aux);
            _context.SaveChanges();
            return Ok(aux);
        }else{
            return BadRequest("Ya tiene ese seguro, intente con otro");
        }
        }catch(Exception ex){
            return BadRequest("Error: "+ex);
        }
        
    }


    [HttpPost("/ModifySeguroAsegurado")]
    public IActionResult ModificarSeguro([FromBody] Seguroasegurado seguroAsegurado)
    {
        try{
            _context.Seguroasegurados.Update(seguroAsegurado);
            _context.SaveChanges();
            return Ok(new Dictionary<string,string>{{"message", "Actualizado con exito"}});
        }catch(Exception ex){
            return BadRequest("Error al actualizar "+ex.ToString());
        }
        
    }

    [HttpPost("/DeleteSeguroAsegurado")]
    public IActionResult EliminarSeguro(int id)
    {
        try{
            var seguro = _context.Seguroasegurados.Where(x=>x.IdseguroAsegurado == id).FirstOrDefault();
            _context.Seguroasegurados.Remove(seguro);
            _context.SaveChanges();
            return Ok(new Dictionary<string,string>{{"message", "Eliminado con exito"}});
        }catch(Exception ex){
            return BadRequest("Error al eliminar "+ex.ToString());
        }
    }





}
