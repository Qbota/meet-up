using Microsoft.AspNetCore.Mvc;
using WebApplication.Mock.Entity;
using WebApplication.Mock.Service;

namespace WebApplication.Mock.Controller
{
    [ApiController]
    [Route("mock")]
    public class MockController : ControllerBase
    {
        private readonly IMockService _mockService = new MockService();

        [HttpGet("entity")]
        public IActionResult FetchAll()
        {
            return Ok(_mockService.GetAllMockEntities());
        }

        [HttpPost("entity")]
        public IActionResult Create(MockEntity mockEntity)
        {
            return Ok(_mockService.InsertMockEntity(mockEntity));
        }
    }
}