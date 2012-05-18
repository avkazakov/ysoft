using System;

namespace Analyzer.Core
{
	public class ProgressNotifier
	{
		private ProgressNotifier(string processName, long expectedOperaions)
		{
			this.processName = processName;
			this.expectedOperaions = expectedOperaions;
		}

		public static ProgressNotifier Start(string processName, long expectedOperaions)
		{
			var pn = new ProgressNotifier(processName, expectedOperaions);
			pn.Start();
			return pn;
		}

		public void ReportOperation()
		{
			ReportCompleted(completedOperations+1);
		}

		public void ReportCompleted(long alreadyCompletedOperations)
		{
			completedOperations = alreadyCompletedOperations;
			double p = ((double)completedOperations / expectedOperaions) * 100;
			progress = (int)p;
			if (progress == prevProgress)
				return;
			PrintCurrentProgress();
			prevProgress = progress;
			if(progress == 100)
				Console.Out.WriteLine("");
		}

		private void PrintCurrentProgress()
		{
			int l = Console.CursorLeft;
			Console.CursorLeft = l - (prevProgress.ToString().Length + " %".Length);
			Console.Out.Write(progress.ToString() + " %");
		}

		public void Finish()
		{
			if (prevProgress != 100)
				prevProgress = 100;
			progress = 100;
			PrintCurrentProgress();
			Console.Out.WriteLine("");
		}


		private void Start()
		{
			Console.Out.Write("{0}. Completed:  0 %", processName);
		}

		private readonly string processName;
		private readonly long expectedOperaions;

		private int prevProgress;
		private int progress;
		private long completedOperations;
	}
}