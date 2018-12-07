using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Web.DTO;
using ZFood.Web.Extensions;

namespace ZFood.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private readonly IVisitService service;

        public VisitsController(IVisitService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageDTO<VisitDTO>>> Get(int skip, int take, bool count, string query)
        {
            var page = await service.Get(skip, take, count, query);
            return page.ToDTO(v => v.ToDTO());
        }
    }
}
