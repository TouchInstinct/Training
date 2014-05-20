using System;
using System.Runtime.InteropServices;
using SQLite;

namespace Touchin.SQLiteExtensions
{
    public static partial class SQLite3Extensions
    {
        public static void CreateDistanceFunction (this SQLiteConnection connection)
        {
            connection.CreateFunction ("DISTANCE", 4, DistanceBody);
        }
     
        private static double Distance (double latitude1, double longitude1, double latitude2, double longitude2)
        {
            const double earthRadius = 6378.1;
     
            double lat1 =  latitude1 * Math.PI / 180;
            double lon1 = longitude1 * Math.PI / 180;
            double lat2 =  latitude2 * Math.PI / 180;
            double lon2 = longitude2 * Math.PI / 180;
     
            return earthRadius * Math.Acos (Math.Sin (lat1) * Math.Sin (lat2) + Math.Cos (lat1) * Math.Cos (lat2) * Math.Cos (lon2 - lon1));
        }

#if __IOS__
        // https://bugzilla.novell.com/show_bug.cgi?id=576775
        [MonoTouch.MonoPInvokeCallback (typeof (Action<IntPtr, int, IntPtr>))]
#endif
        private static void DistanceBody (IntPtr ctx, int argc, IntPtr argv)
        {
            IntPtr[] args = ExtractArgs (argc, argv);
     
            if (ValueType (args [0]) != SQLite3.ColType.Float ||
                ValueType (args [1]) != SQLite3.ColType.Float ||
                ValueType (args [2]) != SQLite3.ColType.Float ||
                ValueType (args [3]) != SQLite3.ColType.Float)
            {
                ResultNull (ctx);
            }
            else
            {
                double  latitude1 = ValueDouble (args [0]);
                double longitude1 = ValueDouble (args [1]);
                double  latitude2 = ValueDouble (args [2]);
                double longitude2 = ValueDouble (args [3]);
                double result = Distance (latitude1, longitude1, latitude2, longitude2);
                ResultDouble (ctx, result);
            }
        }
    }
}