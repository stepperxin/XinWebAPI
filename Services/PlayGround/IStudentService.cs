using XinWebAPI.Models.DTO.PlayGround;
using XinWebAPI.Models.PlayGround;

namespace XinWebAPI.Services.PlayGround
{
    public interface IStudentService
    {
        Task<List<Student>> getAllStudentsAsync();

        Task<Student> addStudent(StudentDTO studentDTO);
    }
}
