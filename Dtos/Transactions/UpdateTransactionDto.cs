namespace BudgetTrackerWithDB.Dtos.Transactions;

public record UpdateTransactionDto(string? Description, decimal? Amount);