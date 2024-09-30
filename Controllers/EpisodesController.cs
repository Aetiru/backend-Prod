using Backend.Contracts;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodesServices _episodesServices;

        private readonly ILogger<EpisodesController> _logger;


        public EpisodesController(IEpisodesServices episodeServices, ILogger<EpisodesController> logger)
        {
            _episodesServices = episodeServices;

            _logger = logger;

        }

        [HttpPost]
        public async Task<IActionResult> CreateEpisodeAsync(CreateEpisodeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {

                await _episodesServices.CreateEpisodeAsync(request);
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
                var (episodes, totalCount) = await _episodesServices.GetAllAsync(page, pageSize, searchTerm);
                if (episodes == null || !episodes.Any())
                {
                    return Ok(new { message = "No Todo Items  found" });
                }


                return Ok(new
                {
                    message = "Successfully retrieved all blog posts",
                    data = episodes,
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
        public async Task<ActionResult<Episode>> GetById(Guid id)
        {
            try
            {
                var episode = await _episodesServices.GetByIdAsync(id);
                if (episode == null)
                {
                    return NotFound();
                }
                return Ok(episode);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEpisode(Guid id, [FromBody] UpdateEpisodeRequest request)
        {
            try
            {
                _logger.LogInformation($"Attempting to update Rick with id: {id}");
                await _episodesServices.UpdateEpisodeAsync(id, request);
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
                await _episodesServices.DeleteEpisodeAsync(id);
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