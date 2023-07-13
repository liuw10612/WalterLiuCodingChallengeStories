using Microsoft.AspNetCore.Mvc;
using NexTechCodingChallengeStories.Web.Services.Repository;
using NexTechCodingChallengeStories.Web.Services.Entities;
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

        [HttpGet("allStories")]
        public async Task<IActionResult> GetAllStories()
        {
            int page = 1;
            int pageSize = 10;

            const int LIMIT = 20;
            try
            {
                //var newStories = await _storyRepository.GetNewStoriesAsync();
                //return Ok(newStories.Select(d =>
                //new
                //{
                //    id=d
                //}));
                List<StoryTitle> stories = new List<StoryTitle>();

                List<int> allStories = await _storyRepository.GetNewStoriesAsync();
                // sort  des
                var allStoriesSortedDes = allStories.OrderByDescending(x => x);

                // take one page
                var selectedPage = allStoriesSortedDes.Skip((page - 1) * pageSize).Take(pageSize);

                int iCount = 0;
                foreach (int storyId in selectedPage)
                {
                    var story = await _storyRepository.GetByIdAsync(storyId);

                    if (story != null && Uri.IsWellFormedUriString(story.url, UriKind.RelativeOrAbsolute))
                    {
                        StoryTitle oneStory = new StoryTitle {
                            id = story.id, 
                            time=story.time,
                            title = story.title,
                            url = story.url,
                        };

                        DateTime testTime = new DateTime((long)story.time);

                        stories.Add(oneStory);

                        // test only too slow to load all
                        iCount += 1;
                        if (iCount > LIMIT)
                            break;
                    }
                }

                return Ok(stories.OrderByDescending(x => x.time));
            }
            catch(Exception e)
            {
                return new ObjectResult($"Error, http error : {e.Message}") { StatusCode=500 };
            }

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
                    return Ok(stories.OrderByDescending(x => x.time));
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while OnePage");
                return  NotFound();
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