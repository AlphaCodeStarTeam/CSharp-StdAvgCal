using System;
using System.Collections.Generic;
using System.Linq;
using StdAvgCal.Controller.Utils;
using StdAvgCal.Model.Existence;

namespace StdAvgCal.Controller
{
    
    public class AlphaController : IInitialize
    {
        public bool IsInit { get; set; }
        public List<IInitialize.Init> Initializers { get; }

        private StudentsMapper _mapper;

        public AlphaController()
        {
            Initializers = new List<IInitialize.Init>() { InitMapper };
        }

        private void InitMapper()
        {
            _mapper = new StudentsMapper();
            ((IInitialize)_mapper).InitMe();
        }

        public List<StudentScore> GetStudentScores(int studentNamber)
        {
            CheckInit();
            return _mapper.GetStudentScores(studentNamber);
        }
        

        public double GetAvg(int studentNumber)
        {
            CheckInit();
            return _mapper.GetAvgByStudent(studentNumber);
        }
        

        public List<Tuple<Student, double>> GetStudentsRanking(int n)
        {
            CheckInit();
            return _mapper.StudentsRanking
                .Take(n)
                .Select(student => new Tuple<Student, double>(student, GetAvg(student.StudentNumber)))
                .ToList();
        }

        public int GetRankingSize()
        {
            CheckInit();
            return _mapper.StudentsRanking.Count;
        }

        public Student GetStudentByFullName(String firstName, String lastName)
        {
            CheckInit();
            return _mapper.GetStudentByFullName(firstName, lastName);
        }
        
        public bool CheckInit()
        {
            if (IsInit)
                return true;
            throw new NotInitializedException();
        }
    }
}