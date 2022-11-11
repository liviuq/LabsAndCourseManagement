using LabsAndManagement.Business;

namespace LabsAndManagement.API.Features.Students
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Delete(string NrMatricol);
        IEnumerable<Student> GetAll();
        void Update(Student student);
    }
}