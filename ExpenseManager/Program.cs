using ExpenseManager.Services.Storage;
using ExpenseManager.UI.Models;

var storageService = new ExpenseStorageService();
var wallets = storageService.GetWallets().ToList();

ShowStartupShowcase(storageService, wallets);

while (true)
{
    PrintWallets(wallets);
    Console.Write("Select wallet #, R refresh, Q quit: ");
    var input = Console.ReadLine()?.Trim();

    if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("The end.");
        return;
    }

    if (string.Equals(input, "r", StringComparison.OrdinalIgnoreCase))
    {
        wallets = storageService.GetWallets().ToList();
        Console.WriteLine("Refreshed.\n");
        continue;
    }

    if (!int.TryParse(input, out var walletIndex) || walletIndex < 1 || walletIndex > wallets.Count)
    {
        Console.WriteLine("Invalid input.\n");
        continue;
    }

    ShowWalletDetails(storageService, wallets[walletIndex - 1]);
}

static void ShowStartupShowcase(ExpenseStorageService storageService, IReadOnlyList<WalletModel> wallets)
{
    if (wallets.Count == 0)
    {
        return;
    }

    var wallet = wallets[0];
    wallet.SetTransactions(storageService.GetTransactions(wallet.Id));

    Console.WriteLine("Sample:");
    Console.WriteLine(wallet.ToListItem());
    var firstTransaction = wallet.Transactions.FirstOrDefault();
    if (firstTransaction is not null)
    {
        Console.WriteLine(firstTransaction.ToListItem());
    }
    Console.WriteLine();
}

static void PrintWallets(IReadOnlyList<WalletModel> wallets)
{
    Console.WriteLine("Wallets:");
    for (var index = 0; index < wallets.Count; index++)
    {
        Console.WriteLine($"{index + 1}. {wallets[index].ToListItem()}");
    }
}

static void ShowWalletDetails(ExpenseStorageService storageService, WalletModel wallet)
{
    if (!wallet.TransactionsLoaded)
    {
        wallet.SetTransactions(storageService.GetTransactions(wallet.Id));
    }

    Console.WriteLine();
    Console.WriteLine(wallet.ToDetailsString());
    Console.WriteLine();

    if (wallet.Transactions.Count == 0)
    {
        Console.WriteLine("No transactions.\n");
        return;
    }

    Console.WriteLine("Transactions:");
    for (var index = 0; index < wallet.Transactions.Count; index++)
    {
        Console.WriteLine($"{index + 1}. {wallet.Transactions[index].ToListItem()}");
    }

    while (true)
    {
        Console.Write("Select tx # for details, B back: ");
        var input = Console.ReadLine()?.Trim();

        if (string.Equals(input, "b", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine();
            return;
        }

        if (!int.TryParse(input, out var transactionIndex) || transactionIndex < 1 || transactionIndex > wallet.Transactions.Count)
        {
            Console.WriteLine("Invalid input.");
            continue;
        }

        Console.WriteLine();
        Console.WriteLine(wallet.Transactions[transactionIndex - 1].ToDetailsString());
        Console.WriteLine();
    }
}
