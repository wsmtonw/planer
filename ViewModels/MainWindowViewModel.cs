using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using Planer.DataModel;
using Planer.Services;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;

namespace Planer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly PlanerService _service;
        private ViewModelBase _contentViewModel;

        public MainWindowViewModel()
        {
            _service = new PlanerService();
            Planer = new PlanerViewModel(_service.GetItems());
            _contentViewModel = Planer;

            ClearAllItems = ReactiveCommand.Create(() =>
            {
                Planer.ListItems.Clear();
                SaveItems();
            });

            ExitApplication = ReactiveCommand.Create(() =>
            {
                SaveItems();
                if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
                {
                    desktopLifetime.Shutdown();
                }
            });
        }

        public ViewModelBase ContentViewModel
        {
            get => _contentViewModel;
            private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
        }

        public PlanerViewModel Planer { get; }

        public ReactiveCommand<Unit, Unit> AddItemCommand => ReactiveCommand.Create(AddItem);

        public ReactiveCommand<Unit, Unit> ClearAllItems { get; }

        public ReactiveCommand<Unit, Unit> ExitApplication { get; }

        public void AddItem()
        {
            AddItemViewModel addItemViewModel = new();

            Observable.Merge(
                addItemViewModel.OkCommand,
                addItemViewModel.CancelCommand.Select(_ => (PlanerItem?)null))
                .Take(1)

                .Subscribe(newItem =>
                {
                    if (newItem != null)
                    {
                        Planer.ListItems.Add(newItem);
                        SaveItems();
                    }
                    ContentViewModel = Planer;
                });

            ContentViewModel = addItemViewModel;
        }

        private void SaveItems()
        {
            _service.SaveItems(Planer.ListItems);
        }
    }
}
