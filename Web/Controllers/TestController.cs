using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTOs.Test;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly ITestTableService _testTableService;

        public TestController(ITestTableService testTableService)
        {
            _testTableService = testTableService;
        }

        [HttpPost]
        public void Post([FromBody]InsertTestTableRequestDto request)
        {
            _testTableService.InsertTestTable(request);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_testTableService.GetAll());
        }

    }
}

