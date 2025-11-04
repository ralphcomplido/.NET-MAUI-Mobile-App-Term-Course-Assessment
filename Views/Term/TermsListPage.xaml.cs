using WGUStudentTracker.Models;
using WGUStudentTracker.Views;

namespace WGUStudentTracker.Views;

public partial class TermsListPage : ContentPage
{
	public TermsListPage()
	{
		InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var terms = await App.Database.GetTermsAsync();
        if (terms.Count == 0)
        {
            await App.Database.SeedSampleDataAsync();
            terms = await App.Database.GetTermsAsync();
        }
        TermsCollectionView.ItemsSource = terms;
    }
    private async void OnAddTermClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new AddTermPage());
    }

    private async void OnEditTermClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedTerm = button?.BindingContext as Term;
        if (selectedTerm != null)
        {
            await Navigation.PushAsync(new EditTermPage(selectedTerm));
        }
    }

    private async void OnDeleteTermClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedTerm = button?.BindingContext as Term;
        if (selectedTerm != null)
        {
            bool confirm = await DisplayAlert("Confirm Delete",
                $"Are you sure you want to delete {selectedTerm.Title}?",
                "Yes", "No");

            if (confirm)
            {
                await App.Database.DeleteTermAsync(selectedTerm);
                TermsCollectionView.ItemsSource = await App.Database.GetTermsAsync();
            }
        }
    }

    private async void OnTermSelected(object sender, SelectionChangedEventArgs e)
    {
        var selectedTerm = e.CurrentSelection.FirstOrDefault() as Term;

        if (selectedTerm == null)
            return;


        await Navigation.PushAsync(new TermDetailPage(selectedTerm));

        ((CollectionView)sender).SelectedItem = null;
    }

    private void TermsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}