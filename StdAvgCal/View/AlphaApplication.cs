using System;
using System.Collections.Generic;
using StdAvgCal.Controller;
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
            
            internal static readonly Tuple<String, ConsoleColor> AlphaHelp = new Tuple<string, ConsoleColor>("-> show (FirstName) (LastName) average \n" +
                                                                                                             "-> show (FirstName) (LastName) scores \n" +
                                                                                                             "-> show rankings (-Number) \n" +
                                                                                                             "-> help \n" +
                                                                                                             "-> exit", ConsoleColor.DarkGreen);

            internal static readonly Tuple<string, ConsoleColor> AlphaExit = new Tuple<string, ConsoleColor>("AlphaTeam Appreciates Your Usage. GoodBye 👋", ConsoleColor.Black);
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
            Executors.Add("^show rankings( -\\d+)?$", args => ShowRankings(args[0].Equals("") ? Int32.MaxValue : Int32.Parse(args[0].Substring(2))));
        }

        private void ShowRankings(int rankingNumber)
        {
            Console.WriteLine("Here");
            if (rankingNumber == 0)
            {
                PrintWithDesign("Please Enter An Integer", true, DefaultBackGroundColor, ConsoleColor.DarkRed);
                return;
            }
            List<Student> studentsOrderedByRank = _alphaController.GetStudentsRanking(rankingNumber);
            int studentRankingSize = _alphaController.GetRankingSize();
            PrintWithDesign("#Student Ranking:\n", true, DefaultBackGroundColor, ConsoleColor.DarkCyan);
            for (int i = 0; i < studentsOrderedByRank.Count; i++)
            {
                Student student = studentsOrderedByRank[i];
                String line = "";
                ConsoleColor lineColor;
                switch (i)
                {
                    case 0:
                        line += "🥇 ";
                        lineColor = ConsoleColor.DarkYellow;
                        break;
                    case 1:
                        line += "🥈 ";
                        lineColor = ConsoleColor.DarkGray;
                        break;
                    case 2:
                        line += "🥉 ";
                        lineColor = ConsoleColor.DarkRed;
                        break;
                    default:
                        line += "" + (i + 1) + ". ";
                        lineColor = ConsoleColor.DarkBlue;
                        break;
                }
                line += student.FullName + ", Avg: " + _alphaController.GetAvg(student.StudentNumber);
                PrintWithDesign(line, true, DefaultBackGroundColor, lineColor);
            }

            if (rankingNumber < studentRankingSize)
            {
                Console.WriteLine("\t.\n\t.\n\t.\n");
            }
        }

        private void ShowScores(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        private void ShowAverage(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }
    }
}