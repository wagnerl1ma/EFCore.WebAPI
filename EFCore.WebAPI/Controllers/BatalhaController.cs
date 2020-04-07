using System;
using System.Collections.Generic;
using System.Linq;
using EFCore.Repositorio;
using EFCore.Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly IEFCoreRepository _repository;

        public BatalhaController(IEFCoreRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Batalha/GetBatalhas
        [HttpGet("GetBatalhas")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var batalhas = await _repository.GetAllBatalhas();

                return Ok(batalhas);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }
        }


        // GET: api/Batalha/getid/1
        [HttpGet("getid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var batalha = await _repository.GetBatalhaById(id);

                return Ok(batalha);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }
        }

        // POST: api/Batalha/InserirBatalha
        [HttpPost("InserirBatalha")]
        public async Task<IActionResult> Post(Batalha model)
        {
            try
            {
                _repository.Add(model);

                if(await _repository.SaveChangeAsync())
                {
                    return Ok("Batalha Cadastrada");

                }
                
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }

            return BadRequest("Batalha não foi cadastrada.");
        }

        // PUT: api/Batalha/AtualizarBatalha/5
        [HttpPut("AtualizarBatalha/{id}")]
        public async Task<IActionResult> Put(int id, Batalha model)
        {
            try
            {
                var batalha = await _repository.GetBatalhaById(id);

                if (batalha != null)
                {
                    _repository.Update(model);

                    if (await _repository.SaveChangeAsync())
                    {
                        return Ok("Batalha Atualizada!");
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }

            return BadRequest("Batalha não foi atualizada!");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var batalha = await _repository.GetBatalhaById(id);

                if (batalha != null)
                {
                    _repository.Delete(batalha);

                    if(await _repository.SaveChangeAsync())
                    {
                        return Ok("Batalha excluída!");
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }

            return BadRequest("NÃO DELETADO!");
        }
    }
}
