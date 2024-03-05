using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class SearchHistoryDto
    {        
        public string Url { get; set; }
        public string Keywords { get; set; }
        public string Ranking { get; set; }
        public DateTime SearchDate { get; set; }
        public int Occurrences { get; set; }
    }
}
