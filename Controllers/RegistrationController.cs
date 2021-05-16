using DummyProjectApi.BusinessModel.Model;
using DummyProjectApi.Repositories.EmployeeRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public RegistrationController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetRegistrationData()
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployeesBasicInformation());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeBasicInformation>> GetRegistrationDataById(int Id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployeeBasicInforamtionOnId(Id);
                if(result==null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
