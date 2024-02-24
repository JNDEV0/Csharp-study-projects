namespace ClassLibrary
{
    public class Assignment49
    {
        //RunValidation gets called by the main method in the Program.cs
        public void RunValidation()
        {
            Library library = new Library();
            Book book1 = new Book("Stephen King","Scary book", 12312442);
            Book book2 = new Book("Aristotle", "Deep philosophy", 3232441);
            Dvd dvd1 = new Dvd("Stephen Spielberg","The big summer movie", 60);
            Dvd dvd2 = new Dvd("M. Night Shyamalan", "The blue people", 120);
            Cd cd1 = new Cd("MC Hammer", " Hammer Time", 12);
            Cd cd2 = new Cd("Madonna", "Popstar", 8);

            Console.WriteLine("adding some items...");
            library.AddItem(book1);
            library.AddItem(book2);
            library.AddItem(dvd1);
            library.AddItem(dvd2);
            library.AddItem(cd1);
            library.AddItem(cd2);

            Console.WriteLine();
            library.PrintLibrary();
            Console.WriteLine();

            Console.WriteLine("removing some items...");
            library.RemoveItem(book1);
            library.RemoveItem(cd2);

            Console.WriteLine("attempting to borrow some items...");
            book2.Borrow("big Jim", 7);
            dvd2.Borrow("Simon", 8);
            dvd2.Borrow("Simon", 6);

            Console.WriteLine();
            library.PrintLibrary();
            Console.WriteLine();

            Console.WriteLine("returning an item...");
            dvd2.Return();

            library.PrintLibrary();
        }
    }

    public interface ILoanable
    {
        int LoanPeriod { get; set; }
        string Title { get; set; }
        string Borrower { get; set; }

        bool Borrow(string borrower, int loanPeriod);
        bool Return();
    }

    public interface IPrintable
    {
        void Print();
    }

    public class Book : ILoanable, IPrintable
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public int ISBN { get; set; }
        public int LoanPeriod { get; set; }
        public string Borrower { get; set; }
        readonly int LoanableInDays = 21;

        public Book(string author, string title, int isbn)
        {
            Author = author;
            Title = title;
            ISBN = isbn;
        }

        public bool Borrow(string borrower, int loanPeriod) 
        {
            if (Borrower == default && loanPeriod <= LoanableInDays)
            {
                Borrower = borrower;
                LoanPeriod = loanPeriod;
                return true;
            }
            else
            {
                if (loanPeriod > LoanableInDays)
                {
                    Console.WriteLine($"{this.Title} cannot be loaned for {loanPeriod} days, the limit is {LoanableInDays}. Try again!");
                    return false;
                }
                else
                {
                    Console.WriteLine($"{this.Title} is unavailable, already loaned to {Borrower} for {LoanPeriod} days.");
                    return false;
                }
            }
        }
        public bool Return()
        {
            if (Borrower != default && LoanPeriod != default)
            {
                Borrower = default;
                LoanPeriod = default;
                return true;
            }
            else
            {
                Console.WriteLine($"{this.Title} cannot be returned because it is already in stock.");
                return false;
            }
        }

        public void Print()
        {
            string borrowStatus = Borrower == default ? "available" : $"borrowed by {Borrower}";
            Console.WriteLine($"{this.GetType().Name}: {Title} by {Author} | {borrowStatus}");
        }
    }

    public class Dvd : ILoanable, IPrintable
    {
        public string Director { get; set; }
        public string Title { get; set; }
        public int LengthInMinutes { get; set; }
        public int LoanPeriod { get; set; }
        public string Borrower { get; set; }
        readonly int LoanableInDays = 7;

        public Dvd(string director, string title, int lengthInMinutes)
        {
            Director = director;
            Title = title;
            LengthInMinutes = lengthInMinutes;
        }

        public bool Borrow(string borrower, int loanPeriod)
        {
            if (Borrower == default && loanPeriod <= LoanableInDays)
            {
                Borrower = borrower;
                LoanPeriod = loanPeriod;
                return true;
            }
            else
            {
                if (loanPeriod > LoanableInDays)
                {
                    Console.WriteLine($"{this.Title} cannot be loaned for {loanPeriod} days, the limit is {LoanableInDays}. Try again!");
                    return false;
                }
                else
                {
                    Console.WriteLine($"{this.Title} is unavailable, already loaned to {Borrower} for {LoanPeriod} days.");
                    return false;
                }
            }
        }
        public bool Return()
        {
            if (Borrower != default && LoanPeriod != default)
            {
                Borrower = default;
                LoanPeriod = default;
                return true;
            }
            else
            {
                Console.WriteLine($"{this.Title} cannot be returned because it is already in stock.");
                return false;
            }
        }

        public void Print()
        {
            string borrowStatus = Borrower == default ? "available" : $"borrowed by {Borrower}";
            Console.WriteLine($"{this.GetType().Name}: {Title} by {Director} | {borrowStatus}");
        }
    }

    public class Cd : ILoanable, IPrintable
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public int NumberOfTracks { get; set; }
        public int LoanPeriod { get; set; }
        public string Borrower { get; set; }
        readonly int LoanableInDays = 14;

        public Cd(string artist, string title,int numberOfTracks)
        {
            Artist = artist;
            Title = title;
            NumberOfTracks = numberOfTracks;
        }

        public bool Borrow(string borrower, int loanPeriod)
        {
            if (Borrower == default && loanPeriod <= LoanableInDays)
            {
                Borrower = borrower;
                LoanPeriod = loanPeriod;
                return true;
            }
            else
            {
                if (loanPeriod > LoanableInDays)
                {
                    Console.WriteLine($"{this.Title} cannot be loaned for {loanPeriod} days, the limit is {LoanableInDays}. Try again!");
                    return false;
                }
                else
                {
                    Console.WriteLine($"{this.Title} is unavailable, already loaned to {Borrower} for {LoanPeriod} days.");
                    return false;
                }
            }
        }
        public bool Return()
        {
            if (Borrower != default && LoanPeriod != default) 
            {
                Borrower = default;
                LoanPeriod = default;
                return true;
            }
            else
            {
                Console.WriteLine($"{this.Title} cannot be returned because it is already in stock.");
                return false;
            }
        }

        public void Print()
        {
            string borrowStatus = Borrower == default ? "available" : $"borrowed by {Borrower}";
            Console.WriteLine($"{this.GetType().Name}: {Title} by {Artist} | {borrowStatus}");
        }

    }

    public class Library
    {
        List<Book> Books { get; set; }
        List<Dvd> Dvds { get; set; }
        List<Cd> Cds { get; set; }
        
        public Library()
        {
            Books = new List<Book>();
            Dvds = new List<Dvd>();
            Cds = new List<Cd>();
        }

        public void AddItem(Book item)
        {
            Books.Add(item);
        }
        public void AddItem(Dvd item)
        {
            Dvds.Add(item);
        }
        public void AddItem(Cd item)
        {
            Cds.Add(item);
        }

        public void RemoveItem(Book item)
        {
            Books.Remove(item);
        }
        public void RemoveItem(Dvd item)
        {
            Dvds.Remove(item);
        }
        public void RemoveItem(Cd item)
        {
            Cds.Remove(item);
        }

        public void PrintLibrary()
        {
            for (int i = 0; i < Books.Count; i++)
            {
                Books[i].Print();
            }

            for (int i = 0; i < Dvds.Count; i++)
            {
                Dvds[i].Print();
            }

            for (int i = 0; i < Cds.Count; i++)
            {
                Cds[i].Print();
            }
        }
    }
}
