using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines methods that allow an <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class to enumerate key container information, and import and export Microsoft Cryptographic API (CAPI)-compatible key blobs.</summary>
	// Token: 0x0200026C RID: 620
	[ComVisible(true)]
	public interface ICspAsymmetricAlgorithm
	{
		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</returns>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060021FC RID: 8700
		CspKeyContainerInfo CspKeyContainerInfo { get; }

		/// <summary>Exports a blob that contains the key information associated with an <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object.</summary>
		/// <param name="includePrivateParameters">
		///   <see langword="true" /> to include the private key; otherwise, <see langword="false" />.</param>
		/// <returns>A byte array that contains the key information associated with an <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object.</returns>
		// Token: 0x060021FD RID: 8701
		byte[] ExportCspBlob(bool includePrivateParameters);

		/// <summary>Imports a blob that represents asymmetric key information.</summary>
		/// <param name="rawData">A byte array that represents an asymmetric key blob.</param>
		// Token: 0x060021FE RID: 8702
		void ImportCspBlob(byte[] rawData);
	}
}
