using Microsoft.EntityFrameworkCore;
using ExpenseManager.Repositories.Enums;
using ExpenseManager.Repositories.Models;

namespace ExpenseManager.Repositories.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Wallets.AnyAsync())
            return;

        var walletCashId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var walletCardId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var walletSavingsId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        context.Wallets.AddRange(
            new WalletEntity(walletCashId, "Готівка", Currency.UAH),
            new WalletEntity(walletCardId, "Карта Monobank", Currency.USD),
            new WalletEntity(walletSavingsId, "Накопичення", Currency.EUR)
        );

        context.Transactions.AddRange(
            new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001"), walletCashId, -420.50m, TransactionCategory.Groceries, "Продукти в Сільпо", new DateTime(2026, 2, 20, 18, 30, 0)),
            new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000002"), walletCashId, -180.00m, TransactionCategory.Cafe, "Кава та десерт", new DateTime(2026, 2, 20, 9, 10, 0)),
            new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000003"), walletCashId, -950.00m, TransactionCategory.Auto, "Заправка авто", new DateTime(2026, 2, 19, 20, 40, 0)),
            new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000004"), walletCashId, -780.00m, TransactionCategory.Utilities, "Комунальні послуги", new DateTime(2026, 2, 18, 8, 25, 0)),
            new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000005"), walletCashId, -350.00m, TransactionCategory.Health, "Аптека", new DateTime(2026, 2, 17, 14, 15, 0)),
            new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000006"), walletCashId, 24500.00m, TransactionCategory.Salary, "Зарплата", new DateTime(2026, 2, 15, 10, 0, 0)),
            new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000007"), walletCashId, -1200.00m, TransactionCategory.Other, "Подарунок на день народження", new DateTime(2026, 2, 14, 19, 30, 0)),
            new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000008"), walletCashId, -260.00m, TransactionCategory.Groceries, "Фрукти та овочі", new DateTime(2026, 2, 13, 17, 50, 0)),
            new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000009"), walletCashId, -540.00m, TransactionCategory.Cafe, "Обід", new DateTime(2026, 2, 12, 13, 5, 0)),
            new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000010"), walletCashId, -90.00m, TransactionCategory.Auto, "Мийка авто", new DateTime(2026, 2, 11, 12, 45, 0)),
            new TransactionEntity(Guid.Parse("bbbbbbbb-0000-0000-0000-000000000011"), walletCardId, -25.50m, TransactionCategory.Cafe, "Тістечко", new DateTime(2026, 2, 16, 7, 55, 0)),
            new TransactionEntity(Guid.Parse("bbbbbbbb-0000-0000-0000-000000000012"), walletCardId, 300.00m, TransactionCategory.Other, "Повернення коштів", new DateTime(2026, 2, 10, 11, 40, 0))
        );

        await context.SaveChangesAsync();
    }
}
