using System.Collections.Generic;
using System.Windows.Input;
using TerraBattle.Model;
using TerraBattle.UI.Command;
using TerraBattle.UI.DataProvider;
using TerraBattle.UI.DataProvider.Lookups;
using TerraBattle.UI.Events;
using TerraBattle.UI.View.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using TerraBattle.UI.Wrapper;

namespace TerraBattle.UI.ViewModel
{
  public interface IUnitConfigEditViewModel
  {
    void Load(int? unitConfigId = null);
    UnitConfigWrapper UnitConfig { get; }
  }
  public class UnitConfigEditViewModel : Observable, IUnitConfigEditViewModel
  {
    private readonly IEventAggregator _eventAggregator;
    private readonly IMessageDialogService _messageDialogService;
    private readonly IUnitConfigDataProvider _unitConfigDataProvider;
    private readonly ILookupProvider<FriendGroup> _friendGroupLookupProvider;
    private UnitConfigWrapper _unitConfigs;
    private IEnumerable<LookupItem> _friendGroups;
    private FriendEmailWrapper _selectedEmail;
    private EquipConfigWrapper _selectedEquipConfig;

    public UnitConfigEditViewModel(IEventAggregator eventAggregator,
        IMessageDialogService messageDialogService,
        IUnitConfigDataProvider unitConfigDataProvider,
        ILookupProvider<FriendGroup> friendGroupLookupProvider)
    {
      _eventAggregator = eventAggregator;
      _messageDialogService = messageDialogService;
      _unitConfigDataProvider = unitConfigDataProvider;
      _friendGroupLookupProvider = friendGroupLookupProvider;

      SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
      ResetCommand = new DelegateCommand(OnResetExecute, OnResetCanExecute);
      DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);

      AddEmailCommand = new DelegateCommand(OnAddEmailExecute);
      RemoveEmailCommand = new DelegateCommand(OnRemoveEmailExecute, OnRemoveEmailCanExecute);

      AddEquipConfigCommand = new DelegateCommand(OnAddEquipConfigExecute);
      RemoveEquipConfigCommand = new DelegateCommand(OnRemoveEquipConfigExecute, OnRemoveEquipConfigCanExecute);
    }

    public void Load(int? unitConfigId = null)
    {
      FriendGroupLookup = _friendGroupLookupProvider.GetLookup();

      var unitConfig = unitConfigId.HasValue
          ? _unitConfigDataProvider.GetUnitConfigById(unitConfigId.Value)
          : new UnitConfig { Address = new Address(), Emails = new List<FriendEmail>(), EquipConfigs = new List<EquipConfig>() };

      UnitConfig = new UnitConfigWrapper(unitConfig);
      UnitConfig.PropertyChanged += (s, e) =>
        {
          if (e.PropertyName == nameof(UnitConfig.IsChanged)
          || e.PropertyName == nameof(UnitConfig.IsValid))
          {
            InvalidateCommands();
          }
        };

      InvalidateCommands();
    }

    public UnitConfigWrapper UnitConfig
    {
      get { return _unitConfigs; }
      private set
      {
        _unitConfigs = value;
        OnPropertyChanged();
      }
    }

    public IEnumerable<LookupItem> FriendGroupLookup
    {
      get { return _friendGroups; }
      set
      {
        _friendGroups = value;
        OnPropertyChanged();
      }
    }

    public FriendEmailWrapper SelectedEmail
    {
      get { return _selectedEmail; }
      set
      {
        _selectedEmail = value;
        OnPropertyChanged();
        ((DelegateCommand)RemoveEmailCommand).RaiseCanExecuteChanged();
      }
    }

    public EquipConfigWrapper SelectedEquipConfig
    {
      get { return _selectedEquipConfig; }
      set
      {
        _selectedEquipConfig = value;
        OnPropertyChanged();
        ((DelegateCommand)RemoveEquipConfigCommand).RaiseCanExecuteChanged();
      }
    }

    public ICommand SaveCommand { get; private set; }

    public ICommand ResetCommand { get; private set; }

    public ICommand DeleteCommand { get; private set; }

    public ICommand AddEmailCommand { get; private set; }

    public ICommand RemoveEmailCommand { get; private set; }
    public ICommand AddEquipConfigCommand { get; private set; }

    public ICommand RemoveEquipConfigCommand { get; private set; }

    private void OnSaveExecute(object obj)
    {
      _unitConfigDataProvider.SaveUnitConfig(UnitConfig.Model);
      UnitConfig.AcceptChanges();
      _eventAggregator.GetEvent<UnitConfigSavedEvent>().Publish(UnitConfig.Model);
      InvalidateCommands();
    }

    private bool OnSaveCanExecute(object arg)
    {
      return UnitConfig.IsChanged && UnitConfig.IsValid;
    }

    private void OnResetExecute(object obj)
    {
      UnitConfig.RejectChanges();
    }

    private bool OnResetCanExecute(object arg)
    {
      return UnitConfig.IsChanged;
    }

    private bool OnDeleteCanExecute(object arg)
    {
      return UnitConfig != null && UnitConfig.Id > 0;
    }

    private void OnDeleteExecute(object obj)
    {
      var result = _messageDialogService.ShowYesNoDialog(
          "Delete Unit",
          string.Format("Do you really want to delete the unit '{0} {1}'", UnitConfig.FirstName, UnitConfig.LastName),
          MessageDialogResult.No);

      if (result == MessageDialogResult.Yes)
      {
        _unitConfigDataProvider.DeleteUnitConfig(UnitConfig.Id);
        _eventAggregator.GetEvent<UnitConfigDeletedEvent>().Publish(UnitConfig.Id);
      }
    }

    private void OnRemoveEmailExecute(object obj)
    {
      UnitConfig.Emails.Remove(SelectedEmail);
      ((DelegateCommand)RemoveEmailCommand).RaiseCanExecuteChanged();
    }

    private bool OnRemoveEmailCanExecute(object arg)
    {
      return SelectedEmail != null;
    }

    private void OnAddEmailExecute(object obj)
    {
      UnitConfig.Emails.Add(new FriendEmailWrapper(new FriendEmail()));
    }

    private void OnRemoveEquipConfigExecute(object obj)
    {
      UnitConfig.EquipConfigs.Remove(SelectedEquipConfig);
      ((DelegateCommand)RemoveEquipConfigCommand).RaiseCanExecuteChanged();
    }

    private bool OnRemoveEquipConfigCanExecute(object arg)
    {
      return SelectedEquipConfig != null;
    }

    private void OnAddEquipConfigExecute(object obj)
    {
      UnitConfig.EquipConfigs.Add(new EquipConfigWrapper(new EquipConfig()));
    }

    private void InvalidateCommands()
    {
      ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
      ((DelegateCommand)ResetCommand).RaiseCanExecuteChanged();
      ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
    }
  }
}
