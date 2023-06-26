﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System
{
	/// <summary>Represents the standard input, output, and error streams for console applications. This class cannot be inherited.</summary>
	// Token: 0x020000C2 RID: 194
	public static class Console
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00022EDC File Offset: 0x000210DC
		private static object InternalSyncObject
		{
			get
			{
				if (Console.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref Console.s_InternalSyncObject, obj, null);
				}
				return Console.s_InternalSyncObject;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00022F0C File Offset: 0x0002110C
		private static object ReadKeySyncObject
		{
			get
			{
				if (Console.s_ReadKeySyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref Console.s_ReadKeySyncObject, obj, null);
				}
				return Console.s_ReadKeySyncObject;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00022F3C File Offset: 0x0002113C
		private static IntPtr ConsoleInputHandle
		{
			[SecurityCritical]
			get
			{
				if (Console._consoleInputHandle == IntPtr.Zero)
				{
					Console._consoleInputHandle = Win32Native.GetStdHandle(-10);
				}
				return Console._consoleInputHandle;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00022F66 File Offset: 0x00021166
		private static IntPtr ConsoleOutputHandle
		{
			[SecurityCritical]
			get
			{
				if (Console._consoleOutputHandle == IntPtr.Zero)
				{
					Console._consoleOutputHandle = Win32Native.GetStdHandle(-11);
				}
				return Console._consoleOutputHandle;
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00022F90 File Offset: 0x00021190
		[SecuritySafeCritical]
		private static bool IsHandleRedirected(IntPtr ioHandle)
		{
			SafeFileHandle safeFileHandle = new SafeFileHandle(ioHandle, false);
			int fileType = Win32Native.GetFileType(safeFileHandle);
			if ((fileType & 2) != 2)
			{
				return true;
			}
			int num;
			bool consoleMode = Win32Native.GetConsoleMode(ioHandle, out num);
			return !consoleMode;
		}

		/// <summary>Gets a value that indicates whether input has been redirected from the standard input stream.</summary>
		/// <returns>
		///   <see langword="true" /> if input is redirected; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00022FC4 File Offset: 0x000211C4
		public static bool IsInputRedirected
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._stdInRedirectQueried)
				{
					return Console._isStdInRedirected;
				}
				object internalSyncObject = Console.InternalSyncObject;
				bool flag2;
				lock (internalSyncObject)
				{
					if (Console._stdInRedirectQueried)
					{
						flag2 = Console._isStdInRedirected;
					}
					else
					{
						Console._isStdInRedirected = Console.IsHandleRedirected(Console.ConsoleInputHandle);
						Console._stdInRedirectQueried = true;
						flag2 = Console._isStdInRedirected;
					}
				}
				return flag2;
			}
		}

		/// <summary>Gets a value that indicates whether output has been redirected from the standard output stream.</summary>
		/// <returns>
		///   <see langword="true" /> if output is redirected; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0002303C File Offset: 0x0002123C
		public static bool IsOutputRedirected
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._stdOutRedirectQueried)
				{
					return Console._isStdOutRedirected;
				}
				object internalSyncObject = Console.InternalSyncObject;
				bool flag2;
				lock (internalSyncObject)
				{
					if (Console._stdOutRedirectQueried)
					{
						flag2 = Console._isStdOutRedirected;
					}
					else
					{
						Console._isStdOutRedirected = Console.IsHandleRedirected(Console.ConsoleOutputHandle);
						Console._stdOutRedirectQueried = true;
						flag2 = Console._isStdOutRedirected;
					}
				}
				return flag2;
			}
		}

		/// <summary>Gets a value that indicates whether the error output stream has been redirected from the standard error stream.</summary>
		/// <returns>
		///   <see langword="true" /> if error output is redirected; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x000230B4 File Offset: 0x000212B4
		public static bool IsErrorRedirected
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._stdErrRedirectQueried)
				{
					return Console._isStdErrRedirected;
				}
				object internalSyncObject = Console.InternalSyncObject;
				bool flag2;
				lock (internalSyncObject)
				{
					if (Console._stdErrRedirectQueried)
					{
						flag2 = Console._isStdErrRedirected;
					}
					else
					{
						IntPtr stdHandle = Win32Native.GetStdHandle(-12);
						Console._isStdErrRedirected = Console.IsHandleRedirected(stdHandle);
						Console._stdErrRedirectQueried = true;
						flag2 = Console._isStdErrRedirected;
					}
				}
				return flag2;
			}
		}

		/// <summary>Gets the standard input stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextReader" /> that represents the standard input stream.</returns>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00023130 File Offset: 0x00021330
		public static TextReader In
		{
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._in == null)
				{
					object internalSyncObject = Console.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (Console._in == null)
						{
							Stream stream = Console.OpenStandardInput(256);
							TextReader textReader;
							if (stream == Stream.Null)
							{
								textReader = StreamReader.Null;
							}
							else
							{
								Encoding inputEncoding = Console.InputEncoding;
								textReader = TextReader.Synchronized(new StreamReader(stream, inputEncoding, false, 256, true));
							}
							Thread.MemoryBarrier();
							Console._in = textReader;
						}
					}
				}
				return Console._in;
			}
		}

		/// <summary>Gets the standard output stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the standard output stream.</returns>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x000231C8 File Offset: 0x000213C8
		public static TextWriter Out
		{
			[HostProtection(SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._out == null)
				{
					Console.InitializeStdOutError(true);
				}
				return Console._out;
			}
		}

		/// <summary>Gets the standard error output stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the standard error output stream.</returns>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x000231E0 File Offset: 0x000213E0
		public static TextWriter Error
		{
			[HostProtection(SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._error == null)
				{
					Console.InitializeStdOutError(false);
				}
				return Console._error;
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000231F8 File Offset: 0x000213F8
		[SecuritySafeCritical]
		private static void InitializeStdOutError(bool stdout)
		{
			object internalSyncObject = Console.InternalSyncObject;
			lock (internalSyncObject)
			{
				if (!stdout || Console._out == null)
				{
					if (stdout || Console._error == null)
					{
						Stream stream;
						if (stdout)
						{
							stream = Console.OpenStandardOutput(256);
						}
						else
						{
							stream = Console.OpenStandardError(256);
						}
						TextWriter textWriter;
						if (stream == Stream.Null)
						{
							textWriter = TextWriter.Synchronized(StreamWriter.Null);
						}
						else
						{
							Encoding outputEncoding = Console.OutputEncoding;
							textWriter = TextWriter.Synchronized(new StreamWriter(stream, outputEncoding, 256, true)
							{
								HaveWrittenPreamble = true,
								AutoFlush = true
							});
						}
						if (stdout)
						{
							Console._out = textWriter;
						}
						else
						{
							Console._error = textWriter;
						}
					}
				}
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000232CC File Offset: 0x000214CC
		private static bool IsStandardConsoleUnicodeEncoding(Encoding encoding)
		{
			UnicodeEncoding unicodeEncoding = encoding as UnicodeEncoding;
			return unicodeEncoding != null && Console.StdConUnicodeEncoding.CodePage == unicodeEncoding.CodePage && Console.StdConUnicodeEncoding.bigEndian == unicodeEncoding.bigEndian;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002330C File Offset: 0x0002150C
		private static bool GetUseFileAPIs(int handleType)
		{
			switch (handleType)
			{
			case -12:
				return !Console.IsStandardConsoleUnicodeEncoding(Console.OutputEncoding) || Console.IsErrorRedirected;
			case -11:
				return !Console.IsStandardConsoleUnicodeEncoding(Console.OutputEncoding) || Console.IsOutputRedirected;
			case -10:
				return !Console.IsStandardConsoleUnicodeEncoding(Console.InputEncoding) || Console.IsInputRedirected;
			default:
				return true;
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00023370 File Offset: 0x00021570
		[SecuritySafeCritical]
		private static Stream GetStandardFile(int stdHandleName, FileAccess access, int bufferSize)
		{
			IntPtr stdHandle = Win32Native.GetStdHandle(stdHandleName);
			SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, false);
			if (safeFileHandle.IsInvalid)
			{
				safeFileHandle.SetHandleAsInvalid();
				return Stream.Null;
			}
			if (stdHandleName != -10 && !Console.ConsoleHandleIsWritable(safeFileHandle))
			{
				return Stream.Null;
			}
			bool useFileAPIs = Console.GetUseFileAPIs(stdHandleName);
			return new __ConsoleStream(safeFileHandle, access, useFileAPIs);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000233C4 File Offset: 0x000215C4
		[SecuritySafeCritical]
		private unsafe static bool ConsoleHandleIsWritable(SafeFileHandle outErrHandle)
		{
			byte b = 65;
			int num2;
			int num = Win32Native.WriteFile(outErrHandle, &b, 0, out num2, IntPtr.Zero);
			return num != 0;
		}

		/// <summary>Gets or sets the encoding the console uses to read input.</summary>
		/// <returns>The encoding used to read console input.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value in a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">Your application does not have permission to perform this operation.</exception>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x000233EC File Offset: 0x000215EC
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x00023468 File Offset: 0x00021668
		public static Encoding InputEncoding
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._inputEncoding != null)
				{
					return Console._inputEncoding;
				}
				object internalSyncObject = Console.InternalSyncObject;
				Encoding encoding;
				lock (internalSyncObject)
				{
					if (Console._inputEncoding != null)
					{
						encoding = Console._inputEncoding;
					}
					else
					{
						uint consoleCP = Win32Native.GetConsoleCP();
						Console._inputEncoding = Encoding.GetEncoding((int)consoleCP);
						encoding = Console._inputEncoding;
					}
				}
				return encoding;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				object internalSyncObject = Console.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (!Console.IsStandardConsoleUnicodeEncoding(value))
					{
						uint codePage = (uint)value.CodePage;
						if (!Win32Native.SetConsoleCP(codePage))
						{
							__Error.WinIOError();
						}
					}
					Console._inputEncoding = (Encoding)value.Clone();
					Console._in = null;
				}
			}
		}

		/// <summary>Gets or sets the encoding the console uses to write output.</summary>
		/// <returns>The encoding used to write console output.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value in a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">Your application does not have permission to perform this operation.</exception>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x000234F4 File Offset: 0x000216F4
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x00023570 File Offset: 0x00021770
		public static Encoding OutputEncoding
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._outputEncoding != null)
				{
					return Console._outputEncoding;
				}
				object internalSyncObject = Console.InternalSyncObject;
				Encoding encoding;
				lock (internalSyncObject)
				{
					if (Console._outputEncoding != null)
					{
						encoding = Console._outputEncoding;
					}
					else
					{
						uint consoleOutputCP = Win32Native.GetConsoleOutputCP();
						Console._outputEncoding = Encoding.GetEncoding((int)consoleOutputCP);
						encoding = Console._outputEncoding;
					}
				}
				return encoding;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				object internalSyncObject = Console.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (Console._out != null && !Console._isOutTextWriterRedirected)
					{
						Console._out.Flush();
						Console._out = null;
					}
					if (Console._error != null && !Console._isErrorTextWriterRedirected)
					{
						Console._error.Flush();
						Console._error = null;
					}
					if (!Console.IsStandardConsoleUnicodeEncoding(value))
					{
						uint codePage = (uint)value.CodePage;
						if (!Win32Native.SetConsoleOutputCP(codePage))
						{
							__Error.WinIOError();
						}
					}
					Console._outputEncoding = (Encoding)value.Clone();
				}
			}
		}

		/// <summary>Plays the sound of a beep through the console speaker.</summary>
		/// <exception cref="T:System.Security.HostProtectionException">This method was executed on a server, such as SQL Server, that does not permit access to a user interface.</exception>
		// Token: 0x06000B29 RID: 2857 RVA: 0x00023640 File Offset: 0x00021840
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static void Beep()
		{
			Console.Beep(800, 200);
		}

		/// <summary>Plays the sound of a beep of a specified frequency and duration through the console speaker.</summary>
		/// <param name="frequency">The frequency of the beep, ranging from 37 to 32767 hertz.</param>
		/// <param name="duration">The duration of the beep measured in milliseconds.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="frequency" /> is less than 37 or more than 32767 hertz.  
		/// -or-  
		/// <paramref name="duration" /> is less than or equal to zero.</exception>
		/// <exception cref="T:System.Security.HostProtectionException">This method was executed on a server, such as SQL Server, that does not permit access to the console.</exception>
		// Token: 0x06000B2A RID: 2858 RVA: 0x00023654 File Offset: 0x00021854
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static void Beep(int frequency, int duration)
		{
			if (frequency < 37 || frequency > 32767)
			{
				throw new ArgumentOutOfRangeException("frequency", frequency, Environment.GetResourceString("ArgumentOutOfRange_BeepFrequency", new object[] { 37, 32767 }));
			}
			if (duration <= 0)
			{
				throw new ArgumentOutOfRangeException("duration", duration, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			Win32Native.Beep(frequency, duration);
		}

		/// <summary>Clears the console buffer and corresponding console window of display information.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B2B RID: 2859 RVA: 0x000236D0 File Offset: 0x000218D0
		[SecuritySafeCritical]
		public static void Clear()
		{
			Win32Native.COORD coord = default(Win32Native.COORD);
			IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
			if (consoleOutputHandle == Win32Native.INVALID_HANDLE_VALUE)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
			}
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
			int num = (int)(bufferInfo.dwSize.X * bufferInfo.dwSize.Y);
			int num2 = 0;
			if (!Win32Native.FillConsoleOutputCharacter(consoleOutputHandle, ' ', num, coord, out num2))
			{
				__Error.WinIOError();
			}
			num2 = 0;
			if (!Win32Native.FillConsoleOutputAttribute(consoleOutputHandle, bufferInfo.wAttributes, num, coord, out num2))
			{
				__Error.WinIOError();
			}
			if (!Win32Native.SetConsoleCursorPosition(consoleOutputHandle, coord))
			{
				__Error.WinIOError();
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00023770 File Offset: 0x00021970
		[SecurityCritical]
		private static Win32Native.Color ConsoleColorToColorAttribute(ConsoleColor color, bool isBackground)
		{
			if ((color & (ConsoleColor)(-16)) != ConsoleColor.Black)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"));
			}
			Win32Native.Color color2 = (Win32Native.Color)color;
			if (isBackground)
			{
				color2 <<= 4;
			}
			return color2;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002379F File Offset: 0x0002199F
		[SecurityCritical]
		private static ConsoleColor ColorAttributeToConsoleColor(Win32Native.Color c)
		{
			if ((c & Win32Native.Color.BackgroundMask) != Win32Native.Color.Black)
			{
				c >>= 4;
			}
			return (ConsoleColor)c;
		}

		/// <summary>Gets or sets the background color of the console.</summary>
		/// <returns>A value that specifies the background color of the console; that is, the color that appears behind each character. The default is black.</returns>
		/// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x000237B4 File Offset: 0x000219B4
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x000237E4 File Offset: 0x000219E4
		public static ConsoleColor BackgroundColor
		{
			[SecuritySafeCritical]
			get
			{
				bool flag;
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out flag);
				if (!flag)
				{
					return ConsoleColor.Black;
				}
				Win32Native.Color color = (Win32Native.Color)(bufferInfo.wAttributes & 240);
				return Console.ColorAttributeToConsoleColor(color);
			}
			[SecuritySafeCritical]
			set
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				Win32Native.Color color = Console.ConsoleColorToColorAttribute(value, true);
				bool flag;
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out flag);
				if (!flag)
				{
					return;
				}
				short num = bufferInfo.wAttributes;
				num &= -241;
				num = (short)((ushort)num | (ushort)color);
				Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, num);
			}
		}

		/// <summary>Gets or sets the foreground color of the console.</summary>
		/// <returns>A <see cref="T:System.ConsoleColor" /> that specifies the foreground color of the console; that is, the color of each character that is displayed. The default is gray.</returns>
		/// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00023834 File Offset: 0x00021A34
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x00023860 File Offset: 0x00021A60
		public static ConsoleColor ForegroundColor
		{
			[SecuritySafeCritical]
			get
			{
				bool flag;
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out flag);
				if (!flag)
				{
					return ConsoleColor.Gray;
				}
				Win32Native.Color color = (Win32Native.Color)(bufferInfo.wAttributes & 15);
				return Console.ColorAttributeToConsoleColor(color);
			}
			[SecuritySafeCritical]
			set
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				Win32Native.Color color = Console.ConsoleColorToColorAttribute(value, false);
				bool flag;
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out flag);
				if (!flag)
				{
					return;
				}
				short num = bufferInfo.wAttributes;
				num &= -16;
				num = (short)((ushort)num | (ushort)color);
				Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, num);
			}
		}

		/// <summary>Sets the foreground and background console colors to their defaults.</summary>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B32 RID: 2866 RVA: 0x000238B0 File Offset: 0x00021AB0
		[SecuritySafeCritical]
		public static void ResetColor()
		{
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			bool flag;
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out flag);
			if (!flag)
			{
				return;
			}
			short num = (short)Console._defaultColors;
			Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, num);
		}

		/// <summary>Copies a specified source area of the screen buffer to a specified destination area.</summary>
		/// <param name="sourceLeft">The leftmost column of the source area.</param>
		/// <param name="sourceTop">The topmost row of the source area.</param>
		/// <param name="sourceWidth">The number of columns in the source area.</param>
		/// <param name="sourceHeight">The number of rows in the source area.</param>
		/// <param name="targetLeft">The leftmost column of the destination area.</param>
		/// <param name="targetTop">The topmost row of the destination area.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the parameters is less than zero.  
		///  -or-  
		///  <paramref name="sourceLeft" /> or <paramref name="targetLeft" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />.  
		///  -or-  
		///  <paramref name="sourceTop" /> or <paramref name="targetTop" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.  
		///  -or-  
		///  <paramref name="sourceTop" /> + <paramref name="sourceHeight" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.  
		///  -or-  
		///  <paramref name="sourceLeft" /> + <paramref name="sourceWidth" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B33 RID: 2867 RVA: 0x000238EC File Offset: 0x00021AEC
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, ' ', ConsoleColor.Black, Console.BackgroundColor);
		}

		/// <summary>Copies a specified source area of the screen buffer to a specified destination area.</summary>
		/// <param name="sourceLeft">The leftmost column of the source area.</param>
		/// <param name="sourceTop">The topmost row of the source area.</param>
		/// <param name="sourceWidth">The number of columns in the source area.</param>
		/// <param name="sourceHeight">The number of rows in the source area.</param>
		/// <param name="targetLeft">The leftmost column of the destination area.</param>
		/// <param name="targetTop">The topmost row of the destination area.</param>
		/// <param name="sourceChar">The character used to fill the source area.</param>
		/// <param name="sourceForeColor">The foreground color used to fill the source area.</param>
		/// <param name="sourceBackColor">The background color used to fill the source area.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the parameters is less than zero.  
		///  -or-  
		///  <paramref name="sourceLeft" /> or <paramref name="targetLeft" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />.  
		///  -or-  
		///  <paramref name="sourceTop" /> or <paramref name="targetTop" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.  
		///  -or-  
		///  <paramref name="sourceTop" /> + <paramref name="sourceHeight" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.  
		///  -or-  
		///  <paramref name="sourceLeft" /> + <paramref name="sourceWidth" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />.</exception>
		/// <exception cref="T:System.ArgumentException">One or both of the color parameters is not a member of the <see cref="T:System.ConsoleColor" /> enumeration.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B34 RID: 2868 RVA: 0x00023910 File Offset: 0x00021B10
		[SecuritySafeCritical]
		public unsafe static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			if (sourceForeColor < ConsoleColor.Black || sourceForeColor > ConsoleColor.White)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"), "sourceForeColor");
			}
			if (sourceBackColor < ConsoleColor.Black || sourceBackColor > ConsoleColor.White)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"), "sourceBackColor");
			}
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
			Win32Native.COORD dwSize = bufferInfo.dwSize;
			if (sourceLeft < 0 || sourceLeft > (int)dwSize.X)
			{
				throw new ArgumentOutOfRangeException("sourceLeft", sourceLeft, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceTop < 0 || sourceTop > (int)dwSize.Y)
			{
				throw new ArgumentOutOfRangeException("sourceTop", sourceTop, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceWidth < 0 || sourceWidth > (int)dwSize.X - sourceLeft)
			{
				throw new ArgumentOutOfRangeException("sourceWidth", sourceWidth, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceHeight < 0 || sourceTop > (int)dwSize.Y - sourceHeight)
			{
				throw new ArgumentOutOfRangeException("sourceHeight", sourceHeight, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (targetLeft < 0 || targetLeft > (int)dwSize.X)
			{
				throw new ArgumentOutOfRangeException("targetLeft", targetLeft, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (targetTop < 0 || targetTop > (int)dwSize.Y)
			{
				throw new ArgumentOutOfRangeException("targetTop", targetTop, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceWidth == 0 || sourceHeight == 0)
			{
				return;
			}
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			Win32Native.CHAR_INFO[] array = new Win32Native.CHAR_INFO[sourceWidth * sourceHeight];
			dwSize.X = (short)sourceWidth;
			dwSize.Y = (short)sourceHeight;
			Win32Native.COORD coord = default(Win32Native.COORD);
			Win32Native.SMALL_RECT small_RECT = default(Win32Native.SMALL_RECT);
			small_RECT.Left = (short)sourceLeft;
			small_RECT.Right = (short)(sourceLeft + sourceWidth - 1);
			small_RECT.Top = (short)sourceTop;
			small_RECT.Bottom = (short)(sourceTop + sourceHeight - 1);
			Win32Native.CHAR_INFO[] array2;
			Win32Native.CHAR_INFO* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			bool flag = Win32Native.ReadConsoleOutput(Console.ConsoleOutputHandle, ptr, dwSize, coord, ref small_RECT);
			array2 = null;
			if (!flag)
			{
				__Error.WinIOError();
			}
			Win32Native.COORD coord2 = default(Win32Native.COORD);
			coord2.X = (short)sourceLeft;
			Win32Native.Color color = Console.ConsoleColorToColorAttribute(sourceBackColor, true);
			color |= Console.ConsoleColorToColorAttribute(sourceForeColor, false);
			short num = (short)color;
			for (int i = sourceTop; i < sourceTop + sourceHeight; i++)
			{
				coord2.Y = (short)i;
				int num2;
				if (!Win32Native.FillConsoleOutputCharacter(Console.ConsoleOutputHandle, sourceChar, sourceWidth, coord2, out num2))
				{
					__Error.WinIOError();
				}
				if (!Win32Native.FillConsoleOutputAttribute(Console.ConsoleOutputHandle, num, sourceWidth, coord2, out num2))
				{
					__Error.WinIOError();
				}
			}
			Win32Native.SMALL_RECT small_RECT2 = default(Win32Native.SMALL_RECT);
			small_RECT2.Left = (short)targetLeft;
			small_RECT2.Right = (short)(targetLeft + sourceWidth);
			small_RECT2.Top = (short)targetTop;
			small_RECT2.Bottom = (short)(targetTop + sourceHeight);
			Win32Native.CHAR_INFO* ptr2;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array2[0];
			}
			flag = Win32Native.WriteConsoleOutput(Console.ConsoleOutputHandle, ptr2, dwSize, coord, ref small_RECT2);
			array2 = null;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00023BF0 File Offset: 0x00021DF0
		[SecurityCritical]
		private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo()
		{
			bool flag;
			return Console.GetBufferInfo(true, out flag);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00023C08 File Offset: 0x00021E08
		[SecuritySafeCritical]
		private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo(bool throwOnNoConsole, out bool succeeded)
		{
			succeeded = false;
			IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
			if (!(consoleOutputHandle == Win32Native.INVALID_HANDLE_VALUE))
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO console_SCREEN_BUFFER_INFO;
				if (!Win32Native.GetConsoleScreenBufferInfo(consoleOutputHandle, out console_SCREEN_BUFFER_INFO))
				{
					bool flag = Win32Native.GetConsoleScreenBufferInfo(Win32Native.GetStdHandle(-12), out console_SCREEN_BUFFER_INFO);
					if (!flag)
					{
						flag = Win32Native.GetConsoleScreenBufferInfo(Win32Native.GetStdHandle(-10), out console_SCREEN_BUFFER_INFO);
					}
					if (!flag)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error == 6 && !throwOnNoConsole)
						{
							return default(Win32Native.CONSOLE_SCREEN_BUFFER_INFO);
						}
						__Error.WinIOError(lastWin32Error, null);
					}
				}
				if (!Console._haveReadDefaultColors)
				{
					Console._defaultColors = (byte)(console_SCREEN_BUFFER_INFO.wAttributes & 255);
					Console._haveReadDefaultColors = true;
				}
				succeeded = true;
				return console_SCREEN_BUFFER_INFO;
			}
			if (!throwOnNoConsole)
			{
				return default(Win32Native.CONSOLE_SCREEN_BUFFER_INFO);
			}
			throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
		}

		/// <summary>Gets or sets the height of the buffer area.</summary>
		/// <returns>The current height, in rows, of the buffer area.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than or equal to zero.  
		///  -or-  
		///  The value in a set operation is greater than or equal to <see cref="F:System.Int16.MaxValue" />.  
		///  -or-  
		///  The value in a set operation is less than <see cref="P:System.Console.WindowTop" /> + <see cref="P:System.Console.WindowHeight" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00023CC4 File Offset: 0x00021EC4
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x00023CE2 File Offset: 0x00021EE2
		public static int BufferHeight
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.dwSize.Y;
			}
			set
			{
				Console.SetBufferSize(Console.BufferWidth, value);
			}
		}

		/// <summary>Gets or sets the width of the buffer area.</summary>
		/// <returns>The current width, in columns, of the buffer area.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than or equal to zero.  
		///  -or-  
		///  The value in a set operation is greater than or equal to <see cref="F:System.Int16.MaxValue" />.  
		///  -or-  
		///  The value in a set operation is less than <see cref="P:System.Console.WindowLeft" /> + <see cref="P:System.Console.WindowWidth" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00023CF0 File Offset: 0x00021EF0
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x00023D0E File Offset: 0x00021F0E
		public static int BufferWidth
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.dwSize.X;
			}
			set
			{
				Console.SetBufferSize(value, Console.BufferHeight);
			}
		}

		/// <summary>Sets the height and width of the screen buffer area to the specified values.</summary>
		/// <param name="width">The width of the buffer area measured in columns.</param>
		/// <param name="height">The height of the buffer area measured in rows.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="height" /> or <paramref name="width" /> is less than or equal to zero.  
		/// -or-  
		/// <paramref name="height" /> or <paramref name="width" /> is greater than or equal to <see cref="F:System.Int16.MaxValue" />.  
		/// -or-  
		/// <paramref name="width" /> is less than <see cref="P:System.Console.WindowLeft" /> + <see cref="P:System.Console.WindowWidth" />.  
		/// -or-  
		/// <paramref name="height" /> is less than <see cref="P:System.Console.WindowTop" /> + <see cref="P:System.Console.WindowHeight" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B3B RID: 2875 RVA: 0x00023D1C File Offset: 0x00021F1C
		[SecuritySafeCritical]
		public static void SetBufferSize(int width, int height)
		{
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
			Win32Native.SMALL_RECT srWindow = bufferInfo.srWindow;
			if (width < (int)(srWindow.Right + 1) || width >= 32767)
			{
				throw new ArgumentOutOfRangeException("width", width, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferLessThanWindowSize"));
			}
			if (height < (int)(srWindow.Bottom + 1) || height >= 32767)
			{
				throw new ArgumentOutOfRangeException("height", height, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferLessThanWindowSize"));
			}
			Win32Native.COORD coord = default(Win32Native.COORD);
			coord.X = (short)width;
			coord.Y = (short)height;
			if (!Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, coord))
			{
				__Error.WinIOError();
			}
		}

		/// <summary>Gets or sets the height of the console window area.</summary>
		/// <returns>The height of the console window measured in rows.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is less than or equal to 0.  
		///  -or-  
		///  The value of the <see cref="P:System.Console.WindowHeight" /> property plus the value of the <see cref="P:System.Console.WindowTop" /> property is greater than or equal to <see cref="F:System.Int16.MaxValue" />.  
		///  -or-  
		///  The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00023DCC File Offset: 0x00021FCC
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x00023DF8 File Offset: 0x00021FF8
		public static int WindowHeight
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)(bufferInfo.srWindow.Bottom - bufferInfo.srWindow.Top + 1);
			}
			set
			{
				Console.SetWindowSize(Console.WindowWidth, value);
			}
		}

		/// <summary>Gets or sets the width of the console window.</summary>
		/// <returns>The width of the console window measured in columns.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is less than or equal to 0.  
		///  -or-  
		///  The value of the <see cref="P:System.Console.WindowHeight" /> property plus the value of the <see cref="P:System.Console.WindowTop" /> property is greater than or equal to <see cref="F:System.Int16.MaxValue" />.  
		///  -or-  
		///  The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00023E08 File Offset: 0x00022008
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00023E34 File Offset: 0x00022034
		public static int WindowWidth
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)(bufferInfo.srWindow.Right - bufferInfo.srWindow.Left + 1);
			}
			set
			{
				Console.SetWindowSize(value, Console.WindowHeight);
			}
		}

		/// <summary>Sets the height and width of the console window to the specified values.</summary>
		/// <param name="width">The width of the console window measured in columns.</param>
		/// <param name="height">The height of the console window measured in rows.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="width" /> or <paramref name="height" /> is less than or equal to zero.  
		/// -or-  
		/// <paramref name="width" /> plus <see cref="P:System.Console.WindowLeft" /> or <paramref name="height" /> plus <see cref="P:System.Console.WindowTop" /> is greater than or equal to <see cref="F:System.Int16.MaxValue" />.  
		/// -or-  
		/// <paramref name="width" /> or <paramref name="height" /> is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B40 RID: 2880 RVA: 0x00023E44 File Offset: 0x00022044
		[SecuritySafeCritical]
		public unsafe static void SetWindowSize(int width, int height)
		{
			if (width <= 0)
			{
				throw new ArgumentOutOfRangeException("width", width, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (height <= 0)
			{
				throw new ArgumentOutOfRangeException("height", height, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
			bool flag = false;
			Win32Native.COORD coord = default(Win32Native.COORD);
			coord.X = bufferInfo.dwSize.X;
			coord.Y = bufferInfo.dwSize.Y;
			if ((int)bufferInfo.dwSize.X < (int)bufferInfo.srWindow.Left + width)
			{
				if ((int)bufferInfo.srWindow.Left >= 32767 - width)
				{
					throw new ArgumentOutOfRangeException("width", Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowBufferSize"));
				}
				coord.X = (short)((int)bufferInfo.srWindow.Left + width);
				flag = true;
			}
			if ((int)bufferInfo.dwSize.Y < (int)bufferInfo.srWindow.Top + height)
			{
				if ((int)bufferInfo.srWindow.Top >= 32767 - height)
				{
					throw new ArgumentOutOfRangeException("height", Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowBufferSize"));
				}
				coord.Y = (short)((int)bufferInfo.srWindow.Top + height);
				flag = true;
			}
			if (flag && !Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, coord))
			{
				__Error.WinIOError();
			}
			Win32Native.SMALL_RECT srWindow = bufferInfo.srWindow;
			srWindow.Bottom = (short)((int)srWindow.Top + height - 1);
			srWindow.Right = (short)((int)srWindow.Left + width - 1);
			if (!Win32Native.SetConsoleWindowInfo(Console.ConsoleOutputHandle, true, &srWindow))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (flag)
				{
					Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, bufferInfo.dwSize);
				}
				Win32Native.COORD largestConsoleWindowSize = Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle);
				if (width > (int)largestConsoleWindowSize.X)
				{
					throw new ArgumentOutOfRangeException("width", width, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowSize_Size", new object[] { largestConsoleWindowSize.X }));
				}
				if (height > (int)largestConsoleWindowSize.Y)
				{
					throw new ArgumentOutOfRangeException("height", height, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowSize_Size", new object[] { largestConsoleWindowSize.Y }));
				}
				__Error.WinIOError(lastWin32Error, string.Empty);
			}
		}

		/// <summary>Gets the largest possible number of console window columns, based on the current font and screen resolution.</summary>
		/// <returns>The width of the largest possible console window measured in columns.</returns>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00024084 File Offset: 0x00022284
		public static int LargestWindowWidth
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.COORD largestConsoleWindowSize = Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle);
				return (int)largestConsoleWindowSize.X;
			}
		}

		/// <summary>Gets the largest possible number of console window rows, based on the current font and screen resolution.</summary>
		/// <returns>The height of the largest possible console window measured in rows.</returns>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x000240A4 File Offset: 0x000222A4
		public static int LargestWindowHeight
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.COORD largestConsoleWindowSize = Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle);
				return (int)largestConsoleWindowSize.Y;
			}
		}

		/// <summary>Gets or sets the leftmost position of the console window area relative to the screen buffer.</summary>
		/// <returns>The leftmost console window position measured in columns.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the value to be assigned is less than zero.  
		///  -or-  
		///  As a result of the assignment, <see cref="P:System.Console.WindowLeft" /> plus <see cref="P:System.Console.WindowWidth" /> would exceed <see cref="P:System.Console.BufferWidth" />.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x000240C4 File Offset: 0x000222C4
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x000240E2 File Offset: 0x000222E2
		public static int WindowLeft
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.srWindow.Left;
			}
			set
			{
				Console.SetWindowPosition(value, Console.WindowTop);
			}
		}

		/// <summary>Gets or sets the top position of the console window area relative to the screen buffer.</summary>
		/// <returns>The uppermost console window position measured in rows.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the value to be assigned is less than zero.  
		///  -or-  
		///  As a result of the assignment, <see cref="P:System.Console.WindowTop" /> plus <see cref="P:System.Console.WindowHeight" /> would exceed <see cref="P:System.Console.BufferHeight" />.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x000240F0 File Offset: 0x000222F0
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0002410E File Offset: 0x0002230E
		public static int WindowTop
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.srWindow.Top;
			}
			set
			{
				Console.SetWindowPosition(Console.WindowLeft, value);
			}
		}

		/// <summary>Sets the position of the console window relative to the screen buffer.</summary>
		/// <param name="left">The column position of the upper left  corner of the console window.</param>
		/// <param name="top">The row position of the upper left corner of the console window.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="left" /> or <paramref name="top" /> is less than zero.  
		/// -or-  
		/// <paramref name="left" /> + <see cref="P:System.Console.WindowWidth" /> is greater than <see cref="P:System.Console.BufferWidth" />.  
		/// -or-  
		/// <paramref name="top" /> + <see cref="P:System.Console.WindowHeight" /> is greater than <see cref="P:System.Console.BufferHeight" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B47 RID: 2887 RVA: 0x0002411C File Offset: 0x0002231C
		[SecuritySafeCritical]
		public unsafe static void SetWindowPosition(int left, int top)
		{
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
			Win32Native.SMALL_RECT srWindow = bufferInfo.srWindow;
			int num = left + (int)srWindow.Right - (int)srWindow.Left + 1;
			if (left < 0 || num > (int)bufferInfo.dwSize.X || num < 0)
			{
				throw new ArgumentOutOfRangeException("left", left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowPos"));
			}
			int num2 = top + (int)srWindow.Bottom - (int)srWindow.Top + 1;
			if (top < 0 || num2 > (int)bufferInfo.dwSize.Y || num2 < 0)
			{
				throw new ArgumentOutOfRangeException("top", top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowPos"));
			}
			srWindow.Bottom -= (short)((int)srWindow.Top - top);
			srWindow.Right -= (short)((int)srWindow.Left - left);
			srWindow.Left = (short)left;
			srWindow.Top = (short)top;
			if (!Win32Native.SetConsoleWindowInfo(Console.ConsoleOutputHandle, true, &srWindow))
			{
				__Error.WinIOError();
			}
		}

		/// <summary>Gets or sets the column position of the cursor within the buffer area.</summary>
		/// <returns>The current position, in columns, of the cursor.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.  
		///  -or-  
		///  The value in a set operation is greater than or equal to <see cref="P:System.Console.BufferWidth" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0002421C File Offset: 0x0002241C
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x0002423A File Offset: 0x0002243A
		public static int CursorLeft
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.dwCursorPosition.X;
			}
			set
			{
				Console.SetCursorPosition(value, Console.CursorTop);
			}
		}

		/// <summary>Gets or sets the row position of the cursor within the buffer area.</summary>
		/// <returns>The current position, in rows, of the cursor.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.  
		///  -or-  
		///  The value in a set operation is greater than or equal to <see cref="P:System.Console.BufferHeight" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00024248 File Offset: 0x00022448
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00024266 File Offset: 0x00022466
		public static int CursorTop
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.dwCursorPosition.Y;
			}
			set
			{
				Console.SetCursorPosition(Console.CursorLeft, value);
			}
		}

		/// <summary>Sets the position of the cursor.</summary>
		/// <param name="left">The column position of the cursor. Columns are numbered from left to right starting at 0.</param>
		/// <param name="top">The row position of the cursor. Rows are numbered from top to bottom starting at 0.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="left" /> or <paramref name="top" /> is less than zero.  
		/// -or-  
		/// <paramref name="left" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />.  
		/// -or-  
		/// <paramref name="top" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B4C RID: 2892 RVA: 0x00024274 File Offset: 0x00022474
		[SecuritySafeCritical]
		public static void SetCursorPosition(int left, int top)
		{
			if (left < 0 || left >= 32767)
			{
				throw new ArgumentOutOfRangeException("left", left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (top < 0 || top >= 32767)
			{
				throw new ArgumentOutOfRangeException("top", top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
			if (!Win32Native.SetConsoleCursorPosition(consoleOutputHandle, new Win32Native.COORD
			{
				X = (short)left,
				Y = (short)top
			}))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				if (left < 0 || left >= (int)bufferInfo.dwSize.X)
				{
					throw new ArgumentOutOfRangeException("left", left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
				}
				if (top < 0 || top >= (int)bufferInfo.dwSize.Y)
				{
					throw new ArgumentOutOfRangeException("top", top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
				}
				__Error.WinIOError(lastWin32Error, string.Empty);
			}
		}

		/// <summary>Gets or sets the height of the cursor within a character cell.</summary>
		/// <returns>The size of the cursor expressed as a percentage of the height of a character cell. The property value ranges from 1 to 100.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is less than 1 or greater than 100.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0002437C File Offset: 0x0002257C
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x000243A8 File Offset: 0x000225A8
		public static int CursorSize
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO console_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
				return console_CURSOR_INFO.dwSize;
			}
			[SecuritySafeCritical]
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("value", value, Environment.GetResourceString("ArgumentOutOfRange_CursorSize"));
				}
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO console_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
				console_CURSOR_INFO.dwSize = value;
				if (!Win32Native.SetConsoleCursorInfo(consoleOutputHandle, ref console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the cursor is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the cursor is visible; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00024414 File Offset: 0x00022614
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x00024440 File Offset: 0x00022640
		public static bool CursorVisible
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO console_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
				return console_CURSOR_INFO.bVisible;
			}
			[SecuritySafeCritical]
			set
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO console_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
				console_CURSOR_INFO.bVisible = value;
				if (!Win32Native.SetConsoleCursorInfo(consoleOutputHandle, ref console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
			}
		}

		// Token: 0x06000B51 RID: 2897
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Ansi)]
		private static extern int GetTitleNative(StringHandleOnStack outTitle, out int outTitleLength);

		/// <summary>Gets or sets the title to display in the console title bar.</summary>
		/// <returns>The string to be displayed in the title bar of the console. The maximum length of the title string is 24500 characters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In a get operation, the retrieved title is longer than 24500 characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the specified title is longer than 24500 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the specified title is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00024488 File Offset: 0x00022688
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x000244D0 File Offset: 0x000226D0
		public static string Title
		{
			[SecuritySafeCritical]
			get
			{
				string text = null;
				int num = -1;
				int titleNative = Console.GetTitleNative(JitHelpers.GetStringHandleOnStack(ref text), out num);
				if (titleNative != 0)
				{
					__Error.WinIOError(titleNative, string.Empty);
				}
				if (num > 24500)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ArgumentOutOfRange_ConsoleTitleTooLong"));
				}
				return text;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 24500)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_ConsoleTitleTooLong"));
				}
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				if (!Win32Native.SetConsoleTitle(value))
				{
					__Error.WinIOError();
				}
			}
		}

		/// <summary>Obtains the next character or function key pressed by the user. The pressed key is displayed in the console window.</summary>
		/// <returns>An object that describes the <see cref="T:System.ConsoleKey" /> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo" /> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values, whether one or more Shift, Alt, or Ctrl modifier keys was pressed simultaneously with the console key.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Console.In" /> property is redirected from some stream other than the console.</exception>
		// Token: 0x06000B54 RID: 2900 RVA: 0x00024525 File Offset: 0x00022725
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static ConsoleKeyInfo ReadKey()
		{
			return Console.ReadKey(false);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002452D File Offset: 0x0002272D
		[SecurityCritical]
		private static bool IsAltKeyDown(Win32Native.InputRecord ir)
		{
			return (ir.keyEvent.controlKeyState & 3) != 0;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002453F File Offset: 0x0002273F
		[SecurityCritical]
		private static bool IsKeyDownEvent(Win32Native.InputRecord ir)
		{
			return ir.eventType == 1 && ir.keyEvent.keyDown;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00024558 File Offset: 0x00022758
		[SecurityCritical]
		private static bool IsModKey(Win32Native.InputRecord ir)
		{
			short virtualKeyCode = ir.keyEvent.virtualKeyCode;
			return (virtualKeyCode >= 16 && virtualKeyCode <= 18) || virtualKeyCode == 20 || virtualKeyCode == 144 || virtualKeyCode == 145;
		}

		/// <summary>Obtains the next character or function key pressed by the user. The pressed key is optionally displayed in the console window.</summary>
		/// <param name="intercept">Determines whether to display the pressed key in the console window. <see langword="true" /> to not display the pressed key; otherwise, <see langword="false" />.</param>
		/// <returns>An object that describes the <see cref="T:System.ConsoleKey" /> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo" /> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values, whether one or more Shift, Alt, or Ctrl modifier keys was pressed simultaneously with the console key.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Console.In" /> property is redirected from some stream other than the console.</exception>
		// Token: 0x06000B58 RID: 2904 RVA: 0x00024594 File Offset: 0x00022794
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			int num = -1;
			object readKeySyncObject = Console.ReadKeySyncObject;
			Win32Native.InputRecord cachedInputRecord;
			lock (readKeySyncObject)
			{
				if (Console._cachedInputRecord.eventType == 1)
				{
					cachedInputRecord = Console._cachedInputRecord;
					if (Console._cachedInputRecord.keyEvent.repeatCount == 0)
					{
						Console._cachedInputRecord.eventType = -1;
					}
					else
					{
						Console._cachedInputRecord.keyEvent.repeatCount = Console._cachedInputRecord.keyEvent.repeatCount - 1;
					}
				}
				else
				{
					for (;;)
					{
						bool flag2 = Win32Native.ReadConsoleInput(Console.ConsoleInputHandle, out cachedInputRecord, 1, out num);
						if (!flag2 || num == 0)
						{
							break;
						}
						short virtualKeyCode = cachedInputRecord.keyEvent.virtualKeyCode;
						if ((Console.IsKeyDownEvent(cachedInputRecord) || virtualKeyCode == 18) && (cachedInputRecord.keyEvent.uChar != '\0' || !Console.IsModKey(cachedInputRecord)))
						{
							ConsoleKey consoleKey = (ConsoleKey)virtualKeyCode;
							if (!Console.IsAltKeyDown(cachedInputRecord) || ((consoleKey < ConsoleKey.NumPad0 || consoleKey > ConsoleKey.NumPad9) && consoleKey != ConsoleKey.Clear && consoleKey != ConsoleKey.Insert && (consoleKey < ConsoleKey.PageUp || consoleKey > ConsoleKey.DownArrow)))
							{
								goto IL_F0;
							}
						}
					}
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConsoleReadKeyOnFile"));
					IL_F0:
					if (cachedInputRecord.keyEvent.repeatCount > 1)
					{
						cachedInputRecord.keyEvent.repeatCount = cachedInputRecord.keyEvent.repeatCount - 1;
						Console._cachedInputRecord = cachedInputRecord;
					}
				}
			}
			Console.ControlKeyState controlKeyState = (Console.ControlKeyState)cachedInputRecord.keyEvent.controlKeyState;
			bool flag3 = (controlKeyState & Console.ControlKeyState.ShiftPressed) > (Console.ControlKeyState)0;
			bool flag4 = (controlKeyState & (Console.ControlKeyState.RightAltPressed | Console.ControlKeyState.LeftAltPressed)) > (Console.ControlKeyState)0;
			bool flag5 = (controlKeyState & (Console.ControlKeyState.RightCtrlPressed | Console.ControlKeyState.LeftCtrlPressed)) > (Console.ControlKeyState)0;
			ConsoleKeyInfo consoleKeyInfo = new ConsoleKeyInfo(cachedInputRecord.keyEvent.uChar, (ConsoleKey)cachedInputRecord.keyEvent.virtualKeyCode, flag3, flag4, flag5);
			if (!intercept)
			{
				Console.Write(cachedInputRecord.keyEvent.uChar);
			}
			return consoleKeyInfo;
		}

		/// <summary>Gets a value indicating whether a key press is available in the input stream.</summary>
		/// <returns>
		///   <see langword="true" /> if a key press is available; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.InvalidOperationException">Standard input is redirected to a file instead of the keyboard.</exception>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00024740 File Offset: 0x00022940
		public static bool KeyAvailable
		{
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._cachedInputRecord.eventType == 1)
				{
					return true;
				}
				Win32Native.InputRecord inputRecord = default(Win32Native.InputRecord);
				int num = 0;
				for (;;)
				{
					if (!Win32Native.PeekConsoleInput(Console.ConsoleInputHandle, out inputRecord, 1, out num))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error == 6)
						{
							break;
						}
						__Error.WinIOError(lastWin32Error, "stdin");
					}
					if (num == 0)
					{
						return false;
					}
					if (Console.IsKeyDownEvent(inputRecord) && !Console.IsModKey(inputRecord))
					{
						return true;
					}
					if (!Win32Native.ReadConsoleInput(Console.ConsoleInputHandle, out inputRecord, 1, out num))
					{
						__Error.WinIOError();
					}
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConsoleKeyAvailableOnFile"));
			}
		}

		/// <summary>Gets a value indicating whether the NUM LOCK keyboard toggle is turned on or turned off.</summary>
		/// <returns>
		///   <see langword="true" /> if NUM LOCK is turned on; <see langword="false" /> if NUM LOCK is turned off.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x000247D0 File Offset: 0x000229D0
		public static bool NumberLock
		{
			[SecuritySafeCritical]
			get
			{
				short keyState = Win32Native.GetKeyState(144);
				return (keyState & 1) == 1;
			}
		}

		/// <summary>Gets a value indicating whether the CAPS LOCK keyboard toggle is turned on or turned off.</summary>
		/// <returns>
		///   <see langword="true" /> if CAPS LOCK is turned on; <see langword="false" /> if CAPS LOCK is turned off.</returns>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x000247F0 File Offset: 0x000229F0
		public static bool CapsLock
		{
			[SecuritySafeCritical]
			get
			{
				short keyState = Win32Native.GetKeyState(20);
				return (keyState & 1) == 1;
			}
		}

		/// <summary>Gets or sets a value indicating whether the combination of the <see cref="F:System.ConsoleModifiers.Control" /> modifier key and <see cref="F:System.ConsoleKey.C" /> console key (Ctrl+C) is treated as ordinary input or as an interruption that is handled by the operating system.</summary>
		/// <returns>
		///   <see langword="true" /> if Ctrl+C is treated as ordinary input; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.IO.IOException">Unable to get or set the input mode of the console input buffer.</exception>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0002480C File Offset: 0x00022A0C
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x00024858 File Offset: 0x00022A58
		public static bool TreatControlCAsInput
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr consoleInputHandle = Console.ConsoleInputHandle;
				if (consoleInputHandle == Win32Native.INVALID_HANDLE_VALUE)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
				}
				int num = 0;
				if (!Win32Native.GetConsoleMode(consoleInputHandle, out num))
				{
					__Error.WinIOError();
				}
				return (num & 1) == 0;
			}
			[SecuritySafeCritical]
			set
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				IntPtr consoleInputHandle = Console.ConsoleInputHandle;
				if (consoleInputHandle == Win32Native.INVALID_HANDLE_VALUE)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
				}
				int num = 0;
				bool consoleMode = Win32Native.GetConsoleMode(consoleInputHandle, out num);
				if (value)
				{
					num &= -2;
				}
				else
				{
					num |= 1;
				}
				if (!Win32Native.SetConsoleMode(consoleInputHandle, num))
				{
					__Error.WinIOError();
				}
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000248BC File Offset: 0x00022ABC
		private static bool BreakEvent(int controlType)
		{
			if (controlType != 0 && controlType != 1)
			{
				return false;
			}
			ConsoleCancelEventHandler cancelCallbacks = Console._cancelCallbacks;
			if (cancelCallbacks == null)
			{
				return false;
			}
			ConsoleSpecialKey consoleSpecialKey = ((controlType == 0) ? ConsoleSpecialKey.ControlC : ConsoleSpecialKey.ControlBreak);
			Console.ControlCDelegateData controlCDelegateData = new Console.ControlCDelegateData(consoleSpecialKey, cancelCallbacks);
			WaitCallback waitCallback = new WaitCallback(Console.ControlCDelegate);
			if (!ThreadPool.QueueUserWorkItem(waitCallback, controlCDelegateData))
			{
				return false;
			}
			TimeSpan timeSpan = new TimeSpan(0, 0, 30);
			controlCDelegateData.CompletionEvent.WaitOne(timeSpan, false);
			if (!controlCDelegateData.DelegateStarted)
			{
				return false;
			}
			controlCDelegateData.CompletionEvent.WaitOne();
			controlCDelegateData.CompletionEvent.Close();
			return controlCDelegateData.Cancel;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00024948 File Offset: 0x00022B48
		private static void ControlCDelegate(object data)
		{
			Console.ControlCDelegateData controlCDelegateData = (Console.ControlCDelegateData)data;
			try
			{
				controlCDelegateData.DelegateStarted = true;
				ConsoleCancelEventArgs consoleCancelEventArgs = new ConsoleCancelEventArgs(controlCDelegateData.ControlKey);
				controlCDelegateData.CancelCallbacks(null, consoleCancelEventArgs);
				controlCDelegateData.Cancel = consoleCancelEventArgs.Cancel;
			}
			finally
			{
				controlCDelegateData.CompletionEvent.Set();
			}
		}

		/// <summary>Occurs when the <see cref="F:System.ConsoleModifiers.Control" /> modifier key (Ctrl) and either the <see cref="F:System.ConsoleKey.C" /> console key (C) or the Break key are pressed simultaneously (Ctrl+C or Ctrl+Break).</summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000B60 RID: 2912 RVA: 0x000249A8 File Offset: 0x00022BA8
		// (remove) Token: 0x06000B61 RID: 2913 RVA: 0x00024A28 File Offset: 0x00022C28
		public static event ConsoleCancelEventHandler CancelKeyPress
		{
			[SecuritySafeCritical]
			add
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				object internalSyncObject = Console.InternalSyncObject;
				lock (internalSyncObject)
				{
					Console._cancelCallbacks = (ConsoleCancelEventHandler)Delegate.Combine(Console._cancelCallbacks, value);
					if (Console._hooker == null)
					{
						Console._hooker = new Console.ControlCHooker();
						Console._hooker.Hook();
					}
				}
			}
			[SecuritySafeCritical]
			remove
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				object internalSyncObject = Console.InternalSyncObject;
				lock (internalSyncObject)
				{
					Console._cancelCallbacks = (ConsoleCancelEventHandler)Delegate.Remove(Console._cancelCallbacks, value);
					if (Console._hooker != null && Console._cancelCallbacks == null)
					{
						Console._hooker.Unhook();
					}
				}
			}
		}

		/// <summary>Acquires the standard error stream.</summary>
		/// <returns>The standard error stream.</returns>
		// Token: 0x06000B62 RID: 2914 RVA: 0x00024AA4 File Offset: 0x00022CA4
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardError()
		{
			return Console.OpenStandardError(256);
		}

		/// <summary>Acquires the standard error stream, which is set to a specified buffer size.</summary>
		/// <param name="bufferSize">The internal stream buffer size.</param>
		/// <returns>The standard error stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero.</exception>
		// Token: 0x06000B63 RID: 2915 RVA: 0x00024AB0 File Offset: 0x00022CB0
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardError(int bufferSize)
		{
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return Console.GetStandardFile(-12, FileAccess.Write, bufferSize);
		}

		/// <summary>Acquires the standard input stream.</summary>
		/// <returns>The standard input stream.</returns>
		// Token: 0x06000B64 RID: 2916 RVA: 0x00024AD4 File Offset: 0x00022CD4
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardInput()
		{
			return Console.OpenStandardInput(256);
		}

		/// <summary>Acquires the standard input stream, which is set to a specified buffer size.</summary>
		/// <param name="bufferSize">The internal stream buffer size.</param>
		/// <returns>The standard input stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero.</exception>
		// Token: 0x06000B65 RID: 2917 RVA: 0x00024AE0 File Offset: 0x00022CE0
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardInput(int bufferSize)
		{
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return Console.GetStandardFile(-10, FileAccess.Read, bufferSize);
		}

		/// <summary>Acquires the standard output stream.</summary>
		/// <returns>The standard output stream.</returns>
		// Token: 0x06000B66 RID: 2918 RVA: 0x00024B04 File Offset: 0x00022D04
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardOutput()
		{
			return Console.OpenStandardOutput(256);
		}

		/// <summary>Acquires the standard output stream, which is set to a specified buffer size.</summary>
		/// <param name="bufferSize">The internal stream buffer size.</param>
		/// <returns>The standard output stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero.</exception>
		// Token: 0x06000B67 RID: 2919 RVA: 0x00024B10 File Offset: 0x00022D10
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardOutput(int bufferSize)
		{
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return Console.GetStandardFile(-11, FileAccess.Write, bufferSize);
		}

		/// <summary>Sets the <see cref="P:System.Console.In" /> property to the specified <see cref="T:System.IO.TextReader" /> object.</summary>
		/// <param name="newIn">A stream that is the new standard input.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newIn" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06000B68 RID: 2920 RVA: 0x00024B34 File Offset: 0x00022D34
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static void SetIn(TextReader newIn)
		{
			if (newIn == null)
			{
				throw new ArgumentNullException("newIn");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			newIn = TextReader.Synchronized(newIn);
			object internalSyncObject = Console.InternalSyncObject;
			lock (internalSyncObject)
			{
				Console._in = newIn;
			}
		}

		/// <summary>Sets the <see cref="P:System.Console.Out" /> property to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="newOut">A stream that is the new standard output.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newOut" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06000B69 RID: 2921 RVA: 0x00024B98 File Offset: 0x00022D98
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static void SetOut(TextWriter newOut)
		{
			if (newOut == null)
			{
				throw new ArgumentNullException("newOut");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			Console._isOutTextWriterRedirected = true;
			newOut = TextWriter.Synchronized(newOut);
			object internalSyncObject = Console.InternalSyncObject;
			lock (internalSyncObject)
			{
				Console._out = newOut;
			}
		}

		/// <summary>Sets the <see cref="P:System.Console.Error" /> property to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="newError">A stream that is the new standard error output.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newError" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06000B6A RID: 2922 RVA: 0x00024C04 File Offset: 0x00022E04
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static void SetError(TextWriter newError)
		{
			if (newError == null)
			{
				throw new ArgumentNullException("newError");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			Console._isErrorTextWriterRedirected = true;
			newError = TextWriter.Synchronized(newError);
			object internalSyncObject = Console.InternalSyncObject;
			lock (internalSyncObject)
			{
				Console._error = newError;
			}
		}

		/// <summary>Reads the next character from the standard input stream.</summary>
		/// <returns>The next character from the input stream, or negative one (-1) if there are currently no more characters to be read.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B6B RID: 2923 RVA: 0x00024C70 File Offset: 0x00022E70
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static int Read()
		{
			return Console.In.Read();
		}

		/// <summary>Reads the next line of characters from the standard input stream.</summary>
		/// <returns>The next line of characters from the input stream, or <see langword="null" /> if no more lines are available.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line of characters is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000B6C RID: 2924 RVA: 0x00024C7C File Offset: 0x00022E7C
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string ReadLine()
		{
			return Console.In.ReadLine();
		}

		/// <summary>Writes the current line terminator to the standard output stream.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B6D RID: 2925 RVA: 0x00024C88 File Offset: 0x00022E88
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine()
		{
			Console.Out.WriteLine();
		}

		/// <summary>Writes the text representation of the specified Boolean value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B6E RID: 2926 RVA: 0x00024C94 File Offset: 0x00022E94
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(bool value)
		{
			Console.Out.WriteLine(value);
		}

		/// <summary>Writes the specified Unicode character, followed by the current line terminator, value to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B6F RID: 2927 RVA: 0x00024CA1 File Offset: 0x00022EA1
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(char value)
		{
			Console.Out.WriteLine(value);
		}

		/// <summary>Writes the specified array of Unicode characters, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="buffer">A Unicode character array.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B70 RID: 2928 RVA: 0x00024CAE File Offset: 0x00022EAE
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(char[] buffer)
		{
			Console.Out.WriteLine(buffer);
		}

		/// <summary>Writes the specified subarray of Unicode characters, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="buffer">An array of Unicode characters.</param>
		/// <param name="index">The starting position in <paramref name="buffer" />.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus <paramref name="count" /> specify a position that is not within <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B71 RID: 2929 RVA: 0x00024CBB File Offset: 0x00022EBB
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(char[] buffer, int index, int count)
		{
			Console.Out.WriteLine(buffer, index, count);
		}

		/// <summary>Writes the text representation of the specified <see cref="T:System.Decimal" /> value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B72 RID: 2930 RVA: 0x00024CCA File Offset: 0x00022ECA
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(decimal value)
		{
			Console.Out.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified double-precision floating-point value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B73 RID: 2931 RVA: 0x00024CD7 File Offset: 0x00022ED7
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(double value)
		{
			Console.Out.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified single-precision floating-point value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B74 RID: 2932 RVA: 0x00024CE4 File Offset: 0x00022EE4
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(float value)
		{
			Console.Out.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit signed integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B75 RID: 2933 RVA: 0x00024CF1 File Offset: 0x00022EF1
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(int value)
		{
			Console.Out.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit unsigned integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B76 RID: 2934 RVA: 0x00024CFE File Offset: 0x00022EFE
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(uint value)
		{
			Console.Out.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit signed integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B77 RID: 2935 RVA: 0x00024D0B File Offset: 0x00022F0B
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(long value)
		{
			Console.Out.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit unsigned integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B78 RID: 2936 RVA: 0x00024D18 File Offset: 0x00022F18
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(ulong value)
		{
			Console.Out.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B79 RID: 2937 RVA: 0x00024D25 File Offset: 0x00022F25
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(object value)
		{
			Console.Out.WriteLine(value);
		}

		/// <summary>Writes the specified string value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B7A RID: 2938 RVA: 0x00024D32 File Offset: 0x00022F32
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string value)
		{
			Console.Out.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">An object to write using <paramref name="format" />.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000B7B RID: 2939 RVA: 0x00024D3F File Offset: 0x00022F3F
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string format, object arg0)
		{
			Console.Out.WriteLine(format, arg0);
		}

		/// <summary>Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />.</param>
		/// <param name="arg1">The second object to write using <paramref name="format" />.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000B7C RID: 2940 RVA: 0x00024D4D File Offset: 0x00022F4D
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string format, object arg0, object arg1)
		{
			Console.Out.WriteLine(format, arg0, arg1);
		}

		/// <summary>Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />.</param>
		/// <param name="arg1">The second object to write using <paramref name="format" />.</param>
		/// <param name="arg2">The third object to write using <paramref name="format" />.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000B7D RID: 2941 RVA: 0x00024D5C File Offset: 0x00022F5C
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			Console.Out.WriteLine(format, arg0, arg1, arg2);
		}

		/// <summary>Writes the text representation of the specified objects and variable-length parameter list, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />.</param>
		/// <param name="arg1">The second object to write using <paramref name="format" />.</param>
		/// <param name="arg2">The third object to write using <paramref name="format" />.</param>
		/// <param name="arg3">The fourth object to write using <paramref name="format" />.</param>
		/// <param name="…">A comma-delimited list of one or more additional objects to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000B7E RID: 2942 RVA: 0x00024D6C File Offset: 0x00022F6C
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int num = argIterator.GetRemainingCount() + 4;
			object[] array = new object[num];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 4; i < num; i++)
			{
				array[i] = TypedReference.ToObject(argIterator.GetNextArg());
			}
			Console.Out.WriteLine(format, array);
		}

		/// <summary>Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg">An array of objects to write using <paramref name="format" />.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="arg" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000B7F RID: 2943 RVA: 0x00024DCB File Offset: 0x00022FCB
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string format, params object[] arg)
		{
			if (arg == null)
			{
				Console.Out.WriteLine(format, null, null);
				return;
			}
			Console.Out.WriteLine(format, arg);
		}

		/// <summary>Writes the text representation of the specified object to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">An object to write using <paramref name="format" />.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000B80 RID: 2944 RVA: 0x00024DEA File Offset: 0x00022FEA
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string format, object arg0)
		{
			Console.Out.Write(format, arg0);
		}

		/// <summary>Writes the text representation of the specified objects to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />.</param>
		/// <param name="arg1">The second object to write using <paramref name="format" />.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000B81 RID: 2945 RVA: 0x00024DF8 File Offset: 0x00022FF8
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string format, object arg0, object arg1)
		{
			Console.Out.Write(format, arg0, arg1);
		}

		/// <summary>Writes the text representation of the specified objects to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />.</param>
		/// <param name="arg1">The second object to write using <paramref name="format" />.</param>
		/// <param name="arg2">The third object to write using <paramref name="format" />.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000B82 RID: 2946 RVA: 0x00024E07 File Offset: 0x00023007
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string format, object arg0, object arg1, object arg2)
		{
			Console.Out.Write(format, arg0, arg1, arg2);
		}

		/// <summary>Writes the text representation of the specified objects and variable-length parameter list to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />.</param>
		/// <param name="arg1">The second object to write using <paramref name="format" />.</param>
		/// <param name="arg2">The third object to write using <paramref name="format" />.</param>
		/// <param name="arg3">The fourth object to write using <paramref name="format" />.</param>
		/// <param name="…">A comma-delimited list of one or more additional objects to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000B83 RID: 2947 RVA: 0x00024E18 File Offset: 0x00023018
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int num = argIterator.GetRemainingCount() + 4;
			object[] array = new object[num];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 4; i < num; i++)
			{
				array[i] = TypedReference.ToObject(argIterator.GetNextArg());
			}
			Console.Out.Write(format, array);
		}

		/// <summary>Writes the text representation of the specified array of objects to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg">An array of objects to write using <paramref name="format" />.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="arg" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000B84 RID: 2948 RVA: 0x00024E77 File Offset: 0x00023077
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string format, params object[] arg)
		{
			if (arg == null)
			{
				Console.Out.Write(format, null, null);
				return;
			}
			Console.Out.Write(format, arg);
		}

		/// <summary>Writes the text representation of the specified Boolean value to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B85 RID: 2949 RVA: 0x00024E96 File Offset: 0x00023096
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(bool value)
		{
			Console.Out.Write(value);
		}

		/// <summary>Writes the specified Unicode character value to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B86 RID: 2950 RVA: 0x00024EA3 File Offset: 0x000230A3
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(char value)
		{
			Console.Out.Write(value);
		}

		/// <summary>Writes the specified array of Unicode characters to the standard output stream.</summary>
		/// <param name="buffer">A Unicode character array.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B87 RID: 2951 RVA: 0x00024EB0 File Offset: 0x000230B0
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(char[] buffer)
		{
			Console.Out.Write(buffer);
		}

		/// <summary>Writes the specified subarray of Unicode characters to the standard output stream.</summary>
		/// <param name="buffer">An array of Unicode characters.</param>
		/// <param name="index">The starting position in <paramref name="buffer" />.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus <paramref name="count" /> specify a position that is not within <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B88 RID: 2952 RVA: 0x00024EBD File Offset: 0x000230BD
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(char[] buffer, int index, int count)
		{
			Console.Out.Write(buffer, index, count);
		}

		/// <summary>Writes the text representation of the specified double-precision floating-point value to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B89 RID: 2953 RVA: 0x00024ECC File Offset: 0x000230CC
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(double value)
		{
			Console.Out.Write(value);
		}

		/// <summary>Writes the text representation of the specified <see cref="T:System.Decimal" /> value to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B8A RID: 2954 RVA: 0x00024ED9 File Offset: 0x000230D9
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(decimal value)
		{
			Console.Out.Write(value);
		}

		/// <summary>Writes the text representation of the specified single-precision floating-point value to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B8B RID: 2955 RVA: 0x00024EE6 File Offset: 0x000230E6
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(float value)
		{
			Console.Out.Write(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit signed integer value to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B8C RID: 2956 RVA: 0x00024EF3 File Offset: 0x000230F3
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(int value)
		{
			Console.Out.Write(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit unsigned integer value to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B8D RID: 2957 RVA: 0x00024F00 File Offset: 0x00023100
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(uint value)
		{
			Console.Out.Write(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit signed integer value to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B8E RID: 2958 RVA: 0x00024F0D File Offset: 0x0002310D
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(long value)
		{
			Console.Out.Write(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit unsigned integer value to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B8F RID: 2959 RVA: 0x00024F1A File Offset: 0x0002311A
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(ulong value)
		{
			Console.Out.Write(value);
		}

		/// <summary>Writes the text representation of the specified object to the standard output stream.</summary>
		/// <param name="value">The value to write, or <see langword="null" />.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B90 RID: 2960 RVA: 0x00024F27 File Offset: 0x00023127
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(object value)
		{
			Console.Out.Write(value);
		}

		/// <summary>Writes the specified string value to the standard output stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06000B91 RID: 2961 RVA: 0x00024F34 File Offset: 0x00023134
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x04000465 RID: 1125
		private const int DefaultConsoleBufferSize = 256;

		// Token: 0x04000466 RID: 1126
		private const short AltVKCode = 18;

		// Token: 0x04000467 RID: 1127
		private const int NumberLockVKCode = 144;

		// Token: 0x04000468 RID: 1128
		private const int CapsLockVKCode = 20;

		// Token: 0x04000469 RID: 1129
		private const int MinBeepFrequency = 37;

		// Token: 0x0400046A RID: 1130
		private const int MaxBeepFrequency = 32767;

		// Token: 0x0400046B RID: 1131
		private const int MaxConsoleTitleLength = 24500;

		// Token: 0x0400046C RID: 1132
		private static readonly UnicodeEncoding StdConUnicodeEncoding = new UnicodeEncoding(false, false);

		// Token: 0x0400046D RID: 1133
		private static volatile TextReader _in;

		// Token: 0x0400046E RID: 1134
		private static volatile TextWriter _out;

		// Token: 0x0400046F RID: 1135
		private static volatile TextWriter _error;

		// Token: 0x04000470 RID: 1136
		private static volatile ConsoleCancelEventHandler _cancelCallbacks;

		// Token: 0x04000471 RID: 1137
		private static volatile Console.ControlCHooker _hooker;

		// Token: 0x04000472 RID: 1138
		[SecurityCritical]
		private static Win32Native.InputRecord _cachedInputRecord;

		// Token: 0x04000473 RID: 1139
		private static volatile bool _haveReadDefaultColors;

		// Token: 0x04000474 RID: 1140
		private static volatile byte _defaultColors;

		// Token: 0x04000475 RID: 1141
		private static volatile bool _isOutTextWriterRedirected = false;

		// Token: 0x04000476 RID: 1142
		private static volatile bool _isErrorTextWriterRedirected = false;

		// Token: 0x04000477 RID: 1143
		private static volatile Encoding _inputEncoding = null;

		// Token: 0x04000478 RID: 1144
		private static volatile Encoding _outputEncoding = null;

		// Token: 0x04000479 RID: 1145
		private static volatile bool _stdInRedirectQueried = false;

		// Token: 0x0400047A RID: 1146
		private static volatile bool _stdOutRedirectQueried = false;

		// Token: 0x0400047B RID: 1147
		private static volatile bool _stdErrRedirectQueried = false;

		// Token: 0x0400047C RID: 1148
		private static bool _isStdInRedirected;

		// Token: 0x0400047D RID: 1149
		private static bool _isStdOutRedirected;

		// Token: 0x0400047E RID: 1150
		private static bool _isStdErrRedirected;

		// Token: 0x0400047F RID: 1151
		private static volatile object s_InternalSyncObject;

		// Token: 0x04000480 RID: 1152
		private static volatile object s_ReadKeySyncObject;

		// Token: 0x04000481 RID: 1153
		private static volatile IntPtr _consoleInputHandle;

		// Token: 0x04000482 RID: 1154
		private static volatile IntPtr _consoleOutputHandle;

		// Token: 0x02000ADA RID: 2778
		[Flags]
		internal enum ControlKeyState
		{
			// Token: 0x04003122 RID: 12578
			RightAltPressed = 1,
			// Token: 0x04003123 RID: 12579
			LeftAltPressed = 2,
			// Token: 0x04003124 RID: 12580
			RightCtrlPressed = 4,
			// Token: 0x04003125 RID: 12581
			LeftCtrlPressed = 8,
			// Token: 0x04003126 RID: 12582
			ShiftPressed = 16,
			// Token: 0x04003127 RID: 12583
			NumLockOn = 32,
			// Token: 0x04003128 RID: 12584
			ScrollLockOn = 64,
			// Token: 0x04003129 RID: 12585
			CapsLockOn = 128,
			// Token: 0x0400312A RID: 12586
			EnhancedKey = 256
		}

		// Token: 0x02000ADB RID: 2779
		internal sealed class ControlCHooker : CriticalFinalizerObject
		{
			// Token: 0x06006A06 RID: 27142 RVA: 0x0016E56B File Offset: 0x0016C76B
			[SecurityCritical]
			internal ControlCHooker()
			{
				this._handler = new Win32Native.ConsoleCtrlHandlerRoutine(Console.BreakEvent);
			}

			// Token: 0x06006A07 RID: 27143 RVA: 0x0016E588 File Offset: 0x0016C788
			~ControlCHooker()
			{
				this.Unhook();
			}

			// Token: 0x06006A08 RID: 27144 RVA: 0x0016E5B4 File Offset: 0x0016C7B4
			[SecuritySafeCritical]
			internal void Hook()
			{
				if (!this._hooked)
				{
					if (!Win32Native.SetConsoleCtrlHandler(this._handler, true))
					{
						__Error.WinIOError();
					}
					this._hooked = true;
				}
			}

			// Token: 0x06006A09 RID: 27145 RVA: 0x0016E5E8 File Offset: 0x0016C7E8
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			internal void Unhook()
			{
				if (this._hooked)
				{
					if (!Win32Native.SetConsoleCtrlHandler(this._handler, false))
					{
						__Error.WinIOError();
					}
					this._hooked = false;
				}
			}

			// Token: 0x0400312B RID: 12587
			private bool _hooked;

			// Token: 0x0400312C RID: 12588
			[SecurityCritical]
			private Win32Native.ConsoleCtrlHandlerRoutine _handler;
		}

		// Token: 0x02000ADC RID: 2780
		private sealed class ControlCDelegateData
		{
			// Token: 0x06006A0A RID: 27146 RVA: 0x0016E619 File Offset: 0x0016C819
			internal ControlCDelegateData(ConsoleSpecialKey controlKey, ConsoleCancelEventHandler cancelCallbacks)
			{
				this.ControlKey = controlKey;
				this.CancelCallbacks = cancelCallbacks;
				this.CompletionEvent = new ManualResetEvent(false);
			}

			// Token: 0x0400312D RID: 12589
			internal ConsoleSpecialKey ControlKey;

			// Token: 0x0400312E RID: 12590
			internal bool Cancel;

			// Token: 0x0400312F RID: 12591
			internal bool DelegateStarted;

			// Token: 0x04003130 RID: 12592
			internal ManualResetEvent CompletionEvent;

			// Token: 0x04003131 RID: 12593
			internal ConsoleCancelEventHandler CancelCallbacks;
		}
	}
}
