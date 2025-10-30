using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmExample {
    public class ComputerScienceViewModel : INotifyPropertyChanged {
        /// <summary>
        /// Property changed notification
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Student to look at
        /// </summary>
        private Student _student;

        /// <summary>
        /// Student First Name
        /// </summary>
        public string FirstName => _student.FirstName;

        /// <summary>
        /// Student Last Name
        /// </summary>
        public string LastName => _student.LastName;

        /// <summary>
        /// Student GPA
        /// </summary>
        public double GPA => _student.GPA;

        /// <summary>
        /// Course Records
        /// </summary>
        public IEnumerable<CourseRecord> CourseRecords => _student.CourseRecords;

        /// <summary>The student's major GPA</summary>
        public double MajorGPA {
            get {
                var points = 0.0;
                var hours = 0.0;
                foreach (var cr in _student.CourseRecords) {
                    if (!cr.CourseName.Contains("CIS")) { continue; }
                    points += (double)cr.Grade * cr.CreditHours;
                    hours += cr.CreditHours;
                }
                return points / hours;
            }
        }

        /// <summary>
        /// Handles the property changed events
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Property event args</param>
        private void HandleStudentPropertyChanged(object? sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(Student.GPA)) {
                PropertyChanged?.Invoke(this, e);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MajorGPA)));
            }
        }

        public ComputerScienceViewModel(Student student) { 
            _student = student;
            student.PropertyChanged += HandleStudentPropertyChanged;
        }
    }
}
