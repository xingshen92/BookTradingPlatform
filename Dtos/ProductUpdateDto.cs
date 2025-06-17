public class ProductUpdateDto
{
    public string SKU { get; set; }
    public string Name { get; set; }
    public string PublishingHouse { get; set; }
    public string PublishingAt { get; set; }
    public decimal Price { get; set; }
    public string Desc { get; set; }
    public byte[] Image { get; set; }
    public string Transaction { get; set; }
}
