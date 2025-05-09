﻿using System.ComponentModel.DataAnnotations;

namespace StockManagementDemo.API.DTOs
{
    public class StockItemCreateDto
    {
        [Required]
        public string RegNo { get; set; } = string.Empty;

        [Required]
        public string Make { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        public int ModelYear { get; set; }

        public int KMS { get; set; }

        public string Colour { get; set; } = string.Empty;

        public string VIN { get; set; } = string.Empty;

        public decimal RetailPrice { get; set; }

        public decimal CostPrice { get; set; }
    }
}
