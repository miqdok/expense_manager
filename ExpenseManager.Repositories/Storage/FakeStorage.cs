using ExpenseManager.Repositories.Enums;
using ExpenseManager.Repositories.Models;

namespace ExpenseManager.Repositories.Storage;

internal static class FakeStorage
{
    private static readonly Guid WalletCashId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid WalletCardId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid WalletSavingsId = Guid.Parse("33333333-3333-3333-3333-333333333333");

    private static readonly List<WalletEntity> Wallets =
    [
        new WalletEntity(WalletCashId, "Готівка", Currency.UAH),
        new WalletEntity(WalletCardId, "Карта Monobank", Currency.USD),
        new WalletEntity(WalletSavingsId, "Накопичення", Currency.EUR)
    ];

    private static readonly List<TransactionEntity> Transactions =
    [
        new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001"), WalletCashId, -420.50m, TransactionCategory.Groceries, "Продукти в Сільпо", new DateTime(2026, 2, 20, 18, 30, 0)),
        new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000002"), WalletCashId, -180.00m, TransactionCategory.Cafe, "Кава та десерт", new DateTime(2026, 2, 20, 9, 10, 0)),
        new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000003"), WalletCashId, -950.00m, TransactionCategory.Auto, "Заправка авто", new DateTime(2026, 2, 19, 20, 40, 0)),
        new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000004"), WalletCashId, -780.00m, TransactionCategory.Utilities, "Комунальні послуги", new DateTime(2026, 2, 18, 8, 25, 0)),
        new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000005"), WalletCashId, -350.00m, TransactionCategory.Health, "Аптека", new DateTime(2026, 2, 17, 14, 15, 0)),
        new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000006"), WalletCashId, 24500.00m, TransactionCategory.Salary, "Зарплата", new DateTime(2026, 2, 15, 10, 0, 0)),
        new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000007"), WalletCashId, -1200.00m, TransactionCategory.Other, "Подарунок на день народження", new DateTime(2026, 2, 14, 19, 30, 0)),
        new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000008"), WalletCashId, -260.00m, TransactionCategory.Groceries, "Фрукти та овочі", new DateTime(2026, 2, 13, 17, 50, 0)),
        new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000009"), WalletCashId, -540.00m, TransactionCategory.Cafe, "Обід", new DateTime(2026, 2, 12, 13, 5, 0)),
        new TransactionEntity(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000010"), WalletCashId, -90.00m, TransactionCategory.Auto, "Мийка авто", new DateTime(2026, 2, 11, 12, 45, 0)),
        new TransactionEntity(Guid.Parse("bbbbbbbb-0000-0000-0000-000000000011"), WalletCardId, -25.50m, TransactionCategory.Cafe, "Тістечко", new DateTime(2026, 2, 16, 7, 55, 0)),
        new TransactionEntity(Guid.Parse("bbbbbbbb-0000-0000-0000-000000000012"), WalletCardId, 300.00m, TransactionCategory.Other, "Повернення коштів", new DateTime(2026, 2, 10, 11, 40, 0))
    ];

    internal static IReadOnlyList<WalletEntity> GetWallets()
    {
        return Wallets;
    }

    internal static IReadOnlyList<TransactionEntity> GetTransactions()
    {
        return Transactions;
    }
}
