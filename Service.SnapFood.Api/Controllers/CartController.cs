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
           
            var cart = await _cartService.GetCartByIdUserAsync(userId);
            return Ok(cart);
            
        }

        [HttpGet("view/{userId}")]
        public async Task<IActionResult> GetCartView(Guid userId)
        {
           
            var cart = await _cartService.GetCartByIdUserAsyncView(userId);
            return Ok(cart);
           
        }

        [HttpGet("CartId/{cartId}")]
        public async Task<IActionResult> GetCartByCartId(Guid cartId)
        {
           
            var cart = await _cartService.GetCartByCartIdAsync(cartId);
            return Ok(cart);
            
        }

        [HttpGet("CartId/View/{cartId}")]
        public async Task<IActionResult> GetCartByCartIdView(Guid cartId)
        {
           
            var cart = await _cartService.GetCartByCartIdAsyncView(cartId);
            return Ok(cart);
           
        }

        [HttpGet("ListCart/{userId}")]
        public IActionResult GetListCart(Guid userId)
        {
           
            var carts =  _cartService.GetListCartByUserId(userId);
            return Ok(carts);
            
        }

        [HttpGet("Address/{userId}")]
        public IActionResult GetAddressCheckout(Guid userId)
        {
           
            var address = _cartService.GetAddressCheckout(userId);
            return Ok(address);
            
        }

        [HttpPost("AddComboToCart")]
        public async Task<IActionResult> AddComboToCart([FromBody] AddComboToCartDto item)
        {
           
            await _cartService.AddComboToCartAsync(item);
            return Ok();
           
        }

        [HttpPost("AddProductToCart")]
        public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartDto item)
        {
           
            if (item == null || item.ProductId == Guid.Empty || item.Quantity <= 0)
            {
                return BadRequest(new { error = "Dữ liệu không hợp lệ" });
            }
            await _cartService.AddProductToCartAsync(item);
            return Ok();
          
        }

        // PUT: api/cart/update/{cartItemId}
        [HttpPut("Update/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(Guid cartItemId, [FromBody] int quantity)
        {
           
            await _cartService.UpdateCartItemAsync(cartItemId, quantity);
            return Ok(new { message = "Cart item update successfully." });
            
        }

        // DELETE: api/cart/remove/{cartItemId}
        [HttpDelete("RemoveCart/{cartId}")]
        public async Task<IActionResult> RemoveCart(Guid cartId)
        {
           
            await _cartService.RemoveCartAsync(cartId);
            return Ok();
           
        }

        // DELETE: api/cart/remove/{cartItemId}
        [HttpDelete("Remove/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(Guid cartItemId)
        {
            
            await _cartService.RemoveCartItemAsync(cartItemId);
            return Ok();
           
        }

       

        // DELETE: api/cart/removecombo/{cartComboItemId}
        [HttpDelete("RemoveCombo/{cartComboItemId}")]
        public async Task<IActionResult> RemoveComboItem(Guid cartComboItemId)
        {
            
            await _cartService.RemoveComboItemAsync(cartComboItemId);
            return Ok();
            
        }

        // PUT: api/cart/updatecombo/{cartComboItemId}
        [HttpPut("UpdateCombo/{cartComboItemId}")]
        public async Task<IActionResult> UpdateComboItem(Guid cartComboItemId, [FromBody] int quantity)
        {
            await _cartService.UpdateComboItemAsync(cartComboItemId, quantity);
            return Ok();
           
        }

        // POST: api/cart/checkout
        [HttpPost("Checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutDto item)
        {

            await _cartService.CheckOut(item);
            return Ok(item);
           
        }

        // POST: api/cart/checkout
        [HttpPost("KiemTraCheckout")]
        public async Task<IActionResult> KiemTraCheckout([FromBody] CheckOutDto item)
        {

            await _cartService.CheckOutValidateOnly(item);
            return Ok(item);

        }

        [HttpPost("KiemTraCheckoutTaiQuay")]
        public async Task<IActionResult> KiemTraCheckoutTaiQuay([FromBody] CheckOutTaiQuayDto item)
        {

            await _cartService.CheckOutDatHangTaiQuay(item);
            return Ok(item);

        }

        // POST: api/cart/checkout
        [HttpPost("CheckOutTaiQuay")]
        public async Task<IActionResult> CheckOutTaiQuay([FromBody] CheckOutTaiQuayDto item)
        {
            
            
            var billId = await _cartService.CheckOutDatHangTaiQuay(item);
            return Ok(billId);
           
        }

        [HttpGet("Quantity/{userId}")]
        public IActionResult GetCartQuantity(Guid userId)
        {
            
            var quantity =  _cartService.GetCartQuantity(userId);
            return Ok(quantity); 
            
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] QuantityInCartDto QuantityInCartDto)
        {
            
            await _cartService.UpdateQuantity(QuantityInCartDto);
            return Ok(); // Trả về số nguyên trực tiếp
          
        }
    }
}