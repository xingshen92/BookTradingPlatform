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
		public string SKU { get; set; } //商品編號
		[Required]
		[MaxLength(100)]
		public string Name { get; set; } //商品名稱
		[MaxLength(100)]
		public string PublishingHouse { get; set; } //出版社
		public string PublishingAt { get; set; } //出版時間
		[Required]
		public decimal Price { get; set; } //價格
		[MaxLength(1000)]
		public string Desc { get; set; } //商品簡介
		public byte[] Image { get; set; } //圖片
		public DateTime ModifiedAt { get; set; } = DateTime.Now; //最後更新時間
		[MaxLength(50)]
		public string Transaction { get; set; } //交易狀態
		[Required]

		// 外鍵與導覽屬性
		[ForeignKey("User")]
		public int Userid { get; set; }
		public User User { get; set; } //擁有者
    }
}
