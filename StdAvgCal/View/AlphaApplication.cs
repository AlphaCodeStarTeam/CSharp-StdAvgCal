using System;
using System.Drawing;
using System.Runtime.Serialization.Formatters;

namespace StdAvgCal.View
{
    public class AlphaApplication : Application
    {

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


        public AlphaApplication() : base(AlphaDesign.AlphaIntro, AlphaDesign.AlphaRun, AlphaDesign.AlphaHelp, AlphaDesign.AlphaExit) { }

        public override void InitExecutors()
        {
            Executors.Add("^show (\\S+) (\\S+) average$", args => ShowAverage(args[0], args[1]));
            Executors.Add("^show (\\S+) (\\S+) scores$", args => ShowScores(args[0], args[1]));
            Executors.Add("^show rankings (\\d+)?$", args => showRankings(args.Length == 0 ? 3 : Int32.Parse(args[0])));
        }

        private void showRankings(int rankingNumber = 3)
        {
            
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