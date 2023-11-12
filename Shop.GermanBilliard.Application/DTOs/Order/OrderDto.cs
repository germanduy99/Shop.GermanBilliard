using Shop.GermanBilliard.Application.DTOs.Common;
using Shop.GermanBilliard.Application.DTOs.ItemOrder;
using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.DTOs.Order
{
    public class OrderDto : BaseDto
    {
        public List<OrderItemDto> Items { get; set; } = new();
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
