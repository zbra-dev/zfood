using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
using ZFood.Web.DTO;
using ZFood.Web.Extensions;

namespace ZFood.Web.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class VisitsController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VisitsController));

        private readonly IVisitService service;

        public VisitsController(IVisitService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageDTO<VisitDTO>>> Get(int skip, int take, bool count, string query)
        {
            log.Debug("Searching for some visits");
            var page = await service.Get(skip, take, count, query);
            return page.ToDTO(v => v.ToDTO());
        }

        // GET /Visit/5
        [HttpGet("{id}", Name = "GetVisit")]
        public async Task<ActionResult<VisitDTO>> Get(string id)
        {
            log.Debug($"Searching for visit {id}");
            var visit = await service.FindById(id);

            if (visit == null)
            {
                log.Debug($"Visit {id} was not found");
                return NotFound();
            }

            return visit.ToDTO();
        }

        // POST Visit
        [HttpPost]
        public async Task<ActionResult<VisitDTO>> Post([FromBody] CreateVisitRequestDTO dto)
        {
            try
            {
                log.Debug("Trying to create a visit");
                var createdVisit = await service.CreateVisit(dto.FromDTO());
                return CreatedAtRoute("GetVisit", new { id = createdVisit.Id }, createdVisit.ToDTO());
            }
            catch
            {
                log.Debug("Fail on trying to create a visit");
                return BadRequest();
            }
        }

        // PUT visit/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateVisitRequestDTO dto)
        {
            log.Debug($"Trying to edit visit {id}");
            try
            {
                await service.UpdateVisit(dto.FromDTO(id));
                return NoContent();
            }
            catch (EntityNotFoundException exception)
            {
                log.Debug($"Fail on trying to edit visit {id}");
                return NotFound(exception.Message);
            }
            catch
            {
                log.Debug($"Fail on trying to edit visit {id}");
                return BadRequest();
            }
        }

        // DELETE visit/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            log.Debug($"Deleting visit {id}");
            await service.DeleteVisit(id);
            return NoContent();
        }
    }
}
