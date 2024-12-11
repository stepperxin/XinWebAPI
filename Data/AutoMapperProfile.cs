using AutoMapper;
using XinWebAPI.Models.DTO.PlayGround;
using XinWebAPI.Models.PlayGround;

namespace XinWebAPI.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StudentDTO, Student>();
            CreateMap<AddressDTO, Address>();
            CreateMap<CustomerDTO, Customer>();
            //CreateMap<XinPublisherDTO, XinPublisher>();
            //CreateMap<XinPublisher, XinPublisherDTO>();
        }
    }
}
