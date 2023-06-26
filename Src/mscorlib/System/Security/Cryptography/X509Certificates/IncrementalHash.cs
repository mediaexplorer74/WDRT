using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B8 RID: 696
	internal class IncrementalHash : IDisposable
	{
		// Token: 0x0600250A RID: 9482 RVA: 0x00087228 File Offset: 0x00085428
		private IncrementalHash(HashAlgorithm algorithm)
		{
			this._algorithm = algorithm;
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00087238 File Offset: 0x00085438
		public static IncrementalHash CreateHash(HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm == HashAlgorithmName.MD5)
			{
				return new IncrementalHash(MD5.Create());
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new IncrementalHash(SHA1.Create());
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new IncrementalHash(SHA256.Create());
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new IncrementalHash(SHA384.Create());
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new IncrementalHash(SHA512.Create());
			}
			throw new CryptographicException();
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000872C4 File Offset: 0x000854C4
		public void AppendData(ReadOnlySpan<byte> data)
		{
			ArraySegment<byte> arraySegment = data.DangerousGetArraySegment();
			this._algorithm.TransformBlock(arraySegment.Array, arraySegment.Offset, arraySegment.Count, null, 0);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000872FC File Offset: 0x000854FC
		public bool TryGetHashAndReset(Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length < this._algorithm.HashSize / 8)
			{
				bytesWritten = 0;
				return false;
			}
			this._algorithm.TransformFinalBlock(IncrementalHash.s_Empty, 0, 0);
			byte[] hash = this._algorithm.Hash;
			this._algorithm.Initialize();
			new ReadOnlyMemory<byte>(hash).CopyTo(destination);
			bytesWritten = hash.Length;
			return true;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x00087363 File Offset: 0x00085563
		public void Dispose()
		{
			this._algorithm.Clear();
		}

		// Token: 0x04000DD3 RID: 3539
		private readonly HashAlgorithm _algorithm;

		// Token: 0x04000DD4 RID: 3540
		private static readonly byte[] s_Empty = new byte[0];
	}
}
