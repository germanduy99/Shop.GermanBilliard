﻿using Shop.GermanBilliard.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.DTOs.Brand
{
    public class BrandDto : BaseDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
