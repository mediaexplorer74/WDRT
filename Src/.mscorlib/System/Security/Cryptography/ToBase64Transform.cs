using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Converts a <see cref="T:System.Security.Cryptography.CryptoStream" /> to base 64.</summary>
	// Token: 0x0200024F RID: 591
	[ComVisible(true)]
	public class ToBase64Transform : ICryptoTransform, IDisposable
	{
		/// <summary>Gets the input block size.</summary>
		/// <returns>The size of the input data blocks in bytes.</returns>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x00072D12 File Offset: 0x00070F12
		public int InputBlockSize
		{
			get
			{
				return 3;
			}
		}

		/// <summary>Gets the output block size.</summary>
		/// <returns>The size of the output data blocks in bytes.</returns>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x00072D15 File Offset: 0x00070F15
		public int OutputBlockSize
		{
			get
			{
				return 4;
			}
		}

		/// <summary>Gets a value that indicates whether multiple blocks can be transformed.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x00072D18 File Offset: 0x00070F18
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always <see langword="true" />.</returns>
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x00072D1B File Offset: 0x00070F1B
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Converts the specified region of the input byte array to base 64 and copies the result to the specified region of the output byte array.</summary>
		/// <param name="inputBuffer">The input to compute to base 64.</param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data.</param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data.</param>
		/// <param name="outputBuffer">The output to which to write the result.</param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data.</param>
		/// <returns>The number of bytes written.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.ToBase64Transform" /> object has already been disposed.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The data size is not valid.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="inputBuffer" /> parameter contains an invalid offset length.  
		///  -or-  
		///  The <paramref name="inputCount" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="inputBuffer" /> parameter requires a non-negative number.</exception>
		// Token: 0x060020F6 RID: 8438 RVA: 0x00072D20 File Offset: 0x00070F20
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, 3, array, 0);
			byte[] bytes = Encoding.ASCII.GetBytes(array);
			if (bytes.Length != 4)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_SSE_InvalidDataSize"));
			}
			Buffer.BlockCopy(bytes, 0, outputBuffer, outputOffset, bytes.Length);
			return bytes.Length;
		}

		/// <summary>Converts the specified region of the specified byte array to base 64.</summary>
		/// <param name="inputBuffer">The input to convert to base 64.</param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data.</param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data.</param>
		/// <returns>The computed base 64 conversion.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.ToBase64Transform" /> object has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="inputBuffer" /> parameter contains an invalid offset length.  
		///  -or-  
		///  The <paramref name="inputCount" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="inputBuffer" /> parameter requires a non-negative number.</exception>
		// Token: 0x060020F7 RID: 8439 RVA: 0x00072DCC File Offset: 0x00070FCC
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (inputCount == 0)
			{
				return EmptyArray<byte>.Value;
			}
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, inputCount, array, 0);
			return Encoding.ASCII.GetBytes(array);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.ToBase64Transform" /> class.</summary>
		// Token: 0x060020F8 RID: 8440 RVA: 0x00072E5A File Offset: 0x0007105A
		public void Dispose()
		{
			this.Clear();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" />.</summary>
		// Token: 0x060020F9 RID: 8441 RVA: 0x00072E62 File Offset: 0x00071062
		public void Clear()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060020FA RID: 8442 RVA: 0x00072E71 File Offset: 0x00071071
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" />.</summary>
		// Token: 0x060020FB RID: 8443 RVA: 0x00072E74 File Offset: 0x00071074
		~ToBase64Transform()
		{
			this.Dispose(false);
		}
	}
}
