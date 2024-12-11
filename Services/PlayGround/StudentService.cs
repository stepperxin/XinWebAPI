using Microsoft.EntityFrameworkCore;
using AutoMapper;
using XinWebAPI.Data.PlayGround;
using XinWebAPI.Models.PlayGround;
using XinWebAPI.Models.DTO.PlayGround;

namespace XinWebAPI.Services.PlayGround
{
    public class StudentService : IStudentService
    {
        private readonly PlayGroundDBContext _studentPortalDbContext;
        private readonly IMapper _mapper;
        public StudentService(PlayGroundDBContext studentPortalDbContext, IMapper mapper)
        {
            this._studentPortalDbContext = studentPortalDbContext;
            this._mapper = mapper;
        }

        public async Task<List<Student>> getAllStudentsAsync()
        {
            var student = await _studentPortalDbContext.Student.Include(_ => _.Address).ToListAsync();
            return student;
        }

        public async Task<Student> addStudent(StudentDTO studentDTO)
        {
            Student student = _mapper.Map<Student>(studentDTO);
            _studentPortalDbContext.Student.Add(student);
            await _studentPortalDbContext.SaveChangesAsync();
            return student;
        }
    }
}
