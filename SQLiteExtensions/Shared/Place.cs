using System;

namespace Touchin.SQLiteExtensions
{
	public class Place
	{
		public int Id { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public double Distance { get; set; }

		public string GetHumanReadableCoordinate()
		{
			return
				GetHumanReadableDegrees (Math.Abs (Latitude))  + (Latitude  < 0 ? "S" : "N")
				+ ", " +
				GetHumanReadableDegrees (Math.Abs (Longitude)) + (Longitude < 0 ? "W" : "E");
		}

		private string GetHumanReadableDegrees (double value)
		{
			var degrees = Math.Truncate (value);
			var minutes = Math.Truncate ((value - degrees) * 60);
			var seconds = (((value - degrees) * 60) - minutes) * 60;
			return String.Format ("{0:00}\u00B0{1:00}\u2032{2:00}\u2033", degrees, minutes, seconds);
		}
	}
}

