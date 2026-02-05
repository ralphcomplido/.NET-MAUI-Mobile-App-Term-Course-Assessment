# WGUStudentTracker ( .NET MAUI )

A cross-platform **.NET MAUI** mobile app that helps **WGU students** track their **Terms, Courses, and Assessments** in one place.  
Data is stored locally using **SQLite**, and the app can optionally send **local notification reminders** for upcoming assessment dates.

> **Why this exists:** built as a practical WGU-style project to demonstrate full mobile app development with local persistence, clean structure, and real-world features.

---

## Features

- **Track Terms**
  - Create and manage academic terms (start/end dates)
- **Track Courses**
  - Add courses under each term with key dates and status
- **Track Assessments**
  - Record performance/assessment items tied to courses
- **Offline-first**
  - Works without an internet connection (local SQLite database)
- **Reminders (Local Notifications)**
  - Schedule device notifications for important dates (e.g., assessments)

---

## Tech Stack

- **.NET MAUI** (cross-platform UI for Android/iOS/Windows/macOS)
- **SQLite** local database  
  - Uses **sqlite-net ORM** for lightweight persistence
- **CommunityToolkit.Maui** (UI helpers + productivity extensions)
- **Plugin.LocalNotification** (local notification scheduling)

---

## Getting Started

### Prerequisites

- **Visual Studio 2022** (Windows) with:
  - **.NET Multi-platform App UI development** workload
- **.NET SDK** compatible with your MAUI target (usually installed via Visual Studio)
- Android Emulator and/or an iOS simulator (Mac required for iOS builds)

### Run the App (Visual Studio)

1. Clone this repository:
   ```bash
   git clone <YOUR_REPO_URL>
   cd <YOUR_REPO_FOLDER>
   ```
2. Open the solution in Visual Studio.
3. Select a target platform (Android Emulator / Windows Machine / iOS Simulator).
4. Press **Start** (F5).

### Run from CLI (optional)

> MAUI CLI setup varies by platform. If you already have MAUI workloads installed:

```bash
dotnet workload install maui
dotnet build
dotnet run
```

---

## Configuration Notes

### SQLite (Local Database)

- The app uses a **local SQLite database** stored on-device.
- Tables are created automatically by the app’s data layer (using sqlite-net).

If you rename models or change schema:
- Consider a simple migration approach (versioning + re-creating tables), or
- Implement migrations explicitly (recommended if you want to preserve data).

### Local Notifications

This project uses **Plugin.LocalNotification**.
- Make sure the app has notification permissions enabled on the device/emulator.
- On iOS, you’ll need permission prompts and proper simulator/device configuration.

---

## Project Structure (Typical)

> Your folder names may differ—update this section to match your repo.

- `Models/` — Term/Course/Assessment entities
- `Data/` — SQLite setup, repositories, data access
- `ViewModels/` — UI state + commands (if using MVVM)
- `Views/` or `Pages/` — Screens (Term list, Course detail, Assessment editor, etc.)
- `Services/` — Notifications, navigation helpers, etc.

---

## Roadmap / Ideas

- Search and filter for courses/assessments
- Export/import data
- Better reporting (completed vs in-progress)
- UI polish + validation improvements

---

## Screenshots

> Add screenshots or GIFs here for your GitHub page.

- Term List:
- Course Detail:
- Assessment Editor:

---

## Contributing

If you want to contribute:
1. Fork the repo
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Commit changes
4. Open a Pull Request

---

## License

Add a license that matches your intent (MIT is common for portfolios).  
If you don’t want others reusing the project, choose a more restrictive license.

---

## Acknowledgements

- Microsoft .NET MAUI docs
- sqlite-net ORM
- CommunityToolkit.Maui
- Plugin.LocalNotification

---

## Author

**Ralph**  
- GitHub: <your-github-profile-link>
- LinkedIn: <your-linkedin-link>
