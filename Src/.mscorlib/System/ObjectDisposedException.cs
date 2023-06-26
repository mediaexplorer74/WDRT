using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when an operation is performed on a disposed object.</summary>
	// Token: 0x02000119 RID: 281
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ObjectDisposedException : InvalidOperationException
	{
		// Token: 0x060010B3 RID: 4275 RVA: 0x000326AF File Offset: 0x000308AF
		private ObjectDisposedException()
			: this(null, Environment.GetResourceString("ObjectDisposed_Generic"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with a string containing the name of the disposed object.</summary>
		/// <param name="objectName">A string containing the name of the disposed object.</param>
		// Token: 0x060010B4 RID: 4276 RVA: 0x000326C2 File Offset: 0x000308C2
		[__DynamicallyInvokable]
		public ObjectDisposedException(string objectName)
			: this(objectName, Environment.GetResourceString("ObjectDisposed_Generic"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with the specified object name and message.</summary>
		/// <param name="objectName">The name of the disposed object.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x060010B5 RID: 4277 RVA: 0x000326D5 File Offset: 0x000308D5
		[__DynamicallyInvokable]
		public ObjectDisposedException(string objectName, string message)
			: base(message)
		{
			base.SetErrorCode(-2146232798);
			this.objectName = objectName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If <paramref name="innerException" /> is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060010B6 RID: 4278 RVA: 0x000326F0 File Offset: 0x000308F0
		[__DynamicallyInvokable]
		public ObjectDisposedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232798);
		}

		/// <summary>Gets the message that describes the error.</summary>
		/// <returns>A string that describes the error.</returns>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00032708 File Offset: 0x00030908
		[__DynamicallyInvokable]
		public override string Message
		{
			[__DynamicallyInvokable]
			get
			{
				string text = this.ObjectName;
				if (text == null || text.Length == 0)
				{
					return base.Message;
				}
				string resourceString = Environment.GetResourceString("ObjectDisposed_ObjectName_Name", new object[] { text });
				return base.Message + Environment.NewLine + resourceString;
			}
		}

		/// <summary>Gets the name of the disposed object.</summary>
		/// <returns>A string containing the name of the disposed object.</returns>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00032754 File Offset: 0x00030954
		[__DynamicallyInvokable]
		public string ObjectName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.objectName == null && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
				{
					return string.Empty;
				}
				return this.objectName;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x060010B9 RID: 4281 RVA: 0x00032771 File Offset: 0x00030971
		protected ObjectDisposedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.objectName = info.GetString("ObjectName");
		}

		/// <summary>Retrieves the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x060010BA RID: 4282 RVA: 0x0003278C File Offset: 0x0003098C
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ObjectName", this.ObjectName, typeof(string));
		}

		// Token: 0x040005CC RID: 1484
		private string objectName;
	}
}
