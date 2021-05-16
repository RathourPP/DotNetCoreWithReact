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

        [HttpGet("{Id:int}")]
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

        [HttpPost]
        public async Task<ActionResult<EmployeeBasicInformation>> RegisterEmployee(EmployeeBasicInformation employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                var createdEmployee = await _employeeRepository.RegisterEmployee(employee);
                return CreatedAtAction(nameof(GetRegistrationData), new { Id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut("Id:int")]
        public async Task<ActionResult<EmployeeBasicInformation>> UpdateEmployee(int Id,EmployeeBasicInformation employee)
        {
            try
            {
                if (Id != employee.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var updateEmployee = await _employeeRepository.GetEmployeeBasicInforamtionOnId(Id);
                if(updateEmployee==null)
                {
                    return NotFound($"Employee with Id {Id} not found");
                }
                return await _employeeRepository.UpdateBasicInformation(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("Id:int")]
        public async Task<ActionResult<EmployeeBasicInformation>> DeleteEmployee(int Id)
        {
            try
            {
                var deleteEmployee = await _employeeRepository.GetEmployeeBasicInforamtionOnId(Id);
                if (deleteEmployee == null)
                {
                    return NotFound($"Employee with Id {Id} not found");
                }
                return await _employeeRepository.DeleteEmployeeBasicInfo(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<EmployeeBasicInformation>>>Search(string name)
        {
            try
            {
                var q =await _employeeRepository.SearchEmployeesBasicInformation(name);
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
