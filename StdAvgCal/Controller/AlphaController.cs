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

        public List<StudentScore> GetStudentScores(string firstName, string lastName)
        {
            CheckInit();
            return _mapper.GetStudentScores(_mapper.GetStudentByFullName(firstName, lastName).StudentNumber);
        }


        public double GetAvg(string firstName, string lastName)
        {
            CheckInit();
            return _mapper.GetAvgByStudent(_mapper.GetStudentByFullName(firstName, lastName).StudentNumber);
        }

        public double GetAvg(int studentNumber)
        {
            CheckInit();
            return _mapper.GetAvgByStudent(studentNumber);
        }
        

        public List<Student> GetStudentsRanking(int n)
        {
            CheckInit();
            return _mapper.StudentsRanking
                .Take(n)
                .ToList();
        }

        public int GetRankingSize()
        {
            CheckInit();
            return _mapper.StudentsRanking.Count;
        }
        
        public bool CheckInit()
        {
            if (IsInit)
                return true;
            throw new NotInitializedException();
        }
    }
}