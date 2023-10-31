using System.ComponentModel.DataAnnotations;

namespace project_airlines.Models.Supplier
{
    public class Supplier
    {
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[MaxLength(50)]
		[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name must contain only alphabetic characters.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Address is required")]
		[MaxLength(100)]
		public string Address { get; set; }

		[Required(ErrorMessage = "Mobile Phone is required")]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Mobile Phone must be a number")]
		public long mobile_phone { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string email { get; set; }




		public byte[]? SupplierImage { get; set; }


		public DateTime CreatedDateTime { get; set; } = DateTime.Now;

	}
}
