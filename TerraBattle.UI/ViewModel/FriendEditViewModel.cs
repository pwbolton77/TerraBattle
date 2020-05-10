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
  public interface IFriendEditViewModel
  {
    void Load(int? friendId = null);
    BattleUnitWrapper Friend { get; }
  }
  public class FriendEditViewModel : Observable, IFriendEditViewModel
  {
    private readonly IEventAggregator _eventAggregator;
    private readonly IMessageDialogService _messageDialogService;
    private readonly IFriendDataProvider _friendDataProvider;
    private readonly ILookupProvider<FriendGroup> _friendGroupLookupProvider;
    private BattleUnitWrapper _friend;
    private IEnumerable<LookupItem> _friendGroups;
    private FriendEmailWrapper _selectedEmail;

    public FriendEditViewModel(IEventAggregator eventAggregator,
        IMessageDialogService messageDialogService,
        IFriendDataProvider friendDataProvider,
        ILookupProvider<FriendGroup> friendGroupLookupProvider)
    {
      _eventAggregator = eventAggregator;
      _messageDialogService = messageDialogService;
      _friendDataProvider = friendDataProvider;
      _friendGroupLookupProvider = friendGroupLookupProvider;

      SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
      ResetCommand = new DelegateCommand(OnResetExecute, OnResetCanExecute);
      DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);

      AddEmailCommand = new DelegateCommand(OnAddEmailExecute);
      RemoveEmailCommand = new DelegateCommand(OnRemoveEmailExecute, OnRemoveEmailCanExecute);
    }

    public void Load(int? friendId = null)
    {
      FriendGroupLookup = _friendGroupLookupProvider.GetLookup();

      var friend = friendId.HasValue
          ? _friendDataProvider.GetFriendById(friendId.Value)
          : new BattleUnit { Address = new Address(), Emails = new List<FriendEmail>() };

      Friend = new BattleUnitWrapper(friend);
      Friend.PropertyChanged += (s, e) =>
        {
          if (e.PropertyName == nameof(Friend.IsChanged)
          || e.PropertyName == nameof(Friend.IsValid))
          {
            InvalidateCommands();
          }
        };

      InvalidateCommands();
    }

    public BattleUnitWrapper Friend
    {
      get { return _friend; }
      private set
      {
        _friend = value;
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

    public ICommand SaveCommand { get; private set; }

    public ICommand ResetCommand { get; private set; }

    public ICommand DeleteCommand { get; private set; }

    public ICommand AddEmailCommand { get; private set; }

    public ICommand RemoveEmailCommand { get; private set; }

    private void OnSaveExecute(object obj)
    {
      _friendDataProvider.SaveFriend(Friend.Model);
      Friend.AcceptChanges();
      _eventAggregator.GetEvent<FriendSavedEvent>().Publish(Friend.Model);
      InvalidateCommands();
    }

    private bool OnSaveCanExecute(object arg)
    {
      return Friend.IsChanged && Friend.IsValid;
    }

    private void OnResetExecute(object obj)
    {
      Friend.RejectChanges();
    }

    private bool OnResetCanExecute(object arg)
    {
      return Friend.IsChanged;
    }

    private bool OnDeleteCanExecute(object arg)
    {
      return Friend != null && Friend.Id > 0;
    }

    private void OnDeleteExecute(object obj)
    {
      var result = _messageDialogService.ShowYesNoDialog(
          "Delete Friend",
          string.Format("Do you really want to delete the friend '{0} {1}'", Friend.FirstName, Friend.LastName),
          MessageDialogResult.No);

      if (result == MessageDialogResult.Yes)
      {
        _friendDataProvider.DeleteFriend(Friend.Id);
        _eventAggregator.GetEvent<FriendDeletedEvent>().Publish(Friend.Id);
      }
    }

    private void OnRemoveEmailExecute(object obj)
    {
      Friend.Emails.Remove(SelectedEmail);
      ((DelegateCommand)RemoveEmailCommand).RaiseCanExecuteChanged();
    }

    private bool OnRemoveEmailCanExecute(object arg)
    {
      return SelectedEmail != null;
    }

    private void OnAddEmailExecute(object obj)
    {
      Friend.Emails.Add(new FriendEmailWrapper(new FriendEmail()));
    }

    private void InvalidateCommands()
    {
      ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
      ((DelegateCommand)ResetCommand).RaiseCanExecuteChanged();
      ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
    }
  }
}
