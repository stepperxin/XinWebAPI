using System.ComponentModel.DataAnnotations;

namespace XinWebAPI.Models.DTO.PlayGround
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Industry { get; set; }
    }
}
