using Shop.GermanBilliard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Domain
{
    public class Order : BaseDomainEntity
    {
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
