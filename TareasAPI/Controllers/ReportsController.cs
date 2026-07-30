using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TareasAPI.Responses;
using TareasBLL.Interfaces;
using TareasDLL.Models;

namespace TareasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportesService _service;

        public ReportsController(IReportesService service)
        {
            _service = service;
        }

        [HttpGet("pending-tasks")]
        public async Task<IActionResult> GetPendingTasksReport()
        {
            var report = await _service.GetPendingTasksReportAsync();
            return Ok(new ApiResponse<SP_GetPendingTasksResult>
            {
                Status = true,
                Datos = report.ToList()
            });
        }
    }
}
