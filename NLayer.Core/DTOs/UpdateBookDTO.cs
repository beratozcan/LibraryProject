﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class UpdateBookDTO
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public string Publisher { get; set; }

        public int Page { get; set; }

        public bool haveRead { get; set; }

        public bool isBorrowed { get; set; }

        public int CategoryId { get; set; }
    }
}