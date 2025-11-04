using WGUStudentTracker.Data;

namespace WGUStudentTracker;

public partial class App : Application
{
    private static DatabaseContext _database;
    public static DatabaseContext Database
    {
        get
        {
            if (_database == null)
            {
                string dbPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "WGUStudentTracker.db3");

                _database = new DatabaseContext(dbPath);
            }
            return _database;
        }
    }

    public App()
    {
        InitializeComponent();
    }

    


    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new NavigationPage(new Views.TermsListPage()));
    }
}