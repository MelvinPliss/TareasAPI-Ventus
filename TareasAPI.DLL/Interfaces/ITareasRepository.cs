using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TareasDLL.Models;

namespace TareasDLL.Interfaces
{
    public interface ITareasRepository
    {
        Task<IEnumerable<Tarea>> GetAllAsync();
        Task<Tarea?> GetByIdAsync(int id);
        Task<Tarea?> GetByUsuarioAndTituloAsync(int usuarioId, string titulo);
        Task AddAsync(Tarea tarea);
        Task UpdateAsync(Tarea tarea);
        Task DeleteAsync(Tarea tarea);
    }
}
