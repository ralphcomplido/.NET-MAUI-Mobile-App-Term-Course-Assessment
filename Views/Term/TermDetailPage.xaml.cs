using WGUStudentTracker.Models;

namespace WGUStudentTracker.Views;

public partial class TermDetailPage : ContentPage
{
    private Term _term;
    public TermDetailPage(Term selectedTerm)
	{
		InitializeComponent();
        _term = selectedTerm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        TermTitleLabel.Text = _term.Title;
        StartDateLabel.Text = _term.StartDate.ToShortDateString();
        EndDateLabel.Text = _term.EndDate.ToShortDateString();

        var courses = await App.Database.GetCoursesByTermAsync(_term.Id);


        CoursesCollectionView.ItemsSource = courses;
    }

    private async void OnAddCourseClicked(object sender, EventArgs e)
    {
        var existingCourses = await App.Database.GetCoursesByTermAsync(_term.Id);

        if (existingCourses.Count >= 6)
        {
            await DisplayAlert("Limit Reached",
                "You can only add up to 6 courses per term.",
                "OK");
            return;
        }

        await Navigation.PushAsync(new Views.Courses.AddCoursePage(_term.Id));
    }

    private async void OnEditCourseClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedCourse = button?.BindingContext as Course;

        if (selectedCourse != null)
        {
            await Navigation.PushAsync(new Views.Courses.EditCoursePage(selectedCourse));
        }
    }

    private async void OnDeleteCourseClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedCourse = button?.BindingContext as Course;

        if (selectedCourse == null)
            return;

        bool confirm = await DisplayAlert(
            "Delete Course",
            $"Are you sure you want to delete \"{selectedCourse.Name}\"?",
            "Yes", "No");

        if (!confirm)
            return;

        try
        {
            await App.Database.DeleteCourseAsync(selectedCourse);
            await DisplayAlert("Deleted", $"{selectedCourse.Name} has been deleted.", "OK");

            var courses = await App.Database.GetCoursesByTermAsync(_term.Id);
            CoursesCollectionView.ItemsSource = courses;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to delete course: {ex.Message}", "OK");
        }
    }

}