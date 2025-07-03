using Microsoft.AspNetCore.Mvc;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Services.Interface;

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
    public async Task<IActionResult> Create(Product product)
    {
        await _service.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id) return BadRequest();

        var result = await _service.UpdateProductAsync(product);
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
