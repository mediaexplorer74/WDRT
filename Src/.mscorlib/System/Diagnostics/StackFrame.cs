using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides information about a <see cref="T:System.Diagnostics.StackFrame" />, which represents a function call on the call stack for the current thread.</summary>
	// Token: 0x020003F7 RID: 1015
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	[Serializable]
	public class StackFrame
	{
		// Token: 0x06003396 RID: 13206 RVA: 0x000C77D7 File Offset: 0x000C59D7
		internal void InitMembers()
		{
			this.method = null;
			this.offset = -1;
			this.ILOffset = -1;
			this.strFileName = null;
			this.iLineNumber = 0;
			this.iColumnNumber = 0;
			this.fIsLastFrameFromForeignExceptionStackTrace = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class.</summary>
		// Token: 0x06003397 RID: 13207 RVA: 0x000C780A File Offset: 0x000C5A0A
		public StackFrame()
		{
			this.InitMembers();
			this.BuildStackFrame(0, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class, optionally capturing source information.</summary>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number of the stack frame; otherwise, <see langword="false" />.</param>
		// Token: 0x06003398 RID: 13208 RVA: 0x000C7820 File Offset: 0x000C5A20
		public StackFrame(bool fNeedFileInfo)
		{
			this.InitMembers();
			this.BuildStackFrame(0, fNeedFileInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip.</param>
		// Token: 0x06003399 RID: 13209 RVA: 0x000C7836 File Offset: 0x000C5A36
		public StackFrame(int skipFrames)
		{
			this.InitMembers();
			this.BuildStackFrame(skipFrames, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame, optionally capturing source information.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip.</param>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number of the stack frame; otherwise, <see langword="false" />.</param>
		// Token: 0x0600339A RID: 13210 RVA: 0x000C784C File Offset: 0x000C5A4C
		public StackFrame(int skipFrames, bool fNeedFileInfo)
		{
			this.InitMembers();
			this.BuildStackFrame(skipFrames, fNeedFileInfo);
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000C7862 File Offset: 0x000C5A62
		internal StackFrame(bool DummyFlag1, bool DummyFlag2)
		{
			this.InitMembers();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that contains only the given file name and line number.</summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="lineNumber">The line number in the specified file.</param>
		// Token: 0x0600339C RID: 13212 RVA: 0x000C7870 File Offset: 0x000C5A70
		public StackFrame(string fileName, int lineNumber)
		{
			this.InitMembers();
			this.BuildStackFrame(0, false);
			this.strFileName = fileName;
			this.iLineNumber = lineNumber;
			this.iColumnNumber = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that contains only the given file name, line number, and column number.</summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="lineNumber">The line number in the specified file.</param>
		/// <param name="colNumber">The column number in the specified file.</param>
		// Token: 0x0600339D RID: 13213 RVA: 0x000C789B File Offset: 0x000C5A9B
		public StackFrame(string fileName, int lineNumber, int colNumber)
		{
			this.InitMembers();
			this.BuildStackFrame(0, false);
			this.strFileName = fileName;
			this.iLineNumber = lineNumber;
			this.iColumnNumber = colNumber;
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000C78C6 File Offset: 0x000C5AC6
		internal virtual void SetMethodBase(MethodBase mb)
		{
			this.method = mb;
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000C78CF File Offset: 0x000C5ACF
		internal virtual void SetOffset(int iOffset)
		{
			this.offset = iOffset;
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000C78D8 File Offset: 0x000C5AD8
		internal virtual void SetILOffset(int iOffset)
		{
			this.ILOffset = iOffset;
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000C78E1 File Offset: 0x000C5AE1
		internal virtual void SetFileName(string strFName)
		{
			this.strFileName = strFName;
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000C78EA File Offset: 0x000C5AEA
		internal virtual void SetLineNumber(int iLine)
		{
			this.iLineNumber = iLine;
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000C78F3 File Offset: 0x000C5AF3
		internal virtual void SetColumnNumber(int iCol)
		{
			this.iColumnNumber = iCol;
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000C78FC File Offset: 0x000C5AFC
		internal virtual void SetIsLastFrameFromForeignExceptionStackTrace(bool fIsLastFrame)
		{
			this.fIsLastFrameFromForeignExceptionStackTrace = fIsLastFrame;
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000C7905 File Offset: 0x000C5B05
		internal virtual bool GetIsLastFrameFromForeignExceptionStackTrace()
		{
			return this.fIsLastFrameFromForeignExceptionStackTrace;
		}

		/// <summary>Gets the method in which the frame is executing.</summary>
		/// <returns>The method in which the frame is executing.</returns>
		// Token: 0x060033A6 RID: 13222 RVA: 0x000C790D File Offset: 0x000C5B0D
		public virtual MethodBase GetMethod()
		{
			return this.method;
		}

		/// <summary>Gets the offset from the start of the native just-in-time (JIT)-compiled code for the method that is being executed. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class.</summary>
		/// <returns>The offset from the start of the JIT-compiled code for the method that is being executed.</returns>
		// Token: 0x060033A7 RID: 13223 RVA: 0x000C7915 File Offset: 0x000C5B15
		public virtual int GetNativeOffset()
		{
			return this.offset;
		}

		/// <summary>Gets the offset from the start of the Microsoft intermediate language (MSIL) code for the method that is executing. This offset might be an approximation depending on whether or not the just-in-time (JIT) compiler is generating debugging code. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" />.</summary>
		/// <returns>The offset from the start of the MSIL code for the method that is executing.</returns>
		// Token: 0x060033A8 RID: 13224 RVA: 0x000C791D File Offset: 0x000C5B1D
		public virtual int GetILOffset()
		{
			return this.ILOffset;
		}

		/// <summary>Gets the file name that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file name, or <see langword="null" /> if the file name cannot be determined.</returns>
		// Token: 0x060033A9 RID: 13225 RVA: 0x000C7928 File Offset: 0x000C5B28
		[SecuritySafeCritical]
		public virtual string GetFileName()
		{
			if (this.strFileName != null)
			{
				new FileIOPermission(PermissionState.None)
				{
					AllFiles = FileIOPermissionAccess.PathDiscovery
				}.Demand();
			}
			return this.strFileName;
		}

		/// <summary>Gets the line number in the file that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file line number, or 0 (zero) if the file line number cannot be determined.</returns>
		// Token: 0x060033AA RID: 13226 RVA: 0x000C7957 File Offset: 0x000C5B57
		public virtual int GetFileLineNumber()
		{
			return this.iLineNumber;
		}

		/// <summary>Gets the column number in the file that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file column number, or 0 (zero) if the file column number cannot be determined.</returns>
		// Token: 0x060033AB RID: 13227 RVA: 0x000C795F File Offset: 0x000C5B5F
		public virtual int GetFileColumnNumber()
		{
			return this.iColumnNumber;
		}

		/// <summary>Builds a readable representation of the stack trace.</summary>
		/// <returns>A readable representation of the stack trace.</returns>
		// Token: 0x060033AC RID: 13228 RVA: 0x000C7968 File Offset: 0x000C5B68
		[SecuritySafeCritical]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			if (this.method != null)
			{
				stringBuilder.Append(this.method.Name);
				if (this.method is MethodInfo && ((MethodInfo)this.method).IsGenericMethod)
				{
					Type[] genericArguments = ((MethodInfo)this.method).GetGenericArguments();
					stringBuilder.Append("<");
					int i = 0;
					bool flag = true;
					while (i < genericArguments.Length)
					{
						if (!flag)
						{
							stringBuilder.Append(",");
						}
						else
						{
							flag = false;
						}
						stringBuilder.Append(genericArguments[i].Name);
						i++;
					}
					stringBuilder.Append(">");
				}
				stringBuilder.Append(" at offset ");
				if (this.offset == -1)
				{
					stringBuilder.Append("<offset unknown>");
				}
				else
				{
					stringBuilder.Append(this.offset);
				}
				stringBuilder.Append(" in file:line:column ");
				bool flag2 = this.strFileName != null;
				if (flag2)
				{
					try
					{
						new FileIOPermission(PermissionState.None)
						{
							AllFiles = FileIOPermissionAccess.PathDiscovery
						}.Demand();
					}
					catch (SecurityException)
					{
						flag2 = false;
					}
				}
				if (!flag2)
				{
					stringBuilder.Append("<filename unknown>");
				}
				else
				{
					stringBuilder.Append(this.strFileName);
				}
				stringBuilder.Append(":");
				stringBuilder.Append(this.iLineNumber);
				stringBuilder.Append(":");
				stringBuilder.Append(this.iColumnNumber);
			}
			else
			{
				stringBuilder.Append("<null>");
			}
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000C7B08 File Offset: 0x000C5D08
		private void BuildStackFrame(int skipFrames, bool fNeedFileInfo)
		{
			using (StackFrameHelper stackFrameHelper = new StackFrameHelper(null))
			{
				stackFrameHelper.InitializeSourceInfo(0, fNeedFileInfo, null);
				int numberOfFrames = stackFrameHelper.GetNumberOfFrames();
				skipFrames += StackTrace.CalculateFramesToSkip(stackFrameHelper, numberOfFrames);
				if (numberOfFrames - skipFrames > 0)
				{
					this.method = stackFrameHelper.GetMethodBase(skipFrames);
					this.offset = stackFrameHelper.GetOffset(skipFrames);
					this.ILOffset = stackFrameHelper.GetILOffset(skipFrames);
					if (fNeedFileInfo)
					{
						this.strFileName = stackFrameHelper.GetFilename(skipFrames);
						this.iLineNumber = stackFrameHelper.GetLineNumber(skipFrames);
						this.iColumnNumber = stackFrameHelper.GetColumnNumber(skipFrames);
					}
				}
			}
		}

		// Token: 0x040016ED RID: 5869
		private MethodBase method;

		// Token: 0x040016EE RID: 5870
		private int offset;

		// Token: 0x040016EF RID: 5871
		private int ILOffset;

		// Token: 0x040016F0 RID: 5872
		private string strFileName;

		// Token: 0x040016F1 RID: 5873
		private int iLineNumber;

		// Token: 0x040016F2 RID: 5874
		private int iColumnNumber;

		// Token: 0x040016F3 RID: 5875
		[OptionalField]
		private bool fIsLastFrameFromForeignExceptionStackTrace;

		/// <summary>Defines the value that is returned from the <see cref="M:System.Diagnostics.StackFrame.GetNativeOffset" /> or <see cref="M:System.Diagnostics.StackFrame.GetILOffset" /> method when the native or Microsoft intermediate language (MSIL) offset is unknown. This field is constant.</summary>
		// Token: 0x040016F4 RID: 5876
		public const int OFFSET_UNKNOWN = -1;
	}
}
