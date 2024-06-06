namespace WebAPINormal.Extension
{
    public static class ConverterExtensions
    {
        public static DAL.Models.Student ToDAL(this WebAPINormal.Models.StudentM studentM)
        {
            return new DAL.Models.Student
            {
                StudentID = studentM.StudentID,
                FirstName = studentM.FirstName,
                LastName = studentM.LastName,
                Email = studentM.Email,
                DateOfBirth = studentM.DateOfBirth,
                Balance = studentM.Balance
            };
        }

        public static WebAPINormal.Models.StudentM ToModel(this DAL.Models.Student student)
        {
            return new WebAPINormal.Models.StudentM
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                Balance = student.Balance
            };
        }
    }
}