using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels;

public partial class GroceryListViewModel : BaseViewModel
{
    public ObservableCollection<GroceryList> GroceryLists { get; set; }
    private readonly IGroceryListService _groceryListService;
    private readonly GlobalViewModel _global;

    // Welkomstbericht property
    public string WelcomeMessage => _global.Client != null ? $"Welkom {_global.Client.Name}!" : "";

    public GroceryListViewModel(IGroceryListService groceryListService, GlobalViewModel global)
    {
        Title = "Boodschappenlijst";
        _groceryListService = groceryListService;
        _global = global;

        GroceryLists = new ObservableCollection<GroceryList>(_groceryListService.GetAll());
    }

    [RelayCommand]
    public async Task SelectGroceryList(GroceryList groceryList)
    {
        var paramater = new Dictionary<string, object> { { nameof(GroceryList), groceryList } };
        await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, paramater);
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
        GroceryLists = new ObservableCollection<GroceryList>(_groceryListService.GetAll());
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        GroceryLists.Clear();
    }
}
