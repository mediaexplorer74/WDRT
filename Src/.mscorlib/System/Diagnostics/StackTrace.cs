using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Represents a stack trace, which is an ordered collection of one or more stack frames.</summary>
	// Token: 0x020003F6 RID: 1014
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	[Serializable]
	public class StackTrace
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame.</summary>
		// Token: 0x06003383 RID: 13187 RVA: 0x000C70B6 File Offset: 0x000C52B6
		public StackTrace()
		{
			this.m_iNumOfFrames = 0;
			this.m_iMethodsToSkip = 0;
			this.CaptureStackTrace(0, false, null, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, optionally capturing source information.</summary>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number; otherwise, <see langword="false" />.</param>
		// Token: 0x06003384 RID: 13188 RVA: 0x000C70D6 File Offset: 0x000C52D6
		public StackTrace(bool fNeedFileInfo)
		{
			this.m_iNumOfFrames = 0;
			this.m_iMethodsToSkip = 0;
			this.CaptureStackTrace(0, fNeedFileInfo, null, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, skipping the specified number of frames.</summary>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative.</exception>
		// Token: 0x06003385 RID: 13189 RVA: 0x000C70F6 File Offset: 0x000C52F6
		public StackTrace(int skipFrames)
		{
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("skipFrames", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_iNumOfFrames = 0;
			this.m_iMethodsToSkip = 0;
			this.CaptureStackTrace(skipFrames, false, null, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, skipping the specified number of frames and optionally capturing source information.</summary>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace.</param>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative.</exception>
		// Token: 0x06003386 RID: 13190 RVA: 0x000C712F File Offset: 0x000C532F
		public StackTrace(int skipFrames, bool fNeedFileInfo)
		{
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("skipFrames", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_iNumOfFrames = 0;
			this.m_iMethodsToSkip = 0;
			this.CaptureStackTrace(skipFrames, fNeedFileInfo, null, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object.</summary>
		/// <param name="e">The exception object from which to construct the stack trace.</param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is <see langword="null" />.</exception>
		// Token: 0x06003387 RID: 13191 RVA: 0x000C7168 File Offset: 0x000C5368
		public StackTrace(Exception e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			this.m_iNumOfFrames = 0;
			this.m_iMethodsToSkip = 0;
			this.CaptureStackTrace(0, false, null, e);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class, using the provided exception object and optionally capturing source information.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is <see langword="null" />.</exception>
		// Token: 0x06003388 RID: 13192 RVA: 0x000C7196 File Offset: 0x000C5396
		public StackTrace(Exception e, bool fNeedFileInfo)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			this.m_iNumOfFrames = 0;
			this.m_iMethodsToSkip = 0;
			this.CaptureStackTrace(0, fNeedFileInfo, null, e);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object and skipping the specified number of frames.</summary>
		/// <param name="e">The exception object from which to construct the stack trace.</param>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace.</param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative.</exception>
		// Token: 0x06003389 RID: 13193 RVA: 0x000C71C4 File Offset: 0x000C53C4
		public StackTrace(Exception e, int skipFrames)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("skipFrames", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_iNumOfFrames = 0;
			this.m_iMethodsToSkip = 0;
			this.CaptureStackTrace(skipFrames, false, null, e);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object, skipping the specified number of frames and optionally capturing source information.</summary>
		/// <param name="e">The exception object from which to construct the stack trace.</param>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace.</param>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative.</exception>
		// Token: 0x0600338A RID: 13194 RVA: 0x000C7218 File Offset: 0x000C5418
		public StackTrace(Exception e, int skipFrames, bool fNeedFileInfo)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("skipFrames", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_iNumOfFrames = 0;
			this.m_iMethodsToSkip = 0;
			this.CaptureStackTrace(skipFrames, fNeedFileInfo, null, e);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class that contains a single frame.</summary>
		/// <param name="frame">The frame that the <see cref="T:System.Diagnostics.StackTrace" /> object should contain.</param>
		// Token: 0x0600338B RID: 13195 RVA: 0x000C726A File Offset: 0x000C546A
		public StackTrace(StackFrame frame)
		{
			this.frames = new StackFrame[1];
			this.frames[0] = frame;
			this.m_iMethodsToSkip = 0;
			this.m_iNumOfFrames = 1;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class for a specific thread, optionally capturing source information.  
		///  Do not use this constructor overload.</summary>
		/// <param name="targetThread">The thread whose stack trace is requested.</param>
		/// <param name="needFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread <paramref name="targetThread" /> is not suspended.</exception>
		// Token: 0x0600338C RID: 13196 RVA: 0x000C7295 File Offset: 0x000C5495
		[Obsolete("This constructor has been deprecated.  Please use a constructor that does not require a Thread parameter.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public StackTrace(Thread targetThread, bool needFileInfo)
		{
			this.m_iNumOfFrames = 0;
			this.m_iMethodsToSkip = 0;
			this.CaptureStackTrace(0, needFileInfo, targetThread, null);
		}

		// Token: 0x0600338D RID: 13197
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetStackFramesInternal(StackFrameHelper sfh, int iSkip, bool fNeedFileInfo, Exception e);

		// Token: 0x0600338E RID: 13198 RVA: 0x000C72B8 File Offset: 0x000C54B8
		internal static int CalculateFramesToSkip(StackFrameHelper StackF, int iNumFrames)
		{
			int num = 0;
			string text = "System.Diagnostics";
			for (int i = 0; i < iNumFrames; i++)
			{
				MethodBase methodBase = StackF.GetMethodBase(i);
				if (methodBase != null)
				{
					Type declaringType = methodBase.DeclaringType;
					if (declaringType == null)
					{
						break;
					}
					string @namespace = declaringType.Namespace;
					if (@namespace == null || string.Compare(@namespace, text, StringComparison.Ordinal) != 0)
					{
						break;
					}
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000C731C File Offset: 0x000C551C
		private void CaptureStackTrace(int iSkip, bool fNeedFileInfo, Thread targetThread, Exception e)
		{
			this.m_iMethodsToSkip += iSkip;
			using (StackFrameHelper stackFrameHelper = new StackFrameHelper(targetThread))
			{
				stackFrameHelper.InitializeSourceInfo(0, fNeedFileInfo, e);
				this.m_iNumOfFrames = stackFrameHelper.GetNumberOfFrames();
				if (this.m_iMethodsToSkip > this.m_iNumOfFrames)
				{
					this.m_iMethodsToSkip = this.m_iNumOfFrames;
				}
				if (this.m_iNumOfFrames != 0)
				{
					this.frames = new StackFrame[this.m_iNumOfFrames];
					for (int i = 0; i < this.m_iNumOfFrames; i++)
					{
						bool flag = true;
						bool flag2 = true;
						StackFrame stackFrame = new StackFrame(flag, flag2);
						stackFrame.SetMethodBase(stackFrameHelper.GetMethodBase(i));
						stackFrame.SetOffset(stackFrameHelper.GetOffset(i));
						stackFrame.SetILOffset(stackFrameHelper.GetILOffset(i));
						stackFrame.SetIsLastFrameFromForeignExceptionStackTrace(stackFrameHelper.IsLastFrameFromForeignExceptionStackTrace(i));
						if (fNeedFileInfo)
						{
							stackFrame.SetFileName(stackFrameHelper.GetFilename(i));
							stackFrame.SetLineNumber(stackFrameHelper.GetLineNumber(i));
							stackFrame.SetColumnNumber(stackFrameHelper.GetColumnNumber(i));
						}
						this.frames[i] = stackFrame;
					}
					if (e == null)
					{
						this.m_iMethodsToSkip += StackTrace.CalculateFramesToSkip(stackFrameHelper, this.m_iNumOfFrames);
					}
					this.m_iNumOfFrames -= this.m_iMethodsToSkip;
					if (this.m_iNumOfFrames < 0)
					{
						this.m_iNumOfFrames = 0;
					}
				}
				else
				{
					this.frames = null;
				}
			}
		}

		/// <summary>Gets the number of frames in the stack trace.</summary>
		/// <returns>The number of frames in the stack trace.</returns>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x000C7490 File Offset: 0x000C5690
		public virtual int FrameCount
		{
			get
			{
				return this.m_iNumOfFrames;
			}
		}

		/// <summary>Gets the specified stack frame.</summary>
		/// <param name="index">The index of the stack frame requested.</param>
		/// <returns>The specified stack frame.</returns>
		// Token: 0x06003391 RID: 13201 RVA: 0x000C7498 File Offset: 0x000C5698
		public virtual StackFrame GetFrame(int index)
		{
			if (this.frames != null && index < this.m_iNumOfFrames && index >= 0)
			{
				return this.frames[index + this.m_iMethodsToSkip];
			}
			return null;
		}

		/// <summary>Returns a copy of all stack frames in the current stack trace.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.StackFrame" /> representing the function calls in the stack trace.</returns>
		// Token: 0x06003392 RID: 13202 RVA: 0x000C74C0 File Offset: 0x000C56C0
		[ComVisible(false)]
		public virtual StackFrame[] GetFrames()
		{
			if (this.frames == null || this.m_iNumOfFrames <= 0)
			{
				return null;
			}
			StackFrame[] array = new StackFrame[this.m_iNumOfFrames];
			Array.Copy(this.frames, this.m_iMethodsToSkip, array, 0, this.m_iNumOfFrames);
			return array;
		}

		/// <summary>Builds a readable representation of the stack trace.</summary>
		/// <returns>A readable representation of the stack trace.</returns>
		// Token: 0x06003393 RID: 13203 RVA: 0x000C7506 File Offset: 0x000C5706
		public override string ToString()
		{
			return this.ToString(StackTrace.TraceFormat.TrailingNewLine);
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000C7510 File Offset: 0x000C5710
		internal string ToString(StackTrace.TraceFormat traceFormat)
		{
			bool flag = true;
			string text = "at";
			string text2 = "in {0}:line {1}";
			if (traceFormat != StackTrace.TraceFormat.NoResourceLookup)
			{
				text = Environment.GetResourceString("Word_At");
				text2 = Environment.GetResourceString("StackTrace_InFileLineNumber");
			}
			bool flag2 = true;
			StringBuilder stringBuilder = new StringBuilder(255);
			for (int i = 0; i < this.m_iNumOfFrames; i++)
			{
				StackFrame frame = this.GetFrame(i);
				MethodBase method = frame.GetMethod();
				if (method != null)
				{
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						stringBuilder.Append(Environment.NewLine);
					}
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "   {0} ", text);
					Type declaringType = method.DeclaringType;
					if (declaringType != null)
					{
						stringBuilder.Append(declaringType.FullName.Replace('+', '.'));
						stringBuilder.Append(".");
					}
					stringBuilder.Append(method.Name);
					if (method is MethodInfo && ((MethodInfo)method).IsGenericMethod)
					{
						Type[] genericArguments = ((MethodInfo)method).GetGenericArguments();
						stringBuilder.Append("[");
						int j = 0;
						bool flag3 = true;
						while (j < genericArguments.Length)
						{
							if (!flag3)
							{
								stringBuilder.Append(",");
							}
							else
							{
								flag3 = false;
							}
							stringBuilder.Append(genericArguments[j].Name);
							j++;
						}
						stringBuilder.Append("]");
					}
					stringBuilder.Append("(");
					ParameterInfo[] parameters = method.GetParameters();
					bool flag4 = true;
					for (int k = 0; k < parameters.Length; k++)
					{
						if (!flag4)
						{
							stringBuilder.Append(", ");
						}
						else
						{
							flag4 = false;
						}
						string text3 = "<UnknownType>";
						if (parameters[k].ParameterType != null)
						{
							text3 = parameters[k].ParameterType.Name;
						}
						stringBuilder.Append(text3 + " " + parameters[k].Name);
					}
					stringBuilder.Append(")");
					if (flag && frame.GetILOffset() != -1)
					{
						string text4 = null;
						try
						{
							text4 = frame.GetFileName();
						}
						catch (NotSupportedException)
						{
							flag = false;
						}
						catch (SecurityException)
						{
							flag = false;
						}
						if (text4 != null)
						{
							stringBuilder.Append(' ');
							stringBuilder.AppendFormat(CultureInfo.InvariantCulture, text2, text4, frame.GetFileLineNumber());
						}
					}
					if (frame.GetIsLastFrameFromForeignExceptionStackTrace())
					{
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(Environment.GetResourceString("Exception_EndStackTraceFromPreviousThrow"));
					}
				}
			}
			if (traceFormat == StackTrace.TraceFormat.TrailingNewLine)
			{
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x000C77BC File Offset: 0x000C59BC
		private static string GetManagedStackTraceStringHelper(bool fNeedFileInfo)
		{
			StackTrace stackTrace = new StackTrace(0, fNeedFileInfo);
			return stackTrace.ToString();
		}

		// Token: 0x040016E9 RID: 5865
		private StackFrame[] frames;

		// Token: 0x040016EA RID: 5866
		private int m_iNumOfFrames;

		/// <summary>Defines the default for the number of methods to omit from the stack trace. This field is constant.</summary>
		// Token: 0x040016EB RID: 5867
		public const int METHODS_TO_SKIP = 0;

		// Token: 0x040016EC RID: 5868
		private int m_iMethodsToSkip;

		// Token: 0x02000B7E RID: 2942
		internal enum TraceFormat
		{
			// Token: 0x040034DC RID: 13532
			Normal,
			// Token: 0x040034DD RID: 13533
			TrailingNewLine,
			// Token: 0x040034DE RID: 13534
			NoResourceLookup
		}
	}
}
