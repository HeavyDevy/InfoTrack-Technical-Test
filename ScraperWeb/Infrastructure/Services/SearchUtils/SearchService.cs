using Application.Common.Interfaces;
using ScraperWeb.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScraperWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using Infrastructure.HttpClients.External;
using Application.Models.DTOs;
using Application.Settings;
using Microsoft.Extensions.Options;
using HtmlAgilityPack;

namespace Infrastructure.Services.SearchUtils
{
    public class SearchService : ISearchService
    {
        private readonly ScraperDbContext _context;
        private readonly ISearchProvider _searchProvider;        

        public SearchService(ScraperDbContext context, ISearchProvider searchProvider)
        {
            _searchProvider = searchProvider;
            _context = context;
        }

        public async Task<IEnumerable<SearchResponseDto>> Get()
        {
            var searches = await _context.Searches.OrderByDescending(x => x.Date).ToListAsync();

            var mapped = searches.Select(x => ToSearchDtoMap(x));
            return mapped;

        }

        public async Task<SearchHistoryDto> Search(string url, string keywords)
        {
            var responseBody = await _searchProvider.GetSearchResponse(keywords);

            var results = GetSearchResults(responseBody, url);

            if (responseBody.Contains("cookies"))
                throw new Exception("VPN not set to non-cookie consent country");

            var searchResult = new Search
            {
                Url = url,
                Keywords = keywords,
                Position = string.Join(", ", results),
                Occurrences = results.Count(),
                Date = DateTime.UtcNow.ToLocalTime()
            };
            await SaveSearch(searchResult);


            // TODO: AutoMapper this Search > SearchDTO
            return new
                SearchHistoryDto
            {
                Keywords = keywords,
                Occurrences = searchResult.Occurrences,
                Ranking = searchResult.Position,
                SearchDate = searchResult.Date,
                Url = searchResult.Url
            };
        }

        #region Private Methods

        private List<string> GetSearchResults(string responseBody, string url)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseBody);
            var results = new List<string>();

            IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants(0).Where(n => n.HasClass("sCuL3"));

            int position = 1;

            foreach (HtmlNode item in nodes)
            {
                if (position > 99)
                    break;

                var webNode = item.FirstChild;

                if (webNode.InnerHtml.StartsWith(url))
                    results.Add(position.ToString());

                position++;


            }



            return results;

        }

        public static SearchResponseDto ToSearchDtoMap(Search search)
        {
            return new SearchResponseDto()
            {
                Id = search.Id,
                Url = search.Url,
                Date = search.Date.ToString("dd-MM-yyyy hh:mm"),
                Occurrences = search.Occurrences,
                Keywords = search.Keywords,
                Position = search.Position



            };
        }

        private async Task SaveSearch(Search search)
        {
            await _context.Searches.AddAsync(search);

            _context.SaveChanges();
        }

        #endregion


    }

    
}