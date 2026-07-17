using BudgetTrackerWithDB.Dtos.Reports;
namespace BudgetTrackerWithDB.Application.Interfaces;


public interface IReportService
{
    Task<SummaryReportResponseDto> GetSummaryAsync(DateOnly start, DateOnly end, string? type);
}