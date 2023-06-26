using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines the basic operations of cryptographic transformations.</summary>
	// Token: 0x02000245 RID: 581
	[ComVisible(true)]
	public interface ICryptoTransform : IDisposable
	{
		/// <summary>Gets the input block size.</summary>
		/// <returns>The size of the input data blocks in bytes.</returns>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060020B6 RID: 8374
		int InputBlockSize { get; }

		/// <summary>Gets the output block size.</summary>
		/// <returns>The size of the output data blocks in bytes.</returns>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060020B7 RID: 8375
		int OutputBlockSize { get; }

		/// <summary>Gets a value indicating whether multiple blocks can be transformed.</summary>
		/// <returns>
		///   <see langword="true" /> if multiple blocks can be transformed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060020B8 RID: 8376
		bool CanTransformMultipleBlocks { get; }

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>
		///   <see langword="true" /> if the current transform can be reused; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060020B9 RID: 8377
		bool CanReuseTransform { get; }

		/// <summary>Transforms the specified region of the input byte array and copies the resulting transform to the specified region of the output byte array.</summary>
		/// <param name="inputBuffer">The input for which to compute the transform.</param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data.</param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data.</param>
		/// <param name="outputBuffer">The output to which to write the transform.</param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data.</param>
		/// <returns>The number of bytes written.</returns>
		// Token: 0x060020BA RID: 8378
		int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		/// <summary>Transforms the specified region of the specified byte array.</summary>
		/// <param name="inputBuffer">The input for which to compute the transform.</param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data.</param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data.</param>
		/// <returns>The computed transform.</returns>
		// Token: 0x060020BB RID: 8379
		byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
	}
}
