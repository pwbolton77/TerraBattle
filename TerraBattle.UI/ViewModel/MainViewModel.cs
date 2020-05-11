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
      UnitConfigEditViewModels = new ObservableCollection<IUnitConfigEditViewModel>();
      CloseUnitConfigTabCommand = new DelegateCommand(OnCloseUnitConfigTabExecute);
      AddUnitConfigCommand = new DelegateCommand(OnAddUnitConfigExecute);
    }

    public void Load()
    {
      NavigationViewModel.Load();
    }

    public void OnClosing(CancelEventArgs e)
    {
      if(UnitConfigEditViewModels.Any(u=>u.UnitConfig.IsChanged))
      {
        var result = _messageDialogService.ShowYesNoDialog("Close application?",
          "You'll lose your changes if you close this application. Close it?",
          MessageDialogResult.No);
        e.Cancel = result == MessageDialogResult.No;
      }
    }

    public ICommand CloseUnitConfigTabCommand { get; private set; }

    public ICommand AddUnitConfigCommand { get; set; }

    public INavigationViewModel NavigationViewModel { get; private set; }

    // Those ViewModels represent the Tab-Pages in the UI
    public ObservableCollection<IUnitConfigEditViewModel> UnitConfigEditViewModels { get; private set; }

    public IUnitConfigEditViewModel SelectedUnitConfigEditViewModel
    {
      get { return _selectedUnitConfigEditViewModel; }
      set
      {
        _selectedUnitConfigEditViewModel = value;
        OnPropertyChanged();
      }
    }

    public bool IsChanged => UnitConfigEditViewModels.Any(u => u.UnitConfig.IsChanged);

    private void OnAddUnitConfigExecute(object obj)
    {
      IUnitConfigEditViewModel unitConfigEditVm = _unitConfigEditViewModelCreator();
      UnitConfigEditViewModels.Add(unitConfigEditVm);
      unitConfigEditVm.Load();
      SelectedUnitConfigEditViewModel = unitConfigEditVm;
    }

    private void OnOpenUnitConfigTab(int unitConfigId)
    {
      IUnitConfigEditViewModel unitConfigEditVm =
        UnitConfigEditViewModels.SingleOrDefault(vm => vm.UnitConfig.Id == unitConfigId);
      if (unitConfigEditVm == null)
      {
        unitConfigEditVm = _unitConfigEditViewModelCreator();
        UnitConfigEditViewModels.Add(unitConfigEditVm);
        unitConfigEditVm.Load(unitConfigId);
      }
      SelectedUnitConfigEditViewModel = unitConfigEditVm;
    }

    private void OnCloseUnitConfigTabExecute(object parameter)
    {
      var unitConfigEditVmToClose = parameter as IUnitConfigEditViewModel;
      if (unitConfigEditVmToClose != null)
      {
        if(unitConfigEditVmToClose.UnitConfig.IsChanged)
        {
          var result = _messageDialogService.ShowYesNoDialog("Close tab?",
            "You'll lose your changes if you close this tab. Close it?",
            MessageDialogResult.No);
          if(result== MessageDialogResult.No)
          {
            return;
          }
        }
        UnitConfigEditViewModels.Remove(unitConfigEditVmToClose);
      }
    }

    private void OnUnitConfigDeleted(int unitConfigId)
    {
      IUnitConfigEditViewModel unitConfigDetailVmToClose
        = UnitConfigEditViewModels.SingleOrDefault(u => u.UnitConfig.Id == unitConfigId);
      if (unitConfigDetailVmToClose != null)
      {
        UnitConfigEditViewModels.Remove(unitConfigDetailVmToClose);
      }
    }
  }
}
