using Shop.GermanBilliard.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Domain
{
    public class Cue : BaseDomainEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public float Price { get; set; }
        public int? Sale { get; set; }
    }
}
