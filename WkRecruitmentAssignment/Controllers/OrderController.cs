﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Order.Create;
using Application.ShoppingCart.AddItem;
using Application.ShoppingCart.UseDiscountCode;
using MediatR;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("Create")]
        public async Task<int> Create([FromBody] CreateRequest createRequest)
        {
            //if (!Enum.TryParse(paymentMethod, out PaymentMethod paymentMethodEnum))
            //{
            //    // Note DK: It's better to use custom Application exception and handle it in Exception middleware.
            //    // Middleware can set up proper HTTP status code and provide consistent payload describing error.
            //    throw new ArgumentException("Wrong payment method value.");
            //}

            //var createRequest = new CreateRequest(shoppingCartId, paymentMethodEnum, remarks);
            return await _mediator.Send(createRequest);
        }
    }
}
