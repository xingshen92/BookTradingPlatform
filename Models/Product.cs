using BookTradingPlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace BookTradingPlatform.Controllers.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		public int SKU { get; set; } //商品編號
		public string Name { get; set; } //商品名稱
		public string PublishingHouse { get; set; } //出版社
		public string PublishingAt { get; set; } //出版時間
		public int Price { get; set; } //價格
		public string Desc { get; set; } //商品簡介
		public string Image { get; set; } //圖片
		public DateTime ModifiedAt { get; set; } = DateTime.Now; //最後更新時間
		public string Transaction {  get; set; } //交易狀態
		public User User { get; set; }
    }
}
