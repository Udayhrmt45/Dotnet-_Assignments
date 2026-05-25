using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.common.Models;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstWebAPIController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Department = "HR" },
            new Employee { Id = 2, Name = "Jane Smith", Department = "IT" },
            new Employee { Id = 3, Name = "Bob Johnson", Department = "Finance" }
        };

        //Get all
        [HttpGet]
        public IActionResult GetEmployee()
        {
            return Ok(employees);
        }

        //get by Id

        [HttpGet("byId")]
        public IActionResult GetEmployeeById([FromQuery] int id)
        {
            var employee = employees.FirstOrDefault(x => x.Id == id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employee);
        }

        //post
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            employees.Add(employee);
            return Ok("Employee Added Successfully");
        }

        //update - put
        [HttpPut]
        public IActionResult UpdateEmployee([FromQuery] int id, [FromBody] Employee updatedEmployee) 
        { 
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if(employee == null)
            {
                return NotFound("Employee not found!");
            }

            employee.Name = updatedEmployee.Name;
            employee.Department = updatedEmployee.Department;

            return Ok("Employee Details updated successfully!");
        }

        //delete
        [HttpDelete]
        public IActionResult DeleteEmployee([FromQuery] int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("Employee not found!");
            }
            employees.Remove(employee);
            return Ok("Employee deleted successfully!");

        }


    }

}
