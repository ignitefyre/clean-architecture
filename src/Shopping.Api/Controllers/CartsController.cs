using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Models;

namespace Shopping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private ISender Mediatr => HttpContext.RequestServices.GetRequiredService<ISender>();
        
        [HttpGet]
        [Route("{cartId}")]
        public async Task<IActionResult> GetCart([FromRoute] string cartId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart()
        {
            return Ok();
        }

        [HttpPost]
        [Route("{cartId}/items")]
        public async Task<IActionResult> AddItem([FromRoute] string cartId, [FromBody] AddItemRequest request)
        {
            return Ok();
        }
        
        [HttpPatch]
        [Route("{cartId}/items/{productId}")]
        public async Task<IActionResult> UpdateItem([FromRoute] string cartId, [FromRoute] string productId, [FromBody] UpdateItemRequest request)
        {
            return Ok();
        }
        
        [HttpDelete]
        [Route("{cartId}/items/{productId}")]
        public async Task<IActionResult> RemoveItem([FromRoute] string cartId, [FromRoute] string productId)
        {
            return Ok();
        }
    }
}
