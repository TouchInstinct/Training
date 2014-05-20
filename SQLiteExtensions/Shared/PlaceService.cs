using System;
using System.Linq;
using SQLite;

namespace Touchin.SQLiteExtensions
{
	public class PlaceService : IDisposable
	{
		private readonly SQLiteConnection _databaseConnection;

		public PlaceService (string databasePath)
		{
			_databaseConnection = new SQLiteConnection (databasePath);
			_databaseConnection.CreateDistanceFunction ();
		}

		public Place[] GetPlaces(double latitude, double longitude)
		{
			const string query = @"
SELECT *, DISTANCE(Latitude, Longitude, @latitude, @longitude) AS Distance
  FROM Place
 WHERE Distance < @radius
 ORDER BY Distance
";
			var command = _databaseConnection.CreateCommand (query);
			command.Bind ("@latitude", latitude);
			command.Bind ("@longitude", longitude);
			command.Bind ("@radius", 100d);
			return command.ExecuteDeferredQuery<Place> ().ToArray ();
		}

		public int GetPlaceCount()
		{
			const string query = @"
SELECT COUNT(*) FROM Place
";
			return _databaseConnection.ExecuteScalar<int> (query);
		}

		public void Dispose ()
		{
			_databaseConnection.Dispose ();
		}
	}
}

