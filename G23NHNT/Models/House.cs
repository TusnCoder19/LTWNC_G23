using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace G23NHNT.Models
{
    public partial class House
    {
        public House()
        {
            HouseDetails = new HashSet<HouseDetail>();
            Reviews = new HashSet<Review>();
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

        // Store AmenityIds as a JSON string in the database
        [Column("AmenityIds")]
        public string AmenityIds { get; set; } = "[]"; // Default to an empty array

        // Use a getter and setter to handle the JSON conversion to List<int>
        [NotMapped]
        public List<int> AmenityIdsArray
        {
            get => string.IsNullOrEmpty(AmenityIds) ? new List<int>() : JsonConvert.DeserializeObject<List<int>>(AmenityIds);
            set => AmenityIds = JsonConvert.SerializeObject(value);
        }
    }
}
