using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G23NHNT.Models
{
    public partial class Amenity
    {
        public Amenity()
        {
        }

        [Key]
        public int IdAmenity { get; set; }

        [Required(ErrorMessage = "Tên tiện nghi là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên tiện nghi phải dưới 100 ký tự.")]
        public string Name { get; set; } = null!;
    }
}
