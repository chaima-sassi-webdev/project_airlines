using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using project_airlines.Models.Market;
using project_airlines.Models.Market_Details;
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

		public string Pays { get; set; }
		[NotMapped]
		public List<string> GroupedCountries { get; set; }
		
	}
}
