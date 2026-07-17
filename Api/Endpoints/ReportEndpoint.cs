using BudgetTrackerWithDB.Application.Interfaces;

public static class ReportEndpoints
{
    public static void MapReports(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/reports/summary");


        group.MapGet("/", async (string startDate, string endDate, string? type, IReportService reportService) =>
        {

            if (!DateOnly.TryParse(startDate, out var parsedStart) || !DateOnly.TryParse(endDate, out var parsedEnd))
            {
                return Results.BadRequest("Invalid date format. Use YYYY-MM-DD.");
            }

            var report = await reportService.GetSummaryAsync(parsedStart, parsedEnd, type);

            return Results.Ok(report);
        });

    }
}