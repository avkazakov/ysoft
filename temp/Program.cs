using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Analyzer.Core;

namespace temp
{
	class Program
	{
		static void Main(string[] args)
		{
//			for(int i = 0; i < int.MaxValue; i++)
//			{
//			}
//			Console.Out.WriteLine("");
//			Console.Out.WriteLine("DONE");

			var count = 33*1333*9333;

			var progressNotifier = ProgressNotifier.Start("Iteration by int", count);
			for(int i = 0; i < count - 1; i++)
			{
				progressNotifier.ReportOperation();
			}
			progressNotifier.Finish();
			Console.Out.WriteLine("");
			Console.Out.WriteLine("DONE");


			return;

//			Test();
//			Console.Out.WriteLine("PeakPagedMemorySize: " + Process.GetCurrentProcess().PeakPagedMemorySize64 / 1024 / 1024);
//			Console.Out.WriteLine("PeakWorkingSet: " + Process.GetCurrentProcess().PeakWorkingSet64 / 1024 / 1024);
//			Console.Out.WriteLine(swr.Elapsed);
//			return;





			Merge();
		}

		private static void Merge()
		{
			var swr = Stopwatch.StartNew();
			string[] fileNames = Directory.GetFiles(@"c:\kazakoff\dev\kazakov.ysoft\temp\bin\Release\workspace\chunks_634725168090484560\");
			FileStream[] files = new FileStream[fileNames.Length];
			Input[] inputs = new Input[fileNames.Length];
			StreamReader[] readers = new StreamReader[fileNames.Length];
			try
			{
				for(int i = 0; i < files.Length; i++)
				{
					files[i] = new FileStream(fileNames[i], FileMode.Open, FileAccess.Read, FileShare.Read, 200 * 1024);
					readers[i] = new StreamReader(files[i], Encoding.UTF8, false, 200 * 1024);
					inputs[i] = new Input(readers[i]);
				}
				using(FileStream output = new FileStream(@"c:\temp\lala\out", FileMode.Create, FileAccess.Write, FileShare.Read, 16 * 1024 * 1024))
					new Merger().Merge(inputs, output);
			}
			finally
			{
				for(int i = 0; i < files.Length; i++)
				{
					if(files[i] != null)
						files[i].Dispose();
					if(readers[i] != null)
						readers[i].Dispose();
				}
			}
			Console.Out.WriteLine("PeakPagedMemorySize: " + Process.GetCurrentProcess().PeakPagedMemorySize64 / 1024 / 1024);
			Console.Out.WriteLine("PeakWorkingSet: " + Process.GetCurrentProcess().PeakWorkingSet64 / 1024 / 1024);
			Console.Out.WriteLine(swr.Elapsed);
		}
		
	}

	public static class Consts
	{
		public const int CHUNK_SIZE = 128*1024*1024;
	}

	public static class Chars
	{
		public static byte SP = 32;
		public static byte EOL = 10;
	}
}
