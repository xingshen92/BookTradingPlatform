using BookTradingPlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace BookTradingPlatform.Controllers.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		public int SKU { get; set; }
		public string SKUName { get; set; } 
		public string Publishing_house { get; set; } //出版社
		public DateTime Publishing_at { get; set; } //出版時間
		public int Price { get; set; } //價格
		public string Desc { get; set; } //商品簡介
		public string Image { get; set; } //圖片
		public DateTime Modified_at { get; set; } = DateTime.Now; //最後更新時間
		public string Transaction {  get; set; } //交易狀態
		public User User { get; set; }

	}
}
