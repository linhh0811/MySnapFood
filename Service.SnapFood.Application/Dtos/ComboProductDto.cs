﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public  class ComboProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }= string.Empty;
        public int Quantity { get; set; }
   
       
    }
}
