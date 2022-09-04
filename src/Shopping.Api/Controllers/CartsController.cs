using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Models;
using Shopping.Application.Carts.Commands;
using Shopping.Application.Carts.Queries;
using Shopping.Domain.Errors;

namespace Shopping.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ILogger<CartsController> _logger;
        private const string Version = "1.0";
        private ISender Mediatr => HttpContext.RequestServices.GetRequiredService<ISender>();

        public CartsController(ILogger<CartsController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCart()
        {
            try
            {
                var result = await Mediatr.Send(new CreateCartCommand());

                return result.IsFailed
                    ? StatusCode(500)
                    : Created(
                        new Uri($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/carts/{result.Value}", UriKind.Absolute),
                        new SuccessResponse(result.Value, Version));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating a cart");
                return StatusCode(500);
            }
        }
        
        [HttpGet]
        [Route("{cartId}")]
        public async Task<IActionResult> GetCart([FromRoute] string cartId)
        {
            try
            {
                var result = await Mediatr.Send(new GetCartByIdQuery(cartId));

                if (result.IsFailed && result.HasError<CartNotFoundError>())
                    return NotFound();

                var cart = result.Value;
                
                return result.IsFailed
                    ? StatusCode(500)
                    : Ok(new CartResponse(cart.Id, Version,
                        new CartResponseData(cart.Id, cart.Items.Select(x => new CartItem(x.Id, x.Quantity)).ToList()) { Updated = cart.ModifiedOn }));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("{cartId}/items")]
        public async Task<IActionResult> AddItem([FromRoute] string cartId, [FromBody] AddItemRequest request)
        {
            try
            {
                var result = await Mediatr.Send(new AddItemCommand(request.ProductId, request.Quantity, cartId));

                if (result.IsFailed && result.HasError<CartNotFoundError>())
                    return NotFound();
                
                return result.IsFailed ? StatusCode(500) : Created(
                    new Uri($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/carts/{cartId}/items/{request.ProductId}", UriKind.Absolute),
                    new SuccessResponse(cartId, Version));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPatch]
        [Route("{cartId}/items/{productId}")]
        public async Task<IActionResult> UpdateItem([FromRoute] string cartId, [FromRoute] string productId, [FromBody] UpdateItemRequest request)
        {
            try
            {
                var result = await Mediatr.Send(new UpdateItemCommand(productId, request.Quantity, cartId));
                
                if (result.IsFailed && result.HasError<CartNotFoundError>())
                    return NotFound();

                return result.IsFailed ? StatusCode(500) : Ok(new SuccessResponse(cartId, Version));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpDelete]
        [Route("{cartId}/items/{productId}")]
        public async Task<IActionResult> RemoveItem([FromRoute] string cartId, [FromRoute] string productId)
        {
            try
            {
                var result = await Mediatr.Send(new RemoveItemCommand(productId, cartId));
                
                if (result.IsFailed && result.HasError<CartNotFoundError>())
                    return NotFound();
                
                return result.IsFailed ? StatusCode(500) : Ok(new SuccessResponse(cartId, Version));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
