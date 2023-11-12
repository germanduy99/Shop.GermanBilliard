using Shop.GermanBilliard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Domain
{
    public class Brand : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
