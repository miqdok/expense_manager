namespace ExpenseManager.WPF.ViewModels;

public interface IParameterReceiver
{
    Task ReceiveParameterAsync(object parameter);
}
