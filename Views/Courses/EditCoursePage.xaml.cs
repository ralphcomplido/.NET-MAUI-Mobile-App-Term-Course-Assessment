using WGUStudentTracker.Models;
using Plugin.LocalNotification;


namespace WGUStudentTracker.Views.Courses;

public partial class EditCoursePage : ContentPage
{
    private Course _courseToEdit;
    public EditCoursePage(Course course)
	{
		InitializeComponent();
        _courseToEdit = course;

        CourseNameEntry.Text = course.Name;
        StartDatePicker.Date = course.StartDate;
        EndDatePicker.Date = course.EndDate;
        StatusPicker.SelectedItem = course.Status;
        InstructorNameEntry.Text = course.InstructorName;
        InstructorPhoneEntry.Text = course.InstructorPhone;
        InstructorEmailEntry.Text = course.InstructorEmail;
        NotesEditor.Text = course.Notes;

        LoadAssessmentsAsync(course.Id);
    }

    private async void LoadAssessmentsAsync(int courseId)
    {
        var assessments = await App.Database.GetAssessmentsByCourseAsync(courseId);

        var objective = assessments.FirstOrDefault(a => a.Type == "Objective");
        var performance = assessments.FirstOrDefault(a => a.Type == "Performance");

        if (objective != null)
        {
            ObjectiveNameEntry.Text = objective.Name;
            ObjectiveDueDatePicker.Date = objective.DueDate;
            ObjectiveStartPicker.Date = objective.StartDate;
            ObjectiveEndPicker.Date = objective.EndDate;
        }

        if (performance != null)
        {
            PerformanceNameEntry.Text = performance.Name;
            PerformanceDueDatePicker.Date = performance.DueDate;
            PerformanceStartPicker.Date = performance.StartDate;
            PerformanceEndPicker.Date = performance.EndDate;
        }
    }
    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CourseNameEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a course name.", "OK");
            return;
        }

        if (StartDatePicker.Date >= EndDatePicker.Date)
        {
            await DisplayAlert("Error", "Start date must be before end date.", "OK");
            return;
        }

        if (StatusPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please select a course status.", "OK");
            return;
        }

        _courseToEdit.Name = CourseNameEntry.Text.Trim();
        _courseToEdit.StartDate = StartDatePicker.Date;
        _courseToEdit.EndDate = EndDatePicker.Date;
        _courseToEdit.Status = StatusPicker.SelectedItem.ToString();
        _courseToEdit.InstructorName = InstructorNameEntry.Text?.Trim();
        _courseToEdit.InstructorPhone = InstructorPhoneEntry.Text?.Trim();
        _courseToEdit.InstructorEmail = InstructorEmailEntry.Text?.Trim();
        _courseToEdit.Notes = NotesEditor.Text?.Trim();

        var objectiveAssessment = new Assessment
        {
            CourseId = _courseToEdit.Id,
            Type = "Objective",
            Name = ObjectiveNameEntry.Text?.Trim(),
            DueDate = ObjectiveDueDatePicker.Date
        };

        var performanceAssessment = new Assessment
        {
            CourseId = _courseToEdit.Id,
            Type = "Performance",
            Name = PerformanceNameEntry.Text?.Trim(),
            DueDate = PerformanceDueDatePicker.Date
        };

        await App.Database.SaveAssessmentAsync(objectiveAssessment);
        await App.Database.SaveAssessmentAsync(performanceAssessment);
        await App.Database.SaveCourseAsync(_courseToEdit);

        await DisplayAlert("Success", "Course updated successfully!", "OK");
        await Navigation.PopAsync();
    }

    private async void OnSetObjectiveNotificationClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ObjectiveNameEntry.Text))
        {
            await DisplayAlert("Missing Info", "Please enter the objective assessment name first.", "OK");
            return;
        }

        var startAlert = new NotificationRequest
        {
            NotificationId = _courseToEdit.Id * 10 + 1,
            Title = "Objective Assessment Starting",
            Description = $"{ObjectiveNameEntry.Text} starts today!",
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = ObjectiveStartPicker.Date
            }
        };

        var endAlert = new NotificationRequest
        {
            NotificationId = _courseToEdit.Id * 10 + 2,
            Title = "Objective Assessment Ending",
            Description = $"{ObjectiveNameEntry.Text} ends today!",
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = ObjectiveEndPicker.Date
            }
        };

        await LocalNotificationCenter.Current.Show(startAlert);
        await LocalNotificationCenter.Current.Show(endAlert);

        await DisplayAlert("Success", "Start and End notifications set for Objective Assessment.", "OK");
    }

    private async void OnSetPerformanceNotificationClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(PerformanceNameEntry.Text))
        {
            await DisplayAlert("Missing Info", "Please enter the performance assessment name first.", "OK");
            return;
        }

        var startAlert = new NotificationRequest
        {
            NotificationId = _courseToEdit.Id * 10 + 3,
            Title = "Performance Assessment Starting",
            Description = $"{PerformanceNameEntry.Text} starts today!",
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = PerformanceStartPicker.Date
            }
        };

        var endAlert = new NotificationRequest
        {
            NotificationId = _courseToEdit.Id * 10 + 4,
            Title = "Performance Assessment Ending",
            Description = $"{PerformanceNameEntry.Text} ends today!",
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = PerformanceEndPicker.Date
            }
        };

        await LocalNotificationCenter.Current.Show(startAlert);
        await LocalNotificationCenter.Current.Show(endAlert);

        await DisplayAlert("Success", "Start and End notifications set for Performance Assessment.", "OK");
    }


    private async void OnShareNotesClicked(object sender, EventArgs e)
    {
        string notes = NotesEditor.Text?.Trim();

        if (string.IsNullOrEmpty(notes))
        {
            await DisplayAlert("No Notes", "There are no notes to share for this course.", "OK");
            return;
        }

        await Share.RequestAsync(new ShareTextRequest
        {
            Text = notes,
            Title = $"Notes for {_courseToEdit.Name}"
        });
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cancel", "Discard changes?", "Yes", "No");
        if (confirm)
            await Navigation.PopAsync();
    }
}