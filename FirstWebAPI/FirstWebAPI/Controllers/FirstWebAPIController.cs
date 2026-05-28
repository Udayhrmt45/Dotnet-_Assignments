using Microsoft.AspNetCore.Mvc;
using WebAPI.common.Models;
using WebAPI.service.Abstraction;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstWebAPIController : ControllerBase
    {   
            private readonly IEmployeeService _employeeService;

            public FirstWebAPIController(IEmployeeService employeeService)
            {
                _employeeService = employeeService;
            }

            /// <summary>
            /// Get all employees
            /// </summary>
            [HttpGet]
            public async Task<IActionResult> GetEmployees()
            {
                try
                {
                    var employees = await _employeeService.GetEmployeesAsync();

                    return Ok(employees);
                }
                catch (Exception)
                {
                    return StatusCode(500, AppConstants.SomethingWentWrong);
                }
            }

            /// <summary>
            /// Get employee by id
            /// </summary>
            [HttpGet("byid")]
            public async Task<IActionResult> GetEmployeeById([FromQuery] string guidId)
            {
                try
                {
                    if (!Guid.TryParseExact(guidId, "N", out Guid parsedGuid))
                    {
                        return BadRequest("Invalid Guid Format");
                    }
                    var employee = await _employeeService.GetEmployeeByIdAsync(parsedGuid);

                    if (employee == null)
                    {
                        return NotFound(AppConstants.EmployeeNotFound);
                    }

                    return Ok(employee);
                }
                catch (Exception)
                {
                    return StatusCode(500, AppConstants.SomethingWentWrong);
                }
            }

            /// <summary>
            /// Insert employee
            /// </summary>
            [HttpPost]
            public async Task<IActionResult> InsertEmployee([FromBody] Employee employee)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    await _employeeService.InsertEmployeeAsync(employee);

                    return Ok(AppConstants.EmployeeAdded);
                }
                catch (Exception)
                {
                    return StatusCode(500, AppConstants.SomethingWentWrong);
                }
            }

            /// <summary>
            /// Update employee
            /// </summary>
            [HttpPut]
            public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
            {
                try
                {
                    var isUpdated = await _employeeService.UpdateEmployeeAsync(employee);

                    if (!isUpdated)
                    {
                        return NotFound(AppConstants.EmployeeNotFound);
                    }

                    return Ok(AppConstants.EmployeeUpdated);
                }
                catch (Exception)
                {
                    return StatusCode(500, AppConstants.SomethingWentWrong);
                }
            }

            /// <summary>
            /// Delete employee
            /// </summary>
            [HttpDelete]
            public async Task<IActionResult> DeleteEmployee([FromQuery] string guidId)
            {
                try
                {
                    if (!Guid.TryParseExact(guidId, "N", out Guid parsedGuid))
                    {
                        return BadRequest("Invalid Guid Format");
                    }

                    var isDeleted = await _employeeService.DeleteEmployeeAsync(parsedGuid);

                    if (!isDeleted)
                    {
                        return NotFound(AppConstants.EmployeeNotFound);
                    }

                    return Ok(AppConstants.EmployeeDeleted);
                }
                catch (Exception)
                {
                    return StatusCode(500, AppConstants.SomethingWentWrong);
                }
            }
        
    }   
}
