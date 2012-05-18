using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Text;
using Analyzer.Core.Analyzers;
using Analyzer.Core.Utilities;

namespace Analyzer.Core
{
	public class FileProcessor
	{
		public void ProcessFile(string fileName, IAnalyzer analyzer)
		{
			var sw = Stopwatch.StartNew();
			StreamReader reader;
			if (!TryOpenFile(fileName, out reader))
				return;
			using(reader)
			{
				PrintFileInfo(fileName, reader);
				ProgressNotifier progressNotifier = ProgressNotifier.Start("Reading and analyzing..", reader.BaseStream.Length - reader.BaseStream.Position);
				var chars = new char[4 * 1024];
				int read;
				while((read = reader.Read(chars, 0, chars.Length)) > 0)
				{
					for(int i = 0; i < read; i++)
					{
						analyzer.TreatChar(chars[i]);
					}
					progressNotifier.ReportCompleted(reader.BaseStream.Position);
				}
				progressNotifier.Finish();
			}
			var analysisResult = analyzer.Finish();
			Console.Out.WriteLine("----");
			Console.Out.WriteLine(analysisResult.ToHumanReadableText());
			Console.Out.WriteLine("----");
			Console.Out.WriteLine("Working time: " + sw.Elapsed);
		}

		private static void PrintFileInfo(string fileName, StreamReader reader)
		{
			Console.Out.WriteLine("Input file:      {0}", fileName);
			Console.Out.WriteLine("Input file size: {0}", HumanReadableFormatter.FormatBytes(reader.BaseStream.Length));
			Console.Out.WriteLine("");
		}

		private bool TryOpenFile(string fileName, out StreamReader reader)
		{
			reader = null;
			if (!File.Exists(fileName))
			{
				Console.Out.WriteLine("File {0} doesn't exist.");
				return false;
			}
			Exception exc;
			try
			{
				reader = new StreamReader(fileName, Encoding.UTF8, false);
				return true;
			}
			catch (SecurityException e)
			{
				exc = e;
				Console.Out.WriteLine("You don't have the required permission.");	
			}
			catch (UnauthorizedAccessException e)
			{
				exc = e;
				Console.Out.WriteLine("The access requested is not permitted by the operating system for the file {0}", fileName);
			}
			catch(Exception e)
			{
				exc = e;
				Console.Out.WriteLine("Unable to work with file {0}. ", fileName);
			}
			Console.Out.WriteLine("Please contact your system administrator and provide him with error details: {0}", exc);
			return false;
		}
	}
}