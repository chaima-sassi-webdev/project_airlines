using System.ComponentModel.DataAnnotations;
namespace project_airlines.Models.GlobalTraffic
{
	public class GlobalTraffic
	{

        [Key]
        public int YEAR { get; set; }

        public int FLOWN { get; set; }
        public int BOOKED { get; set; }
        public double CR { get; set; }
        
    }
}
