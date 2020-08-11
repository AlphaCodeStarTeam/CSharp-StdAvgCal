using System;
using System.Drawing;

namespace StdAvgCal.View
{
    public class AlphaApplication : Application
    {
        private new const string ApplicationName = "Alpha-Avg";
        private new const ConsoleColor ApplicationNameColor = ConsoleColor.Magenta;

        public AlphaApplication() : base(ApplicationName, ApplicationNameColor) { }

        protected override void SetConsoleDesign()
        {
            throw new System.NotImplementedException();
        }

        protected override void SayHello()
        {
            throw new System.NotImplementedException();
        }

        protected override void ShowHelp()
        {
            throw new System.NotImplementedException();
        }

        public override void InitExecutors()
        {
            throw new System.NotImplementedException();
        }
    }
}