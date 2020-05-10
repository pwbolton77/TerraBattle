using System.Collections.ObjectModel;
using TerraBattle.Model;
using TerraBattle.UI.DataProvider.Lookups;
using TerraBattle.UI.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Linq;
using System.Windows.Input;
using TerraBattle.UI.Command;

namespace TerraBattle.UI.ViewModel
{
  public interface INavigationViewModel
  {
    void Load();
  }

  public class NavigationViewModel : INavigationViewModel
  {
    private readonly IEventAggregator _eventAggregator;
    private readonly ILookupProvider<UnitConfig> _unitConfigLookupProvider;

    public NavigationViewModel(IEventAggregator eventAggregator,
      ILookupProvider<UnitConfig> unitConfigLookupProvider)
    {
      _eventAggregator = eventAggregator;
      _eventAggregator.GetEvent<UnitConfigSavedEvent>().Subscribe(OnFriendSaved);
      _eventAggregator.GetEvent<UnitConfigDeletedEvent>().Subscribe(OnFriendDeleted);
      _unitConfigLookupProvider = unitConfigLookupProvider;
      NavigationItems = new ObservableCollection<NavigationItemViewModel>();
    }

    public void Load()
    {
      NavigationItems.Clear();
      foreach (var unitConfigLookupItem in _unitConfigLookupProvider.GetLookup())
      {
        NavigationItems.Add(
          new NavigationItemViewModel(
            unitConfigLookupItem.Id,
            unitConfigLookupItem.DisplayValue,
            _eventAggregator));
      }
    }

    public ObservableCollection<NavigationItemViewModel> NavigationItems { get; set; }

    private void OnFriendDeleted(int unitConfigId)
    {
      var navigationItem =
        NavigationItems.SingleOrDefault(item => item.FriendId == unitConfigId);
      if (navigationItem != null)
      {
        NavigationItems.Remove(navigationItem);
      }
    }

    private void OnFriendSaved(UnitConfig savedFriend)
    {
      var navigationItem =
        NavigationItems.SingleOrDefault(item => item.FriendId == savedFriend.Id);
      if (navigationItem != null)
      {
        navigationItem.DisplayValue = string.Format("{0} {1}", savedFriend.FirstName, savedFriend.LastName);
      }
      else
      {
        Load();
      }
    }
  }

  public class NavigationItemViewModel : Observable
  {
    private readonly IEventAggregator _eventAggregator;
    private string _displayValue;

    public NavigationItemViewModel(int unitConfigId,
      string displayValue,
      IEventAggregator eventAggregator)
    {
      FriendId = unitConfigId;
      DisplayValue = displayValue;
      _eventAggregator = eventAggregator; ;
      OpenFriendEditViewCommand = new DelegateCommand(OpenFriendEditViewExecute);
    }

    public ICommand OpenFriendEditViewCommand { get; set; }

    public int FriendId { get; private set; }

    public string DisplayValue
    {
      get { return _displayValue; }
      set
      {
        _displayValue = value;
        OnPropertyChanged();
      }
    }

    private void OpenFriendEditViewExecute(object obj)
    {
      _eventAggregator.GetEvent<OpenUnitConfigEditViewEvent>().Publish(FriendId);
    }
  }
}
