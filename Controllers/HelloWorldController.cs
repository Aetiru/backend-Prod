using Backend.Contracts;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RickController : ControllerBase
    {
        private readonly IRickServices _rickServices;

        private readonly ILogger<RickController> _logger;


        public RickController(IRickServices rickServices, ILogger<RickController> logger)
        {
            _rickServices = rickServices;

            _logger = logger;

        }

        [HttpPost]
        public async Task<IActionResult> CreateRickAsync(CreateRickRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {

                await _rickServices.CreateRickAsync(request);
                return Ok(new { message = "Blog post successfully created" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });

            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int page = 1, int pageSize = 10, string searchTerm = "")
        {
            try
            {
                var (characters, totalCount) = await _rickServices.GetAllAsync(page, pageSize, searchTerm);
                if (characters == null || !characters.Any())
                {
                    return Ok(new { message = "No Todo Items  found" });
                }


                return Ok(new
                {
                    message = "Successfully retrieved all blog posts",
                    data = characters,
                    totalCount = totalCount,
                    page = page,
                    pageSize = pageSize
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all Tood it posts", error = ex.Message });


            }
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<Rick>> GetById(Guid id)
        {
            try
            {
                var rick = await _rickServices.GetByIdAsync(id);
                if (rick == null)
                {
                    return NotFound();
                }
                return Ok(rick);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRick(Guid id, [FromBody] UpdateRickRequest request)
        {
            try
            {
                _logger.LogInformation($"Attempting to update Rick with id: {id}");
                await _rickServices.UpdateRickAsync(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating Rick with id {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRick(Guid id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete Rick with id: {id}");
                await _rickServices.DeleteRickAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting Rick with id {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }


}