using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TareasDLL.Models;

namespace TareasBLL.Interfaces
{
    public interface ITareasService
    {
        Task<(IEnumerable<Tarea> Items, int TotalRecords)> GetFilteredAsync(string? prioridad, string? estatus, int? usuarioId,
                                                   DateTime? fechaInicio, DateTime? fechaFin,
                                                   int page, int pageSize);
        Task<Tarea?> GetByIdAsync(int id);
        Task<int> CreateAsync(Tarea tarea);
        Task<int> UpdateAsync(int id, Tarea tarea);
        Task<bool> DeleteAsync(int id);
    }

}
