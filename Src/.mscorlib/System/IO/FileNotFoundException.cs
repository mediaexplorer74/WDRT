using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
	/// <summary>The exception that is thrown when an attempt to access a file that does not exist on disk fails.</summary>
	// Token: 0x02000187 RID: 391
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class FileNotFoundException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with its message string set to a system-supplied message.</summary>
		// Token: 0x06001820 RID: 6176 RVA: 0x0004D734 File Offset: 0x0004B934
		[__DynamicallyInvokable]
		public FileNotFoundException()
			: base(Environment.GetResourceString("IO.FileNotFound"))
		{
			base.SetErrorCode(-2147024894);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with a specified error message.</summary>
		/// <param name="message">A description of the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06001821 RID: 6177 RVA: 0x0004D751 File Offset: 0x0004B951
		[__DynamicallyInvokable]
		public FileNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024894);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A description of the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001822 RID: 6178 RVA: 0x0004D765 File Offset: 0x0004B965
		[__DynamicallyInvokable]
		public FileNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024894);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with a specified error message, and the file name that cannot be found.</summary>
		/// <param name="message">A description of the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="fileName">The full name of the file with the invalid image.</param>
		// Token: 0x06001823 RID: 6179 RVA: 0x0004D77A File Offset: 0x0004B97A
		[__DynamicallyInvokable]
		public FileNotFoundException(string message, string fileName)
			: base(message)
		{
			base.SetErrorCode(-2147024894);
			this._fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with a specified error message, the file name that cannot be found, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="fileName">The full name of the file with the invalid image.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001824 RID: 6180 RVA: 0x0004D795 File Offset: 0x0004B995
		[__DynamicallyInvokable]
		public FileNotFoundException(string message, string fileName, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024894);
			this._fileName = fileName;
		}

		/// <summary>Gets the error message that explains the reason for the exception.</summary>
		/// <returns>The error message.</returns>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0004D7B1 File Offset: 0x0004B9B1
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

		// Token: 0x06001826 RID: 6182 RVA: 0x0004D7C0 File Offset: 0x0004B9C0
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this._fileName == null && base.HResult == -2146233088)
				{
					this._message = Environment.GetResourceString("IO.FileNotFound");
					return;
				}
				if (this._fileName != null)
				{
					this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
				}
			}
		}

		/// <summary>Gets the name of the file that cannot be found.</summary>
		/// <returns>The name of the file, or <see langword="null" /> if no file name was passed to the constructor for this instance.</returns>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x0004D81A File Offset: 0x0004BA1A
		[__DynamicallyInvokable]
		public string FileName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._fileName;
			}
		}

		/// <summary>Returns the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</summary>
		/// <returns>The fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</returns>
		// Token: 0x06001828 RID: 6184 RVA: 0x0004D824 File Offset: 0x0004BA24
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

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">An object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">An object that contains contextual information about the source or destination.</param>
		// Token: 0x06001829 RID: 6185 RVA: 0x0004D910 File Offset: 0x0004BB10
		protected FileNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._fileName = info.GetString("FileNotFound_FileName");
			try
			{
				this._fusionLog = info.GetString("FileNotFound_FusionLog");
			}
			catch
			{
				this._fusionLog = null;
			}
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0004D964 File Offset: 0x0004BB64
		private FileNotFoundException(string fileName, string fusionLog, int hResult)
			: base(null)
		{
			base.SetErrorCode(hResult);
			this._fileName = fileName;
			this._fusionLog = fusionLog;
			this.SetMessageField();
		}

		/// <summary>Gets the log file that describes why loading of an assembly failed.</summary>
		/// <returns>The errors reported by the assembly cache.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x0004D988 File Offset: 0x0004BB88
		public string FusionLog
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)]
			get
			{
				return this._fusionLog;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The object that contains contextual information about the source or destination.</param>
		// Token: 0x0600182C RID: 6188 RVA: 0x0004D990 File Offset: 0x0004BB90
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileNotFound_FileName", this._fileName, typeof(string));
			try
			{
				info.AddValue("FileNotFound_FusionLog", this.FusionLog, typeof(string));
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x0400083E RID: 2110
		private string _fileName;

		// Token: 0x0400083F RID: 2111
		private string _fusionLog;
	}
}
