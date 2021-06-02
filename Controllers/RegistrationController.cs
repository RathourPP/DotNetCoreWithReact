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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Registration>> GetRegistrationDataById(int id)
        {
            try
            {
                var result = await _employeeRepository.GetById(id);
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Registration>> Register([FromBody] Registration employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                employee.CreatedDate = DateTime.UtcNow;
                employee.UpdatedDate = DateTime.UtcNow;
                employee.IsActive = true;
                var createdEmployee = await _employeeRepository.Save(employee);
                return CreatedAtAction(nameof(GetRegistrationData), new { Id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut("id:int")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Registration>> Update(int id,Registration employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var updateEmployee = await _employeeRepository.GetById(id);
                if(updateEmployee==null)
                {
                    return NotFound($"Employee with Id {id} not found");
                }
                return await _employeeRepository.Update(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Registration>> Delete(int id)
        {
            try
            {
                var deleteEmployee = await _employeeRepository.GetById(id);
                if (deleteEmployee == null)
                {
                    return NotFound($"Employee with Id {id} not found");
                }
                return await _employeeRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{search}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
