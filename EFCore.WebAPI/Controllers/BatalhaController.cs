using System;
using System.Collections.Generic;
using System.Linq;
using EFCore.Repositorio;
using EFCore.Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly HeroiContext _context;

        public BatalhaController(HeroiContext context)
        {
            _context = context;
        }

        // GET: api/Batalha
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Batalha());  // pegar o json vazio
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }
        }


        // GET: api/Batalha/getid/1
        [HttpGet("getid/{id}")]
        public ActionResult GetById(int id)
        {
            return Ok();
        }

        // POST: api/Batalha/InserirBatalha
        [HttpPost("InserirBatalha")]
        public ActionResult Post(Batalha model)
        {
            try
            {
                _context.Batalhas.Add(model);
                _context.SaveChanges();

                return Ok("Batalha Cadastrada");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }
        }

        // PUT: api/Batalha/AtualizarBatalha/5
        [HttpPut("AtualizarBatalha/{id}")]
        public ActionResult Put(int id, Batalha model)
        {
            try
            {
                if (_context.Batalhas.AsNoTracking().FirstOrDefault(x => x.Id == id) != null)
                {
                    _context.Batalhas.Update(model);
                    _context.SaveChanges();
                }

                return Ok("Batalha alterada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
