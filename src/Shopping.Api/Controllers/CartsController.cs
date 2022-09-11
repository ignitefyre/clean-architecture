using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Extensions;
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
        private readonly IMapper _mapper;
        private const string Version = "1.0";
        private ISender Mediatr => HttpContext.RequestServices.GetRequiredService<ISender>();

        public CartsController(ILogger<CartsController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
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
                        HttpContext.Request.AsCartResourceUri(result.Value),
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
                    : Ok(_mapper.Map<CartResponse>(cart));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting the cart");
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
                    HttpContext.Request.AsCartItemResourceUri(cartId, request.ProductId),
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
