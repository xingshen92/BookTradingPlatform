public class ProductBO
{
    public string SKU { get; set; }
    public string Name { get; set; }
    public string PublishingHouse { get; set; }
    public string PublishingAt { get; set; }
    public decimal Price{ get; set; }
    public string Desc { get; set; }
    public string Image { get; set; }
    public string Transaction { get; set; }
    public string Userid { get; set; }

    // 業務邏輯層可以加入額外欄位或方法
    public bool IsAvailable => Transaction == "OnSale";
}
