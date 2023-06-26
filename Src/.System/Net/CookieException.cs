using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error is made adding a <see cref="T:System.Net.Cookie" /> to a <see cref="T:System.Net.CookieContainer" />.</summary>
	// Token: 0x020000DA RID: 218
	[global::__DynamicallyInvokable]
	[Serializable]
	public class CookieException : FormatException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieException" /> class.</summary>
		// Token: 0x0600076D RID: 1901 RVA: 0x00029742 File Offset: 0x00027942
		[global::__DynamicallyInvokable]
		public CookieException()
		{
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0002974A File Offset: 0x0002794A
		internal CookieException(string message)
			: base(message)
		{
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00029753 File Offset: 0x00027953
		internal CookieException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieException" /> class with specific values of <paramref name="serializationInfo" /> and <paramref name="streamingContext" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used.</param>
		// Token: 0x06000770 RID: 1904 RVA: 0x0002975D File Offset: 0x0002795D
		protected CookieException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.CookieException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used.</param>
		// Token: 0x06000771 RID: 1905 RVA: 0x00029767 File Offset: 0x00027967
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.CookieException" />.</summary>
		/// <param name="serializationInfo">The object that holds the serialized object data. The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">The contextual information about the source or destination. A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06000772 RID: 1906 RVA: 0x00029771 File Offset: 0x00027971
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
