using Microsoft.EntityFrameworkCore;
using TareasBLL.Interfaces;
using TareasDLL.Interfaces;
using TareasDLL.Models;

namespace TareasBLL.Services
{
    public class TareasService : ITareasService
    {
        private readonly ITareasRepository _repo;

        public TareasService(ITareasRepository repo)
        {
            _repo = repo;
        }

        public async Task<(IEnumerable<Tarea> Items, int TotalRecords)> GetFilteredAsync(
            string? prioridad, string? estatus, int? usuarioId,
            DateTime? fechaInicio, DateTime? fechaFin,
            int page, int pageSize)
        {
            var query = (await _repo.GetAllAsync()).AsQueryable();

            if (!string.IsNullOrEmpty(prioridad))
                query = query.Where(t => t.Prioridad == prioridad);

            if (!string.IsNullOrEmpty(estatus))
                query = query.Where(t => t.Estatus == estatus);

            if (usuarioId.HasValue)
                query = query.Where(t => t.UsuarioId == usuarioId.Value);

            if (fechaInicio.HasValue)
                query = query.Where(t => t.FechaInicio >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(t => t.FechaFinalizacion <= fechaFin.Value);

            var totalRecords = query.Count();

            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (items, totalRecords);
        }

        public async Task<Tarea?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(Tarea tarea)
        {
            if (string.IsNullOrWhiteSpace(tarea.Titulo))
                throw new ArgumentException("El título es obligatorio");

            if (tarea.Descripcion?.Length > 500)
                throw new ArgumentException("La descripción máximo 500 caracteres");

            if (tarea.FechaLimite.HasValue && tarea.FechaLimite.Value.Date < DateTime.Today)
                throw new ArgumentException("No se permiten fechas límite menores a hoy");

            var existe = await _repo.GetByUsuarioAndTituloAsync(tarea.UsuarioId, tarea.Titulo);
            if (existe != null)
                throw new ArgumentException("Ya existe una tarea con el mismo título para este usuario");

            try
            {
                await _repo.AddAsync(tarea);
                return tarea.TareaId;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Error al agregar tarea.");
            }
        }

        public async Task<int> UpdateAsync(int id, Tarea tarea)
        {
            if (id != tarea.TareaId)
                throw new ArgumentException("El id de la URL no coincide con el id de la tarea.");

            if (string.IsNullOrWhiteSpace(tarea.Titulo))
                throw new ArgumentException("El título es obligatorio");

            if (tarea.Descripcion?.Length > 500)
                throw new ArgumentException("La descripción máximo 500 caracteres");

            if (tarea.FechaLimite.HasValue && tarea.FechaLimite.Value.Date < DateTime.Today)
                throw new ArgumentException("No se permiten fechas límite menores a hoy");

            try
            {
                await _repo.UpdateAsync(tarea);
                return tarea.TareaId;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Error al actualizar tarea.");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tarea = await _repo.GetByIdAsync(id);
            if (tarea == null)
                throw new KeyNotFoundException("Tarea no encontrada");

            await _repo.DeleteAsync(tarea);
            return true;
        }
    }
}
