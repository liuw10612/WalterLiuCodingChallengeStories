using Microsoft.AspNetCore.Mvc;
using NexTechCodingChallengeStories.Web.Services.Repository;
using NexTechCodingChallengeStories.Web.Services.Model;
using NexTechCodingChallengeStories.Web.Services.Interfaces;

namespace NexTechCodingChallengeStories.Web.Controllers
{
    [ApiController]
    [Route("codechallenge")]
    public class StoriesController : Controller
    {
        private readonly ILogger<StoriesController> _logger;
        private IStoryRepository _storyRepository;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public StoriesController(ILogger<StoriesController> logger, StoryRepository storyRepository)
        {
            _logger = logger;
            _storyRepository = storyRepository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var newStories = _storyRepository.GetNewStoriesAsync();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
 

        [HttpGet("onePage")]
        public async Task<IActionResult> OnePage(
            [FromQuery(Name = "p")] int currentPage,
            [FromQuery(Name = "ps")] int pageSize)
        {
            try
            {
                var stories = await _storyRepository.GetOnePageStoriesAsync(currentPage, pageSize);
                if (stories.Count>0)
                    return Ok(stories.OrderByDescending(x => x.Time));
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while OnePage");
                return  NotFound();
            }
        }

        [HttpGet("onePageFullSearch")]
        public async Task<IActionResult> OnePageFullSearch([FromQuery(Name = "s")]  string searchText)
        {
            try
            {
                var stories = await _storyRepository.GetOnePageFullSearchStoriesAsync(searchText);
                if (stories.Count > 0)
                    return Ok(stories.OrderByDescending(x => x.Time));
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while OnePage");
                return NotFound();
            }

        }
        [HttpGet("storiesCount")]
        public async Task<IActionResult> GetStoriesCount()
        {
            try
            {
                int storiesCount = await _storyRepository.GetStoriesCountAsync();
                return Ok(storiesCount);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while OnePage");
                return new ObjectResult($"Error, http error : {e.Message}") { StatusCode = 500 };
            }

        }
    }
}