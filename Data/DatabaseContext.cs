using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGUStudentTracker.Models;

namespace WGUStudentTracker.Data
{
    public class DatabaseContext
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseContext(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<Term>().Wait();
            _database.CreateTableAsync<Course>().Wait();
            _database.CreateTableAsync<Assessment>().Wait();

        }

        public Task<List<Term>> GetTermsAsync()
        {
            return _database.Table<Term>().ToListAsync();
        }

        public Task<int> SaveTermAsync(Term term)
        {
            if (term.Id != 0)
            {
                return _database.UpdateAsync(term);
            }
            else
            {
                return _database.InsertAsync(term);
            }
        }

        public Task<int> DeleteTermAsync(Term term)
        {
            return _database.DeleteAsync(term);
        }

        public Task<List<Course>> GetCoursesByTermAsync(int termId)
        {
            return _database.Table<Course>().Where(c => c.TermId == termId).ToListAsync();
        }

        public Task<int> SaveCourseAsync(Course course)
        {
            if (course.Id != 0)
                return _database.UpdateAsync(course);
            else
                return _database.InsertAsync(course);
        }

        public Task<int> DeleteCourseAsync(Course course)
        {
            return _database.DeleteAsync(course);
        }

        public Task<List<Assessment>> GetAssessmentsByCourseAsync(int courseId)
        {
            return _database.Table<Assessment>().Where(a => a.CourseId == courseId).ToListAsync();
        }

        public Task<int> SaveAssessmentAsync(Assessment assessment)
        {
            if (assessment.Id != 0)
                return _database.UpdateAsync(assessment);
            else
                return _database.InsertAsync(assessment);
        }

        public Task<int> DeleteAssessmentAsync(Assessment assessment)
        {
            return _database.DeleteAsync(assessment);
        }

        public async Task SeedSampleDataAsync()
        {
            var existingTerms = await _database.Table<Term>().ToListAsync();
            if (existingTerms.Any())
                return;

            var term = new Term
            {
                Title = "Fall 2025 Term",
                StartDate = new DateTime(2025, 1, 6),
                EndDate = new DateTime(2025, 4, 25)
            };
            await _database.InsertAsync(term);

            var course = new Course
            {
                TermId = term.Id,
                Name = "Mobile App Development (C971)",
                StartDate = new DateTime(2025, 1, 10),
                EndDate = new DateTime(2025, 4, 20),
                Status = "In Progress",
                InstructorName = "Anika Patel",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "anika.patel@strimeuniversity.edu",
                Notes = "Sample course for rubric C6 dataset."
            };
            await _database.InsertAsync(course);

            var objective = new Assessment
            {
                CourseId = course.Id,
                Type = "Objective",
                Name = "Objective Assessment",
                StartDate = new DateTime(2025, 1, 15),
                EndDate = new DateTime(2025, 1, 30),
                DueDate = new DateTime(2025, 1, 30)
            };
            await _database.InsertAsync(objective);

            var performance = new Assessment
            {
                CourseId = course.Id,
                Type = "Performance",
                Name = "Performance Assessment",
                StartDate = new DateTime(2025, 2, 10),
                EndDate = new DateTime(2025, 3, 5),
                DueDate = new DateTime(2025, 3, 5)
            };
            await _database.InsertAsync(performance);
        }


    }
}
