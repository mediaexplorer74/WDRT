using System;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200019A RID: 410
	internal sealed class ODataJsonLightParameterWriter : ODataParameterWriterCore
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x0002AE64 File Offset: 0x00029064
		internal ODataJsonLightParameterWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmFunctionImport functionImport)
			: base(jsonLightOutputContext, functionImport)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightValueSerializer = new ODataJsonLightValueSerializer(this.jsonLightOutputContext);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002AE86 File Offset: 0x00029086
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002AE93 File Offset: 0x00029093
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002AEA0 File Offset: 0x000290A0
		protected override Task FlushAsynchronously()
		{
			return this.jsonLightOutputContext.FlushAsync();
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002AEAD File Offset: 0x000290AD
		protected override void StartPayload()
		{
			this.jsonLightValueSerializer.WritePayloadStart();
			this.jsonLightOutputContext.JsonWriter.StartObjectScope();
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002AECA File Offset: 0x000290CA
		protected override void EndPayload()
		{
			this.jsonLightOutputContext.JsonWriter.EndObjectScope();
			this.jsonLightValueSerializer.WritePayloadEnd();
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002AEE8 File Offset: 0x000290E8
		protected override void WriteValueParameter(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			if (parameterValue == null)
			{
				this.jsonLightOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			ODataComplexValue odataComplexValue = parameterValue as ODataComplexValue;
			if (odataComplexValue != null)
			{
				this.jsonLightValueSerializer.WriteComplexValue(odataComplexValue, expectedTypeReference, false, false, base.DuplicatePropertyNamesChecker);
				base.DuplicatePropertyNamesChecker.Clear();
				return;
			}
			this.jsonLightValueSerializer.WritePrimitiveValue(parameterValue, expectedTypeReference);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002AF53 File Offset: 0x00029153
		protected override ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataJsonLightCollectionWriter(this.jsonLightOutputContext, expectedItemType, this);
		}

		// Token: 0x0400043F RID: 1087
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000440 RID: 1088
		private readonly ODataJsonLightValueSerializer jsonLightValueSerializer;
	}
}
