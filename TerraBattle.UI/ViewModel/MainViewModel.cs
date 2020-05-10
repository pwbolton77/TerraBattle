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
    private IFriendEditViewModel _selectedFriendEditViewModel;

    private Func<IFriendEditViewModel> _unitConfigEditViewModelCreator;

    public MainViewModel(IEventAggregator eventAggregator,
        IMessageDialogService messageDialogService,
        INavigationViewModel navigationViewModel,
        Func<IFriendEditViewModel> unitConfigViewModelCreator)
    {
      _eventAggregator = eventAggregator;
      _messageDialogService = messageDialogService;
      _eventAggregator.GetEvent<OpenFriendEditViewEvent>().Subscribe(OnOpenFriendTab);
      _eventAggregator.GetEvent<FriendDeletedEvent>().Subscribe(OnFriendDeleted);

      NavigationViewModel = navigationViewModel;
      _unitConfigEditViewModelCreator = unitConfigViewModelCreator;
      FriendEditViewModels = new ObservableCollection<IFriendEditViewModel>();
      CloseFriendTabCommand = new DelegateCommand(OnCloseFriendTabExecute);
      AddFriendCommand = new DelegateCommand(OnAddFriendExecute);
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
    public ObservableCollection<IFriendEditViewModel> FriendEditViewModels { get; private set; }

    public IFriendEditViewModel SelectedFriendEditViewModel
    {
      get { return _selectedFriendEditViewModel; }
      set
      {
        _selectedFriendEditViewModel = value;
        OnPropertyChanged();
      }
    }

    public bool IsChanged => FriendEditViewModels.Any(f => f.Friend.IsChanged);

    private void OnAddFriendExecute(object obj)
    {
      IFriendEditViewModel unitConfigEditVm = _unitConfigEditViewModelCreator();
      FriendEditViewModels.Add(unitConfigEditVm);
      unitConfigEditVm.Load();
      SelectedFriendEditViewModel = unitConfigEditVm;
    }

    private void OnOpenFriendTab(int unitConfigId)
    {
      IFriendEditViewModel unitConfigEditVm =
        FriendEditViewModels.SingleOrDefault(vm => vm.Friend.Id == unitConfigId);
      if (unitConfigEditVm == null)
      {
        unitConfigEditVm = _unitConfigEditViewModelCreator();
        FriendEditViewModels.Add(unitConfigEditVm);
        unitConfigEditVm.Load(unitConfigId);
      }
      SelectedFriendEditViewModel = unitConfigEditVm;
    }

    private void OnCloseFriendTabExecute(object parameter)
    {
      var unitConfigEditVmToClose = parameter as IFriendEditViewModel;
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

    private void OnFriendDeleted(int unitConfigId)
    {
      IFriendEditViewModel unitConfigDetailVmToClose
        = FriendEditViewModels.SingleOrDefault(u => u.Friend.Id == unitConfigId);
      if (unitConfigDetailVmToClose != null)
      {
        FriendEditViewModels.Remove(unitConfigDetailVmToClose);
      }
    }
  }
}
