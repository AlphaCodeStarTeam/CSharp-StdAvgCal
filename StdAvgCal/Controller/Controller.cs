using System.Collections.Generic;
using StdAvgCal.Controller.InvertedMap;
using StdAvgCal.Controller.Utils;
using StdAvgCal.Model;
using StdAvgCal.Model.Existence;

namespace StdAvgCal.Controller
{
    public class Controller
    {
        private List<IInitialize> _initializes;
        
        private AvgIndexer _avgIndexer;
        private Dictionary<Student, HashSet<StudentScore>> _studentLogs;
        private Dictionary<int, Student> _students;

        public Controller()
        {
            _initializes = new List<IInitialize>();
            IInitialize initialize;
        }

        private void InitStudents()
        {
            _students = new Dictionary<int, Student>();
            
            foreach (Student student in FileReader.GetAllStudents())
            {
                _students.Add(student.StudentNumber, student);
            }
        }
        
        private void InitStudentLogs()
        {
            _studentLogs = new Dictionary<Student, HashSet<StudentScore>>();

            foreach (StudentScore studentScore in FileReader.GetAllScores())
            {
                PutScoreInStudentLogs(studentScore);
            }
        }

        private void PutScoreInStudentLogs(StudentScore studentScore)
        {
            Student student = _students[studentScore.StudentNumber];
            
            if (_studentLogs.ContainsKey(student))
                _studentLogs[student].Add(studentScore);
            else
                _studentLogs.Add(student, new HashSet<StudentScore>(){studentScore});
        }

        private void InitAvgIndexer()
        {
            _avgIndexer = new AvgIndexer();
            
            foreach (Student student in _studentLogs.Keys)
                _avgIndexer.AddToAvgScores(_avgIndexer.CalculateAvg(_studentLogs[student]), student);
        }
        
    }
}