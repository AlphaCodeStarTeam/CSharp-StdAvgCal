using System.Collections.Generic;
using System.Linq;
using StdAvgCal.Controller.Utils;
using StdAvgCal.Model.Existence;

namespace StdAvgCal.Controller
{
    
    public class Controller : IInitialize
    {
        public List<IInitialize.Init> Initializers { get; }
        private StudentsMapper _mapper;

        public Controller()
        {
            Initializers = new List<IInitialize.Init>() { InitMapper };
        }

        public void InitMapper()
        {
            _mapper = new StudentsMapper();
            ((IInitialize)_mapper).InitMe();
        }

        public List<StudentScore> GetStudentScores(string firstName, string lastName)
        {
            return _mapper.GetStudentScores(_mapper.GetStudentByFullName(firstName, lastName).StudentNumber);
        }


        public double GetAvg(string firstName, string lastName)
        {
            return _mapper.GetAvgByStudent(_mapper.GetStudentByFullName(firstName, lastName).StudentNumber);
        }
        

        public List<Student> GetStudentsRanking(int n)
        {
            return _mapper.StudentsRanking
                .Take(n)
                .ToList();
        }

    }
}