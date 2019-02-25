using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZFood.Core.API;
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

        /// <summary>
        /// Searches for visits
        /// </summary>
        /// <param name="take">Number of visits to take</param>
        /// <param name="skip">Number of visits to skip</param>
        /// <param name="query">Query</param>
        /// <param name="count">Count visits</param>
        /// <response code="200">
        /// Returned code when a page, with the given parameters, can be build successfully
        /// </response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PageDTO<VisitDTO>>> Get(int skip, int take, bool count, string query)
        {
            log.Debug("Searching for some visits");
            var page = await service.Get(skip, take, count, query);
            return page.ToDTO(v => v.ToDTO());
        }

        // GET /Visit/5
        /// <summary>
        ///  Finds a Visit by Id
        /// </summary>
        /// <param name="id">Id of the Visit to be found</param>
        /// <response code="200">
        /// Returned code when a Visit with the specified Id can be found
        /// </response>
        /// <response code="404">
        /// Returned code when a Visit with the specified Id cannot be found
        /// </response>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetVisit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Creates a Visit with the given data
        /// </summary>
        /// <param name="dto">Contains the necessary fields to create a Visit.</param>
        /// <response code="201">
        /// Returned code when the Visit can be created successfully
        /// </response>
        /// <response code="404">
        /// Returned code when trying to create a new Visit with either an nonexistent User or Restaurant in it
        /// </response> 
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<VisitDTO>> Post([FromBody] CreateVisitRequestDTO dto)
        {
            log.Debug("Trying to create a visit");
            var createdVisit = await service.CreateVisit(dto.FromDTO());
            return CreatedAtRoute("GetVisit", new { id = createdVisit.Id }, createdVisit.ToDTO());
        }

        // PUT visit/5
        /// <summary>
        /// Updates a Visit with the given data
        /// </summary>
        /// <param name="id">Id of the Visit to be updated. Be sure that this Visit already exists</param>
        /// <param name="dto">Contains the data of the Visit to be changed</param>
        /// <response code="204">
        /// Returned code when the Visit can be updated successfully
        /// </response>
        /// <response code="404">
        /// Returned code when:
        /// - cannot find the Visit with the given Id, 
        /// - neither the User nor the Restaurant present in the request exist 
        /// </response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateVisitRequestDTO dto)
        {
            log.Debug($"Trying to edit visit {id}");
            await service.UpdateVisit(dto.FromDTO(id));
            return NoContent();
        }

        // DELETE visit/5
        /// <summary>
        /// Deletes a Visit by Id
        /// </summary>
        /// <param name="id">Id of the Visit to be deleted</param>
        /// <response code="204">
        /// Returned code when a Visit can be deleted successfully
        /// </response> 
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Delete(string id)
        {
            log.Debug($"Deleting visit {id}");
            await service.DeleteVisit(id);
            return NoContent();
        }
    }
}
