namespace StdAvgCal.Model.Existence
{
    public class Student
    {
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        
        
    }
}