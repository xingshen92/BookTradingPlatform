public class ProductCreateDto
{
    public string SKU { get; set; } // 商品編號
    public string Name { get; set; } // 商品名稱
    public string PublishingHouse { get; set; } // 出版社
    public string PublishingAt { get; set; } // 出版時間
    public decimal Price { get; set; } // 價格
    public string Desc { get; set; } // 商品簡介
    public string Image { get; set; } // 圖片
    
    public string MemberNumber { get; set; } // 新增擁有者ID
}