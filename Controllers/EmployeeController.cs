using CoreWebAPI.Models;
using CoreWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employee;
        private readonly IDepartmentRepository _department;
        private readonly IWebHostEnvironment  _env;
        public EmployeeController(IEmployeeRepository employee, IDepartmentRepository department, IWebHostEnvironment env)
        {
            _employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _department = department ?? throw new ArgumentNullException(nameof(department));
            _env = env;
        }
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employee.GetEmployees());
        }
        [HttpGet]
        [Route("GetEmployeeID/{Id}")]
        public async Task<IActionResult> GetEmpByID(int id)
        {
            return Ok(await _employee.GetEmployeeByID(id));
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(Employee emp)
        {
            var result = await _employee.InsertEmployee(emp);
            if(result.EmployeeID == 0) { 
            return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put(Employee emp)
        {
           var result  =  await _employee.UpdateEmployee(emp);
            return Ok($"Updated Successfully: {result}");
        }
        [HttpDelete]
        [Route("DeleteEmployee")]
        public JsonResult Delete(int id) { 
        var result = _employee.DeleteEmployee(id);
            return new JsonResult($"deleted Successfully : {result}");
        }


        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {

                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/"+ filename;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    stream.CopyTo(stream);
                }
                return new JsonResult(filename);
            
            }
            catch (Exception) {
                return new JsonResult("anonymous.png");
            }
        }

        [HttpGet]
        [Route("GetDepartment")]
        public async Task <IActionResult> GetAllDepartmentNames()
        {
            return Ok(await _department.GetDepartment());
        }
    }
}
