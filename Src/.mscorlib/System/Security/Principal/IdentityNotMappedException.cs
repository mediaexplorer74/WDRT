using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Principal
{
	/// <summary>Represents an exception for a principal whose identity could not be mapped to a known identity.</summary>
	// Token: 0x0200033C RID: 828
	[ComVisible(false)]
	[Serializable]
	public sealed class IdentityNotMappedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class.</summary>
		// Token: 0x0600296E RID: 10606 RVA: 0x0009A5D4 File Offset: 0x000987D4
		public IdentityNotMappedException()
			: base(Environment.GetResourceString("IdentityReference_IdentityNotMapped"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class by using the specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600296F RID: 10607 RVA: 0x0009A5E6 File Offset: 0x000987E6
		public IdentityNotMappedException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class by using the specified error message and inner exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If <paramref name="inner" /> is not null, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002970 RID: 10608 RVA: 0x0009A5EF File Offset: 0x000987EF
		public IdentityNotMappedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x0009A5F9 File Offset: 0x000987F9
		internal IdentityNotMappedException(string message, IdentityReferenceCollection unmappedIdentities)
			: this(message)
		{
			this.unmappedIdentities = unmappedIdentities;
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x0009A609 File Offset: 0x00098809
		internal IdentityNotMappedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets serialization information with the data needed to create an instance of this <see cref="T:System.Security.Principal.IdentityNotMappedException" /> object.</summary>
		/// <param name="serializationInfo">The object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="streamingContext">The object that contains contextual information about the source or destination.</param>
		// Token: 0x06002973 RID: 10611 RVA: 0x0009A613 File Offset: 0x00098813
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Represents the collection of unmapped identities for an <see cref="T:System.Security.Principal.IdentityNotMappedException" /> exception.</summary>
		/// <returns>The collection of unmapped identities.</returns>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x0009A61D File Offset: 0x0009881D
		public IdentityReferenceCollection UnmappedIdentities
		{
			get
			{
				if (this.unmappedIdentities == null)
				{
					this.unmappedIdentities = new IdentityReferenceCollection();
				}
				return this.unmappedIdentities;
			}
		}

		// Token: 0x0400110E RID: 4366
		private IdentityReferenceCollection unmappedIdentities;
	}
}
