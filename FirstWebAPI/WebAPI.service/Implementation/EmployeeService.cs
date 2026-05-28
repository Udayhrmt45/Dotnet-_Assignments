using WebAPI.common.Models;
using WebAPI.service.Abstraction;
using WebAPI.store.Abstraction;

namespace WebAPI.service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Handles employee business logic
        /// </summary>
        private readonly IEmployeeStore _employeeStore;

        /// <summary>
        /// Constructor Injection
        /// </summary>
        public EmployeeService(IEmployeeStore employeeStore)
        {
            _employeeStore = employeeStore;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _employeeStore.GetEmployeesAsync();
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        public async Task<Employee> GetEmployeeByIdAsync(Guid guidId)
        {
            return await _employeeStore.GetEmployeeByIdAsync(guidId);
        }

        /// <summary>
        /// Insert employee
        /// </summary>
        public async Task<bool> InsertEmployeeAsync(Employee employee)
        {
            return await _employeeStore.InsertEmployeeAsync(employee);
        }

        /// <summary>
        /// Update employee
        /// </summary>
        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            return await _employeeStore.UpdateEmployeeAsync(employee);
        }

        /// <summary>
        /// Delete employee
        /// </summary>
        public async Task<bool> DeleteEmployeeAsync(Guid guidId)
        {
            return await _employeeStore.DeleteEmployeeAsync(guidId);
        }
    }
}
