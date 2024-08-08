using Microsoft.AspNetCore.Mvc;
using ECommerce.Business.Absract;
using ECommerce.DataAcces.Models;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ProductReviewController : ControllerBase
{
    private readonly IProductReviewService _productReviewService;

    public ProductReviewController(IProductReviewService productReviewService)
    {
        _productReviewService = productReviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productReviewService.GetAllProductReviewsAsync();
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _productReviewService.GetProductReviewByIdAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductReview productReview)
    {
        var result = await _productReviewService.AddProductReviewAsync(productReview);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductReview productReview)
    {
        var result = await _productReviewService.UpdateProductReviewAsync(productReview);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, int deletedBy)
    {
        var result = await _productReviewService.DeleteProductReviewAsync(id, deletedBy);
        return StatusCode((int)result.StatusCode, result);
    }
}
