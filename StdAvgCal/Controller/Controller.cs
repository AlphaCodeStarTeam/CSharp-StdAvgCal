using System;
using System.Collections.Generic;
using System.Linq;
using StdAvgCal.Controller.InvertedMap;
using StdAvgCal.Controller.Utils;
using StdAvgCal.Model;
using StdAvgCal.Model.Existence;

namespace StdAvgCal.Controller
{
    public class Controller
    {
        private AvgIndexer _avgIndexer;
        // private Dictionary<Student, HashSet<StudentScore>> _studentLogs;
        // private Dictionary<int, Student> _students;
        private List<StudentScore> _scores;
        private List<Student> _students;

        public Controller()
        {
            _avgIndexer = new AvgIndexer();
            _scores = FileReader.GetAllScores();
            _students = FileReader.GetAllStudents();
        }

        public double GetAvg(string firstName, string lastName)
        {
            var student = _students.Find(std =>
                std.FullName.ToLower().Equals(firstName.ToLower() + " " + lastName.ToLower()));
            if (student == null)
                throw new StudentNotFoundException();
            return _avgIndexer.CalculateAvg(GetScoresByStudentNumber(student.StudentNumber));
        }

        private double GetAvg(int studentNumber)
        {
            var student = _students.Find(std => std.StudentNumber == studentNumber);
            if (student == null)
                throw new StudentNotFoundException();
            return _avgIndexer.CalculateAvg(GetScoresByStudentNumber(student.StudentNumber));
        }

        private List<double> GetScoresByStudentNumber(int studentNumber)
        {
            return _scores
                .Where(score => score.StudentNumber == studentNumber)
                .Select(score => score.Score)
                .ToList();
        }

        public List<Student> GetStudentsRanking(int n)
        {
            return _students
                .Select(student => new {student, avg = GetAvg(student.StudentNumber)})
                .OrderByDescending(stdAvg => stdAvg.avg)
                .Select(stdAvg => stdAvg.student)
                .Take(n)
                .ToList();
        }
        
    }

    public class StudentNotFoundException : Exception
    {
        
    }
}