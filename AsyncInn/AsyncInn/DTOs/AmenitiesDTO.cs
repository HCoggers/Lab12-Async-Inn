﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.DTOs
{
    public class AmenitiesDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IEnumerator<AmenitiesDTO> GetEnumerator()
        {
            return null;
        }
    }
}