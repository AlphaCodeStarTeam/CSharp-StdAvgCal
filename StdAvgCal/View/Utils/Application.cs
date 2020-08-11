using System;
using System.Collections.Generic;
using System.Drawing;
using StdAvgCal.View.Utils;

namespace StdAvgCal.View
{

    public abstract class Application : IExecute
    {
        protected readonly string ApplicationName;
        protected readonly ConsoleColor ApplicationNameColor;

        protected ConsoleColor DefaultBackGroundColor = Console.BackgroundColor;
        protected ConsoleColor DefaultForeGroundColor = Console.ForegroundColor;
        
        protected abstract void SetConsoleDesign();
        protected abstract void SayHello();
        protected abstract void ShowHelp();

        protected Application(string applicationName, ConsoleColor applicationNameColor)
        {
            ApplicationName = applicationName;
            ApplicationNameColor = applicationNameColor;
            SetConsoleDesign();
            SayHello();
            Executors = new Dictionary<string, IExecute.Execute>();
            Executors.Add("^show help$", args => ShowHelp());
            InitExecutors();
        }

        public void run()
        {
            string commandPrefix = ApplicationName + "> ";
            string input = "";
            while (true)
            {
                PrintWithDesign(commandPrefix, false, DefaultBackGroundColor, ApplicationNameColor);
                input = Console.ReadLine();

                try
                {
                    ((IExecute) this).ExecuteCommand(input);
                }
                catch (MethodNotFoundException e)
                {
                    PrintWithDesign("Invalid Command", true, DefaultBackGroundColor, ConsoleColor.DarkRed);
                }
            }
        }

        protected void PrintWithDesign(string text, bool isLine, ConsoleColor backGroundColor , ConsoleColor foreGroundColor)
        {
            Console.BackgroundColor = backGroundColor;
            Console.ForegroundColor = foreGroundColor;

            Console.Write("{0}{1}", text, (isLine ? "\n" : ""));

            Console.BackgroundColor = DefaultBackGroundColor;
            Console.ForegroundColor = DefaultForeGroundColor;
        }

        public Dictionary<string, IExecute.Execute> Executors { get; }
        public abstract void InitExecutors();
    }
    
}