using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for storing serialization data for the <see cref="T:System.ComponentModel.Design.Serialization.ComponentSerializationService" />.</summary>
	// Token: 0x02000614 RID: 1556
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class SerializationStore : IDisposable
	{
		/// <summary>Gets a collection of errors that occurred during serialization or deserialization.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains errors that occurred during serialization or deserialization.</returns>
		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060038E1 RID: 14561
		public abstract ICollection Errors { get; }

		/// <summary>Closes the serialization store.</summary>
		// Token: 0x060038E2 RID: 14562
		public abstract void Close();

		/// <summary>Saves the store to the given stream.</summary>
		/// <param name="stream">The stream to which the store will be serialized.</param>
		// Token: 0x060038E3 RID: 14563
		public abstract void Save(Stream stream);

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
		// Token: 0x060038E4 RID: 14564 RVA: 0x000F1A2F File Offset: 0x000EFC2F
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060038E5 RID: 14565 RVA: 0x000F1A38 File Offset: 0x000EFC38
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}
	}
}
