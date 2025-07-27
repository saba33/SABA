using System.ComponentModel.DataAnnotations;

namespace SABA.Core.Models.ProductModel
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public byte[] ImageData { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

    }
}
