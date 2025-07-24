using BookTradingPlatform.Controllers.Models;

public class ProductUpdateDto
{
    public string? SKU { get; set; }
    public string? Name { get; set; }
    public string? PublishingHouse { get; set; }
    public string? PublishingAt { get; set; }
    public decimal? Price { get; set; } // 價格，由於是decimal類型，不允許有空格出現在價格中，
										// 因此在前端輸入時需要確保價格格式正確。
	public string? Desc { get; set; }
    public IFormFile? Image { get; set; }
    public string? Transaction { get; set; }

	public static bool IsSame(ProductUpdateDto dto, Product product) //檢查是否有任何變更
	{
		return dto.Name == product.Name &&
			   dto.PublishingHouse == product.PublishingHouse &&
			   dto.PublishingAt == product.PublishingAt &&
			   dto.Price == product.Price &&
			   dto.Desc == product.Desc &&
			   dto.Transaction == product.Transaction;
	}
}