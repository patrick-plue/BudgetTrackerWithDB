using BudgetTrackerWithDB.Models;

namespace BudgetTrackerWithDB.Dtos.Transactions;

public record CreateTransactionDto(TransactionType Type, string Description, decimal Amount, DateOnly TransactionDate);