using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TareasDLL.Models;

namespace TareasBLL.Interfaces
{
    public interface IReportesService
    {
        Task<IEnumerable<SP_GetPendingTasksResult>> GetPendingTasksReportAsync();
    }
}
