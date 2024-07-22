using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegratorApexPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        [HttpGet]
        [Route("/dowloand/{id}")]
        public async Task downloadFile(int id)
        {

        }
    }
}
