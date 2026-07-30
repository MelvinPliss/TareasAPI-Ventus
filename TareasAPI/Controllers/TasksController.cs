using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TareasAPI.Responses;
using TareasBLL.Interfaces;
using TareasDLL.Models;

namespace TareasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITareasService _service;

        public TasksController(ITareasService service)
        {
            _service = service;
        }

        // GET /tasks con filtros y paginación
        [HttpGet]
        public async Task<IActionResult> GetTasks(string? prioridad, string? estatus, int? usuarioId,
                                                  DateTime? fechaInicio, DateTime? fechaFin,
                                                  int page = 1, int pageSize = 20)
        {
            var (tareas, totalRecords) = await _service.GetFilteredAsync(prioridad, estatus, usuarioId, fechaInicio, fechaFin, page, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var response = new ApiResponse<Tarea>
            {
                Status = true,
                Msg = "Consulta realizada correctamente",
                Datos = tareas.ToList(),
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        // GET /tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var tarea = await _service.GetByIdAsync(id);
            if (tarea == null)
            {
                return NotFound(new ApiResponse<Tarea>
                {
                    Status = false,
                    Msg = "Tarea no encontrada",
                    Value = null
                });
            }

            return Ok(new ApiResponse<Tarea>
            {
                Status = true,
                Msg = "Tarea encontrada",
                Value = tarea
            });
        }

        // POST /tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Tarea tarea)
        {
            var createdId = await _service.CreateAsync(tarea);

            return CreatedAtAction(nameof(GetTask), new { id = createdId }, new ApiResponse<Tarea>
            {
                Status = true,
                Value = tarea,
                Msg = "Tarea creada exitosamente"
            });
        }

        // PUT /tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Tarea tarea)
        {
            // Si ocurre un error, el middleware lo captura
            var updatedId = await _service.UpdateAsync(id, tarea);

            return Ok(new ApiResponse<Tarea>
            {
                Status = true,
                Value = tarea,
                Msg = "Tarea actualizada correctamente"
            });
        }

        // DELETE /tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            // Si ocurre un error, el middleware lo captura
            await _service.DeleteAsync(id);

            return Ok(new ApiResponse<Tarea>
            {
                Status = true,
                Msg = "Tarea eliminada correctamente"
            });
        }
    }
}
