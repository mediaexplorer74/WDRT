using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Management
{
	/// <summary>Represents management exceptions.</summary>
	// Token: 0x0200001A RID: 26
	[Serializable]
	public class ManagementException : SystemException
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004ABC File Offset: 0x00002CBC
		internal static void ThrowWithExtendedInfo(ManagementStatus errorCode)
		{
			ManagementBaseObject managementBaseObject = null;
			string text = null;
			IWbemClassObjectFreeThreaded errorInfo = WbemErrorInfo.GetErrorInfo();
			if (errorInfo != null)
			{
				managementBaseObject = new ManagementBaseObject(errorInfo);
			}
			if ((text = ManagementException.GetMessage(errorCode)) == null && managementBaseObject != null)
			{
				try
				{
					text = (string)managementBaseObject["Description"];
				}
				catch
				{
				}
			}
			throw new ManagementException(errorCode, text, managementBaseObject);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004B18 File Offset: 0x00002D18
		internal static void ThrowWithExtendedInfo(Exception e)
		{
			ManagementBaseObject managementBaseObject = null;
			string text = null;
			IWbemClassObjectFreeThreaded errorInfo = WbemErrorInfo.GetErrorInfo();
			if (errorInfo != null)
			{
				managementBaseObject = new ManagementBaseObject(errorInfo);
			}
			if ((text = ManagementException.GetMessage(e)) == null && managementBaseObject != null)
			{
				try
				{
					text = (string)managementBaseObject["Description"];
				}
				catch
				{
				}
			}
			throw new ManagementException(e, text, managementBaseObject);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004B74 File Offset: 0x00002D74
		internal ManagementException(ManagementStatus errorCode, string msg, ManagementBaseObject errObj)
			: base(msg)
		{
			this.errorCode = errorCode;
			this.errorObject = errObj;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004B8C File Offset: 0x00002D8C
		internal ManagementException(Exception e, string msg, ManagementBaseObject errObj)
			: base(msg, e)
		{
			try
			{
				if (e is ManagementException)
				{
					this.errorCode = ((ManagementException)e).ErrorCode;
					if (this.errorObject != null)
					{
						this.errorObject = (ManagementBaseObject)((ManagementException)e).errorObject.Clone();
					}
					else
					{
						this.errorObject = null;
					}
				}
				else if (e is COMException)
				{
					this.errorCode = (ManagementStatus)((COMException)e).ErrorCode;
				}
				else
				{
					this.errorCode = (ManagementStatus)base.HResult;
				}
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementException" /> class that is serializable.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> destination for this serialization.</param>
		// Token: 0x06000092 RID: 146 RVA: 0x00004C24 File Offset: 0x00002E24
		protected ManagementException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.errorCode = (ManagementStatus)info.GetValue("errorCode", typeof(ManagementStatus));
			this.errorObject = info.GetValue("errorObject", typeof(ManagementBaseObject)) as ManagementBaseObject;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementException" /> class.</summary>
		// Token: 0x06000093 RID: 147 RVA: 0x00004C79 File Offset: 0x00002E79
		public ManagementException()
			: this(ManagementStatus.Failed, "", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000094 RID: 148 RVA: 0x00004C8C File Offset: 0x00002E8C
		public ManagementException(string message)
			: this(ManagementStatus.Failed, message, null)
		{
		}

		/// <summary>Initializes an empty new instance of the <see cref="T:System.Management.ManagementException" /> class. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a catch block that handles the inner exception.</summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000095 RID: 149 RVA: 0x00004C9B File Offset: 0x00002E9B
		public ManagementException(string message, Exception innerException)
			: this(innerException, message, null)
		{
			if (!(innerException is ManagementException))
			{
				this.errorCode = ManagementStatus.Failed;
			}
		}

		/// <summary>Populates the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the <see cref="T:System.Management.ManagementException" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> destination for this serialization.</param>
		// Token: 0x06000096 RID: 150 RVA: 0x00004CB9 File Offset: 0x00002EB9
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorCode", this.errorCode);
			info.AddValue("errorObject", this.errorObject);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004CEC File Offset: 0x00002EEC
		private static string GetMessage(Exception e)
		{
			string text = null;
			if (e is COMException)
			{
				text = ManagementException.GetMessage((ManagementStatus)((COMException)e).ErrorCode);
			}
			if (text == null)
			{
				text = e.Message;
			}
			return text;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004D20 File Offset: 0x00002F20
		private static string GetMessage(ManagementStatus errorCode)
		{
			string text = null;
			IWbemStatusCodeText wbemStatusCodeText = (IWbemStatusCodeText)new WbemStatusCodeText();
			if (wbemStatusCodeText != null)
			{
				try
				{
					int num = wbemStatusCodeText.GetErrorCodeText_((int)errorCode, 0U, 1, out text);
					if (num != 0)
					{
						num = wbemStatusCodeText.GetErrorCodeText_((int)errorCode, 0U, 0, out text);
					}
				}
				catch
				{
				}
			}
			return text;
		}

		/// <summary>Gets the extended error object provided by WMI.</summary>
		/// <returns>The extended error information.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004D70 File Offset: 0x00002F70
		public ManagementBaseObject ErrorInformation
		{
			get
			{
				return this.errorObject;
			}
		}

		/// <summary>Gets the error code reported by WMI, which caused this exception.</summary>
		/// <returns>One of the enumeration values that indicates the error code.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004D78 File Offset: 0x00002F78
		public ManagementStatus ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x04000104 RID: 260
		private ManagementBaseObject errorObject;

		// Token: 0x04000105 RID: 261
		private ManagementStatus errorCode;
	}
}
