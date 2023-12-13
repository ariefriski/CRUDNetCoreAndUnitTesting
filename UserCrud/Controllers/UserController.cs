using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserCrud.Models;

namespace UserCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();

            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveAsync(User newUser)
        {
            await _service.SaveAsync(newUser);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result =  _service.GetById(id);
            if(result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
                return NotFound();

            _service.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAsync(User data)
        {
            var result =  _service.GetById(data.UserId);
            if (result == null)
                return NotFound();

            _service.Update(data);
            return NoContent();
        }



    }
}
