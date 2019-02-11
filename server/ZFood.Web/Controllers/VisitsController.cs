﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IVisitService service;

        public VisitsController(IVisitService service)
        {
            this.service = service;
        }

        // GET /Visit/5
        [HttpGet("{id}", Name = "GetVisit")]
        public async Task<ActionResult<VisitDTO>> Get(string id)
        {
            var visit = await service.FindById(id);

            if (visit == null)
            {
                return NotFound();
            }

            return visit.ToDTO();
        }

        [HttpGet]
        public async Task<ActionResult<PageDTO<VisitDTO>>> Get(int skip, int take, bool count, string query)
        {
            var page = await service.Get(skip, take, count, query);
            return page.ToDTO(v => v.ToDTO());
        }

        // POST Visit
        [HttpPost]
        public async Task<ActionResult<VisitDTO>> Post([FromBody] CreateVisitRequestDTO dto)
        {
            var createdVisit = await service.CreateVisit(dto.FromDTO());
            return CreatedAtRoute("GetVisit", new { id = createdVisit.Id }, createdVisit.ToDTO());
        }

        // PUT visit/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateVisitRequestDTO dto)
        {
            await service.UpdateVisit(dto.FromDTO(id));
            return NoContent();
        }

        // DELETE visit/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await service.DeleteVisit(id);
            return NoContent();
        }
    }
}
