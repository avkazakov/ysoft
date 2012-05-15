using System;
using System.Diagnostics;

namespace temp
{
	public static class Log 
	{
		static Log()
		{
			IsInfoEnabled = true;
			IsWarnEnabled = true;
			IsDebugEnabled = false;
			IsErrorEnabled = true;
			IsFatalEnabled = true;
			TuneDebug();
		}


		# region INFO

		public static void Info(object message)
		{
			CallsInfoCount++;
			lock (sync)
			{
				SetInfoColor();
				Console.WriteLine(Format("INFO " + message));
			}
		}

		public static void Info(string message)
		{
			CallsInfoCount++;
			lock (sync)
			{
				SetInfoColor();
				Console.WriteLine(Format("INFO " + message));
			}
		}

		public static void Info(string format, object arg0)
		{
			CallsInfoCount++;
			lock (sync)
			{
				SetInfoColor();
				Console.WriteLine(Format("INFO " + format, arg0));
			}
		}

		public static void Info(string format, object arg0, object arg1)
		{
			CallsInfoCount++;
			lock (sync)
			{
				SetInfoColor();
				Console.WriteLine(Format("INFO " + format, arg0, arg1));
			}
		}

		public static void Info(string format, object arg0, object arg1, object arg2)
		{
			CallsInfoCount++;
			lock (sync)
			{
				SetInfoColor();
				Console.WriteLine(Format("INFO " + format, arg0, arg1, arg2));
			}
		}

		public static void Info(string format, params object[] parameters)
		{
			CallsInfoCount++;
			lock (sync)
			{
				SetInfoColor();
				Console.WriteLine(Format("INFO " + format, parameters));
			}
		}

		public static void Info(IFormatProvider provider, string format, params object[] args)
		{
			CallsInfoCount++;
			lock (sync)
			{
				SetInfoColor();
				Console.WriteLine(Format(provider, "INFO " + format, args));
			}
		}

		#endregion

		# region WARN

		public static void Warn(object message)
		{
			CallsWarnCount++;
			lock (sync)
			{
				SetWarnColor();
				Console.WriteLine(Format("WARN " + message));
			}
		}

		public static void Warn(string message)
		{
			CallsWarnCount++;
			lock (sync)
			{
				SetWarnColor();
				Console.WriteLine(Format("WARN " + message));
			}
		}

		public static void Warn(string format, object arg0)
		{
			CallsWarnCount++;
			lock (sync)
			{
				SetWarnColor();
				Console.WriteLine(Format("WARN " + format, arg0));
			}
		}

		public static void Warn(string format, object arg0, object arg1)
		{
			CallsWarnCount++;
			lock (sync)
			{
				SetWarnColor();
				Console.WriteLine(Format("WARN " + format, arg0, arg1));
			}
		}

		public static void Warn(string format, object arg0, object arg1, object arg2)
		{
			CallsWarnCount++;
			lock (sync)
			{
				SetWarnColor();
				Console.WriteLine(Format("WARN " + format, arg0, arg1, arg2));
			}
		}

		public static void Warn(string format, params object[] parameters)
		{
			CallsWarnCount++;
			lock (sync)
			{
				SetWarnColor();
				Console.WriteLine(Format("WARN " + format, parameters));
			}
		}

		public static void Warn(IFormatProvider provider, string format, params object[] args)
		{
			CallsWarnCount++;
			lock (sync)
			{
				SetWarnColor();
				Console.WriteLine(Format(provider, "WARN " + format, args));
			}
		}

		#endregion

		# region ERROR

		public static void Error(object message)
		{
			CallsErrorCount++;
			lock (sync)
			{
				SetErrorColor();
				Console.WriteLine(Format("ERROR " + message));
			}
		}

		public static void Error(string message)
		{
			CallsErrorCount++;
			lock (sync)
			{
				SetErrorColor();
				Console.WriteLine(Format("ERROR " + message));
			}
		}

		public static void Error(string format, object arg0)
		{
			CallsErrorCount++;
			lock (sync)
			{
				SetErrorColor();
				Console.WriteLine(Format("ERROR " + format, arg0));
			}
		}

		public static void Error(string format, object arg0, object arg1)
		{
			CallsErrorCount++;
			lock (sync)
			{
				SetErrorColor();
				Console.WriteLine(Format("ERROR " + format, arg0, arg1));
			}
		}

		public static void Error(string format, object arg0, object arg1, object arg2)
		{
			CallsErrorCount++;
			lock (sync)
			{
				SetErrorColor();
				Console.WriteLine(Format("ERROR " + format, arg0, arg1, arg2));
			}
		}

		public static void Error(string format, params object[] parameters)
		{
			CallsErrorCount++;
			lock (sync)
			{
				SetErrorColor();
				Console.WriteLine(Format("ERROR " + format, parameters));
			}
		}

		public static void Error(IFormatProvider provider, string format, params object[] args)
		{
			CallsErrorCount++;
			lock (sync)
			{
				SetErrorColor();
				Console.WriteLine(Format(provider, "ERROR " + format, args));
			}
		}

		#endregion

		# region DEBUG

		public static void Debug(object message)
		{
			CallsDebugCount++;
			if (! IsDebugEnabled) return;
			lock (sync)
			{
				SetDebugColor();
				Console.WriteLine(Format("DEBUG " + message));
			}
		}

		public static void Debug(string message)
		{
			CallsDebugCount++;
			if (! IsDebugEnabled) return;
			lock (sync)
			{
				SetDebugColor();
				Console.WriteLine(Format("DEBUG " + message));
			}
		}

		public static void Debug(string format, object arg0)
		{
			CallsDebugCount++;
			if (! IsDebugEnabled) return;
			lock (sync)
			{
				SetDebugColor();
				Console.WriteLine(Format("DEBUG " + format, arg0));
			}
		}

		public static void Debug(string format, object arg0, object arg1)
		{
			CallsDebugCount++;
			if (! IsDebugEnabled) return;
			lock (sync)
			{
				SetDebugColor();
				Console.WriteLine(Format("DEBUG " + format, arg0, arg1));
			}
		}

		public static void Debug(string format, object arg0, object arg1, object arg2)
		{
			CallsDebugCount++;
			if (! IsDebugEnabled) return;
			lock (sync)
			{
				SetDebugColor();
				Console.WriteLine(Format("DEBUG " + format, arg0, arg1, arg2));
			}
		}

		public static void Debug(string format, params object[] parameters)
		{
			CallsDebugCount++;
			if (! IsDebugEnabled) return;
			lock (sync)
			{
				SetDebugColor();
				Console.WriteLine(Format("DEBUG " + format, parameters));
			}
		}

		public static void Debug(IFormatProvider provider, string format, params object[] args)
		{
			CallsDebugCount++;
			if (! IsDebugEnabled) return;
			lock (sync)
			{
				SetDebugColor();
				Console.WriteLine(Format(provider, "DEBUG " + format, args));
			}
		}

		#endregion

		#region FATAL

		public static void Fatal(object message)
		{
			CallsFatalCount++;
			Console.WriteLine(Format("FATAL " + message));
		}

		public static void Fatal(string message)
		{
			CallsFatalCount++;
			Console.WriteLine(Format("FATAL " + message));
		}

		public static void Fatal(string format, object arg0)
		{
			CallsFatalCount++;
			Console.WriteLine(Format("FATAL " + format, arg0));
		}

		public static void Fatal(string format, object arg0, object arg1)
		{
			CallsFatalCount++;
			Console.WriteLine(Format("FATAL " + format, arg0, arg1));
		}

		public static void Fatal(string format, object arg0, object arg1, object arg2)
		{
			CallsFatalCount++;
			Console.WriteLine(Format("FATAL " + format, arg0, arg1, arg2));
		}

		public static void Fatal(string format, params object[] parameters)
		{
			CallsFatalCount++;
			Console.WriteLine(Format("FATAL " + format, parameters));
		}

		public static void Fatal(IFormatProvider provider, string format, params object[] args)
		{
			CallsFatalCount++;
			Console.WriteLine(Format(provider, "FATAL " + format, args));
		}

		#endregion

		private static readonly object sync = new object();
		public static int CallsInfoCount { get; set; }
		public static int CallsWarnCount { get; set; }
		public static int CallsDebugCount { get; set; }
		public static int CallsErrorCount { get; set; }
		public static int CallsFatalCount { get; set; }

		#region ILog Members

		public static bool IsInfoEnabled { get; set; }
		public static bool IsWarnEnabled { get; set; }
		public static bool IsDebugEnabled { get; set; }
		public static bool IsErrorEnabled { get; set; }
		public static bool IsFatalEnabled { get; set; }

		#endregion

		private static void SetInfoColor()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		private static void SetWarnColor()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
		}

		private static void SetErrorColor()
		{
			Console.ForegroundColor = ConsoleColor.Red;
		}

		private static void SetDebugColor()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
		}

		public static void ResetCounters()
		{
			CallsInfoCount = 0;
			CallsWarnCount = 0;
			CallsDebugCount = 0;
			CallsErrorCount = 0;
			CallsFatalCount = 0;
		}

		[Conditional("DEBUG")]		
		private static void TuneDebug()
		{
			IsDebugEnabled = true;
		}

		#region FORMATS

		private static string Format(string format, object arg0)
		{
			try
			{
				return string.Format(format, arg0);
			}
			catch (Exception e)
			{
				return "Exception while rendering format [" + format + "]; Exception: " + e;
			}
		}

		private static string Format(string format, object arg0, object arg1)
		{
			try
			{
				return string.Format(format, arg0, arg1);
			}
			catch (Exception e)
			{
				return "Exception while rendering format [" + format + "]; Exception: " + e;
			}
		}

		private static string Format(string format, object arg0, object arg1, object arg2)
		{
			try
			{
				return string.Format(format, arg0, arg1, arg2);
			}
			catch (Exception e)
			{
				return "Exception while rendering format [" + format + "]; Exception: " + e;
			}
		}

		private static string Format(string format, params object[] args)
		{
			try
			{
				return string.Format(format, args);
			}
			catch (Exception e)
			{
				return "Exception while rendering format [" + format + "]; Exception: " + e;
			}
		}

		private static string Format(IFormatProvider prov, string format, params object[] args)
		{
			try
			{
				return string.Format(prov, format, args);
			}
			catch (Exception e)
			{
				return "Exception while rendering format [" + format + "]; Exception: " + e;
			}
		}

		#endregion
	}
}