using System;
using System.Diagnostics;
using Analyzer.Core.Utilities;

namespace Analyzer.Core
{
	public abstract class AnalyzerUtilityBase
	{
		protected static bool CheckParams(Parameters p)
		{
			if (!p.HasParam("--file"))
			{
				Console.Out.WriteLine("This program analyzes plane text file in UTF-8 encoding.");
				Console.Out.WriteLine("");
				Console.Out.WriteLine("USAGE: {0}.exe --file <path_to_the_file>", Process.GetCurrentProcess().ProcessName);
				Console.Out.WriteLine("");
				return false;
			}

			return true;
		}

		protected static string GetFileName(Parameters p)
		{
			return p.GetValue("--file");
		}
	}
}