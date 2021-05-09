using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using(Models.CrudVanillaJSContext db= new Models.CrudVanillaJSContext())
            {
                    var lst = (from d in db.TbPersonas
                              select d).ToList();

                    return Ok(lst);  
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Models.Request.PersonaRequest model)
        {
            using(Models.CrudVanillaJSContext db = new Models.CrudVanillaJSContext())
            {
                Models.TbPersona oPersona = new Models.TbPersona();
                oPersona.Nombre = model.nombre;
                oPersona.Edad = model.edad;
                db.Add(oPersona);
                db.SaveChanges();
            }            
            return Ok();
        }
        [HttpPut]
        public ActionResult Put([FromBody] Models.Request.PersonaEditRequest model)
        {
            using(Models.CrudVanillaJSContext db = new Models.CrudVanillaJSContext())
            {
                Models.TbPersona oPersona = db.TbPersonas.Find(model.Id);
                oPersona.Nombre = model.Nombre;
                oPersona.Edad = model.Edad;
                db.Entry(oPersona).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }            
            return Ok();
        }
        [HttpDelete]
        public ActionResult Delete([FromBody] Models.Request.PersonaEditRequest model)
        {
            using(Models.CrudVanillaJSContext db = new Models.CrudVanillaJSContext())
            {
                Models.TbPersona oPersona = db.TbPersonas.Find(model.Id);
                db.Remove(oPersona);
                db.SaveChanges();
            }            
            return Ok();
        }

    }
}