using DummyProjectApi.BusinessModel.Model;
using DummyProjectApi.Repositories.UserTypeRepository;
using Microsoft.AspNetCore.Authorization;
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
    public class UserTypeController : Controller
    {
        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeController(IUserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }

        
        [HttpGet("GetAllActiveUserTypes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userTypeRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("GetUserTypeById/{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserType>> GetUserTypeById(int id)
        {
            try
            {
                var result = await _userTypeRepository.GetById(id);
                if (result == null)
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

        [HttpPost("AddNewUserType")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserType>> AddUserType([FromBody] UserType userType)
        {
            try
            {
                if (userType == null)
                {
                    return BadRequest();
                }
                userType.CreatedDate = DateTime.UtcNow;
                userType.UpdatedDate = DateTime.UtcNow;
                userType.IsActive = true;
                var createdUser = await _userTypeRepository.Save(userType);
                return CreatedAtAction(nameof(GetAllUsers), new { Id = createdUser.Id }, createdUser);
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
        public async Task<ActionResult<UserType>> Update(int id, UserType userType)
        {
            try
            {
                if (id != userType.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var updateUserType = await _userTypeRepository.GetById(id);
                if (updateUserType == null)
                {
                    return NotFound($"User Type with Id {id} not found");
                }
                return await _userTypeRepository.Update(userType);
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
        public async Task<ActionResult<UserType>> Delete(int id)
        {
            try
            {
                var deleteUserType = await _userTypeRepository.GetById(id);
                if (deleteUserType == null)
                {
                    return NotFound($"User Type with Id {id} not found");
                }
                return await _userTypeRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("SearchUserType/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<UserType>>> Search(string name)
        {
            try
            {
                var q = await _userTypeRepository.Search(name);
                if (q.Any())
                {
                    return Ok(q);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
