namespace UnitTestingUsingMoq.Models
{
    public class User
    {
        //User: Id, FirstName, LastName, Email
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        //navigation
        public List<Order> Orders { get; set; }
    }
}
