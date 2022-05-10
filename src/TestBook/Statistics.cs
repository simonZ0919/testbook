namespace TestBook
{
    public class Statistics
    {
        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }
        public double Low;
        public char Letter
        {
            get
            {
                switch (Average)
                {
                    // 
                    case var d when d >= 90.0:
                        return 'A';
                    case var d when d >= 80.0:
                        return 'B';
                    default:
                        return 'C';
                }
            }
        }
        public double Sum;
        public int Count;

        public void Add(double number)
        {
            Sum += number;
            Count++;
            Low = Math.Min(number, Low);
        }


        public Statistics()
        {
            Sum = 0.0;
            Count = 0;
            Low = double.MaxValue;
        }
    }
}