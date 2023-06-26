using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
	/// <summary>The exception that is thrown when a managed assembly is found but cannot be loaded.</summary>
	// Token: 0x02000185 RID: 389
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class FileLoadException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "Could not load the specified file." This message takes into account the current system culture.</summary>
		// Token: 0x06001810 RID: 6160 RVA: 0x0004D47C File Offset: 0x0004B67C
		[__DynamicallyInvokable]
		public FileLoadException()
			: base(Environment.GetResourceString("IO.FileLoad"))
		{
			base.SetErrorCode(-2146232799);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06001811 RID: 6161 RVA: 0x0004D499 File Offset: 0x0004B699
		[__DynamicallyInvokable]
		public FileLoadException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232799);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001812 RID: 6162 RVA: 0x0004D4AD File Offset: 0x0004B6AD
		[__DynamicallyInvokable]
		public FileLoadException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232799);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with a specified error message and the name of the file that could not be loaded.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="fileName">A <see cref="T:System.String" /> containing the name of the file that was not loaded.</param>
		// Token: 0x06001813 RID: 6163 RVA: 0x0004D4C2 File Offset: 0x0004B6C2
		[__DynamicallyInvokable]
		public FileLoadException(string message, string fileName)
			: base(message)
		{
			base.SetErrorCode(-2146232799);
			this._fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with a specified error message, the name of the file that could not be loaded, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="fileName">A <see cref="T:System.String" /> containing the name of the file that was not loaded.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001814 RID: 6164 RVA: 0x0004D4DD File Offset: 0x0004B6DD
		[__DynamicallyInvokable]
		public FileLoadException(string message, string fileName, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232799);
			this._fileName = fileName;
		}

		/// <summary>Gets the error message and the name of the file that caused this exception.</summary>
		/// <returns>A string containing the error message and the name of the file that caused this exception.</returns>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x0004D4F9 File Offset: 0x0004B6F9
		[__DynamicallyInvokable]
		public override string Message
		{
			[__DynamicallyInvokable]
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0004D507 File Offset: 0x0004B707
		private void SetMessageField()
		{
			if (this._message == null)
			{
				this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
			}
		}

		/// <summary>Gets the name of the file that causes this exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the file with the invalid image, or a null reference if no file name was passed to the constructor for the current instance.</returns>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x0004D528 File Offset: 0x0004B728
		[__DynamicallyInvokable]
		public string FileName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._fileName;
			}
		}

		/// <summary>Returns the fully qualified name of the current exception, and possibly the error message, the name of the inner exception, and the stack trace.</summary>
		/// <returns>A string containing the fully qualified name of this exception, and possibly the error message, the name of the inner exception, and the stack trace, depending on which <see cref="T:System.IO.FileLoadException" /> constructor is used.</returns>
		// Token: 0x06001818 RID: 6168 RVA: 0x0004D530 File Offset: 0x0004B730
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string text = base.GetType().FullName + ": " + this.Message;
			if (this._fileName != null && this._fileName.Length != 0)
			{
				text = text + Environment.NewLine + Environment.GetResourceString("IO.FileName_Name", new object[] { this._fileName });
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			try
			{
				if (this.FusionLog != null)
				{
					if (text == null)
					{
						text = " ";
					}
					text += Environment.NewLine;
					text += Environment.NewLine;
					text += this.FusionLog;
				}
			}
			catch (SecurityException)
			{
			}
			return text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06001819 RID: 6169 RVA: 0x0004D61C File Offset: 0x0004B81C
		protected FileLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._fileName = info.GetString("FileLoad_FileName");
			try
			{
				this._fusionLog = info.GetString("FileLoad_FusionLog");
			}
			catch
			{
				this._fusionLog = null;
			}
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0004D670 File Offset: 0x0004B870
		private FileLoadException(string fileName, string fusionLog, int hResult)
			: base(null)
		{
			base.SetErrorCode(hResult);
			this._fileName = fileName;
			this._fusionLog = fusionLog;
			this.SetMessageField();
		}

		/// <summary>Gets the log file that describes why an assembly load failed.</summary>
		/// <returns>A string containing errors reported by the assembly cache.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x0004D694 File Offset: 0x0004B894
		public string FusionLog
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)]
			get
			{
				return this._fusionLog;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the file name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x0600181C RID: 6172 RVA: 0x0004D69C File Offset: 0x0004B89C
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileLoad_FileName", this._fileName, typeof(string));
			try
			{
				info.AddValue("FileLoad_FusionLog", this.FusionLog, typeof(string));
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0004D6FC File Offset: 0x0004B8FC
		[SecuritySafeCritical]
		internal static string FormatFileLoadExceptionMessage(string fileName, int hResult)
		{
			string text = null;
			FileLoadException.GetFileLoadExceptionMessage(hResult, JitHelpers.GetStringHandleOnStack(ref text));
			string text2 = null;
			FileLoadException.GetMessageForHR(hResult, JitHelpers.GetStringHandleOnStack(ref text2));
			return string.Format(CultureInfo.CurrentCulture, text, fileName, text2);
		}

		// Token: 0x0600181E RID: 6174
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetFileLoadExceptionMessage(int hResult, StringHandleOnStack retString);

		// Token: 0x0600181F RID: 6175
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetMessageForHR(int hresult, StringHandleOnStack retString);

		// Token: 0x04000835 RID: 2101
		private string _fileName;

		// Token: 0x04000836 RID: 2102
		private string _fusionLog;
	}
}
