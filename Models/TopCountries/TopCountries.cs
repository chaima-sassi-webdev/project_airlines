using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_airlines.Models.TopCountries
{
	public class TopCountries
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public float Filling_Ratio { get; set; }

	public int Traffic_Passenger { get; set; }
	
	public string Zonegeo { get; set; }

	
	}
}
