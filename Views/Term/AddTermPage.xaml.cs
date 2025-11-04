using WGUStudentTracker.Models;

namespace WGUStudentTracker.Views;

public partial class AddTermPage : ContentPage
{
	public AddTermPage()
	{
		InitializeComponent();
	}
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a term title.", "OK");
            return;
        }

        if (StartDatePicker.Date >= EndDatePicker.Date)
        {
            await DisplayAlert("Error", "Start date must be before end date.", "OK");
            return;
        }

        var newTerm = new Term
        {
            Title = TitleEntry.Text.Trim(),
            StartDate = StartDatePicker.Date,
            EndDate = EndDatePicker.Date
        };

        await App.Database.SaveTermAsync(newTerm);

        await DisplayAlert("Success", "Term saved successfully!", "OK");

        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}