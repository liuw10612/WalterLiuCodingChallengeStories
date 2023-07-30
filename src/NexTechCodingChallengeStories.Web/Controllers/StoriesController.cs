using Microsoft.AspNetCore.Mvc;
using CodingChallengeStories.Web.Services.Model;
using CodingChallengeStories.Web.Services.DataProvider;


/**
 * 
 * The HTTP routes controller
 * To receive HTTP requests from httpData.service.ts
 * To call CodingChallengeStories.Web.Services to finish the job
 * 
 */
namespace CodingChallengeStories.Web.Controllers
{
    [ApiController]
    [Route("codechallenge")]
    public class StoriesController : Controller
    {
        private readonly ILogger<StoriesController> _logger;
        private IStoryDataProvider _storyDataProvider;

        public StoriesController(ILogger<StoriesController> logger, StoryDataProvider storyDataProvider)
        {
            _logger = logger;
            _storyDataProvider = storyDataProvider;
        }
        /// <summary>
        /// Asking to load one page of stories
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("onePage")]
        public async Task<IActionResult> OnePage(
            [FromQuery(Name = "p")] int currentPage,
            [FromQuery(Name = "ps")] int pageSize)
        {
            try
            {
                var stories = await _storyDataProvider.GetOnePageStoriesAsync(currentPage, pageSize);
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
        /// <summary>
        /// Asking to do a full search for all the stories
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet("onePageFullSearch")]
        public async Task<IActionResult> OnePageFullSearch([FromQuery(Name = "s")]  string searchText)
        {
            try
            {
                var stories = await _storyDataProvider.GetStoriesFullSearchAsync(searchText);
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
        /// <summary>
        /// Asked to get the total number of stories
        /// </summary>
        /// <returns></returns>
        [HttpGet("storiesCount")]
        public async Task<IActionResult> GetStoriesCount()
        {
            try
            {
                int storiesCount = await _storyDataProvider.GetStoriesCountAsync();
                return Ok(storiesCount);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while OnePage");
                return new ObjectResult($"Error, http error : {e.Message}") { StatusCode = 500 };
            }
        }
        [HttpGet("cacheInfo")]
        public  ActionResult  GetCacheInfo()
        {
            return Ok(_storyDataProvider.GetCacheInfo());
        }
    }
}