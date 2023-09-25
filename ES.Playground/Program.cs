using ES.Playground.Domain;
using ES.Playground.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using static ES.Playground.Domain.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<EventStore>();
builder.Services.AddSingleton<BankAccountsRegistry>();
builder.Services.AddTransient<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<IAggregateEventsManager<BankAccount, Guid>, BankAccountAggregateEventsManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/account/status/{aggregateId}", ([FromServices] IBankAccountRepository accountsRepo, [FromServices] EventStore eventStore, Guid aggregateId) =>
{
    var aggregate = accountsRepo.GetBankAccount(aggregateId);
    aggregate.ApplyAllDomainEvents();

    return aggregate;
});

app.MapPost("/account/new", ([FromServices] IBankAccountRepository accountsRepo, [FromServices] EventStore eventStore, [FromServices] IAggregateEventsManager<BankAccount, Guid> eventsManager) =>
{
    var newBankAccount = BankAccount.CreateNewBankAccount(eventsManager);
    accountsRepo.AddBankAccount(newBankAccount);

    newBankAccount.StoreDomainEvent(new AccountCreatedEvent());

    return newBankAccount.Id;
});

app.MapPost("/account/income/{aggregateId}/{amount}", ([FromServices] IBankAccountRepository accountsRepo, [FromServices] EventStore eventStore, Guid aggregateId, decimal amount) =>
{
    var bankAccount = accountsRepo.GetBankAccount(aggregateId);

    if (bankAccount is null) throw new ArgumentNullException(nameof(bankAccount));

    bankAccount.StoreDomainEvent(new AccountIncomeEvent(amount));

    return Results.Ok();
});

app.MapPost("/account/withdraw/{aggregateId}/{amount}", ([FromServices] IBankAccountRepository accountsRepo, [FromServices] EventStore eventStore, Guid aggregateId, decimal amount) =>
{
    var bankAccount = accountsRepo.GetBankAccount(aggregateId);

    if (bankAccount is null) throw new ArgumentNullException(nameof(bankAccount));

    bankAccount.StoreDomainEvent(new AccountWithdrawalEvent(amount));

    return Results.Ok();
});

app.Run();
