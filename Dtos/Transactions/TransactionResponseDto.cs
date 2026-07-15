using BudgetTrackerWithDB.Models;

namespace BudgetTrackerWithDB.Dtos.Transactions;

public record TransactionResponseDto(Guid Id, DateTime Timestamp, TransactionType Type, string Description, decimal Amount, DateOnly TransactionDate);