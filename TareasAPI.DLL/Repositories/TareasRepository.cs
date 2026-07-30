using Microsoft.EntityFrameworkCore;
using TareasDLL.Context;
using TareasDLL.Interfaces;
using TareasDLL.Models;

namespace TareasDLL.Repositories
{
    public class TareasRepository : ITareasRepository
    {
        private readonly DB_AppDbContext _context;

        public TareasRepository(DB_AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarea>> GetAllAsync()
        {
            return await _context.Tareas.ToListAsync();
        }

        public async Task<Tarea?> GetByIdAsync(int id)
        {
            return await _context.Tareas.FindAsync(id);
        }

        public async Task<Tarea?> GetByUsuarioAndTituloAsync(int userId, string titulo)
        {
            return await _context.Tareas.FirstOrDefaultAsync(n => n.UsuarioId == userId && n.Titulo == titulo);
        }

        public async Task AddAsync(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tarea tarea)
        {
            _context.Tareas.Update(tarea);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tarea tarea)
        {
            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
        }
    }
}

