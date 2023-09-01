using Microsoft.AspNetCore.Mvc;
using PruebaIdHealth.Services.Interfaces;
using PruebaIdHealth.Entities;
using Microsoft.AspNetCore.Authorization;

namespace PruebaIdHealth.Controllers.V1;

[Controller]
[Route("api/v1/product")]
public class ProductController : Controller
{

    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Product> products = await _productService.GetAsync();
        return Ok(products);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        await _productService.CreateAsync(product);
        return Ok(new { success = true, message = "The product has been created" });
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] Product product)
    {
        await _productService.UpdateAsync(id, product);
        return Ok(new { success = true, message = "The product has been updated" });
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _productService.DeleteAsync(id);
        return Ok(new { success = true, message = "The product has been deleted" });
    }

}