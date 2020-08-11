using System;
using System.Collections.Generic;
using System.Linq;
using StdAvgCal.Controller;
using StdAvgCal.Controller.Utils;
using StdAvgCal.Model.Existence;

namespace StdAvgCal.View
{
    public class AlphaApplication : Application
    {
        private AlphaController _alphaController;
        
        public static class AlphaDesign
        {
            internal static readonly Tuple<string, ConsoleColor> AlphaRun = new Tuple<string, ConsoleColor>("Alpha-Avg", ConsoleColor.Magenta);
            
            internal static readonly Tuple<string, ConsoleColor> AlphaIntro = new Tuple<string, ConsoleColor>("Hello, This Is " + AlphaRun.Item1 + "." +
                                                                                                              "\nHow Can We Help You?" +
                                                                                                              "\n", ConsoleColor.DarkCyan);
            
            internal static readonly Tuple<String, ConsoleColor> AlphaHelp = new Tuple<string, ConsoleColor>("-* Commands(IgnoreCase): \n" +
                                                                                                             "-> show (FirstName) (LastName) average \n" +
                                                                                                             "-> show (FirstName) (LastName) scores \n" +
                                                                                                             "-> show rankings (-Number) \n" +
                                                                                                             "-> show help \n" +
                                                                                                             "-> exit", ConsoleColor.DarkGreen);

            internal static readonly Tuple<string, ConsoleColor> AlphaExit = new Tuple<string, ConsoleColor>("AlphaTeam Appreciates Your Usage. GoodBye", ConsoleColor.Black);
        }


        public AlphaApplication() : 
            base(AlphaDesign.AlphaIntro, AlphaDesign.AlphaRun, AlphaDesign.AlphaHelp, AlphaDesign.AlphaExit)
        {
            _alphaController = new AlphaController();
            ((IInitialize) _alphaController).InitMe();
        }

        public override void InitExecutors()
        {
            Executors.Add("^show (\\S+) (\\S+) average$", args => ShowAverage(args[0], args[1]));
            Executors.Add("^show (\\S+) (\\S+) scores$", args => ShowScores(args[0], args[1]));
            // Executors.Add("^show rankings( -\\d+)?$", args => ShowRankings(args[0].Equals("") ? Int32.MaxValue : Int32.Parse(args[0].Substring(2))));
            Executors.Add("^show rankings( -\\d+)?$", args =>
            {
                switch (args[0])
                {
                    case "":
                        ShowRankings();
                        break;
                    case " -0":
                        PrintErr("Please Enter An Integer", true);
                        break;
                    default:
                        ShowRankings(Int32.Parse(args[0].Substring(2)));
                        break;
                }
            });
        }

        private void ShowRankings(int rankingNumber = Int32.MaxValue)
        {
            List<Tuple<Student, double>> studentsOrderedByRank = _alphaController.GetStudentsRanking(rankingNumber);
            int studentRankingSize = _alphaController.GetRankingSize();
            PrintRankings(studentsOrderedByRank);
            if (rankingNumber < studentRankingSize)
            {
                Console.WriteLine("\t\t.\n\t\t.\n\t\t.\n");
            }
            else
            {
                Console.WriteLine("");
            }
        }

        private void PrintRankings(List<Tuple<Student, double>> studentsOrderedByRank)
        {
            PrintWithDesign("#Student Ranking:", true, DefaultBackGroundColor, ConsoleColor.White);
            for (int i = 0; i < studentsOrderedByRank.Count; i++)
            {
                Student student = studentsOrderedByRank[i].Item1;
                double avg = studentsOrderedByRank[i].Item2;
                var tuple = SetPosition(i + 1);
                string line = tuple.Item1;
                var lineColor = tuple.Item2;
                line += student.FullName + ", Avg: " + avg;
                PrintWithDesign(line, true, DefaultBackGroundColor, lineColor);
            }
        }

        private Tuple<string, ConsoleColor> SetPosition(int number)
        {
            string line = "";
            ConsoleColor lineColor;
            switch (number)
            {
                case 1:
                    line += "1. ";
                    lineColor = ConsoleColor.DarkYellow;
                    break;
                case 2:
                    line += "2. ";
                    lineColor = ConsoleColor.Gray;
                    break;
                case 3:
                    line += "3. ";
                    lineColor = ConsoleColor.DarkRed;
                    break;
                default:
                    line += "" + number + ". ";
                    lineColor = ConsoleColor.DarkBlue;
                    break;
            }

            return new Tuple<string, ConsoleColor>(line, lineColor);
        }

        private void ShowScores(string firstName, string lastName)
        {
            try
            {
                Student student = _alphaController.GetStudentByFullName(firstName, lastName);
                PrintWithDesign(student.FullName + "Scores (descending Order):", true, DefaultBackGroundColor, ConsoleColor.Yellow);
                var scores = _alphaController.GetStudentScores(student.StudentNumber);
                scores.Sort((s1, s2) => s2.Score.CompareTo(s1.Score));
                int i = 0;
                foreach (var score in scores)
                {
                    PrintWithDesign("" + (++i) + ". " + score, true, DefaultBackGroundColor, ConsoleColor.Blue);
                }

                Console.WriteLine();
            }
            catch (StudentNotFoundException e)
            {
                PrintErr("No Student With Such Name!", true);
            }
        }

        private void ShowAverage(string firstName, string lastName)
        {
            try
            {
                Student student = _alphaController.GetStudentByFullName(firstName, lastName);
                PrintWithDesign(student.FullName, false, DefaultBackGroundColor, ConsoleColor.White);
                Console.Write(", ");
                PrintWithDesign("Lessons: " + GetCollectionAsString(student.Lessons) , false, DefaultBackGroundColor, ConsoleColor.DarkMagenta);
                Console.Write(", ");
                PrintWithDesign("Average: " + _alphaController.GetAvg(student.StudentNumber), true, DefaultBackGroundColor, ConsoleColor.Cyan);
                Console.WriteLine();
            }
            catch (StudentNotFoundException e)
            {
                PrintErr("No Student With Such Name!", true);
            }
        }

        private String GetCollectionAsString<T>(IEnumerable<T> collection)
        {
            String line = "{";
            foreach (var collectible in collection)
            {
                line += collectible + ", ";
            }

            return line.Substring(0, line.Length - 2) + "}";
        }
    }
}