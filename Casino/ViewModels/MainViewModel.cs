using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Casino.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object? _currentViewModel;
        public object? CurrentViewModel
        {
            get => _currentViewModel;
            set { _currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); }
        }

        public ICommand NavigateStartCommand { get; }
        public ICommand NavigateLoginCommand { get; }
        public ICommand NavigateRegisterCommand { get; }

        // Deine vorhandenen VMs
        private readonly StartPageViewModel _startVM = new();
        private readonly LoginPageViewModel _loginVM = new();
        private readonly RegisterPageViewModel _registerVM = new();

        public MainViewModel()
        {
            NavigateStartCommand = new RelayCommand(_ => CurrentViewModel = _startVM);
            NavigateLoginCommand = new RelayCommand(_ => CurrentViewModel = _loginVM);
            NavigateRegisterCommand = new RelayCommand(_ => CurrentViewModel = _registerVM);

            CurrentViewModel = _startVM; // Startansicht
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string n) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        { _execute = execute; _canExecute = canExecute; }

        public bool CanExecute(object? p) => _canExecute?.Invoke(p) ?? true;
        public void Execute(object? p) => _execute(p);

        public event EventHandler? CanExecuteChanged
        { add => CommandManager.RequerySuggested += value; remove => CommandManager.RequerySuggested -= value; }
    }
}
