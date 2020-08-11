using System.Collections.Generic;

namespace StdAvgCal.Model.Existence
{
    public class Student
    {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public HashSet<string> Lessons { get; set; }  = new HashSet<string>();

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        
    }
}