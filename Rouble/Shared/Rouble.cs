using System;
using System.Collections.Generic;

namespace Rouble
{
	public static class Roubles
	{
		private static readonly Dictionary<RoubleType, char> _roubleSymbols;

		public static char RegularSymbol
		{
			get { return GetRoubleSymbFor(RoubleType.Regular); }
		}

		public static char MediumSymbol
		{
			get { return GetRoubleSymbFor(RoubleType.Medium); }
		}

		public static char BoldSympbol
		{
			get { return GetRoubleSymbFor(RoubleType.Bold); }
		}

		static Roubles()
		{
			_roubleSymbols = new Dictionary<RoubleType, char>(3)
			{
				{ RoubleType.Regular, 'r' },
				{ RoubleType.Medium, 'm' },
				{ RoubleType.Bold, 'b' },
			};
		}

		public static char GetRoubleSymbFor(RoubleType type)
		{
			return _roubleSymbols[type];
		}
	}
}
