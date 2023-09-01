using Microsoft.AspNetCore.Mvc;
using PruebaIdHealth.Services.Interfaces;
using PruebaIdHealth.Entities;
using Microsoft.AspNetCore.Authorization;

namespace PruebaIdHealth.Controllers.V1;

[Controller]
[Route("api/v1/category")]
public class CategoryController : Controller
{

    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Category> categories = await _categoryService.GetAsync();
        return Ok(categories);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Category category)
    {
        await _categoryService.CreateAsync(category);
        return Ok(new { success = true, message = "The category has been created" });
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] Category category)
    {
        await _categoryService.UpdateAsync(id, category);
        return Ok(new { success = true, message = "The category has been updated" });
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _categoryService.DeleteAsync(id);
        return Ok(new { success = true, message = "The category has been deleted" });
    }

    [Authorize]
    [HttpPut("{id}/add-product/{productId}")]
    public async Task<IActionResult> AddProductToCategory(string id, string productId)
    {
        await _categoryService.AddProductToCategoryAsync(id, productId);
        return Ok(new { success = true, message = "The product has been added" });
    }

}