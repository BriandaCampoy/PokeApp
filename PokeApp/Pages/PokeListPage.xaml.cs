using PokeApp.ViewModels;

namespace PokeApp.Pages;

public partial class PokeListPage : ContentPage
{
	public PokeListPage()
	{
		InitializeComponent();
	}

    private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {

        if (!(sender is ScrollView scrollView)) return;

        var scrollSpace = scrollView.ContentSize.Height - scrollView.Height;

        if (scrollSpace > e.ScrollY) return;

        if (BindingContext is PokeListViewModel viewModel)
        {
            viewModel.LoadMorePokemon();
        }

        //load more items when coming to the end of the list of ScrollView 
        //System.Diagnostics.Debug.WriteLine("Load more items");
    }
}