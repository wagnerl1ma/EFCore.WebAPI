using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Dominio;
using EFCore.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        private readonly HeroiContext _context;
        public HeroiController(HeroiContext context)
        {
            _context = context;
        }

        // GET: api/Heroi
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Heroi());  // pegar o json vazio
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET: api/Heroi/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            return Ok();
        }

        //// POST: api/Heroi
        //[HttpPost]
        //public ActionResult Post()
        //{
        //    try
        //    {
        //        var heroi = new Heroi  // add um Heroi com armas
        //        {
        //            Nome = "Wolverine",
        //            Armas = new List<Arma>     // add armas para o heroi
        //            {
        //                new Arma{Nome = "Garras de Adamantio"},
        //                new Arma{Nome = "Regeneração"}
        //            }
        //        };

        //        _context.Herois.Add(heroi);
        //        _context.SaveChanges();

        //        return Ok("BAZINGA!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Erro: {ex}");
        //    }
        //}

        // POST: api/Heroi
        [HttpPost]
        public ActionResult Post(Heroi model)
        {
            try
            {

                _context.Herois.Add(model);
                _context.SaveChanges();

                return Ok("BAZINGA!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT: api/Heroi/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Heroi model)
        {
            try
            {
               if(_context.Herois.AsNoTracking().FirstOrDefault(x => x.Id == id) != null)           //AsNoTracking = desbloquear para fazer um consulta
                {
                    _context.Herois.Update(model);
                    _context.SaveChanges();

                    return Ok("BAZINGA!");
                }
                else
                {
                    return Ok("Não Encontrado!");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        //// PUT: api/Heroi/5
        //[HttpPut("{id}")]
        //public ActionResult Put(int id)
        //{
        //    try
        //    {
        //        var heroi = new Heroi  // add um Heroi com armas
        //        {
        //            Id = id,
        //            Nome = "Homem de Ferro",
        //            Armas = new List<Arma>     // add armas para o heroi
        //            {
        //                new Arma{Id = 5, Nome = "Mark III"},
        //                new Arma{Id = 6, Nome = "Mark VIII"}
        //            }
        //        };

        //        _context.Herois.Update(heroi);
        //        _context.SaveChanges();

        //        return Ok("BAZINGA!");

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Erro: {ex}");
        //    }
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        // DELETE: api/Heroi/DeleteArma/id
        [HttpDelete("DeleteArma/{id}")]
        public ActionResult DeleteArma(int id)
        {
            try
            {
                var arma = _context.Armas.Find(id);

                _context.Armas.Remove(arma);
                _context.SaveChanges();

                return Ok("Arma Excluída");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }
    }
}
