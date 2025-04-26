public class ProductCreateDto
{
    public int SKU { get; set; }
    public string Name { get; set; }
    public string PublishingHouse { get; set; }
    public string PublishingAt { get; set; }
    public int Price { get; set; }
    public string Desc { get; set; }
    public string Image { get; set; }
}

public class ProductUpdateDto
{
    public int SKU { get; set; }
    public string Name { get; set; }
    public string PublishingHouse { get; set; }
    public string PublishingAt { get; set; }
    public int Price { get; set; }
    public string Desc { get; set; }
    public string Image { get; set; }
    public string Transaction { get; set; }
}
