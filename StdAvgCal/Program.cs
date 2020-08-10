using System;
using Newtonsoft.Json;
using StdAvgCal.Controller;
using StdAvgCal.Controller.InvertedMap;
using StdAvgCal.Model.Existence;

namespace StdAvgCal
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller.Controller controller = new Controller.Controller();
            foreach (Student student in controller.GetStudentsRanking(3))
            {
                Console.WriteLine(JsonConvert.SerializeObject(student));
                Console.WriteLine("Avg : " + controller.GetAvg(student.FirstName, student.LastName));
            }
        }
    }
}