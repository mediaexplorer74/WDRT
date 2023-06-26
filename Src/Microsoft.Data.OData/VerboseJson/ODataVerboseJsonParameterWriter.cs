using System;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001DD RID: 477
	internal sealed class ODataVerboseJsonParameterWriter : ODataParameterWriterCore
	{
		// Token: 0x06000ECC RID: 3788 RVA: 0x0003499E File Offset: 0x00032B9E
		internal ODataVerboseJsonParameterWriter(ODataVerboseJsonOutputContext verboseJsonOutputContext, IEdmFunctionImport functionImport)
			: base(verboseJsonOutputContext, functionImport)
		{
			this.verboseJsonOutputContext = verboseJsonOutputContext;
			this.verboseJsonPropertyAndValueSerializer = new ODataVerboseJsonPropertyAndValueSerializer(this.verboseJsonOutputContext);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x000349C0 File Offset: 0x00032BC0
		protected override void VerifyNotDisposed()
		{
			this.verboseJsonOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x000349CD File Offset: 0x00032BCD
		protected override void FlushSynchronously()
		{
			this.verboseJsonOutputContext.Flush();
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x000349DA File Offset: 0x00032BDA
		protected override Task FlushAsynchronously()
		{
			return this.verboseJsonOutputContext.FlushAsync();
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000349E7 File Offset: 0x00032BE7
		protected override void StartPayload()
		{
			this.verboseJsonPropertyAndValueSerializer.WritePayloadStart();
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00034A04 File Offset: 0x00032C04
		protected override void EndPayload()
		{
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
			this.verboseJsonPropertyAndValueSerializer.WritePayloadEnd();
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00034A24 File Offset: 0x00032C24
		protected override void WriteValueParameter(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference)
		{
			this.verboseJsonOutputContext.JsonWriter.WriteName(parameterName);
			if (parameterValue == null)
			{
				this.verboseJsonOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			ODataComplexValue odataComplexValue = parameterValue as ODataComplexValue;
			if (odataComplexValue != null)
			{
				this.verboseJsonPropertyAndValueSerializer.WriteComplexValue(odataComplexValue, expectedTypeReference, false, base.DuplicatePropertyNamesChecker, null);
				base.DuplicatePropertyNamesChecker.Clear();
				return;
			}
			this.verboseJsonPropertyAndValueSerializer.WritePrimitiveValue(parameterValue, null, expectedTypeReference);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00034A90 File Offset: 0x00032C90
		protected override ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.verboseJsonOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataVerboseJsonCollectionWriter(this.verboseJsonOutputContext, expectedItemType, this);
		}

		// Token: 0x0400051F RID: 1311
		private readonly ODataVerboseJsonOutputContext verboseJsonOutputContext;

		// Token: 0x04000520 RID: 1312
		private readonly ODataVerboseJsonPropertyAndValueSerializer verboseJsonPropertyAndValueSerializer;
	}
}
