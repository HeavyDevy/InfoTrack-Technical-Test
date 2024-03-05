using Application.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using ScraperWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchResponseDto>> Get();

        Task<Search> SearchKeywords(Search searchParameter);

        Task<SearchHistoryDto> Search(string url, string keywords);
    }
}
