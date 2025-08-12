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
        [HttpPost("AddCartNew/{userId}")]
        public async Task<IActionResult> AddCartNew(Guid userId)
        {
            var id = await _cartService.AddCartNew(userId);
            return Ok(id);
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

        [HttpGet("ListCart/{userId}")]
        public IActionResult GetListCart(Guid userId)
        {
            try
            {
                var carts =  _cartService.GetListCartByUserId(userId);
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Address/{userId}")]
        public IActionResult GetAddressCheckout(Guid userId)
        {
            try
            {
                var address = _cartService.GetAddressCheckout(userId);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddComboToCart")]
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

        [HttpPost("AddProductToCart")]
        public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartDto item)
        {
            try
            {
                if (item == null || item.ProductId == Guid.Empty || item.Quantity <= 0)
                {
                    return BadRequest(new { error = "Dữ liệu không hợp lệ" });
                }
                await _cartService.AddProductToCartAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product to cart: {ex}"); // Thêm logging
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT: api/cart/update/{cartItemId}
        [HttpPut("Update/{cartItemId}")]
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
        [HttpDelete("Remove/{cartItemId}")]
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
        //[HttpDelete("Clear/{cartId}")]
        //public async Task<IActionResult> ClearCart(Guid cartId)
        //{
        //    try
        //    {
        //        await _cartService.ClearCart(cartId);
        //        return Ok(new { message = "Cart cleared successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        // DELETE: api/cart/removecombo/{cartComboItemId}
        [HttpDelete("RemoveCombo/{cartComboItemId}")]
        public async Task<IActionResult> RemoveComboItem(Guid cartComboItemId)
        {
            try
            {
                await _cartService.RemoveComboItemAsync(cartComboItemId);
                return Ok(new { message = "Combo item removed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/cart/updatecombo/{cartComboItemId}
        [HttpPut("UpdateCombo/{cartComboItemId}")]
        public async Task<IActionResult> UpdateComboItem(Guid cartComboItemId, [FromBody] int quantity)
        {
            try
            {
                await _cartService.UpdateComboItemAsync(cartComboItemId, quantity);
                return Ok(new { message = "Combo item updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/cart/checkout
        [HttpPost("Checkout")]
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

        [HttpGet("Quantity/{userId}")]
        public IActionResult GetCartQuantity(Guid userId)
        {
            try
            {
                var quantity =  _cartService.GetCartQuantity(userId);
                return Ok(quantity); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] QuantityInCartDto QuantityInCartDto)
        {
            try
            {
                await _cartService.UpdateQuantity(QuantityInCartDto);
                return Ok(); // Trả về số nguyên trực tiếp
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}