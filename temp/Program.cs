using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace temp
{
	class Program
	{
		static void Main(string[] args)
		{
			var swr = Stopwatch.StartNew();
			string[] fileNames = Directory.GetFiles(@"c:\kazakoff\dev\kazakov.ysoft\temp\bin\Release\workspace\chunks_634725078589815388\");

			FileStream[] files = new FileStream[fileNames.Length];
			Input[] inputs = new Input[fileNames.Length];
			StreamReader[] readers = new StreamReader[fileNames.Length];
			try
			{
				for(int i = 0; i < files.Length; i++)
				{
					files[i] = new FileStream(fileNames[i], FileMode.Open, FileAccess.Read, FileShare.Read, 200*1024);
					readers[i] = new StreamReader(files[i], Encoding.UTF8, false, 200*1024);
					inputs[i] = new Input(readers[i]);
				}
				using(FileStream output = new FileStream(@"c:\temp\lala\out", FileMode.Create, FileAccess.Write, FileShare.Read, 16*1024*1024))
				{
					new Merger().Merge(inputs, output);
				}
			}
			finally
			{
				for(int i = 0; i < files.Length; i++)
				{
					if (files[i] != null)
						files[i].Dispose();
					if (readers[i] != null)
						readers[i].Dispose();
				}	
			}

			Console.Out.WriteLine("PeakPagedMemorySize: " + Process.GetCurrentProcess().PeakPagedMemorySize64 / 1024 / 1024);
			Console.Out.WriteLine("PeakWorkingSet: " + Process.GetCurrentProcess().PeakWorkingSet64 / 1024 / 1024);
			Console.Out.WriteLine(swr.Elapsed);
			return;
			Test();
		}

		private static void Test()
		{
			var sw = Stopwatch.StartNew();
			TimeSpan prev = TimeSpan.MinValue;
			int count = 100 * 1000 * 1000;
			int ten = count / 100;
			var processor = new WordsProcessor();
			var rnd = new Random();
			for(int i = 0; i < count; i++)
			{
				char[] charArray = rnd.Next(0, 10*1000*1000).ToString(CultureInfo.InvariantCulture).ToCharArray();
//				char[] charArray = i.ToString().ToCharArray();
				processor.Add(charArray);
				if(i >= ten && i % ten == 0)
				{
					var mem = Process.GetCurrentProcess().PagedMemorySize64 / 1024 / 1024;
					string p = "";
					if(prev != TimeSpan.MinValue)
						p = (sw.Elapsed - prev).ToString();
					Console.Out.WriteLine(sw.Elapsed + " " + p + " " + mem + " MB" + " Count: " + i);
					prev = sw.Elapsed;
				}
			}
			processor.Flush();
		}
	}

	public static class Consts
	{
		public const int CHUNK_SIZE = 64*1024*1024;
	}

	public static class Chars
	{
		public static byte SP = 32;
		public static byte EOL = 10;
	}
}
