namespace Analyzer.Core.Utilities
{
	public static class HumanReadableFormatter
	{
		public static string FormatBytes(long sizeInB)
		{
			double sizeInKB = (double)sizeInB / 1024;
			if(sizeInKB < 1)
				return string.Format("{0} {1}", sizeInB, B);
			return FormatKB(sizeInKB);
		}

		public static string FormatBytes(int sizeInB)
		{
			double sizeInKB = (double)sizeInB / 1024;
			if(sizeInKB < 1)
				return string.Format("{0} {1}", sizeInB, B);
			return FormatKB(sizeInKB);
		}

		public static string FormatKB(int sizeInKB)
		{
			return FormatKB((double)sizeInKB);
		}

		public static string FormatKB(double sizeInKB)
		{
			double sizeInMB = sizeInKB / 1024;
			if(sizeInMB < 1)
				return string.Format("{0} {1}", sizeInKB.ToString(".00"), KB);
			double sizeInGB = sizeInMB / 1024;
			if(sizeInGB < 1)
				return string.Format("{0} {1}", sizeInMB.ToString(".00"), MB);
			return string.Format("{0} {1}", sizeInGB.ToString(".00"), GB);
		}

		public const string B = "B";
		public const string KB = "KB";
		public const string MB = "MB";
		public const string GB = "GB";
	}
}