using BudgetTrackerWithDB.Application.Interfaces;
using BudgetTrackerWithDB.Models;
using BudgetTrackerWithDB.Dtos.Transactions;

namespace BudgetTrackerWithDB.Application.Services;

public class TransactionServiceMock : ITransactionService
{

    private readonly ICollection<Transaction> _transactions = new List<Transaction>
    {
        new Transaction(TransactionType.Expense, "travel", 1200.00M, new DateOnly(2025,10,02)),
        new Transaction(TransactionType.Income, "salary", 12000.00M, new DateOnly(2026,04,28)),
    };

    public IReadOnlyCollection<Transaction> Transactions => _transactions.ToList().AsReadOnly();

    public Task<IEnumerable<Transaction>> ListAsync()
    {

        return Task.FromResult<IEnumerable<Transaction>>(_transactions);

    }

    public Task<Transaction?> GetAsync(Guid id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(transaction);
    }


    public Task<Transaction> CreateAsync(CreateTransactionDto dto)
    {
        var transaction = new Transaction(dto.Type, dto.Description, dto.Amount, dto.TransactionDate);
        _transactions.Add(transaction);

        return Task.FromResult(transaction);
    }

    public Task<Transaction?> UpdateAsync(Guid id, UpdateTransactionDto dto)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);

        if (transaction == null)
        {
            return Task.FromResult<Transaction?>(null);
        }

        // pattern matching necessary because of nullability
        if (dto.Amount is decimal amount)
        {
            transaction.Amount = amount;
        }

        if (dto.Description is string description)
        {
            transaction.Description = description;
        }

        return Task.FromResult<Transaction?>(transaction);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);

        var wasRemoved = transaction is not null && _transactions.Remove(transaction);

        return Task.FromResult(wasRemoved);
    }
}