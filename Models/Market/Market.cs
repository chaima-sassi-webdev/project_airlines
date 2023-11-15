using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_airlines.Models.Market
{
	[Table("Market")]
	public class Market
	{
		[Key]
		public string escale_D { get; set; }
		public string zonegeo { get; set; }
		 public string zonegeoarabe { get; set; }
		public string pays { get; set; }
		public string paysArabe { get; set; }
		public string paysArRapport { get; set; }
		
	}
}
