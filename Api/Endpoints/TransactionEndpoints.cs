using BudgetTrackerWithDB.Application.Interfaces;
using BudgetTrackerWithDB.Dtos.Transactions;

public static class TransactionEndpoints
{
    public static void MapTransactions(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/transactions");

        group.MapGet("/", async (ITransactionService transactionService) =>
        {
            var transactions = await transactionService.ListAsync();

            var transactionDtos = transactions.Select(t => new TransactionResponseDto(t.Id, t.Timestamp, t.Type, t.Description, t.Amount, t.TransactionDate));

            return Results.Ok(transactionDtos);
        });


        group.MapGet("/{id:guid}", async (Guid id, ITransactionService transactionService) =>
        {
            var transaction = await transactionService.GetAsync(id);

            if (transaction is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(transaction);
        });


        group.MapPost("/", async (CreateTransactionDto dto, ITransactionService transactionService, HttpContext context) =>
        {
            var transaction = await transactionService.CreateAsync(dto);

            var transactionDto = new TransactionResponseDto(transaction.Id, transaction.Timestamp, transaction.Type, transaction.Description, transaction.Amount, transaction.TransactionDate);

            var location = $"{context.Request.Scheme}://{context.Request.Host}/books/{transaction.Id}";

            return Results.Created(location, transactionDto);
        });


        group.MapPatch("/{id:guid}", async (Guid id, UpdateTransactionDto dto, ITransactionService transactionService) =>
        {
            var transaction = await transactionService.UpdateAsync(id, dto);

            if (transaction == null)
                return Results.NotFound();

            var transactionDto = new TransactionResponseDto(transaction.Id, transaction.Timestamp, transaction.Type, transaction.Description, transaction.Amount, transaction.TransactionDate);


            return Results.Ok(transactionDto);
        });


        group.MapDelete("/{id:guid}", async (Guid id, ITransactionService transactionService) =>
        {
            var deleted = await transactionService.DeleteAsync(id);

            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}