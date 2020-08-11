using System;

namespace StdAvgCal.Model.Existence
{
    public class StudentScore
    {
        public int StudentNumber { get; set; }
        public string Lesson { get; set; }

        public double Score { get; set; }

        private double RoundDouble(double number, int roundingBound)
        {
            int temp = (int) (number * PowerInteger(10, roundingBound));
            double score = ((double)temp) / PowerInteger(10, roundingBound);
            return score;
        }

        private int PowerInteger(int number, int pow)
        {
            int answer = 1;
            for (int i = 0; i < pow; i++)
            {
                answer *= number;
            }

            return answer;
        }

        public double GetRoundedScore(int roundingBound)
        {
            return RoundDouble(Score, roundingBound);
        }

        public override string ToString()
        {
            return "\"" + Lesson + "\" With Score: " + Score;
        }
    }
}