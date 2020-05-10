using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TerraBattle.UI.Command;
using TerraBattle.UI.Events;
using TerraBattle.UI.View.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.ComponentModel;

namespace TerraBattle.UI.ViewModel
{
  public class MainViewModel : Observable
  {
    private readonly IEventAggregator _eventAggregator;
    private readonly IMessageDialogService _messageDialogService;
    private IUnitConfigEditViewModel _selectedUnitConfigEditViewModel;

    private Func<IUnitConfigEditViewModel> _unitConfigEditViewModelCreator;

    public MainViewModel(IEventAggregator eventAggregator,
        IMessageDialogService messageDialogService,
        INavigationViewModel navigationViewModel,
        Func<IUnitConfigEditViewModel> unitConfigViewModelCreator)
    {
      _eventAggregator = eventAggregator;
      _messageDialogService = messageDialogService;
      _eventAggregator.GetEvent<OpenUnitConfigEditViewEvent>().Subscribe(OnOpenUnitConfigTab);
      _eventAggregator.GetEvent<UnitConfigDeletedEvent>().Subscribe(OnUnitConfigDeleted);

      NavigationViewModel = navigationViewModel;
      _unitConfigEditViewModelCreator = unitConfigViewModelCreator;
      FriendEditViewModels = new ObservableCollection<IUnitConfigEditViewModel>();
      CloseFriendTabCommand = new DelegateCommand(OnCloseUnitConfigTabExecute);
      AddFriendCommand = new DelegateCommand(OnAddUnitConfigExecute);
    }

    public void Load()
    {
      NavigationViewModel.Load();
    }

    public void OnClosing(CancelEventArgs e)
    {
      if(FriendEditViewModels.Any(f=>f.Friend.IsChanged))
      {
        var result = _messageDialogService.ShowYesNoDialog("Close application?",
          "You'll lose your changes if you close this application. Close it?",
          MessageDialogResult.No);
        e.Cancel = result == MessageDialogResult.No;
      }
    }

    public ICommand CloseFriendTabCommand { get; private set; }

    public ICommand AddFriendCommand { get; set; }

    public INavigationViewModel NavigationViewModel { get; private set; }

    // Those ViewModels represent the Tab-Pages in the UI
    public ObservableCollection<IUnitConfigEditViewModel> FriendEditViewModels { get; private set; }

    public IUnitConfigEditViewModel SelectedFriendEditViewModel
    {
      get { return _selectedUnitConfigEditViewModel; }
      set
      {
        _selectedUnitConfigEditViewModel = value;
        OnPropertyChanged();
      }
    }

    public bool IsChanged => FriendEditViewModels.Any(u => u.Friend.IsChanged);

    private void OnAddUnitConfigExecute(object obj)
    {
      IUnitConfigEditViewModel unitConfigEditVm = _unitConfigEditViewModelCreator();
      FriendEditViewModels.Add(unitConfigEditVm);
      unitConfigEditVm.Load();
      SelectedFriendEditViewModel = unitConfigEditVm;
    }

    private void OnOpenUnitConfigTab(int unitConfigId)
    {
      IUnitConfigEditViewModel unitConfigEditVm =
        FriendEditViewModels.SingleOrDefault(vm => vm.Friend.Id == unitConfigId);
      if (unitConfigEditVm == null)
      {
        unitConfigEditVm = _unitConfigEditViewModelCreator();
        FriendEditViewModels.Add(unitConfigEditVm);
        unitConfigEditVm.Load(unitConfigId);
      }
      SelectedFriendEditViewModel = unitConfigEditVm;
    }

    private void OnCloseUnitConfigTabExecute(object parameter)
    {
      var unitConfigEditVmToClose = parameter as IUnitConfigEditViewModel;
      if (unitConfigEditVmToClose != null)
      {
        if(unitConfigEditVmToClose.Friend.IsChanged)
        {
          var result = _messageDialogService.ShowYesNoDialog("Close tab?",
            "You'll lose your changes if you close this tab. Close it?",
            MessageDialogResult.No);
          if(result== MessageDialogResult.No)
          {
            return;
          }
        }
        FriendEditViewModels.Remove(unitConfigEditVmToClose);
      }
    }

    private void OnUnitConfigDeleted(int unitConfigId)
    {
      IUnitConfigEditViewModel unitConfigDetailVmToClose
        = FriendEditViewModels.SingleOrDefault(u => u.Friend.Id == unitConfigId);
      if (unitConfigDetailVmToClose != null)
      {
        FriendEditViewModels.Remove(unitConfigDetailVmToClose);
      }
    }
  }
}
