using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using project_airlines.Models.Market_Details;
namespace project_airlines.Models.MiddleWare
{
	public class MiddleWare
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Zonegeo {  get; set; }
		public string Pays { get; set; }
		public int Traffic_Passenger { get; set; }
		public Market_Details.MarketDetails marketDetails { get; set; }

	}
}
