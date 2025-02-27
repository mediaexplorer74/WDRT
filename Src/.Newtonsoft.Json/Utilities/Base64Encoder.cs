﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000041 RID: 65
	[NullableContext(1)]
	[Nullable(0)]
	internal class Base64Encoder
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x00010210 File Offset: 0x0000E410
		public Base64Encoder(TextWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = writer;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00010238 File Offset: 0x0000E438
		private void ValidateEncode(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00010284 File Offset: 0x0000E484
		public void Encode(byte[] buffer, int index, int count)
		{
			this.ValidateEncode(buffer, index, count);
			if (this._leftOverBytesCount > 0)
			{
				if (this.FulfillFromLeftover(buffer, index, ref count))
				{
					return;
				}
				int num = Convert.ToBase64CharArray(this._leftOverBytes, 0, 3, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, num);
			}
			this.StoreLeftOverBytes(buffer, index, ref count);
			int num2 = index + count;
			int num3 = 57;
			while (index < num2)
			{
				if (index + num3 > num2)
				{
					num3 = num2 - index;
				}
				int num4 = Convert.ToBase64CharArray(buffer, index, num3, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, num4);
				index += num3;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00010318 File Offset: 0x0000E518
		private void StoreLeftOverBytes(byte[] buffer, int index, ref int count)
		{
			int num = count % 3;
			if (num > 0)
			{
				count -= num;
				if (this._leftOverBytes == null)
				{
					this._leftOverBytes = new byte[3];
				}
				for (int i = 0; i < num; i++)
				{
					this._leftOverBytes[i] = buffer[index + count + i];
				}
			}
			this._leftOverBytesCount = num;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001036C File Offset: 0x0000E56C
		private bool FulfillFromLeftover(byte[] buffer, int index, ref int count)
		{
			int leftOverBytesCount = this._leftOverBytesCount;
			while (leftOverBytesCount < 3 && count > 0)
			{
				this._leftOverBytes[leftOverBytesCount++] = buffer[index++];
				count--;
			}
			if (count == 0 && leftOverBytesCount < 3)
			{
				this._leftOverBytesCount = leftOverBytesCount;
				return true;
			}
			return false;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000103B8 File Offset: 0x0000E5B8
		public void Flush()
		{
			if (this._leftOverBytesCount > 0)
			{
				int num = Convert.ToBase64CharArray(this._leftOverBytes, 0, this._leftOverBytesCount, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, num);
				this._leftOverBytesCount = 0;
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000103FD File Offset: 0x0000E5FD
		private void WriteChars(char[] chars, int index, int count)
		{
			this._writer.Write(chars, index, count);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00010410 File Offset: 0x0000E610
		public async Task EncodeAsync(byte[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			this.ValidateEncode(buffer, index, count);
			if (this._leftOverBytesCount > 0)
			{
				if (this.FulfillFromLeftover(buffer, index, ref count))
				{
					return;
				}
				int num5 = Convert.ToBase64CharArray(this._leftOverBytes, 0, 3, this._charsLine, 0);
				await this.WriteCharsAsync(this._charsLine, 0, num5, cancellationToken).ConfigureAwait(false);
			}
			this.StoreLeftOverBytes(buffer, index, ref count);
			int num4 = index + count;
			int length = 57;
			while (index < num4)
			{
				if (index + length > num4)
				{
					length = num4 - index;
				}
				int num6 = Convert.ToBase64CharArray(buffer, index, length, this._charsLine, 0);
				await this.WriteCharsAsync(this._charsLine, 0, num6, cancellationToken).ConfigureAwait(false);
				index += length;
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00010474 File Offset: 0x0000E674
		private Task WriteCharsAsync(char[] chars, int index, int count, CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(chars, index, count, cancellationToken);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00010488 File Offset: 0x0000E688
		public Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			if (this._leftOverBytesCount > 0)
			{
				int num = Convert.ToBase64CharArray(this._leftOverBytes, 0, this._leftOverBytesCount, this._charsLine, 0);
				this._leftOverBytesCount = 0;
				return this.WriteCharsAsync(this._charsLine, 0, num, cancellationToken);
			}
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0400014E RID: 334
		private const int Base64LineSize = 76;

		// Token: 0x0400014F RID: 335
		private const int LineSizeInBytes = 57;

		// Token: 0x04000150 RID: 336
		private readonly char[] _charsLine = new char[76];

		// Token: 0x04000151 RID: 337
		private readonly TextWriter _writer;

		// Token: 0x04000152 RID: 338
		[Nullable(2)]
		private byte[] _leftOverBytes;

		// Token: 0x04000153 RID: 339
		private int _leftOverBytesCount;
	}
}
