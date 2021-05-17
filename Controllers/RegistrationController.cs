using DummyProjectApi.BusinessModel.Model;
using DummyProjectApi.Repositories.RegistrationRepository;
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
                return Ok(await _employeeRepository.GetAll());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Registration>> GetRegistrationDataById(int Id)
        {
            try
            {
                var result = await _employeeRepository.GetById(Id);
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

        [HttpPost]
        public async Task<ActionResult<Registration>> Register(Registration employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                var createdEmployee = await _employeeRepository.Save(employee);
                return CreatedAtAction(nameof(GetRegistrationData), new { Id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut("Id:int")]
        public async Task<ActionResult<Registration>> Update(int Id,Registration employee)
        {
            try
            {
                if (Id != employee.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var updateEmployee = await _employeeRepository.GetById(Id);
                if(updateEmployee==null)
                {
                    return NotFound($"Employee with Id {Id} not found");
                }
                return await _employeeRepository.Update(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("Id:int")]
        public async Task<ActionResult<Registration>> Delete(int Id)
        {
            try
            {
                var deleteEmployee = await _employeeRepository.GetById(Id);
                if (deleteEmployee == null)
                {
                    return NotFound($"Employee with Id {Id} not found");
                }
                return await _employeeRepository.Delete(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Registration>>>Search(string name)
        {
            try
            {
                var q =await _employeeRepository.Search(name);
                if(q.Any())
                {
                    return Ok(q);
                }
                return NotFound();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
