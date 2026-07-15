using BudgetTrackerWithDB.Application.Interfaces;
using BudgetTrackerWithDB.Application.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<ITransactionService, TransactionServiceMock>();
builder.Services.AddSingleton<IReportSerivce, ReportServiceMock>();

builder.Services.AddOpenApi();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.MapTransactions();

app.Run();