using Microsoft.AspNetCore.Mvc;
using PruebaIdHealth.Services.Interfaces;
using PruebaIdHealth.Entities;

namespace PruebaIdHealth.Controllers.V1;

[Controller]
[Route("api/v1/store")]
public class StoreController : Controller
{

    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Store> categories = await _storeService.GetAsync();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Store store)
    {
        await _storeService.CreateAsync(store);
        return Ok(new { success = true, message = "The store has been created" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] Store store)
    {
        await _storeService.UpdateAsync(id, store);
        return Ok(new { success = true, message = "The store has been updated" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _storeService.DeleteAsync(id);
        return Ok(new { success = true, message = "The store has been deleted" });
    }

    [HttpPut("{id}/add-category/{categoryId}")]
    public async Task<IActionResult> AddCategoryToStore(string id, string categoryId)
    {
        await _storeService.AddCategoryToStoreAsync(id, categoryId);
        return Ok(new { success = true, message = "The category has been added" });
    }

}