using System;
using System.Diagnostics;
using System.Linq;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SQLite;

namespace Touchin.SQLiteExtensions
{
	public class MainViewController : UITableViewController
	{
		private readonly PlaceService _placeService;
		private readonly CLLocationManager _locationManager;
		private readonly Stopwatch _stopwatch;

		private int _totalCount;
		private Place[] _places;

		public MainViewController ()
		{
			Title = "SQLite Extensions";

			var databasePath = NSBundle.MainBundle.PathForResource ("Place", "sqlite");
			_placeService = new PlaceService (databasePath);
			_locationManager = new CLLocationManager();
			_stopwatch = new Stopwatch ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableView.AllowsSelection = false;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			_totalCount = _placeService.GetPlaceCount ();

			if (CLLocationManager.LocationServicesEnabled)
			{
				_locationManager.LocationsUpdated += HandleLocationsUpdated;
				_locationManager.StartUpdatingLocation ();
			}
		}

		private void HandleLocationsUpdated (object sender, CLLocationsUpdatedEventArgs e)
		{
			var location = e.Locations.Last ();

			_stopwatch.Restart ();
			_places = _placeService.GetPlaces (location.Coordinate.Latitude, location.Coordinate.Longitude);
			_stopwatch.Stop ();

			TableView.ReloadData ();
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _places != null ? _places.Length : 0;
		}

		public override string TitleForHeader (UITableView tableView, int section)
		{
			return String.Format ("Places ({0}/{1}) : {2} ms", RowsInSection (tableView, section), _totalCount, _stopwatch.ElapsedMilliseconds);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var reuseIdentifier = "Touchin.SQLiteExtensions.MainViewController.PlaceCell";
			var cell = tableView.DequeueReusableCell (reuseIdentifier) ?? new UITableViewCell (UITableViewCellStyle.Value1, reuseIdentifier);

			var place = _places [indexPath.Row];
			cell.TextLabel.Text = place.GetHumanReadableCoordinate ();
			cell.DetailTextLabel.Text = String.Format ("{0:F3} km", place.Distance);
			return cell;
		}
	}
}

