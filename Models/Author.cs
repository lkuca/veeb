namespace veeb.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public int ContactDataId { get; set; }
        public ContactData Contact { get; set; }
    }
}
