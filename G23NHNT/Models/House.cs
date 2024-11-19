﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G23NHNT.Models
{
    public partial class House
    {
        public House()
        {
            HouseDetails = new HashSet<HouseDetail>();
            Reviews = new HashSet<Review>();
            IdAmenities = new HashSet<Amenity>();
        }

        [Key]
        public int IdHouse { get; set; }

        [Required(ErrorMessage = "Tên nhà trọ là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên nhà trọ phải dưới 100 ký tự.")]
        public string NameHouse { get; set; } = null!;

        [Required(ErrorMessage = "Tiêu đề bài viết là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tiêu đề bài buộc phải dưới 100 ký tự.")]
        public string PostTitle { get; set; } = null!;

        [Required(ErrorMessage = "IdUser không được để trống.")]
        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public virtual Account? IdUserNavigation { get; set; }


        [Required]
        [ForeignKey("IdHouseType")]
        public int HouseTypeId { get; set; }

        public virtual HouseType? HouseType { get; set; }


        public virtual ICollection<HouseDetail> HouseDetails { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Amenity> IdAmenities { get; set; }
    }
}
