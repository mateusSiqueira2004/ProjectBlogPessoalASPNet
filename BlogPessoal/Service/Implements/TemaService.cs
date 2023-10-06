using BlogPessoal.Data;
using BlogPessoal.Model;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Service.Implements
{
    public class TemaService : ITemaService
    {
        private readonly AppDbContext _context;

        public TemaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tema>> GetAll()
        {
            return await _context.Temas.
                            Include(p => p.Postagem).
                            ToListAsync();
        }

        public async Task<Tema?> Create(Tema tema)
        {
            await _context.Temas.AddAsync(tema);
            await _context.SaveChangesAsync();
            return tema;
        }

        public async Task Delete(Tema tema)
        {
            _context.Temas.Remove(tema);
            await _context.SaveChangesAsync();
        }

        public async Task<Tema?> GetById(long id)
        {
            try {
                var tema = await _context.Temas.
                                Include(p => p.Postagem).
                                FirstAsync(i => i.Id == id);
                return tema;
            }
            catch {
                return null;
            }
        }

        public async Task<IEnumerable<Tema>> GetByDescricao(string descricao)
        {
            var Tema = await _context.Temas.
                                Include(p => p.Postagem).
                                Where(p => p.Descricao.Contains(descricao)).
                                ToListAsync();

            return Tema;
        }

        public async Task<Tema?> Update(Tema tema)
        {
            var TemaUpdate = await _context.Temas.FindAsync(tema.Id);
            if(TemaUpdate is null)
                return null;
            _context.Entry(TemaUpdate).State = EntityState.Detached;
            _context.Entry(tema).State = EntityState.Modified;
            await _context.SaveChangesAsync(); 

            return tema;
        }
    }
}
