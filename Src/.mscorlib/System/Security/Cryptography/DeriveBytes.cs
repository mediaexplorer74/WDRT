using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all classes that derive byte sequences of a specified length inherit.</summary>
	// Token: 0x0200025A RID: 602
	[ComVisible(true)]
	public abstract class DeriveBytes : IDisposable
	{
		/// <summary>When overridden in a derived class, returns pseudo-random key bytes.</summary>
		/// <param name="cb">The number of pseudo-random key bytes to generate.</param>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		// Token: 0x06002162 RID: 8546
		public abstract byte[] GetBytes(int cb);

		/// <summary>When overridden in a derived class, resets the state of the operation.</summary>
		// Token: 0x06002163 RID: 8547
		public abstract void Reset();

		/// <summary>When overridden in a derived class, releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.DeriveBytes" /> class.</summary>
		// Token: 0x06002164 RID: 8548 RVA: 0x00076385 File Offset: 0x00074585
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.DeriveBytes" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002165 RID: 8549 RVA: 0x00076394 File Offset: 0x00074594
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
