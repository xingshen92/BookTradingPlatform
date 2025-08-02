public class ProductVO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PublishingHouse { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Transaction { get; set; } = string.Empty;

    public ProductVO() { }
}
