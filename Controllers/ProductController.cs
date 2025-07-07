using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.ProductDto;
using itsc_dotnet_practice.Services;
using itsc_dotnet_practice.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace itsc_dotnet_practice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllProductsAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductModelRequest request)
    {
        try
        {
            var product = await _service.CreateProductAsync(request);
            return Ok(product);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductModelRequest product)
    {
        var result = await _service.UpdateProductAsync(id, product);
        if (result == null) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteProductAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }
}