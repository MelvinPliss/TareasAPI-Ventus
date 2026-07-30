using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TareasDLL.Models;

namespace TareasDLL.Interfaces
{
    public interface IReportesRepository
    {
        Task<IEnumerable<SP_GetPendingTasksResult>> GetPendingTasksReportAsync();
    }
}
