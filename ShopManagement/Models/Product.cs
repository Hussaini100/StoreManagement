using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class Product
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "نام محصول الزامی است")]
        //[StringLength(100, ErrorMessage = "نام محصول نمی‌تواند بیش از 100 کاراکتر باشد")]
        //[Display(Name = "نام محصول")]
        public string Name { get; set; }

        //[StringLength(500, ErrorMessage = "توضیحات نمی‌تواند بیش از 500 کاراکتر باشد")]
        //[Display(Name = "توضیحات")]
        public string Description { get; set; } = string.Empty;

        //[Required(ErrorMessage = "قیمت الزامی است")]
        //[Range(0, double.MaxValue, ErrorMessage = "قیمت باید عدد مثبت باشد")]
        //[Display(Name = "قیمت")]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = "تعداد موجودی الزامی است")]
        //[Range(0, int.MaxValue, ErrorMessage = "تعداد موجودی باید عدد مثبت باشد")]
        //[Display(Name = "موجودی")]
        public int StockQuantity { get; set; }

        //[Required(ErrorMessage = "دسته‌بندی الزامی است")]
        //[StringLength(50, ErrorMessage = "دسته‌بندی نمی‌تواند بیش از 50 کاراکتر باشد")]
        //[Display(Name = "دسته‌بندی")]
        public string Category { get; set; } = string.Empty;

        //[Display(Name = "آدرس تصویر")]
        public string ImageUrl { get; set; } = "/images/default-product.png";

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}