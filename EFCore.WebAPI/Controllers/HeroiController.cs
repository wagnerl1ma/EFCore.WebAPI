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
        //private readonly HeroiContext _context;
        private readonly IEFCoreRepository _repository;

        public HeroiController(IEFCoreRepository repository)
        {
            _repository = repository;
        }



        //// GET: api/Heroi/GetHerois
        //[HttpGet("GetHerois")]
        //public ActionResult Get()
        //{
        //    try
        //    {
        //        return Ok(new Heroi());    // pegar o json vazio
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Erro: {ex}");
        //    }
        //}


        // GET: api/Heroi/GetHerois
        [HttpGet("GetHerois")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var herois = await _repository.GetAllHerois();

                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }
        }

        // GET: api/Heroi/getid/2
        [HttpGet("getid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var heroi = await _repository.GetHeroiById(id);

                return Ok(heroi);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }
        }

        //// POST: api/Heroi
        //[HttpPost]
        //public ActionResult Post()                        //POST orientado a objeto
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
        public async Task<IActionResult> Post(Heroi model)
        {
            try
            {
                _repository.Add(model);

                if (await _repository.SaveChangeAsync())
                {
                    return Ok("Heroi Cadastrado com sucesso!");
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

            return BadRequest("Heroi não Cadastrado!");
        }


        // PUT: api/Heroi/AtualizarHeroi/5
        [HttpPut("AtualizarHeroi/{id}")]    
        public async Task<IActionResult> Put(int id, Heroi model)
        {
            try
            {
                var heroi = await _repository.GetHeroiById(id);

                if (heroi != null)           
                {
                    _repository.Update(model);
                    
                    if (await _repository.SaveChangeAsync())
                    {
                        return Ok("BAZINGA!");
                    }
                }
            }

            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

            return Ok("Heroi não atualizado!");
        }

        //PUT ANTIGO USANDO O AsNoTracking
        //// PUT: api/Heroi/5
        //[HttpPut("{id}")]           //PUT ANTIGO USANDO O AsNoTracking
        //public ActionResult Put(int id, Heroi model)
        //{
        //    try
        //    {
        //       if(_context.Herois.AsNoTracking().FirstOrDefault(x => x.Id == id) != null)           //AsNoTracking = desbloquear para fazer um consulta
        //        {
        //            _context.Herois.Update(model);
        //            _context.SaveChanges();

        //            return Ok("BAZINGA!");
        //        }
        //        else
        //        {
        //            return Ok("Não Encontrado!");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Erro: {ex}");
        //    }
        //}

        //// PUT: api/Heroi/5
        //[HttpPut("{id}")]
        //public ActionResult Put(int id)                   //PUT orientado a objeto
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

        // DELETE: api/deleteHeroi/5
        [HttpDelete("deleteHeroi/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var heroi = await _repository.GetHeroiById(id);

                if(heroi != null)
                {
                    _repository.Delete(heroi);

                    if(await _repository.SaveChangeAsync())
                    {
                        return Ok("Heroi excluído com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }

            return BadRequest("NÃO DELETADO!");
        }


        //TODO: fazer delete de armas
        // DELETE: api/Heroi/DeleteArma/id          //fazer delete de armas
        //[HttpDelete("DeleteArma/{id}")]
        //public ActionResult DeleteArma(int id)
        //{
        //    try
        //    {
        //        var arma = _context.Armas.Find(id);

        //        _context.Armas.Remove(arma);
        //        _context.SaveChanges();

        //        return Ok("Arma Excluída");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Erro: {ex}");
        //    }
        //}
    }
}
