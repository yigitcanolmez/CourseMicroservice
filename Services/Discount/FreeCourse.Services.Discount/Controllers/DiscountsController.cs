using FreeCourse.Services.Discount.Models;
using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IDiscountService _discountService;

        public DiscountsController(ISharedIdentityService sharedIdentityService, IDiscountService discountService)
        {
            _sharedIdentityService = sharedIdentityService;
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAll());
        }   
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResultInstance(await _discountService.GetById(id));
        } 
           
        [HttpGet("GetByCode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            return CreateActionResultInstance(await _discountService.GetByCodeAndUserId(code, _sharedIdentityService.GetUserId));
        }   
        [HttpPost]
        public async Task<IActionResult> Save(DiscountModel model)
        {
            return CreateActionResultInstance(await _discountService.Save(model));
        }
        [HttpPut]
        public async Task<IActionResult> Update(DiscountModel model)
        {
            return CreateActionResultInstance(await _discountService.Update(model));
        }  
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _discountService.Delete(id));
        }


    }
}
