using BethanysPieShopHRM.Shared;
using System;
using System.Linq;

namespace BethanysPieShopHRM.UI.Services
{
    public interface IExpenseApprovalService
    {
        ExpenseStatus GetExpenseStatus(Expense expense, Employee employee);
    }
}
