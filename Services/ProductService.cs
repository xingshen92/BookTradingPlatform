using AutoMapper;
using BookTradingPlatform.Common;
using BookTradingPlatform.Controllers.Models;
using BookTradingPlatform.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

public interface IProductService
{
	Task<Result<IEnumerable<ProductVO>>> GetAllAsync();
	Task<Result<IEnumerable<ProductVO>>> GetByIdAsync(int id);
	Task<Result<ProductVO>> CreateAsync(ProductCreateDto dto);
	Task<Result<ProductVO>> UpdateAsync(int id, ProductUpdateDto dto);
	//Task<Result<ProductVO>> DeleteAsync(int id);
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

    private bool GetMemberNumberById(int id, out string result) //取得擁有者ID
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            result = ErrorCodes.UserNotFound;
            return false; //如果找不到使用者，則返回空字串
		}

        result = user.MemberNumber;
        return true;
	}

	private bool CheckName(string name, out string result) //檢查商品名稱是否符合規範
    {
        if (string.IsNullOrEmpty(name)) //檢查商品名稱是否為空
		{
            result = ErrorCodes.ProductNameIsNull;
			return false;
        }

        if (name.Length < 3) //檢查商品名稱長度是否小於3
		{
            result = ErrorCodes.ProductNameTooShort;
			return false;
		}
        else if(name.Length > 50) //檢查商品名稱長度是否大於50
        {
            result = ErrorCodes.ProductNameTooLong;
            return false;
		}

		result = "";
		return true;
	}

    private bool CheckPublishingHouse(string publishingHouse, out string result) //檢查出版社是否符合規範
    {
        if (string.IsNullOrEmpty(publishingHouse)) //檢查出版社是否為空
        {
            result = ErrorCodes.ProductPublishingHouseIsNull;
            return false;
		}

        if (publishingHouse.Any(char.IsWhiteSpace)) //檢查出版社是否包含空格
        {
            result = ErrorCodes.ProductPublishingHouseSpace;
            return false;
        }
        // 檢查出版社格式是否正確 (未完成)
        //if (!System.Text.RegularExpressions.Regex.IsMatch(publishingHouse, @"^[a-zA-Z0-9\s]+$"))
        //{
        //  result = ErrorCodes.ProductPublishingHouseFormatError;
        //  return false;
		//}

        result = "";
        return true;
    }

    private bool CheckPublishingAt(string publishingAt, out string result) //檢查出版時間是否符合規範
    {
        DateTime currentyear = default;// 取得出版時間 

		if (string.IsNullOrEmpty(publishingAt)) //檢查出版時間是否為空
        {
            result = ErrorCodes.ProductPublishingAtIsNull;
            return false;
        }

        if (publishingAt.Any(char.IsWhiteSpace)) //檢查出版時間是否包含空格
        {
            result = ErrorCodes.ProductPublishingAtSpace;
            return false;
		}

		if (!DateTime.TryParse(publishingAt,out DateTime number)) //檢查出版時間格式是否正確
        {
            result = ErrorCodes.ProductPublishingAtFormatError;
            return false;
		}
        else
        {
			currentyear = number;
		}

        if(currentyear > DateTime.UtcNow) //檢查出版時間年分是否大於當前時間年分
        {
            result = ErrorCodes.ProductPublishingAtTimeError;
		    return false;
        }
       
		result = "";
        return true;
    }

    private bool CheckPrice(decimal? price, out string result) //檢查價格是否符合規範
    {
        if (string.IsNullOrEmpty(price.ToString())) //檢查價格是否為空
        {
            result = ErrorCodes.ProductPriceIsNull;
            return false;
        }

		if (decimal.IsNegative((decimal)price!)) //檢查價格是否為負數
        {
            result = ErrorCodes.ProductPriceIsNegative;
            return false;
        }

        if(price > 100000) //檢查價格是否大於100000
        {
            result = ErrorCodes.ProductPriceTooHigh;
            return false;
        }

        result = "";
        return true;
    }

    private bool CheckDesc(string desc, out string result) //檢查描述是否符合規範
    {
        if (string.IsNullOrEmpty(desc)) //檢查描述是否為空
        {
            result = ErrorCodes.ProductDescIsNull;
            return false;
        }

        if (desc.Length > 1000) //檢查描述長度是否大於1000
        {
            result = ErrorCodes.ProductDescTooLong;
            return false;
        }

        result = "";
        return true;
	}

    private bool CheckImage(IFormFile? image, out string result) //檢查圖片是否符合規範
    {
        if (image == null || image.Length == 0) //檢查圖片是否為空
        {
            result = ErrorCodes.ProductImageIsNull;
            return false;
        }

		result = "";
        return true;
	}

	public static async Task<byte[]> ToByteArrayAsync(IFormFile file) //將IFormFile轉換為byte陣列
	{
		using var ms = new MemoryStream();
		await file.CopyToAsync(ms);
		return ms.ToArray();
	}

	public async Task<Result<IEnumerable<ProductVO>>> GetAllAsync() //取得所有商品
    {
		var products = await _context.Products.Include(p => p.User).ToListAsync();

		if (!products.Any())
            return Result<IEnumerable<ProductVO>>.Failure(ErrorCodes.ProductNotFound);

		return Result<IEnumerable<ProductVO>>.Success(_mapper.Map<IEnumerable<ProductVO>>(products));
	}

    public async Task<Result<IEnumerable<ProductVO>>> GetByIdAsync(int id) //根據ID取得商品
    {
        var userid = ""; //擁有者ID (MemberNumber)

		if (!GetMemberNumberById(id, out string useridresult)) //檢查是否能取得擁有者ID
        {
            return Result<IEnumerable<ProductVO>>.Failure(useridresult);
        }
        else
        {
            userid = useridresult; //取得擁有者ID
		}

		var product = await _context.Products.Include(p => p.User).Where(p => p.Userid == userid).ToListAsync();

		if (product == null)
            return Result<IEnumerable<ProductVO>>.Failure(ErrorCodes.ProductNotFound);

		return Result<IEnumerable<ProductVO>>.Success(_mapper.Map<IEnumerable<ProductVO>>(product));
	}

    public async Task<Result<ProductVO>> CreateAsync(ProductCreateDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.MemberNumber == dto.MemberNumber);
		byte[] imageBytes; //用於存儲圖片的位元組陣列

		if (user == null) 
            return Result<ProductVO>.Failure(ErrorCodes.UserNotFound);

        if (!CheckName(dto.Name ?? string.Empty, out string nameresult)) //檢查商品名稱
            return Result<ProductVO>.Failure(nameresult);

        if (!CheckPublishingHouse(dto.PublishingHouse ?? string.Empty, out string publishinghouseresult)) //檢查出版社
            return Result<ProductVO>.Failure(publishinghouseresult);

        if (!CheckPublishingAt(dto.PublishingAt ?? string.Empty, out string publishingatresult)) //檢查出版時間
            return Result<ProductVO>.Failure(publishingatresult);

        if (!CheckPrice(dto.Price, out string priceresult)) //檢查價格
            return Result<ProductVO>.Failure(priceresult);

        if (!CheckDesc(dto.Desc ?? string.Empty, out string desresult)) //檢查描述
            return Result<ProductVO>.Failure(desresult);

        if (!CheckImage(dto.Image, out string imageresult)) //檢查圖片
        {
            return Result<ProductVO>.Failure(imageresult);
        }
        else
        {
            imageBytes = await ToByteArrayAsync(dto.Image!); //將圖片轉換為byte陣列
		}
           
		var product = _mapper.Map<Product>(dto); //使用 AutoMapper 將 DTO 轉換為 Entity
        product.ModifiedAt = DateTime.UtcNow; //設定最後更新時間
        product.Transaction = "OnSale"; //預設狀態為 OnSale
        product.Userid = user.MemberNumber; //設定擁有者ID
        product.User = user; //設定擁有者
        product.Image = imageBytes; //設定圖片

		_context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Result<ProductVO>.Success(_mapper.Map<ProductVO>(product));//使用 AutoMapper 將 Entity 轉換為 VO
	}

    public async Task<Result<ProductVO>> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        var changeproduct = product!;
		byte[] imageBytes;

		if (product == null) 
            return Result<ProductVO>.Failure(ErrorCodes.ProductNotFound);

		if (dto.Image != null && dto.Image.Length > 0) //先檢查是否有上傳圖片
		{
			if (!CheckImage(dto.Image, out string imageresult))
			{
				return Result<ProductVO>.Failure(imageresult, _mapper.Map<ProductVO>(product));
			}

            imageBytes = await ToByteArrayAsync(dto.Image); //將圖片轉換為byte陣列

            if (product.Image.SequenceEqual(imageBytes)) // 確認圖片有無變更
            {
                if (ProductUpdateDto.IsSame(dto, product)) //檢查是否有其他屬性變更
				{
                    return Result<ProductVO>.Success(_mapper.Map<ProductVO>(product)); //如果沒有變更，直接返回原有的商品資料
	            }     
            }

            changeproduct.Image = imageBytes;
        }


        if(dto.Name != product.Name) //檢查商品名稱是否有變更
        {
            if (!CheckName(dto.Name ?? string.Empty, out string nameresult)) //檢查商品名稱
            {
                return Result<ProductVO>.Failure(nameresult, _mapper.Map<ProductVO>(product));
			}
            
            changeproduct.Name = dto.Name!; //更新商品名稱
        }

        if(dto.PublishingHouse != product.PublishingHouse) //檢查出版社是否有變更
        {
            if (!CheckPublishingHouse(dto.PublishingHouse ?? string.Empty, out string publishinghouseresult)) //檢查出版社
            {
                return Result<ProductVO>.Failure(publishinghouseresult, _mapper.Map<ProductVO>(product));
			}

            changeproduct.PublishingHouse = dto.PublishingHouse!; //更新出版社
		}

        if(dto.PublishingAt != product.PublishingAt) //檢查出版時間是否有變更
        {
            if (!CheckPublishingAt(dto.PublishingAt ?? string.Empty, out string publishingatresult)) //檢查出版時間
            {
                return Result<ProductVO>.Failure(publishingatresult, _mapper.Map<ProductVO>(product));
			}

			changeproduct.PublishingAt = dto.PublishingAt!; //更新出版時間
		}

        if(dto.Price != product.Price) //檢查價格是否有變更
        {
            if (!CheckPrice(dto.Price, out string priceresult)) //檢查價格
            {
                return Result<ProductVO>.Failure(priceresult, _mapper.Map<ProductVO>(product));
            }

            changeproduct.Price = (decimal)dto.Price!; //更新價格
        }

        if(dto.Desc != product.Desc) //檢查描述是否有變更
        {
            if (!CheckDesc(dto.Desc ?? string.Empty, out string desresult)) //檢查描述
            {
                return Result<ProductVO>.Failure(desresult, _mapper.Map<ProductVO>(product));
            }

            changeproduct.Desc = dto.Desc!; //更新描述
        }

		changeproduct.SKU = dto.SKU!;
        changeproduct.Transaction = dto.Transaction!;
        changeproduct.ModifiedAt = DateTime.Now;

        _context.Products.Update(changeproduct); //更新商品資料
		await _context.SaveChangesAsync();

        return Result<ProductVO>.Success(_mapper.Map<ProductVO>(changeproduct)); //使用 AutoMapper 將 Entity 轉換為 VO
	}

	//public async Task<Result<ProductVO>> DeleteAsync(int id) //刪除商品(未完成)
	//{
	//    var product = await _context.Products.FindAsync(id);
	//    if (product == null) return false;

	//    _context.Products.Remove(product);
	//    await _context.SaveChangesAsync();
	//    return true;
	//}
}