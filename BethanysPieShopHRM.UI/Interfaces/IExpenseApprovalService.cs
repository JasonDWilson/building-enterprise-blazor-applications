using BethanysPieShopHRM.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Services
{
    public interface IExpenseApprovalService
    {
        Task<ExpenseStatus> GetExpenseStatusAsync(Expense expense);
    }
}
