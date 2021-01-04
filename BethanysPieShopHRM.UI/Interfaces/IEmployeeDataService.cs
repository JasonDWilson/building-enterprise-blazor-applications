using BethanysPieShopHRM.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Services
{
    public interface IEmployeeDataService
    {
        Employee SavedEmployee { get; set; }

        Task<Employee> AddEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeDetailsAsync(int employeeId);
        Task UpdateEmployee(Employee employee);
    }
}
