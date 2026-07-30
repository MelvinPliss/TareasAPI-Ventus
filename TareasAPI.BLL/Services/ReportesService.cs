using TareasBLL.Interfaces;
using TareasDLL.Interfaces;
using TareasDLL.Models;

namespace TareasBLL.Services
{
    public class ReportesService : IReportesService
    {
        private readonly IReportesRepository _reportesRepository;

        public ReportesService(IReportesRepository reportesRepository)
        {
            _reportesRepository = reportesRepository;
        }

        public async Task<IEnumerable<SP_GetPendingTasksResult>> GetPendingTasksReportAsync()
        {
            return await _reportesRepository.GetPendingTasksReportAsync();
        }
    }
}
