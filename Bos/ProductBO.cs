namespace BookTradingPlatform.Bos
{
    public class ProductBO
    {
        public string SKU { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string PublishingHouse { get; set; } = string.Empty;
		public string PublishingAt { get; set; } = string.Empty;
		public decimal Price{ get; set; }
		public string Desc { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public string Transaction { get; set; } = string.Empty;
		public string Userid { get; set; } = string.Empty;

		// 業務邏輯層可以加入額外欄位或方法
		public bool IsAvailable => Transaction == "OnSale";

        public ProductBO() { }
	}
}