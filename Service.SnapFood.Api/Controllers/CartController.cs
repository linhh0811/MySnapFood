using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using System;
using System.Threading.Tasks;

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

        // GET: api/cart/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(Guid userId)
        {
            try
            {
                var cart = await _cartService.GetCartByIdUserAsync(userId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/cart/addcombo
        [HttpPost("addcombo")]
        public async Task<IActionResult> AddComboToCart([FromBody] AddComboToCartDto item)
        {
            try
            {
                await _cartService.AddComboToCartAsync(item);
                return Ok(new { message = "Combo added to cart successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/cart/addproduct
        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartDto item)
        {
            try
            {
                if (item == null || item.ProductId == Guid.Empty || item.Quantity <= 0)
                {
                    return BadRequest(new { error = "Dữ liệu không hợp lệ" });
                }
                await _cartService.AddProductToCartAsync(item);
                return Ok(new { message = "Product added to cart successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product to cart: {ex}"); // Thêm logging
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT: api/cart/update/{cartItemId}
        [HttpPut("update/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(Guid cartItemId, [FromBody] int quantity)
        {
            try
            {
                await _cartService.UpdateCartItemAsync(cartItemId, quantity);
                return Ok(new { message = "Cart item update successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/cart/remove/{cartItemId}
        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(Guid cartItemId)
        {
            try
            {
                await _cartService.RemoveCartItemAsync(cartItemId);
                return Ok(new { message = "Cart item removed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/cart/clear/{cartId}
        [HttpDelete("clear/{cartId}")]
        public async Task<IActionResult> ClearCart(Guid cartId)
        {
            try
            {
                await _cartService.ClearCart(cartId);
                return Ok(new { message = "Cart cleared successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/cart/checkout
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutDto item)
        {
            try
            {
                await _cartService.CheckOut(item);
                return Ok(new { message = "Checkout successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("quantity/{userId}")]
        public async Task<IActionResult> GetCartQuantity(Guid userId)
        {
            try
            {
                var quantity = await _cartService.GetCartQuantityAsync(userId);
                return Ok(quantity); // Trả về số nguyên trực tiếp
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}