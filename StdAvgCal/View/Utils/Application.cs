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
            throw new System.NotImplementedException();
        }

        public Dictionary<string, IExecute.Execute> Executors { get; }
        public abstract void InitExecutors();
    }
    
}