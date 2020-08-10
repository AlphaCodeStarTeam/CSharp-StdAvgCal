using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using StdAvgCal.Model.Existence;

namespace StdAvgCal.Model
{
    public class FileReader
    {
        private static readonly string DocUrl = GetSlnDir() + "Docs\\";
        private static readonly string ScoresFileName = "Scores.json";
        private static readonly string StudentsFileName = "Students.json";
        
        public static List<StudentScore> GetAllScores()
        {
            string json = File.ReadAllText( DocUrl + ScoresFileName);
            StudentScore[] scores = JsonConvert.DeserializeObject<StudentScore[]>(json);
            return scores.ToList();
        }

        public static List<Student> GetAllStudents()
        {
            string json = File.ReadAllText(DocUrl + StudentsFileName);
            Student[] students = JsonConvert.DeserializeObject<Student[]>(json);
            return students.ToList();
        }

        private static string GetSlnDir()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            for (int i = 0; i < 4; i++)
            {
                path = Path.GetDirectoryName(path);
            }
            
            return path + "\\";
        }
    }
}