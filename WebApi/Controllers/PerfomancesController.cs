using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics.Contracts;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfomancesController : ControllerBase
    {
        private readonly IPerfomanceService _perfomanceService;

        public PerfomancesController(IPerfomanceService perfomanceService)
        {
            _perfomanceService = perfomanceService;
        }

        ///GET api/perfomances
        [HttpGet]
        public async Task<IEnumerable> GetPerfomances(string? name, DateTimeOffset? dateAndTime)
        {
            var perfomances = await _perfomanceService.GetPerfomances(dateAndTime, name);
            return perfomances.Select(MapPerfomanceToDTO);
        }

        ///GET api/perfomances/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPerfomanceById([FromRoute] Guid id)
        {
            var perfomance = await _perfomanceService.GetPerfomanceById(id);
            return perfomance is null ? NotFound() : Ok(MapPerfomanceToDTO(perfomance));
        }

        ///PUT api/perfomances/{id}
        [HttpPut("{id:guid}")]
        public async Task PutPerfomance([FromRoute] Guid id, [FromBody] PerfomanceDTO perfomance)
        {
            await _perfomanceService.SavePerfomance(id, perfomance);
        }


        ///POST api/perfomances
        [HttpPost]
        public async Task CreatePerfomance([FromBody] PerfomanceDTO perfomance)
        {
            await _perfomanceService.CreatePerfomance(perfomance);
        }


        ///DELETE api/perfomances/{id}
        [HttpDelete("{id:guid}")]        
        public async Task DeletePerfomance(Guid id)
        {
            await _perfomanceService.DeletePerfomance(id);
        }

        private PerfomanceDTO MapPerfomanceToDTO(Perfomance perfomance)
        {
            return new PerfomanceDTO
            {
                Id = perfomance.Id,
                Name = perfomance.Name,
                Description = perfomance.Description,
                DateAndTime = perfomance.DateAndTime,
            };
        }
    }
}
