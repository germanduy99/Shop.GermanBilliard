using Shop.GermanBilliard.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Domain
{
    public class OrderItem : BaseDomainEntity
    {
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int CueId { get; set; }
        public Cue Cue { get; set; }
    }
}
