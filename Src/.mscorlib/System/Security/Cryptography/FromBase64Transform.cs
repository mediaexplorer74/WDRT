using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Converts a <see cref="T:System.Security.Cryptography.CryptoStream" /> from base 64.</summary>
	// Token: 0x02000250 RID: 592
	[ComVisible(true)]
	public class FromBase64Transform : ICryptoTransform, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> class.</summary>
		// Token: 0x060020FD RID: 8445 RVA: 0x00072EAC File Offset: 0x000710AC
		public FromBase64Transform()
			: this(FromBase64TransformMode.IgnoreWhiteSpaces)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> class with the specified transformation mode.</summary>
		/// <param name="whitespaces">One of the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> values.</param>
		// Token: 0x060020FE RID: 8446 RVA: 0x00072EB5 File Offset: 0x000710B5
		public FromBase64Transform(FromBase64TransformMode whitespaces)
		{
			this._whitespaces = whitespaces;
			this._inputIndex = 0;
		}

		/// <summary>Gets the input block size.</summary>
		/// <returns>The size of the input data blocks in bytes.</returns>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x00072ED7 File Offset: 0x000710D7
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		/// <summary>Gets the output block size.</summary>
		/// <returns>The size of the output data blocks in bytes.</returns>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x00072EDA File Offset: 0x000710DA
		public int OutputBlockSize
		{
			get
			{
				return 3;
			}
		}

		/// <summary>Gets a value that indicates whether multiple blocks can be transformed.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x00072EDD File Offset: 0x000710DD
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always <see langword="true" />.</returns>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x00072EE0 File Offset: 0x000710E0
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Converts the specified region of the input byte array from base 64 and copies the result to the specified region of the output byte array.</summary>
		/// <param name="inputBuffer">The input to compute from base 64.</param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data.</param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data.</param>
		/// <param name="outputBuffer">The output to which to write the result.</param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data.</param>
		/// <returns>The number of bytes written.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.FromBase64Transform" /> object has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputCount" /> uses an invalid value.  
		/// -or-  
		/// <paramref name="inputBuffer" /> has an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is <see langword="null" />.</exception>
		// Token: 0x06002103 RID: 8451 RVA: 0x00072EE4 File Offset: 0x000710E4
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
			if (this._inputBuffer == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			byte[] array = new byte[inputCount];
			int num;
			if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
			{
				array = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
				num = array.Length;
			}
			else
			{
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
				num = inputCount;
			}
			if (num + this._inputIndex < 4)
			{
				Buffer.InternalBlockCopy(array, 0, this._inputBuffer, this._inputIndex, num);
				this._inputIndex += num;
				return 0;
			}
			int num2 = (num + this._inputIndex) / 4;
			byte[] array2 = new byte[this._inputIndex + num];
			Buffer.InternalBlockCopy(this._inputBuffer, 0, array2, 0, this._inputIndex);
			Buffer.InternalBlockCopy(array, 0, array2, this._inputIndex, num);
			this._inputIndex = (num + this._inputIndex) % 4;
			Buffer.InternalBlockCopy(array, num - this._inputIndex, this._inputBuffer, 0, this._inputIndex);
			char[] chars = Encoding.ASCII.GetChars(array2, 0, 4 * num2);
			byte[] array3 = Convert.FromBase64CharArray(chars, 0, 4 * num2);
			Buffer.BlockCopy(array3, 0, outputBuffer, outputOffset, array3.Length);
			return array3.Length;
		}

		/// <summary>Converts the specified region of the specified byte array from base 64.</summary>
		/// <param name="inputBuffer">The input to convert from base 64.</param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data.</param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data.</param>
		/// <returns>The computed conversion.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.FromBase64Transform" /> object has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputBuffer" /> has an invalid offset length.  
		/// -or-  
		/// <paramref name="inputCount" /> has an invalid value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is <see langword="null" />.</exception>
		// Token: 0x06002104 RID: 8452 RVA: 0x00073058 File Offset: 0x00071258
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
			if (this._inputBuffer == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			byte[] array = new byte[inputCount];
			int num;
			if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
			{
				array = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
				num = array.Length;
			}
			else
			{
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
				num = inputCount;
			}
			if (num + this._inputIndex < 4)
			{
				this.Reset();
				return EmptyArray<byte>.Value;
			}
			int num2 = (num + this._inputIndex) / 4;
			byte[] array2 = new byte[this._inputIndex + num];
			Buffer.InternalBlockCopy(this._inputBuffer, 0, array2, 0, this._inputIndex);
			Buffer.InternalBlockCopy(array, 0, array2, this._inputIndex, num);
			this._inputIndex = (num + this._inputIndex) % 4;
			Buffer.InternalBlockCopy(array, num - this._inputIndex, this._inputBuffer, 0, this._inputIndex);
			char[] chars = Encoding.ASCII.GetChars(array2, 0, 4 * num2);
			byte[] array3 = Convert.FromBase64CharArray(chars, 0, 4 * num2);
			this.Reset();
			return array3;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000731A8 File Offset: 0x000713A8
		private byte[] DiscardWhiteSpaces(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace((char)inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			byte[] array = new byte[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace((char)inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return array;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> class.</summary>
		// Token: 0x06002106 RID: 8454 RVA: 0x00073203 File Offset: 0x00071403
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00073212 File Offset: 0x00071412
		private void Reset()
		{
			this._inputIndex = 0;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.FromBase64Transform" />.</summary>
		// Token: 0x06002108 RID: 8456 RVA: 0x0007321B File Offset: 0x0007141B
		public void Clear()
		{
			this.Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002109 RID: 8457 RVA: 0x00073223 File Offset: 0x00071423
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._inputBuffer != null)
				{
					Array.Clear(this._inputBuffer, 0, this._inputBuffer.Length);
				}
				this._inputBuffer = null;
				this._inputIndex = 0;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.FromBase64Transform" />.</summary>
		// Token: 0x0600210A RID: 8458 RVA: 0x00073254 File Offset: 0x00071454
		~FromBase64Transform()
		{
			this.Dispose(false);
		}

		// Token: 0x04000BED RID: 3053
		private byte[] _inputBuffer = new byte[4];

		// Token: 0x04000BEE RID: 3054
		private int _inputIndex;

		// Token: 0x04000BEF RID: 3055
		private FromBase64TransformMode _whitespaces;
	}
}
