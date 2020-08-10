namespace StdAvgCal.Model.Existence
{
    public class Student
    {
        public int StudentNumber { get; set; }
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