using System;
using System.Drawing;

namespace StdAvgCal.View
{
    public class AlphaApplication : Application
    {
        private new const string ApplicationName = "Alpha-Avg";
        private new const ConsoleColor AlphaNameFontColor = ConsoleColor.Magenta;
        private new const ConsoleColor AlphaIntroFontColor = ConsoleColor.DarkCyan;
        private new const ConsoleColor AlphaHelpFontColor = ConsoleColor.DarkGreen;

        public AlphaApplication() : base(ApplicationName, AlphaNameFontColor) { }

        protected override void SetConsoleDesign()
        {
            //Todo
        }

        protected override void SayHello()
        {
            string hello = "Hello, This Is " + ApplicationName + "." +
                           "\nHow Can We Help You?" +
                           "\n";
            PrintWithDesign(hello, true, DefaultBackGroundColor, AlphaIntroFontColor);
        }

        protected override void ShowHelp()
        {
            string help = "";
            PrintWithDesign(help, true, DefaultBackGroundColor, AlphaHelpFontColor);
        }

        public override void InitExecutors()
        {
            //Todo
        }
    }
}