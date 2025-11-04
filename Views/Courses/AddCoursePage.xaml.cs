using WGUStudentTracker.Models;

namespace WGUStudentTracker.Views.Courses;

public partial class AddCoursePage : ContentPage
{
    private int _termId;
    public AddCoursePage(int termId)
	{
		InitializeComponent();
        _termId = termId;
    }

    private async void OnSaveCourseClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CourseNameEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a course name.", "OK");
            return;
        }

        if (StartDatePicker.Date >= EndDatePicker.Date)
        {
            await DisplayAlert("Error", "Start date must be before the end date.", "OK");
            return;
        }

        if (StatusPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please select a course status.", "OK");
            return;
        }

        var course = new Course
        {
            TermId = _termId,
            Name = CourseNameEntry.Text.Trim(),
            StartDate = StartDatePicker.Date,
            EndDate = EndDatePicker.Date,
            Status = StatusPicker.SelectedItem.ToString(),
            InstructorName = InstructorNameEntry.Text?.Trim(),
            InstructorPhone = InstructorPhoneEntry.Text?.Trim(),
            InstructorEmail = InstructorEmailEntry.Text?.Trim()
        };

        await App.Database.SaveCourseAsync(course);
        await DisplayAlert("Success", "Course added successfully!", "OK");
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}