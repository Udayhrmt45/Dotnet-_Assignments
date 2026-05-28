using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.common.Models;

namespace WebAPI.store.Abstraction
{
    public interface IEmployeeStore
    {
        Task<List<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(Guid guidId);

        Task<bool> InsertEmployeeAsync(Employee employee);

        Task<bool> UpdateEmployeeAsync(Employee employee);

        Task<bool> DeleteEmployeeAsync(Guid guidId);

    }
}
