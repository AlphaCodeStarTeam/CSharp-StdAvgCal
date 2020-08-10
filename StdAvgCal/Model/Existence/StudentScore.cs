using System;

namespace StdAvgCal.Model.Existence
{
    public class StudentScore
    {
        public int StudentNumber { get; set; }
        public string Lesson { get; set; }

        private double _score;
        private int _roundingBound = 2;

        public int RoundingBound
        {
            set
            {
                _roundingBound = value;
            }
        }

        public double Score
        {
            get
            {
                return RoundDouble(_score, _roundingBound);
            }

            set
            {
                _score = value;
            }
        }

        private double RoundDouble(double number, int roundingBound)
        {
            int temp = (int) (_score * PowerInteger(10, roundingBound));
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

    }
}