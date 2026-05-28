using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.common.Models
{
    public static class SqlConstants
    {
        public const string GetEmployees = "sp_GetEmployees";

        public const string GetEmployeeById = "sp_GetEmployeeById";

        public const string InsertEmployee = "sp_InsertEmployee";

        public const string UpdateEmployee = "sp_UpdateEmployee";

        public const string DeleteEmployee = "sp_DeleteEmployee";
    }
}
