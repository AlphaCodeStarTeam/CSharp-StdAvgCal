using System;
using System.Collections.Generic;
using StdAvgCal.Model;
using StdAvgCal.Model.Existence;

namespace StdAvgCal.Controller.InvertedMap
{
    public class AvgIndexer
    {
        public Dictionary<double, HashSet<Student>> InvertedAvgScores { get; }

        public AvgIndexer()
        {
            InvertedAvgScores = new Dictionary<double, HashSet<Student>>();
        }

        public void AddToAvgScores(double studentAvg, Student student)
        {
            if (InvertedAvgScores.ContainsKey(studentAvg))
                InvertedAvgScores[studentAvg].Add(student);
            else
                InvertedAvgScores.Add(studentAvg, new HashSet<Student>() {student});
        }

        public double CalculateAvg(List<double> scores)
        {
            double avg = 0;
            foreach (double score in scores)
            {
                avg += score;
            }

            return avg / scores.Count;
        }
    }
}