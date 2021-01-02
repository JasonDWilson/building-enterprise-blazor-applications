using BethanysPieShopHRM.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Services
{
    public class ExpenseApprovalService : IExpenseApprovalService
    {
        private readonly IEmployeeDataService _employeeDataService;

        public ExpenseApprovalService(IEmployeeDataService employeeDataService) =>
            _employeeDataService = employeeDataService ?? throw new ArgumentNullException(nameof(employeeDataService));

        public async Task<ExpenseStatus> GetExpenseStatusAsync(Expense expense)
        {
            var employee = await _employeeDataService.GetEmployeeDetailsAsync(expense.EmployeeId);

            ExpenseStatus status = ExpenseStatus.Pending;

            if (employee.IsOPEX)
            {
                switch (expense.ExpenseType)
                {
                    case ExpenseType.Conference:
                        status = ExpenseStatus.Denied;
                        break;
                    case ExpenseType.Transportation:
                        status = ExpenseStatus.Denied;
                        break;
                    case ExpenseType.Hotel:
                        status = ExpenseStatus.Denied;
                        break;
                }

                if (status != ExpenseStatus.Denied)
                {
                    expense.CoveredAmount = expense.Amount / 2;
                }
            }

            if (!employee.IsFTE)
            {
                if (expense.ExpenseType != ExpenseType.Training)
                {
                    status = ExpenseStatus.Denied;
                }
            }

            if (expense.ExpenseType == ExpenseType.Food && expense.Amount > 100)
            {
                status = ExpenseStatus.Pending;
            }

            if (expense.Amount > 5000)
            {
                status = ExpenseStatus.Pending;
            }

            return status;
        }
    }
}
