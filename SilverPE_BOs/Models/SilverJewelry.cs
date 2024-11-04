using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SilverPE_BOs.Models;

public partial class SilverJewelry
{
    [Required]
    public string SilverJewelryId { get; set; } = null!;

    [Required]
    [RegularExpression("^[A-Z][a-zA-Z0-9\\s]*$", ErrorMessage = "SilverJewelryName must start with an uppercase letter followed by letters or digits.")]
    public string SilverJewelryName { get; set; } = null!;

    [Required]
    public string? SilverJewelryDescription { get; set; }

    [Required]
    public decimal? MetalWeight { get; set; }

    [Required]
    [Range(0.0, double.MaxValue, ErrorMessage = "Price must be >= 0.")]
    public decimal? Price { get; set; }

    [Required]
    [Range(1900, double.MaxValue, ErrorMessage = "Production year must be >= 1900.")]
    public int? ProductionYear { get; set; }

    [Required]
    public DateTime? CreatedDate { get; set; } = DateTime.Now;

    [Required]
    public string? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
