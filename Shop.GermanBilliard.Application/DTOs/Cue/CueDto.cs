using Shop.GermanBilliard.Application.DTOs.Brand;
using Shop.GermanBilliard.Application.DTOs.Common;
using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.DTOs.Cue
{
    public class CueDto : BaseDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int BrandId { get; set; }
        public float Price { get; set; }
        public int? Sale { get; set; }
    }
}
