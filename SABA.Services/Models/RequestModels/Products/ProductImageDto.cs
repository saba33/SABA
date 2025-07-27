namespace SABA.Services.Models.RequestModels.Products
{
    public class ProductImageDto
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] ImageData { get; set; }
    }
}
