using System;
using System.Runtime.InteropServices;
using SQLite;

namespace Touchin.SQLiteExtensions
{
	public static partial class SQLite3Extensions
	{
		private static void CreateFunction (this SQLiteConnection connection, string functionName, int paramCount, Action<IntPtr, int, IntPtr> functionBody)
		{
			var result = CreateFunction (connection.Handle, functionName, paramCount, TextEncoding.UTF8, IntPtr.Zero, functionBody, null, null);
			if (result != SQLite3.Result.OK)
			{
				throw SQLiteException.New (result, "Can not create function");
			}
		}

		private static IntPtr [] ExtractArgs (int argc, IntPtr argv)
		{
			IntPtr [] args = new IntPtr [argc];
			Marshal.Copy (argv, args, 0, argc);
			return args;
		}

		// SQLITE_API int sqlite3_create_function(
		//     sqlite3 *db,
		//     const char *zFunctionName,
		//     int nArg,
		//     int eTextRep,
		//     void *pApp,
		//     void (*xFunc)(sqlite3_context*,int,sqlite3_value**),
		//     void (*xStep)(sqlite3_context*,int,sqlite3_value**),
		//     void (*xFinal)(sqlite3_context*)
		// );
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_create_function")]
		private static extern SQLite3.Result CreateFunction (
			IntPtr db,
			string zFunctionName,
			int nArg,
			TextEncoding eTextRep,
			IntPtr pApp,
			Action<IntPtr, int, IntPtr> xFunc,
			Action<IntPtr, int, IntPtr> xStep,
			Action<IntPtr, int, IntPtr> xFinal
		);

		#region sqlite3_value

		// SQLITE_API int sqlite3_value_type(sqlite3_value*);
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_value_type")]
		private static extern SQLite3.ColType ValueType (IntPtr value);

		// SQLITE_API double sqlite3_value_double(sqlite3_value*);
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_value_double")]
		private static extern double ValueDouble (IntPtr value);

		#endregion

		#region sqlite3_context

		// SQLITE_API void sqlite3_result_null(sqlite3_context*);
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_result_null")]
		private static extern void ResultNull (IntPtr ctx);

		// SQLITE_API void sqlite3_result_double(sqlite3_context*, double);
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_result_double")]
		private static extern void ResultDouble (IntPtr ctx, double value);

		#endregion

		// These constant define integer codes that represent the various text encodings supported by SQLite.
		private enum TextEncoding
		{
			UTF8         = 1,
			UTF16LE      = 2,
			UTF16BE      = 3,
			UTF16        = 4, /* Use native byte order */
			Any          = 5, /* sqlite3_create_function only */
			UTF16Aligned = 8, /* sqlite3_create_collation only */
		}
	}
}