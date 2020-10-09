namespace Zora.Students.Application.Commands.Create
{
    public class CreateStudentsOutputModel
    {
        public CreateStudentsOutputModel(int studentId)
            => this.StudentId = studentId;

        public int StudentId { get; set; }
    }
}
