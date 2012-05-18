using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Analyzer.Core
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = Stopwatch.StartNew();
//			var file = new FileInfo(@"c:\temp\lala\out");
//			var file = new FileInfo(@"c:\kazakoff\dev\kazakov.ysoft\temp\bin\Release\workspace\chunks_634725168090484560\chunk_231");
			var file = new FileInfo(@"c:\kazakoff\dev\kazakov.ysoft\temp\bin\Release\workspace\chunks_634725168090484560\lalala");
			sw.Restart();
			//ReadFile1(file);
			Console.Out.WriteLine(sw.Elapsed);
			sw.Restart();
			ReadFile2(file);
			Console.Out.WriteLine(sw.Elapsed);
			sw.Restart();
			//ReadFile1(file);
			Console.Out.WriteLine(sw.Elapsed);
			sw.Restart();
			ReadFile2(file);
			Console.Out.WriteLine(sw.Elapsed);
		}

		static void ReadFile1(FileInfo file)
		{
			int sum = 0;
			using(var sr = new StreamReader(file.FullName, Encoding.UTF8, false))
			{
				int ci;
				while((ci = sr.Read()) >= 0)
					sum += (char)ci;
			}
			Console.Out.WriteLine(sum);
		}

		static void ReadFile2(FileInfo file)
		{
			int sum = 0;
//			int fsize = 32 * 1024 * 1024;
//			int fsize = 32 * 1024;
			int fsize = 4 * 1024;
			//int bsize = fsize / 4;
			int bsize = 4 * 1024;
//			char[] cbuf = new char[Encoding.UTF8.GetMaxCharCount(bsize)];
			char[] cbuf = new char[bsize];
//			using(var fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, fsize))
			//using(var sr = new StreamReader(fs, Encoding.UTF8, false, bsize))
			using(var sr = new StreamReader(file.FullName, Encoding.UTF8))
			{
				int read;
				while((read = sr.Read(cbuf, 0, cbuf.Length)) > 0)
//				while((read = sr.ReadBlock(cbuf, 0, cbuf.Length)) > 0)
				{
					for(int i = 0; i < read; i++)
					{
						sum += char.IsLetter(cbuf[i]) ? 1 : 0;
					}
				}
			}
			Console.Out.WriteLine(sum);
		}
	}
}
