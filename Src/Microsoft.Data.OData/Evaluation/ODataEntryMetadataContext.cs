using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000105 RID: 261
	internal abstract class ODataEntryMetadataContext : IODataEntryMetadataContext
	{
		// Token: 0x06000708 RID: 1800 RVA: 0x00018432 File Offset: 0x00016632
		protected ODataEntryMetadataContext(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext)
		{
			this.entry = entry;
			this.typeContext = typeContext;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00018448 File Offset: 0x00016648
		public ODataEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00018450 File Offset: 0x00016650
		public IODataFeedAndEntryTypeContext TypeContext
		{
			get
			{
				return this.typeContext;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600070B RID: 1803
		public abstract string ActualEntityTypeName { get; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600070C RID: 1804
		public abstract ICollection<KeyValuePair<string, object>> KeyProperties { get; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600070D RID: 1805
		public abstract IEnumerable<KeyValuePair<string, object>> ETagProperties { get; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600070E RID: 1806
		public abstract IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties { get; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600070F RID: 1807
		public abstract IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties { get; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000710 RID: 1808
		public abstract IEnumerable<IEdmFunctionImport> SelectedAlwaysBindableOperations { get; }

		// Token: 0x06000711 RID: 1809 RVA: 0x00018458 File Offset: 0x00016658
		internal static ODataEntryMetadataContext Create(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, IODataMetadataContext metadataContext, SelectedPropertiesNode selectedProperties)
		{
			if (serializationInfo != null)
			{
				return new ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel(entry, typeContext, serializationInfo);
			}
			return new ODataEntryMetadataContext.ODataEntryMetadataContextWithModel(entry, typeContext, actualEntityType, metadataContext, selectedProperties);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00018490 File Offset: 0x00016690
		private static object GetPrimitivePropertyClrValue(ODataEntry entry, string propertyName, string entityTypeName, bool isKeyProperty)
		{
			ODataProperty odataProperty = ((entry.NonComputedProperties == null) ? null : entry.NonComputedProperties.SingleOrDefault((ODataProperty p) => p.Name == propertyName));
			if (odataProperty == null)
			{
				throw new ODataException(Strings.EdmValueUtils_PropertyDoesntExist(entry.TypeName, propertyName));
			}
			return ODataEntryMetadataContext.GetPrimitivePropertyClrValue(entityTypeName, odataProperty, isKeyProperty);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000184F0 File Offset: 0x000166F0
		private static object GetPrimitivePropertyClrValue(string entityTypeName, ODataProperty property, bool isKeyProperty)
		{
			object value = property.Value;
			if (value == null && isKeyProperty)
			{
				throw new ODataException(Strings.ODataEntryMetadataContext_NullKeyValue(property.Name, entityTypeName));
			}
			if (value is ODataValue)
			{
				throw new ODataException(Strings.ODataEntryMetadataContext_KeyOrETagValuesMustBePrimitiveValues(property.Name, entityTypeName));
			}
			return value;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00018537 File Offset: 0x00016737
		private static void ValidateEntityTypeHasKeyProperties(KeyValuePair<string, object>[] keyProperties, string actualEntityTypeName)
		{
			if (keyProperties == null || keyProperties.Length == 0)
			{
				throw new ODataException(Strings.ODataEntryMetadataContext_EntityTypeWithNoKeyProperties(actualEntityTypeName));
			}
		}

		// Token: 0x040002A8 RID: 680
		private static readonly KeyValuePair<string, object>[] EmptyProperties = new KeyValuePair<string, object>[0];

		// Token: 0x040002A9 RID: 681
		private readonly ODataEntry entry;

		// Token: 0x040002AA RID: 682
		private readonly IODataFeedAndEntryTypeContext typeContext;

		// Token: 0x040002AB RID: 683
		private KeyValuePair<string, object>[] keyProperties;

		// Token: 0x040002AC RID: 684
		private IEnumerable<KeyValuePair<string, object>> etagProperties;

		// Token: 0x040002AD RID: 685
		private IEnumerable<IEdmNavigationProperty> selectedNavigationProperties;

		// Token: 0x040002AE RID: 686
		private IDictionary<string, IEdmStructuralProperty> selectedStreamProperties;

		// Token: 0x040002AF RID: 687
		private IEnumerable<IEdmFunctionImport> selectedAlwaysBindableOperations;

		// Token: 0x02000106 RID: 262
		private sealed class ODataEntryMetadataContextWithoutModel : ODataEntryMetadataContext
		{
			// Token: 0x06000716 RID: 1814 RVA: 0x0001855A File Offset: 0x0001675A
			internal ODataEntryMetadataContextWithoutModel(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo)
				: base(entry, typeContext)
			{
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001856B File Offset: 0x0001676B
			public override ICollection<KeyValuePair<string, object>> KeyProperties
			{
				get
				{
					if (this.keyProperties == null)
					{
						this.keyProperties = ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.GetPropertiesBySerializationInfoPropertyKind(this.entry, ODataPropertyKind.Key, this.ActualEntityTypeName);
						ODataEntryMetadataContext.ValidateEntityTypeHasKeyProperties(this.keyProperties, this.ActualEntityTypeName);
					}
					return this.keyProperties;
				}
			}

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x06000718 RID: 1816 RVA: 0x000185A4 File Offset: 0x000167A4
			public override IEnumerable<KeyValuePair<string, object>> ETagProperties
			{
				get
				{
					IEnumerable<KeyValuePair<string, object>> enumerable;
					if ((enumerable = this.etagProperties) == null)
					{
						enumerable = (this.etagProperties = ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.GetPropertiesBySerializationInfoPropertyKind(this.entry, ODataPropertyKind.ETag, this.ActualEntityTypeName));
					}
					return enumerable;
				}
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x06000719 RID: 1817 RVA: 0x000185D6 File Offset: 0x000167D6
			public override string ActualEntityTypeName
			{
				get
				{
					if (string.IsNullOrEmpty(base.Entry.TypeName))
					{
						throw new ODataException(Strings.ODataFeedAndEntryTypeContext_ODataEntryTypeNameMissing);
					}
					return base.Entry.TypeName;
				}
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x0600071A RID: 1818 RVA: 0x00018600 File Offset: 0x00016800
			public override IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties
			{
				get
				{
					return ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.EmptyNavigationProperties;
				}
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x0600071B RID: 1819 RVA: 0x00018607 File Offset: 0x00016807
			public override IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties
			{
				get
				{
					return ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.EmptyStreamProperties;
				}
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001860E File Offset: 0x0001680E
			public override IEnumerable<IEdmFunctionImport> SelectedAlwaysBindableOperations
			{
				get
				{
					return ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.EmptyOperations;
				}
			}

			// Token: 0x0600071D RID: 1821 RVA: 0x00018660 File Offset: 0x00016860
			private static KeyValuePair<string, object>[] GetPropertiesBySerializationInfoPropertyKind(ODataEntry entry, ODataPropertyKind propertyKind, string actualEntityTypeName)
			{
				KeyValuePair<string, object>[] array = ODataEntryMetadataContext.EmptyProperties;
				if (entry.NonComputedProperties != null)
				{
					array = (from p in entry.NonComputedProperties
						where p.SerializationInfo != null && p.SerializationInfo.PropertyKind == propertyKind
						select new KeyValuePair<string, object>(p.Name, ODataEntryMetadataContext.GetPrimitivePropertyClrValue(actualEntityTypeName, p, propertyKind == ODataPropertyKind.Key))).ToArray<KeyValuePair<string, object>>();
				}
				return array;
			}

			// Token: 0x040002B0 RID: 688
			private static readonly IEdmNavigationProperty[] EmptyNavigationProperties = new IEdmNavigationProperty[0];

			// Token: 0x040002B1 RID: 689
			private static readonly Dictionary<string, IEdmStructuralProperty> EmptyStreamProperties = new Dictionary<string, IEdmStructuralProperty>(StringComparer.Ordinal);

			// Token: 0x040002B2 RID: 690
			private static readonly IEdmFunctionImport[] EmptyOperations = new IEdmFunctionImport[0];

			// Token: 0x040002B3 RID: 691
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;
		}

		// Token: 0x02000107 RID: 263
		private sealed class ODataEntryMetadataContextWithModel : ODataEntryMetadataContext
		{
			// Token: 0x0600071F RID: 1823 RVA: 0x000186F3 File Offset: 0x000168F3
			internal ODataEntryMetadataContextWithModel(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, IEdmEntityType actualEntityType, IODataMetadataContext metadataContext, SelectedPropertiesNode selectedProperties)
				: base(entry, typeContext)
			{
				this.actualEntityType = actualEntityType;
				this.metadataContext = metadataContext;
				this.selectedProperties = selectedProperties;
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001873C File Offset: 0x0001693C
			public override ICollection<KeyValuePair<string, object>> KeyProperties
			{
				get
				{
					if (this.keyProperties == null)
					{
						IEnumerable<IEdmStructuralProperty> enumerable = this.actualEntityType.Key();
						if (enumerable != null)
						{
							this.keyProperties = enumerable.Select((IEdmStructuralProperty p) => new KeyValuePair<string, object>(p.Name, ODataEntryMetadataContext.GetPrimitivePropertyClrValue(this.entry, p.Name, this.ActualEntityTypeName, true))).ToArray<KeyValuePair<string, object>>();
						}
						ODataEntryMetadataContext.ValidateEntityTypeHasKeyProperties(this.keyProperties, this.ActualEntityTypeName);
					}
					return this.keyProperties;
				}
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000721 RID: 1825 RVA: 0x000187CC File Offset: 0x000169CC
			public override IEnumerable<KeyValuePair<string, object>> ETagProperties
			{
				get
				{
					if (this.etagProperties == null)
					{
						IEnumerable<IEdmStructuralProperty> enumerable = this.actualEntityType.StructuralProperties();
						IEnumerable<KeyValuePair<string, object>> enumerable2;
						if (enumerable == null)
						{
							enumerable2 = ODataEntryMetadataContext.EmptyProperties;
						}
						else
						{
							enumerable2 = (from p in enumerable
								where p.ConcurrencyMode == EdmConcurrencyMode.Fixed
								select new KeyValuePair<string, object>(p.Name, ODataEntryMetadataContext.GetPrimitivePropertyClrValue(this.entry, p.Name, this.ActualEntityTypeName, false))).ToArray<KeyValuePair<string, object>>();
						}
						this.etagProperties = enumerable2;
					}
					return this.etagProperties;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x06000722 RID: 1826 RVA: 0x00018843 File Offset: 0x00016A43
			public override string ActualEntityTypeName
			{
				get
				{
					return this.actualEntityType.FullName();
				}
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x06000723 RID: 1827 RVA: 0x00018850 File Offset: 0x00016A50
			public override IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties
			{
				get
				{
					IEnumerable<IEdmNavigationProperty> enumerable;
					if ((enumerable = this.selectedNavigationProperties) == null)
					{
						enumerable = (this.selectedNavigationProperties = this.selectedProperties.GetSelectedNavigationProperties(this.actualEntityType));
					}
					return enumerable;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06000724 RID: 1828 RVA: 0x00018884 File Offset: 0x00016A84
			public override IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties
			{
				get
				{
					IDictionary<string, IEdmStructuralProperty> dictionary;
					if ((dictionary = this.selectedStreamProperties) == null)
					{
						dictionary = (this.selectedStreamProperties = this.selectedProperties.GetSelectedStreamProperties(this.actualEntityType));
					}
					return dictionary;
				}
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000725 RID: 1829 RVA: 0x000188E4 File Offset: 0x00016AE4
			public override IEnumerable<IEdmFunctionImport> SelectedAlwaysBindableOperations
			{
				get
				{
					if (this.selectedAlwaysBindableOperations == null)
					{
						bool mustBeContainerQualified = this.metadataContext.OperationsBoundToEntityTypeMustBeContainerQualified(this.actualEntityType);
						this.selectedAlwaysBindableOperations = (from operation in this.metadataContext.GetAlwaysBindableOperationsForType(this.actualEntityType)
							where this.selectedProperties.IsOperationSelected(this.actualEntityType, operation, mustBeContainerQualified)
							select operation).ToArray<IEdmFunctionImport>();
					}
					return this.selectedAlwaysBindableOperations;
				}
			}

			// Token: 0x040002B4 RID: 692
			private readonly IEdmEntityType actualEntityType;

			// Token: 0x040002B5 RID: 693
			private readonly IODataMetadataContext metadataContext;

			// Token: 0x040002B6 RID: 694
			private readonly SelectedPropertiesNode selectedProperties;
		}
	}
}
