using BookTradingPlatform.Controllers.Models;
using BookTradingPlatform.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

public interface IProductService
{
    Task<IEnumerable<ProductVO>> GetAllAsync();
    Task<ProductVO> GetByIdAsync(int id);
    Task<ProductVO> CreateAsync(ProductCreateDto dto);
    Task<bool> UpdateAsync(int id, ProductUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

public class ProductService : IProductService
{
    private readonly WebDatabase _context;
    private readonly IMapper _mapper;

	public ProductService(WebDatabase context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }

    public async Task<IEnumerable<ProductVO>> GetAllAsync() // 取得所有商品
    {
        var products = await _context.Products.Include(p => p.User).ToListAsync();
        return _mapper.Map<IEnumerable<ProductVO>>(products);
    }

    public async Task<ProductVO> GetByIdAsync(int id) // 根據ID取得商品
    {
        var product = await _context.Products.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
        return product == null ? null : _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> CreateAsync(ProductCreateDto dto)
    {
        
        var user = await _context.Users.FirstOrDefaultAsync(u => u.MemberNumber == dto.MemberNumber);
        if (user == null) throw new Exception($"找不到 MemberNumber = {dto.MemberNumber}");

        var product = _mapper.Map<Product>(dto); // 使用 AutoMapper 將 DTO 轉換為 Entity
        product.ModifiedAt = DateTime.Now; // 設定最後更新時間
        product.Transaction = "OnSale"; // 預設狀態為 OnSale
        product.Userid = user.Id; // 設定擁有者ID
        product.User = user; // 設定擁有者

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductVO>(product); // 使用 AutoMapper 將 Entity 轉換為 VO
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
