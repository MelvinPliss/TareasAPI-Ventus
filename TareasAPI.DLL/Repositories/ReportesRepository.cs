using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TareasDLL.Context;
using TareasDLL.Interfaces;
using TareasDLL.Models;

namespace TareasDLL.Repositories
{
    public class ReportesRepository : IReportesRepository
    {
        private readonly DB_AppDbContext _dbContext;

        public ReportesRepository(DB_AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SP_GetPendingTasksResult>> GetPendingTasksReportAsync()
        {
            return await _dbContext.Procedures.SP_GetPendingTasksAsync();
        }
    }
}
