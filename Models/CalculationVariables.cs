namespace project_airlines.Models
{
	public class CountryTraffic
	{
		public string Country { get; set; }
		public int TrafficPassenger { get; set; }

		public int OfferedSeats { get; set; }
		public float FillingRatio { get; set; }
		public double MarketPassengerShare { get; set; }
		public int TotalFirstClassPassenger { get; set; }
		public int AverageTicketValue_TND { get; set; }
		public double TransactionNumber_Million_TND { get; set; }
		public double MarketTransactionShare { get; set; }
		public double MarketTransactionShareT { get; set; }
	}
	public class CalculationVariables
	{

		public Market.Market Market {  get; set; }
		
		public Market_Details.MarketDetails MarketDetails { get; set; }
		public float Traffic_Passenger {  get; set; }
		public List<CountryTraffic> Countries { get; set; }
		public float Offered_seats { get; set; }
		public float Filling_Ratio { get; set; }
		public double MarketPassengerShare_T { get; set; }
		public int TotalFirstClassPassenger_T { get; set; }
		public int AverageTicketValue_TND_T { get; set; }

		public double TransactionNumber_Million_TND_T { get; set; }

		public double MarketTransactionShare {  get; set; }
		public double MarketTransactionShareT { get; set; }

		public string Pays { get; set; }

		public string ZoneGeo {  get; set; }
		


	}
}
