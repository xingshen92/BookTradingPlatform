using BookTradingPlatform.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTradingPlatform.Controllers.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string SKU { get; set; } = string.Empty;//商品編號
		[Required]
		[MaxLength(100)]
		public string Name { get; set; } = string.Empty;//商品名稱
		[MaxLength(100)]
		public string PublishingHouse { get; set; } = string.Empty;//出版社
		public string PublishingAt { get; set; } = string.Empty;//出版時間
		[Required]
		public decimal Price { get; set; } //價格
		[MaxLength(1000)]
		public string Desc { get; set; } = string.Empty;//商品簡介
		public byte[] Image { get; set; } = Array.Empty<byte>();//圖片
		public DateTime ModifiedAt { get; set; } = DateTime.Now; //最後更新時間
		[MaxLength(50)]
		public string Transaction { get; set; } = string.Empty;//交易狀態
		[Required]

		// 外鍵與導覽屬性
		[ForeignKey("User")]
		public string Userid { get; set; } = string.Empty;
		public User User { get; set; } = new User();//擁有者
    }
}