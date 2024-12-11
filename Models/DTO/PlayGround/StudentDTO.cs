namespace XinWebAPI.Models.DTO.PlayGround
{
    public class StudentDTO
    {
        public int Id { get; set; }

        public string? StudentName { get; set; }

        public int Age { get; set; }

        public List<AddressDTO>? Address { get; set; }
    }
}
