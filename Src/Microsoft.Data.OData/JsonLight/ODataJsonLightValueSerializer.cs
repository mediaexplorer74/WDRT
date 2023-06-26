using System;
using System.Collections;
using System.Diagnostics;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000146 RID: 326
	internal class ODataJsonLightValueSerializer : ODataJsonLightSerializer, IODataJsonLightValueSerializer
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x0001C31A File Offset: 0x0001A51A
		internal ODataJsonLightValueSerializer(ODataJsonLightPropertySerializer propertySerializer)
			: base(propertySerializer.JsonLightOutputContext)
		{
			this.propertySerializer = propertySerializer;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001C32F File Offset: 0x0001A52F
		internal ODataJsonLightValueSerializer(ODataJsonLightOutputContext outputContext)
			: base(outputContext)
		{
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0001C338 File Offset: 0x0001A538
		IJsonWriter IODataJsonLightValueSerializer.JsonWriter
		{
			get
			{
				return base.JsonWriter;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0001C340 File Offset: 0x0001A540
		ODataVersion IODataJsonLightValueSerializer.Version
		{
			get
			{
				return base.Version;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0001C348 File Offset: 0x0001A548
		IEdmModel IODataJsonLightValueSerializer.Model
		{
			get
			{
				return base.Model;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001C350 File Offset: 0x0001A550
		ODataMessageWriterSettings IODataJsonLightValueSerializer.Settings
		{
			get
			{
				return base.JsonLightOutputContext.MessageWriterSettings;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0001C35D File Offset: 0x0001A55D
		private ODataJsonLightPropertySerializer PropertySerializer
		{
			get
			{
				if (this.propertySerializer == null)
				{
					this.propertySerializer = new ODataJsonLightPropertySerializer(base.JsonLightOutputContext);
				}
				return this.propertySerializer;
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001C37E File Offset: 0x0001A57E
		public void WriteNullValue()
		{
			base.JsonWriter.WriteValue(null);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001C38C File Offset: 0x0001A58C
		public void WriteComplexValue(ODataComplexValue complexValue, IEdmTypeReference metadataTypeReference, bool isTopLevel, bool isOpenPropertyType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			this.IncreaseRecursionDepth();
			if (!isTopLevel)
			{
				base.JsonWriter.StartObjectScope();
			}
			string text = complexValue.TypeName;
			if (isTopLevel)
			{
				if (text == null)
				{
					throw new ODataException(Strings.ODataJsonLightValueSerializer_MissingTypeNameOnComplex);
				}
			}
			else if (metadataTypeReference == null && !base.WritingResponse && text == null && base.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForComplexValueRequest);
			}
			IEdmComplexTypeReference edmComplexTypeReference = (IEdmComplexTypeReference)TypeNameOracle.ResolveAndValidateTypeNameForValue(base.Model, metadataTypeReference, complexValue, isOpenPropertyType);
			text = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(complexValue, metadataTypeReference, edmComplexTypeReference, isOpenPropertyType);
			if (text != null)
			{
				ODataJsonLightWriterUtils.WriteODataTypeInstanceAnnotation(base.JsonWriter, text);
			}
			this.PropertySerializer.WriteProperties((edmComplexTypeReference == null) ? null : edmComplexTypeReference.ComplexDefinition(), complexValue.Properties, true, duplicatePropertyNamesChecker, null);
			if (!isTopLevel)
			{
				base.JsonWriter.EndObjectScope();
			}
			this.DecreaseRecursionDepth();
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001C45C File Offset: 0x0001A65C
		public void WriteCollectionValue(ODataCollectionValue collectionValue, IEdmTypeReference metadataTypeReference, bool isTopLevelProperty, bool isInUri, bool isOpenPropertyType)
		{
			this.IncreaseRecursionDepth();
			string text = collectionValue.TypeName;
			if (isTopLevelProperty)
			{
				if (text == null)
				{
					throw new ODataException(Strings.ODataJsonLightValueSerializer_MissingTypeNameOnCollection);
				}
			}
			else if (metadataTypeReference == null && !base.WritingResponse && text == null && base.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest);
			}
			IEdmCollectionTypeReference edmCollectionTypeReference = (IEdmCollectionTypeReference)TypeNameOracle.ResolveAndValidateTypeNameForValue(base.Model, metadataTypeReference, collectionValue, isOpenPropertyType);
			text = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(collectionValue, metadataTypeReference, edmCollectionTypeReference, isOpenPropertyType);
			bool flag = isInUri && !string.IsNullOrEmpty(text);
			if (flag)
			{
				base.JsonWriter.StartObjectScope();
				ODataJsonLightWriterUtils.WriteODataTypeInstanceAnnotation(base.JsonWriter, text);
				base.JsonWriter.WriteValuePropertyName();
			}
			base.JsonWriter.StartArrayScope();
			IEnumerable items = collectionValue.Items;
			if (items != null)
			{
				IEdmTypeReference edmTypeReference = ((edmCollectionTypeReference == null) ? null : edmCollectionTypeReference.ElementType());
				DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = null;
				foreach (object obj in items)
				{
					ValidationUtils.ValidateCollectionItem(obj, false);
					ODataComplexValue odataComplexValue = obj as ODataComplexValue;
					if (odataComplexValue != null)
					{
						if (duplicatePropertyNamesChecker == null)
						{
							duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
						}
						this.WriteComplexValue(odataComplexValue, edmTypeReference, false, false, duplicatePropertyNamesChecker);
						duplicatePropertyNamesChecker.Clear();
					}
					else
					{
						this.WritePrimitiveValue(obj, edmTypeReference);
					}
				}
			}
			base.JsonWriter.EndArrayScope();
			if (flag)
			{
				base.JsonWriter.EndObjectScope();
			}
			this.DecreaseRecursionDepth();
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001C5DC File Offset: 0x0001A7DC
		public void WritePrimitiveValue(object value, IEdmTypeReference expectedTypeReference)
		{
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(value.GetType());
			if (expectedTypeReference != null)
			{
				ValidationUtils.ValidateIsExpectedPrimitiveType(value, primitiveTypeReference, expectedTypeReference);
			}
			if (primitiveTypeReference != null && primitiveTypeReference.IsSpatial())
			{
				PrimitiveConverter.Instance.WriteJsonLight(value, base.JsonWriter, base.Version);
				return;
			}
			base.JsonWriter.WritePrimitiveValue(value, base.Version);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001C635 File Offset: 0x0001A835
		DuplicatePropertyNamesChecker IODataJsonLightValueSerializer.CreateDuplicatePropertyNamesChecker()
		{
			return base.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001C63D File Offset: 0x0001A83D
		[Conditional("DEBUG")]
		internal void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001C63F File Offset: 0x0001A83F
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001C65C File Offset: 0x0001A85C
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x04000353 RID: 851
		private int recursionDepth;

		// Token: 0x04000354 RID: 852
		private ODataJsonLightPropertySerializer propertySerializer;
	}
}
