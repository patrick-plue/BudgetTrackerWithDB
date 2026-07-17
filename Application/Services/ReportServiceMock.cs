using BudgetTrackerWithDB.Application.Interfaces;
using BudgetTrackerWithDB.Dtos.Reports;
using BudgetTrackerWithDB.Models;

namespace BudgetTrackerWithDB.Application.Services;

public class ReportServiceMock : IReportService
{


    public Task<SummaryReportResponseDto> GetSummaryAsync(DateOnly start, DateOnly end, string? type)
    {
        var filterType = type?.ToLowerInvariant() ?? "all";


        // Generate deterministic mock categories
        var mockExpenses = new List<Transaction>
        {
            new Transaction(TransactionType.Expense,"tv", 1200.00m, new DateOnly(2026,12,1)),
            new Transaction(TransactionType.Expense,"groceries", 53.00m, new DateOnly(2026,08,12)),
            new Transaction(TransactionType.Expense,"car", 2000.00m, new DateOnly(2026,05,20)),
            new Transaction(TransactionType.Expense,"travel", 2500.00m, new DateOnly(2026,03,06)),

        };

        var mockIncomes = new List<Transaction>
        {
            new(TransactionType.Income, "Salary", 3000.00M, new DateOnly(2026,03,31)),
            new(TransactionType.Income, "Freelance", 10000.00M, new DateOnly(2026,06,20)),
        };

        // Filter line items based on requested type
        var lineItems = new List<Transaction>();

        if (filterType is "all" or "expense")
        {
            lineItems.AddRange(mockExpenses);
        }

        if (filterType is "all" or "income")
        {
            lineItems.AddRange(mockIncomes);
        }

        // Calculate totals based on filtered items
        var totalIncome = lineItems.Where(i => i.Type == TransactionType.Income).Sum(i => i.Amount);
        var totalExpense = lineItems.Where(i => i.Type == TransactionType.Expense).Sum(i => i.Amount);
        decimal? net = filterType == "all" ? (totalIncome - totalExpense) : null;

        // Return the report wrapper
        var report = new SummaryReportResponseDto(
            StartDate: start,
            EndDate: end,
            TotalIncome: totalIncome,
            TotalExpense: totalExpense,
            Net: net
        );

        return Task.FromResult(report);
    }
}