﻿using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpGet]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(StatusCodes.Status200OK));
        }
    }
}