using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001E3 RID: 483
	internal sealed class ODataVerboseJsonParameterReader : ODataParameterReaderCore
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x00035674 File Offset: 0x00033874
		internal ODataVerboseJsonParameterReader(ODataVerboseJsonInputContext verboseJsonInputContext, IEdmFunctionImport functionImport)
			: base(verboseJsonInputContext, functionImport)
		{
			this.verboseJsonInputContext = verboseJsonInputContext;
			this.verboseJsonPropertyAndValueDeserializer = new ODataVerboseJsonPropertyAndValueDeserializer(verboseJsonInputContext);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00035694 File Offset: 0x00033894
		protected override bool ReadAtStartImplementation()
		{
			this.verboseJsonPropertyAndValueDeserializer.ReadPayloadStart(false);
			if (this.verboseJsonPropertyAndValueDeserializer.JsonReader.NodeType == JsonNodeType.EndOfInput)
			{
				base.PopScope(ODataParameterReaderState.Start);
				base.EnterScope(ODataParameterReaderState.Completed, null, null);
				return false;
			}
			this.verboseJsonPropertyAndValueDeserializer.JsonReader.ReadStartObject();
			if (this.EndOfParameters())
			{
				this.ReadParametersEnd();
				return false;
			}
			this.ReadNextParameter();
			return true;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x000356F9 File Offset: 0x000338F9
		protected override bool ReadNextParameterImplementation()
		{
			base.PopScope(this.State);
			if (this.EndOfParameters())
			{
				this.ReadParametersEnd();
				return false;
			}
			this.ReadNextParameter();
			return true;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0003571E File Offset: 0x0003391E
		protected override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataVerboseJsonCollectionReader(this.verboseJsonInputContext, expectedItemTypeReference, this);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003572D File Offset: 0x0003392D
		private bool EndOfParameters()
		{
			return this.verboseJsonPropertyAndValueDeserializer.JsonReader.NodeType == JsonNodeType.EndObject;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00035742 File Offset: 0x00033942
		private void ReadParametersEnd()
		{
			this.verboseJsonPropertyAndValueDeserializer.JsonReader.ReadEndObject();
			this.verboseJsonPropertyAndValueDeserializer.ReadPayloadEnd(false);
			base.PopScope(ODataParameterReaderState.Start);
			base.EnterScope(ODataParameterReaderState.Completed, null, null);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00035770 File Offset: 0x00033970
		private void ReadNextParameter()
		{
			string text = this.verboseJsonPropertyAndValueDeserializer.JsonReader.ReadPropertyName();
			IEdmTypeReference parameterTypeReference = base.GetParameterTypeReference(text);
			object obj;
			ODataParameterReaderState odataParameterReaderState;
			switch (parameterTypeReference.TypeKind())
			{
			case EdmTypeKind.Primitive:
			{
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = parameterTypeReference.AsPrimitive();
				if (edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Stream)
				{
					throw new ODataException(Strings.ODataJsonParameterReader_UnsupportedPrimitiveParameterType(text, edmPrimitiveTypeReference.PrimitiveKind()));
				}
				obj = this.verboseJsonPropertyAndValueDeserializer.ReadNonEntityValue(edmPrimitiveTypeReference, null, null, true, text);
				odataParameterReaderState = ODataParameterReaderState.Value;
				goto IL_122;
			}
			case EdmTypeKind.Complex:
				obj = this.verboseJsonPropertyAndValueDeserializer.ReadNonEntityValue(parameterTypeReference, null, null, true, text);
				odataParameterReaderState = ODataParameterReaderState.Value;
				goto IL_122;
			case EdmTypeKind.Collection:
				obj = null;
				if (this.verboseJsonPropertyAndValueDeserializer.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
				{
					obj = this.verboseJsonPropertyAndValueDeserializer.JsonReader.ReadPrimitiveValue();
					if (obj != null)
					{
						throw new ODataException(Strings.ODataJsonParameterReader_NullCollectionExpected(JsonNodeType.PrimitiveValue, obj));
					}
					odataParameterReaderState = ODataParameterReaderState.Value;
					goto IL_122;
				}
				else
				{
					if (((IEdmCollectionType)parameterTypeReference.Definition).ElementType.TypeKind() == EdmTypeKind.Entity)
					{
						throw new ODataException(Strings.ODataJsonParameterReader_UnsupportedParameterTypeKind(text, "Entity Collection"));
					}
					odataParameterReaderState = ODataParameterReaderState.Collection;
					goto IL_122;
				}
				break;
			}
			throw new ODataException(Strings.ODataJsonParameterReader_UnsupportedParameterTypeKind(text, parameterTypeReference.TypeKind()));
			IL_122:
			base.EnterScope(odataParameterReaderState, text, obj);
		}

		// Token: 0x04000526 RID: 1318
		private readonly ODataVerboseJsonInputContext verboseJsonInputContext;

		// Token: 0x04000527 RID: 1319
		private readonly ODataVerboseJsonPropertyAndValueDeserializer verboseJsonPropertyAndValueDeserializer;
	}
}
