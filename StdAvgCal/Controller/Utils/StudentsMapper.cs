﻿using System;
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
        public bool IsInit { get; set; }
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
                    .Select(std => new {std, scores = GetScores(std.StudentNumber)}))
            {
                _studentScores.Add(studentWithScores.std.StudentNumber, studentWithScores.scores);
                studentWithScores.std.Lessons.UnionWith(_scores.Select(score => score.Lesson));
                _logs.Add(studentWithScores.std.StudentNumber, studentWithScores.scores.Average(score => score.Score));
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
            CheckInit();
            return _logs[studentNumber];
        }

        public List<StudentScore> GetStudentScores(int studentNumber)
        {
            CheckInit();
            return _studentScores[studentNumber];
        }
        
        public Student GetStudentByFullName(string firstName, string lastName)
        {
            CheckInit();
            Student student = _students.Find(std => std.FullName.Equals(firstName + " " + lastName, StringComparison.InvariantCultureIgnoreCase));
            if(student == null)
                throw new StudentNotFoundException();
            return student;
        }
        
        public bool CheckInit()
        {
            if (IsInit)
                return true;
            throw new NotInitializedException();
        }
    }
}