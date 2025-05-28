using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        //// GET: api/cart/{userId}
        //[HttpGet("{userId}")]
        //public async Task<IActionResult> GetCart(Guid userId)
        //{
        //    var cart = await _cartService.GetCartByIdUserAsync(userId);
        //    return Ok(cart);
        //}

        //// POST: api/cart/add-combo
        //[HttpPost("addcombo")]
        //public async Task<IActionResult> AddComboToCart([FromBody] CartDto item)
        //{
        //    await _cartService.AddComboToCartAsync(item.Id, item.ProductOrComboId, item.Quantity);
        //    return Ok("Combo added to cart successfully.");
        //}

        //[HttpPost("addproduct")]
        //public async Task<IActionResult> AddProductToCart([FromBody] CartDto item)
        //{
        //    await _cartService.AddProductToCartAsync(item.Id, item.ProductOrComboId, item.Quantity);
        //    return Ok(new { Message = "Product added to cart successfully." });
        //}

        //// PUT: api/cart/update
        //[HttpPut("update")]
        //public async Task<IActionResult> UpdateCartItem([FromBody] CartDto item)
        //{
        //    await _cartService.UpdateCartItem(item.ProductOrComboId, item.Quantity);
        //    return Ok("Cart item updated successfully.");
        //}

        //// DELETE: api/cart/remove/{cartItemId}
        //[HttpDelete("remove/{cartItemId}")]
        //public async Task<IActionResult> RemoveCartItem(Guid cartItemId)
        //{
        //    await _cartService.RemoveCartItem(cartItemId);
        //    return Ok("Cart item removed successfully.");
        //}

        //// DELETE: api/cart/clear/{cartId}
        //[HttpDelete("clear/{cartId}")]
        //public async Task<IActionResult> ClearCart(Guid cartId)
        //{
        //    await _cartService.ClearCart(cartId);
        //    return Ok("Cart cleared successfully.");
        //}

        //// POST: api/cart/checkout
        //[HttpPost("checkout")]
        //public async Task<IActionResult> CheckOut([FromBody] CheckOutDto item)
        //{
        //    await _cartService.CheckOut(item);
        //    return Ok("Checkout successful.");
        //}
    }
}
