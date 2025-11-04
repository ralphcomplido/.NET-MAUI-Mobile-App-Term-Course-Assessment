using WGUStudentTracker.Models;

namespace WGUStudentTracker.Views;

public partial class EditTermPage : ContentPage
{
    private Term _termToEdit;
    public EditTermPage(Term term)
	{
		InitializeComponent();
        _termToEdit = term;

        TitleEntry.Text = term.Title;
        StartDatePicker.Date = term.StartDate;
        EndDatePicker.Date = term.EndDate;
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
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

        _termToEdit.Title = TitleEntry.Text.Trim();
        _termToEdit.StartDate = StartDatePicker.Date;
        _termToEdit.EndDate = EndDatePicker.Date;

        await App.Database.SaveTermAsync(_termToEdit);

        await DisplayAlert("Success", "Term updated successfully!", "OK");

        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}