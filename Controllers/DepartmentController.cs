using CoreWebAPI.Models;
using CoreWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _department;
        public DepartmentController(IDepartmentRepository department)
        {
            _department = department;
            throw new ArgumentNullException(nameof(department));
        }

        [HttpGet]
        [Route ("GetDepartment")]
        public async Task <IActionResult> Get()
        {
            return Ok(await _department.GetDepartment());
        }
        [HttpGet]
        [Route("GetDepartmentByID/{Id}")]
        public async Task <IActionResult> GetDeptById(int id)
        {
            return Ok(await _department.GetDepartmentByID(id));
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> Post(Department dep)
        {
            var result = await _department.InsertDepartment(dep);
            if(result.DepartmentId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");

            }
            return Ok("Added Successfully");

        }

        [HttpPut]
        [Route("UpdateDeparment")]
        public async Task<IActionResult> Put(Department dep)
        {
            await  _department.UpdateDepartment(dep);
            return Ok("Updated Successfullu");
        }

        [HttpDelete]
        [Route("DeletDeparment")]
        public JsonResult Delete(int id)
        {
            _department.DeleteDepartment(id);
            return new JsonResult("Deleted Successfully");
        }
    }
}
