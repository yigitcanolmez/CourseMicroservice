using FreeCourse.Services.Catalog.DTOs;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services.CourseServices;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        [HttpGet("getall")]

        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("getallbyuserid/{id}")]

        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var response = await _courseService.GetAllByUserId(userId);
            return CreateActionResultInstance(response);
        }

        [HttpPost("create")]

        public async Task<IActionResult> Create([FromBody] CourseCreateDTO courseDTO)
        {
            var response = await _courseService.CreateAsync(courseDTO);
            return CreateActionResultInstance(response);
        }
        
        [HttpPut("update")]

        public async Task<IActionResult> Update([FromBody] CourseUpdateDTO courseDTO)
        {
            var response = await _courseService.UpdateAsync(courseDTO);
            return CreateActionResultInstance(response);
        } 
        [HttpDelete("delete/{id}")]

        public async Task<IActionResult> DeleteAsync(string Id)
        {
            var response = await _courseService.DeleteAsync(Id);
            return CreateActionResultInstance(response);
        }
    }
}
