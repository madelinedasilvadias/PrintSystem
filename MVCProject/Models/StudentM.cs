namespace MVCProject.Models
{
    public class StudentM
    {
        public decimal Balance { get; set; }
        public int StudentID { get; set; }
        public int ClasseID { get; set; }
        public int AccountID { get; set; } // Add this property
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
