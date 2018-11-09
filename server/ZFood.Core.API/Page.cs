using System;
using System.Collections.Generic;

namespace ZFood.Core.API
{
    public class Page<T> 
    {
        public IEnumerable<T> Items { get; set; }

        public bool HasMore { get; set; }

        public int ItemsQuantity { get; set; }
    }
}
