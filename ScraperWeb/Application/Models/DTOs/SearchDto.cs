using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class SearchDto
    {
        [Required]
        public string Keywords { get; set; }
        [Required]
        public string Url { get; set; }
    }
}
