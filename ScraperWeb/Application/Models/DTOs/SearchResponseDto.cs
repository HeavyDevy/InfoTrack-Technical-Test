using ScraperWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Models.DTOs
{
    public class SearchResponseDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Keywords { get; set; }
        public string Position { get; set; }
        public string Date { get; set; }
        public int Occurrences { get; set; }


    }


}
