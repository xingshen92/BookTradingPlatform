using BookTradingPlatform.Dtos;
using BookTradingPlatform.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IAdminLogService _adminLogService;

    public ProductController(IProductService productService, IAdminLogService adminLogService)
    {
        _productService = productService;
        _adminLogService = adminLogService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _productService.GetAllAsync();

        if (!response.IsSuccess)
            return NotFound(new { IsSuccess = response.IsSuccess, ErrorCode = response.ErrorCode, ErrorMessage = response.ErrorMessage });

        return Ok(new { IsSuccess = response.IsSuccess, Data = response.Data });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _productService.GetByIdAsync(id);

        if (!response.IsSuccess)
            return NotFound(new { IsSuccess = response.IsSuccess, ErrorCode = response.ErrorCode, ErrorMessage = response.ErrorMessage });

        return Ok(new { IsSuccess = response.IsSuccess, Data = response.Data });
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] int id, ProductCreateDto dto)
    {
        var response = await _productService.CreateAsync(dto);

        if (!response.IsSuccess)
            return NotFound(new { IsSuccess = response.IsSuccess, ErrorCode = response.ErrorCode, ErrorMessage = response.ErrorMessage });

        await _adminLogService.AddLogAdminAsync(id, "新增商品資料");

        return Ok(new { IsSuccess = response.IsSuccess, Data = response.Data });
    }

    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update([FromForm] int id, ProductUpdateDto dto)
    {
        var response = await _productService.UpdateAsync(id, dto);

		if (!response.IsSuccess)
			return NotFound(new { IsSuccess = response.IsSuccess, ErrorCode = response.ErrorCode, ErrorMessage = response.ErrorMessage });

        await _adminLogService.AddLogAdminAsync(id, "修改商品資料");

		return Ok(new { IsSuccess = response.IsSuccess, Data = response.Data });
	}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //   var response = await _productService.DeleteAsync(id);

	//	if (!response.IsSuccess)
	//		return NotFound(new { IsSuccess = response.IsSuccess, ErrorCode = response.ErrorCode, ErrorMessage = response.ErrorMessage });

	//	return Ok(new { IsSuccess = response.IsSuccess, Data = response.Data });
	//}
}