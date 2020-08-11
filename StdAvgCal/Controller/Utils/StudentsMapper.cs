using System;
using System.Collections.Generic;
using System.Linq;
using StdAvgCal.Model;
using StdAvgCal.Model.Existence;

namespace StdAvgCal.Controller.Utils
{
    
    public class StudentNotFoundException : Exception { }
    public class StudentsMapper : IInitialize
    {
        private Dictionary<int, double> _logs;
        private Dictionary<int, List<StudentScore>> _studentScores;
        private List<StudentScore> _scores;
        private List<Student> _students;
        private List<Student> _studentsRanking;
        public List<IInitialize.Init> Initializers { get; }

        public List<Student> StudentsRanking { get =>_studentsRanking;}
        public StudentsMapper()
        {
            Initializers = new List<IInitialize.Init>() { InitFromFiles, InitStudentScores, InitStudentsRanking };
        }

        private void InitFromFiles()
        {
            _scores = FileReader.GetAllScores();
            _students = FileReader.GetAllStudents();
        }
        
        private void InitStudentScores()
        {
            _studentScores = new Dictionary<int, List<StudentScore>>();
            _logs = new Dictionary<int, double>();
            foreach (var studentWithScores in 
                _students
                    .Select(std => new {numb = std.StudentNumber, scores = GetScores(std.StudentNumber)}))
            {
                _studentScores.Add(studentWithScores.numb, studentWithScores.scores);
                _logs.Add(studentWithScores.numb, studentWithScores.scores.Average(score => score.Score));
            }
        }
        

        private void InitStudentsRanking()
        {
            _studentsRanking = _students
                .Select(student => new {student, avg = _logs[student.StudentNumber]})
                .OrderByDescending(stdAvg => stdAvg.avg)
                .Select(stdAvg => stdAvg.student)
                .ToList();
        }

        private List<StudentScore> GetScores(int studentNumber)
        {
            return _scores
                .Where(score => score.StudentNumber == studentNumber)
                .ToList();
        }

        public double GetAvgByStudent(int studentNumber)
        {
            return _logs[studentNumber];
        }

        public List<StudentScore> GetStudentScores(int studentNumber)
        {
            return _studentScores[studentNumber];
        }
        
        public Student GetStudentByFullName(string firstName, string lastName)
        {
            Student student = _students.Find(std => std.FullName.Equals(firstName + " " + lastName));
            if(student == null)
                throw new StudentNotFoundException();
            return student;
        }
    }
}