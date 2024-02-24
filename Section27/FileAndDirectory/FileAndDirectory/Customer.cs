namespace FileAndDirectory
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }

        public Customer(int id, string name, string city, string streetAddress)
        {
            Id = id;
            Name = name;
            City = city;
            StreetAddress = streetAddress;
        }

        private Customer()
        {
            Id = default;
            Name = string.Empty;
            City = string.Empty;
            StreetAddress = string.Empty;
        }
    }
}
