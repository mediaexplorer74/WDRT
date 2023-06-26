using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001F6 RID: 502
	internal abstract class EpmReader
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x00037178 File Offset: 0x00035378
		protected EpmReader(IODataAtomReaderEntryState entryState, ODataAtomInputContext inputContext)
		{
			this.entryState = entryState;
			this.atomInputContext = inputContext;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0003718E File Offset: 0x0003538E
		protected IODataAtomReaderEntryState EntryState
		{
			get
			{
				return this.entryState;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00037196 File Offset: 0x00035396
		protected ODataVersion Version
		{
			get
			{
				return this.atomInputContext.Version;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x000371A3 File Offset: 0x000353A3
		protected ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.atomInputContext.MessageReaderSettings;
			}
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x000371B0 File Offset: 0x000353B0
		protected void SetEntryEpmValue(EntityPropertyMappingInfo epmInfo, object propertyValue)
		{
			this.SetEpmValue(this.entryState.Entry.Properties.ToReadOnlyEnumerable("Properties"), this.entryState.EntityType.ToTypeReference(), epmInfo, propertyValue);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x000371E4 File Offset: 0x000353E4
		protected void SetEpmValue(ReadOnlyEnumerable<ODataProperty> targetList, IEdmTypeReference targetTypeReference, EntityPropertyMappingInfo epmInfo, object propertyValue)
		{
			this.SetEpmValueForSegment(epmInfo, 0, targetTypeReference.AsStructuredOrNull(), targetList, propertyValue);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00037218 File Offset: 0x00035418
		private void SetEpmValueForSegment(EntityPropertyMappingInfo epmInfo, int propertyValuePathIndex, IEdmStructuredTypeReference segmentStructuralTypeReference, ReadOnlyEnumerable<ODataProperty> existingProperties, object propertyValue)
		{
			string propertyName = epmInfo.PropertyValuePath[propertyValuePathIndex].PropertyName;
			if (epmInfo.Attribute.KeepInContent)
			{
				return;
			}
			ODataProperty odataProperty = existingProperties.FirstOrDefault((ODataProperty p) => string.CompareOrdinal(p.Name, propertyName) == 0);
			ODataComplexValue odataComplexValue = null;
			if (odataProperty != null)
			{
				odataComplexValue = odataProperty.Value as ODataComplexValue;
				if (odataComplexValue == null)
				{
					return;
				}
			}
			IEdmProperty edmProperty = segmentStructuralTypeReference.FindProperty(propertyName);
			if (edmProperty == null && propertyValuePathIndex != epmInfo.PropertyValuePath.Length - 1)
			{
				throw new ODataException(Strings.EpmReader_OpenComplexOrCollectionEpmProperty(epmInfo.Attribute.SourcePath));
			}
			IEdmTypeReference edmTypeReference;
			if (edmProperty == null || (this.MessageReaderSettings.DisablePrimitiveTypeConversion && edmProperty.Type.IsODataPrimitiveTypeKind()))
			{
				edmTypeReference = EdmCoreModel.Instance.GetString(true);
			}
			else
			{
				edmTypeReference = edmProperty.Type;
			}
			switch (edmTypeReference.TypeKind())
			{
			case EdmTypeKind.Primitive:
			{
				if (edmTypeReference.IsStream())
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmReader_SetEpmValueForSegment_StreamProperty));
				}
				object obj;
				if (propertyValue == null)
				{
					ReaderValidationUtils.ValidateNullValue(this.atomInputContext.Model, edmTypeReference, this.atomInputContext.MessageReaderSettings, true, this.atomInputContext.Version, propertyName);
					obj = null;
				}
				else
				{
					obj = AtomValueUtils.ConvertStringToPrimitive((string)propertyValue, edmTypeReference.AsPrimitive());
				}
				this.AddEpmPropertyValue(existingProperties, propertyName, obj, segmentStructuralTypeReference.IsODataEntityTypeKind());
				return;
			}
			case EdmTypeKind.Complex:
			{
				if (odataComplexValue == null)
				{
					odataComplexValue = new ODataComplexValue
					{
						TypeName = edmTypeReference.ODataFullName(),
						Properties = new ReadOnlyEnumerable<ODataProperty>()
					};
					this.AddEpmPropertyValue(existingProperties, propertyName, odataComplexValue, segmentStructuralTypeReference.IsODataEntityTypeKind());
				}
				IEdmComplexTypeReference edmComplexTypeReference = edmTypeReference.AsComplex();
				this.SetEpmValueForSegment(epmInfo, propertyValuePathIndex + 1, edmComplexTypeReference, odataComplexValue.Properties.ToReadOnlyEnumerable("Properties"), propertyValue);
				return;
			}
			case EdmTypeKind.Collection:
			{
				ODataCollectionValue odataCollectionValue = new ODataCollectionValue
				{
					TypeName = edmTypeReference.ODataFullName(),
					Items = new ReadOnlyEnumerable((List<object>)propertyValue)
				};
				this.AddEpmPropertyValue(existingProperties, propertyName, odataCollectionValue, segmentStructuralTypeReference.IsODataEntityTypeKind());
				return;
			}
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmReader_SetEpmValueForSegment_TypeKind));
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00037440 File Offset: 0x00035640
		private void AddEpmPropertyValue(ReadOnlyEnumerable<ODataProperty> properties, string propertyName, object propertyValue, bool checkDuplicateEntryPropertyNames)
		{
			ODataProperty odataProperty = new ODataProperty
			{
				Name = propertyName,
				Value = propertyValue
			};
			if (checkDuplicateEntryPropertyNames)
			{
				this.entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
			}
			properties.AddToSourceList(odataProperty);
		}

		// Token: 0x04000572 RID: 1394
		private readonly ODataAtomInputContext atomInputContext;

		// Token: 0x04000573 RID: 1395
		private readonly IODataAtomReaderEntryState entryState;
	}
}
