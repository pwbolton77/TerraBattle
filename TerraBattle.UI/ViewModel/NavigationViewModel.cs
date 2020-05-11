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
      _eventAggregator.GetEvent<UnitConfigSavedEvent>().Subscribe(OnUnitConfigSaved);
      _eventAggregator.GetEvent<UnitConfigDeletedEvent>().Subscribe(OnUnitConfigDeleted);
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

    private void OnUnitConfigDeleted(int unitConfigId)
    {
      var navigationItem =
        NavigationItems.SingleOrDefault(item => item.UnitConfigId == unitConfigId);
      if (navigationItem != null)
      {
        NavigationItems.Remove(navigationItem);
      }
    }

    private void OnUnitConfigSaved(UnitConfig savedUnitConfig)
    {
      var navigationItem =
        NavigationItems.SingleOrDefault(item => item.UnitConfigId == savedUnitConfig.Id);
      if (navigationItem != null)
      {
        navigationItem.DisplayValue = string.Format("{0} {1}", savedUnitConfig.FirstName, savedUnitConfig.LastName);
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
      UnitConfigId = unitConfigId;
      DisplayValue = displayValue;
      _eventAggregator = eventAggregator; ;
      OpenUnitConfigEditViewCommand = new DelegateCommand(OpenUnitConfigEditViewExecute);
    }

    public ICommand OpenUnitConfigEditViewCommand { get; set; }

    public int UnitConfigId { get; private set; }

    public string DisplayValue
    {
      get { return _displayValue; }
      set
      {
        _displayValue = value;
        OnPropertyChanged();
      }
    }

    private void OpenUnitConfigEditViewExecute(object obj)
    {
      _eventAggregator.GetEvent<OpenUnitConfigEditViewEvent>().Publish(UnitConfigId);
    }
  }
}
