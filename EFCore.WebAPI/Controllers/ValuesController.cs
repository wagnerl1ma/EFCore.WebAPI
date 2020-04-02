using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Dominio;
using EFCore.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Controllers
{
   //rota: "api/nomedacontroller"
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly HeroiContext _context;

        public ValuesController(HeroiContext context)
        {
            _context = context;
        }

        // GET api/values/pegartodos
        [HttpGet("getid/{id}")]
        public ActionResult GetById(int id)
        {
            //select na tabela de Herois com LinQ
            var listHerois = _context.Herois
                            .Where(x => x.Id == id)
                            .ToList();

            return Ok(listHerois);
        }

        // GET api/values/pegartodos
        [HttpGet("pegartodos")]
        public ActionResult GetAll()
        {
            //select na tabela de Herois com LinQ
            var listHerois = _context.Herois.ToList();

            return Ok(listHerois);
        }

        // GET api/values/filtro/nomeDoHeroi
        [HttpGet("filtro/{nome}")]
        public ActionResult GetFiltro(string nome)
        {
            //select na tabela de Herois com LinQ
            var listHerois = _context.Herois
                            .Where(h => h.Nome.Contains(nome))
                            .ToList();


            //Outra forma de aplicar um Select com o LinQ, usando o LIKE
            //var listHerois2 = _context.Herois
            //                .Where(h => EF.Functions.Like(h.Nome, $"%{nome}%"))
            //                .ToList();


            //select na tabela de Herois como query
            //var listHerois = (from heroi in _context.Herois
            //                  where heroi.Nome.Contains(nome)
            //                  select heroi).ToList();

            return Ok(listHerois);
        }

        // GET api/values/InserirHeroi/nomeDoHeroi
        [HttpGet("InserirHeroi/{nameHero}")]  // mudar para o verbo POST no postman
        public ActionResult<string> Get(string nameHero)
        {
            //teste pra inserir um heroi no banco passando um nome
            var heroi = new Heroi { Nome = nameHero };

            _context.Herois.Add(heroi);
            _context.SaveChanges();


            return Ok();
        }

        // GET api/values/AddRange
        [HttpGet("AddRange")]   // mudar para o verbo POST no postman
        public ActionResult<string> GetAddRange()
        {
            // adicionar vários usando AddRange
            _context.AddRange(
                new Heroi { Nome = "Capitão América"},
                new Heroi { Nome = "Doutor Estranho" },
                new Heroi { Nome = "Pantera Negra" },
                new Heroi { Nome = "Viúva Negra" },
                new Heroi { Nome = "Hulk" },
                new Heroi { Nome = "Gavião Arqueiro" },
                new Heroi { Nome = "Capitã Marvel" }
                );

            _context.SaveChanges();

            return Ok();
        }

        // GET api/values/Atualizar/qualquer coisa
        [HttpGet("Atualizar/{nameHero}")]   // mudar para o verbo PUT no postman
        public ActionResult<string> AtualizarNomeHeroi(string nameHero)
        {
            //teste para atualizar UPDATE
            //var heroi = new Heroi { Nome = nameHero };

            //pegando o id 3
            var heroi = _context.Herois
                .Where(h => h.Id == 3)
                .FirstOrDefault();

            // atualizando o nome
            heroi.Nome = "Homem Aranha";

            // atualizando no banco
            _context.Herois.Update(heroi);
            _context.SaveChanges();


            return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/Deletar/id
        //[HttpDelete("Deletar/{id}")]  // mudar para o verbo DELETE no postman
        [HttpGet("Delete/{id}")]  // mudar para o verbo DELETE no postman
        public void DeleteById(int id)
        {
            //var heroi = _context.Herois.Find(id);

            //pegando Id com o Linq
            var heroi = _context.Herois
                                .Where(x => x.Id == id)
                                .Single();

            _context.Herois.Remove(heroi);
            _context.SaveChanges();
        }
    }
}
