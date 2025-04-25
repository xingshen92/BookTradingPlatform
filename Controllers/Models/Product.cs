using System.ComponentModel.DataAnnotations;

namespace BookTradingPlatform.Controllers.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		public int SKU { get; set; }
		public string SKUName { get; set; } 
		public string publishing_house { get; set; } //出版社
		public DateTime publishing_at { get; set; } //出版時間
		public int price { get; set; } //價格
		public string desc { get; set; } //商品簡介
		public string image { get; set; } //圖片
		public DateTime Modified_at { get; set; } = DateTime.Now; //最後更新時間
		public string transaction {  get; set; } //交易狀態
	}
}
