using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G23NHNT.Models
{
    public partial class HouseDetail
    {
        [Key]
        public int IdHouseDetail { get; set; }

        [Required(ErrorMessage = "IdHouse không được để trống.")]
        public int IdHouse { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được quá 200 ký tự.")]
        public string Address { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "Giá thuê phải là một số dương.")]
        public decimal Price { get; set; }

        public double DienTich { get; set; }

        [Required(ErrorMessage = "Tiền điện là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tiền điện không được quá 50 ký tự.")]
        public string TienDien { get; set; } = null!;

        [Required(ErrorMessage = "Tiền nước là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tiền nước không được quá 50 ký tự.")]
        public string TienNuoc { get; set; } = null!;

        [Required(ErrorMessage = "Tiền dịch vụ là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tiền dịch vụ không được quá 50 ký tự.")]
        public string TienDv { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự.")]
        public string? Describe { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        public string Status { get; set; } = null!;

        [StringLength(255, ErrorMessage = "Tên hình ảnh không được quá 255 ký tự.")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Số người ở bắt buộc.")]
        public int SoNguoiO { get; set; } = 1;

        public DateTime TimePost { get; set; }


        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được quá 15 ký tự.")]
        public string PhoneNumber { get; set; } = null!;

        // Foreign Key for House
        [ForeignKey("IdHouse")]
        public virtual House? IdHouseNavigation { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
