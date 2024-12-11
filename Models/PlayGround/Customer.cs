using System.ComponentModel.DataAnnotations;
namespace XinWebAPI.Models.PlayGround
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name field is required.", AllowEmptyStrings = false)]
        [StringLength(200,MinimumLength = 1, ErrorMessage = "Name is required and must be less than 200 characters.")]
        public required string Name { get; set; }
        public required string Industry { get; set; }   
    }
}
