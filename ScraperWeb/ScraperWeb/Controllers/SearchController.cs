using Application.Common.Interfaces;
using Application.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;


namespace ScraperWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;

        }

        [HttpGet(Name = "GetSearches")]
        public async Task<IEnumerable<SearchResponseDto>> Get()
        {

            return await _searchService.Get();
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Post([FromBody] SearchDto dto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);


            var result = await _searchService.Search(dto.Url, dto.Keywords);

            if (result == null)
                return NotFound();

            var searchResult = new SearchResponseDto()
            {
                Url = result.Url,
                Keywords = result.Keywords,
                Position = result.Occurrences > 0 ? result.Ranking.Trim() : "0",
                Date = result.SearchDate.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture),
                Occurrences = result.Occurrences
            };

            return Ok(searchResult);
        }
    }
}
