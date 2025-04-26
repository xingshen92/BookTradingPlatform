using BookTradingPlatform.Controllers.Models;
using BookTradingPlatform.Data;
using Microsoft.EntityFrameworkCore;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task<Product> CreateAsync(ProductCreateDto dto);
    Task<bool> UpdateAsync(int id, ProductUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

public class ProductService : IProductService
{
    private readonly WebDatabase _context;

    public ProductService(WebDatabase context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> CreateAsync(ProductCreateDto dto)
    {
        var product = new Product
        {
            SKU = dto.SKU,
            Name = dto.Name,
            PublishingHouse = dto.PublishingHouse,
            PublishingAt = dto.PublishingAt,
            Price = dto.Price,
            Desc = dto.Desc,
            Image = dto.Image,
            ModifiedAt = DateTime.Now,
            Transaction = "OnSale" // 新增時預設狀態
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        product.SKU = dto.SKU;
        product.Name = dto.Name;
        product.PublishingHouse = dto.PublishingHouse;
        product.PublishingAt = dto.PublishingAt;
        product.Price = dto.Price;
        product.Desc = dto.Desc;
        product.Image = dto.Image;
        product.Transaction = dto.Transaction;
        product.ModifiedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
