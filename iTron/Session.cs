using System;

namespace iTron
{
	public static class Session
	{
		private static bool _firstLaunch = true;

		public static bool FirstLaunch {
			get { return _firstLaunch; }
			set { _firstLaunch = value; }
		}
	}
}

