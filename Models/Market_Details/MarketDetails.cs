using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace project_airlines.Models.Market_Details
{
	public class MarketDetails
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarketIdentifier { get; set; }
		public string ID { get; set; }
        public string AC_REGISTRATION { get; set; }

        public string DAY_OF_ORIGIN { get; set; }
        public int PAX_FLOWN_C { get; set;}
        public int PAX_FLOWN_Y { get; set; }
        public string FN_CARRIER { get; set; }
        public int FN_NUMBER { get; set; }
        public string ARR_AP_ACTUAL { get; set; }
        public int PAX_BOOKED_C { get; set; }

        public int PAX_BOOKED_Y { get; set; }

        public string ESCALESTAT { get; set; }

        public int AVG_FARE { get; set; }


    }
}
