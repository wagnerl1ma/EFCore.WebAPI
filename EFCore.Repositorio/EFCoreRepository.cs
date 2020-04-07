using EFCore.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repositorio
{
    public class EFCoreRepository : IEFCoreRepository
    {
        private readonly HeroiContext _context;

        public EFCoreRepository(HeroiContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<IEnumerable<Heroi>> GetAllHerois(bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if(incluirBatalha)
            {
                query = query.Include(h => h.HeroiBatalhas).ThenInclude(hb => hb.Batalha);
            }
           
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Heroi> GetHeroiById(int id, bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
               .Include(h => h.Identidade)
               .Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroiBatalhas).ThenInclude(hb => hb.Batalha);
            }

            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Heroi>> GetHeroisByNome(string nome, bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
               .Include(h => h.Identidade)
               .Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroiBatalhas).ThenInclude(hb => hb.Batalha);
            }

            query = query.AsNoTracking()
                .Where(h => h.Nome.Contains(nome))
                .OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<IEnumerable<Batalha>> GetAllBatalhas(bool incluirHerois = true)
        {
            IQueryable<Batalha> query = _context.Batalhas;

            if (incluirHerois)
            {
                query = query.Include(h => h.HeroiBatalhas).ThenInclude(hb => hb.Heroi);
            }

            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Batalha> GetBatalhaById(int id)
        {
            IQueryable<Batalha> query = _context.Batalhas
                .Include(b => b.HeroiBatalhas).ThenInclude(h => h.Heroi);

            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
