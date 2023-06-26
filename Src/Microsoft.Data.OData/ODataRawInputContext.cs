using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x02000240 RID: 576
	internal sealed class ODataRawInputContext : ODataInputContext
	{
		// Token: 0x06001266 RID: 4710 RVA: 0x000452B0 File Offset: 0x000434B0
		internal ODataRawInputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver, ODataPayloadKind readerPayloadKind)
			: base(format, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			try
			{
				this.stream = messageStream;
				this.encoding = encoding;
				this.readerPayloadKind = readerPayloadKind;
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && messageStream != null)
				{
					messageStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00045324 File Offset: 0x00043524
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004532C File Offset: 0x0004352C
		internal override ODataBatchReader CreateBatchReader(string batchBoundary)
		{
			return this.CreateBatchReaderImplementation(batchBoundary, true);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00045354 File Offset: 0x00043554
		internal override Task<ODataBatchReader> CreateBatchReaderAsync(string batchBoundary)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchReader>(() => this.CreateBatchReaderImplementation(batchBoundary, false));
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00045386 File Offset: 0x00043586
		internal override object ReadValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			return this.ReadValueImplementation(expectedPrimitiveTypeReference);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000453AC File Offset: 0x000435AC
		internal override Task<object> ReadValueAsync(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<object>(() => this.ReadValueImplementation(expectedPrimitiveTypeReference));
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000453E0 File Offset: 0x000435E0
		protected override void DisposeImplementation()
		{
			try
			{
				if (this.textReader != null)
				{
					this.textReader.Dispose();
				}
				else if (this.stream != null)
				{
					this.stream.Dispose();
				}
			}
			finally
			{
				this.textReader = null;
				this.stream = null;
			}
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00045438 File Offset: 0x00043638
		private ODataBatchReader CreateBatchReaderImplementation(string batchBoundary, bool synchronous)
		{
			return new ODataBatchReader(this, batchBoundary, this.encoding, synchronous);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00045448 File Offset: 0x00043648
		private object ReadValueImplementation(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			bool flag;
			if (expectedPrimitiveTypeReference == null)
			{
				flag = this.readerPayloadKind == ODataPayloadKind.BinaryValue;
			}
			else
			{
				flag = expectedPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Binary;
			}
			if (flag)
			{
				return this.ReadBinaryValue();
			}
			this.textReader = ((this.encoding == null) ? new StreamReader(this.stream) : new StreamReader(this.stream, this.encoding));
			return this.ReadRawValue(expectedPrimitiveTypeReference);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000454B0 File Offset: 0x000436B0
		private byte[] ReadBinaryValue()
		{
			long num = 0L;
			List<byte[]> list = new List<byte[]>();
			byte[] array;
			int num2;
			do
			{
				array = new byte[4096];
				num2 = this.stream.Read(array, 0, array.Length);
				num += (long)num2;
				list.Add(array);
			}
			while (num2 == array.Length);
			array = new byte[num];
			for (int i = 0; i < list.Count - 1; i++)
			{
				Buffer.BlockCopy(list[i], 0, array, i * 4096, 4096);
			}
			Buffer.BlockCopy(list[list.Count - 1], 0, array, (list.Count - 1) * 4096, num2);
			return array;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00045554 File Offset: 0x00043754
		private object ReadRawValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			string text = this.textReader.ReadToEnd();
			object obj;
			if (expectedPrimitiveTypeReference != null && !base.MessageReaderSettings.DisablePrimitiveTypeConversion)
			{
				obj = AtomValueUtils.ConvertStringToPrimitive(text, expectedPrimitiveTypeReference);
			}
			else
			{
				obj = text;
			}
			return obj;
		}

		// Token: 0x040006A6 RID: 1702
		private const int BufferSize = 4096;

		// Token: 0x040006A7 RID: 1703
		private readonly ODataPayloadKind readerPayloadKind;

		// Token: 0x040006A8 RID: 1704
		private readonly Encoding encoding;

		// Token: 0x040006A9 RID: 1705
		private Stream stream;

		// Token: 0x040006AA RID: 1706
		private TextReader textReader;
	}
}
