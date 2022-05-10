namespace TestBook
{
    //  delegate for event, pointer to method
    // public class, method, member: UpperCased
    public delegate void GradeAddDelegate(object sender, EventArgs args);
    public class NamedObject
    {
        // constructor method, initialze member
        public NamedObject(string name)
        {
            Name = name;
        }

        //  property field, 
        public string Name
        {
            get;
            // private set; //can read external but not set(private)
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddDelegate GradeAddEvent;

    }

    // NamedObject: parent class     
    // object is the base class of all type
    // abstract class, can contain method implementation 
    public abstract class Book : NamedObject, IBook
    {
        // base: create instance of base class
        protected Book(string name) : base(name)
        { }

        // abstract method force to be implemented
        // virtul: implemented method, can overrride
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStatistics();
        public abstract event GradeAddDelegate GradeAddEvent;
    }

    //  write the grade to text
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddDelegate GradeAddEvent;

        public override void AddGrade(double grade)
        {
            // open the file for writing, auto-closed(if implement IDisposable)
            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
            }
            // trigger the event 
            if (GradeAddEvent != null)
            {
                GradeAddEvent(this, new EventArgs());
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            string line;
            using (var reader = File.OpenText($"{Name}.txt"))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(double.Parse(line));
                }
            }
            
            return result;
        }
    }

    // write the grade in memory
    //  derive from base class, implement interface
    public class InMemoryBook : Book, IBook
    {
        // prviate member accesible only inside class
        private List<double> grades;
        readonly string category;// read-only: visible only inside constructor
        public const string CATEGORY = "SCIENCE";// const: constant value, accessed by static class

        // define a delegate variable for event, only intialized by +=, -=
        public override event GradeAddDelegate GradeAddEvent;

        public InMemoryBook(string name) : base(name)
        {
            category = "";
            grades = new List<double>();
            Name = name;
        }

        // overrride abstract method
        public override void AddGrade(double grade)
        {
            // add to list
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if (GradeAddEvent != null)
                {
                    GradeAddEvent(this, new EventArgs());
                }
            }
            else
            {
                //  throw exception, nameof: get variable name
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        // method overload, same method name/return type, different params
        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }

        public override Statistics GetStatistics()
        {
            // init for the lowest value and average
            var result = new Statistics();

            // foreach loop
            foreach (var grade in grades)
            {
                result.Add(grade);
            }
            return result;
        }
    }

}