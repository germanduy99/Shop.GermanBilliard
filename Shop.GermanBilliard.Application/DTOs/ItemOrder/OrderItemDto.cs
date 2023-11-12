using Shop.GermanBilliard.Application.DTOs.Common;
using Shop.GermanBilliard.Application.DTOs.Cue;
using Shop.GermanBilliard.Application.DTOs.Order;
using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.DTOs.ItemOrder
{
    public class OrderItemDto : BaseDto
    {
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int OrderId { get; set; }
        public OrderDto Order { get; set; }
        public int CueId { get; set; }
        public CueDto Cue { get; set; }
    }
}
