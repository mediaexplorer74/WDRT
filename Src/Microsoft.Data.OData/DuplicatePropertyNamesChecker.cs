using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData
{
	// Token: 0x0200022E RID: 558
	internal sealed class DuplicatePropertyNamesChecker
	{
		// Token: 0x060011C0 RID: 4544 RVA: 0x0004234E File Offset: 0x0004054E
		public DuplicatePropertyNamesChecker(bool allowDuplicateProperties, bool isResponse)
		{
			this.allowDuplicateProperties = allowDuplicateProperties;
			this.isResponse = isResponse;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0004236F File Offset: 0x0004056F
		public DuplicatePropertyNamesChecker.PropertyAnnotationCollector AnnotationCollector
		{
			get
			{
				return this.annotationCollector;
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00042378 File Offset: 0x00040578
		internal void CheckForDuplicatePropertyNames(ODataProperty property)
		{
			string name = property.Name;
			DuplicatePropertyNamesChecker.DuplicationKind duplicationKind = DuplicatePropertyNamesChecker.GetDuplicationKind(property);
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(name, out duplicationRecord))
			{
				this.propertyNameCache.Add(name, new DuplicatePropertyNamesChecker.DuplicationRecord(duplicationKind));
				return;
			}
			if (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen)
			{
				duplicationRecord.DuplicationKind = duplicationKind;
				return;
			}
			if (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.Prohibited || duplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.Prohibited || (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && duplicationRecord.AssociationLink != null) || !this.allowDuplicateProperties)
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(name));
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000423F4 File Offset: 0x000405F4
		internal void CheckForDuplicatePropertyNamesOnNavigationLinkStart(ODataNavigationLink navigationLink)
		{
			string name = navigationLink.Name;
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (this.propertyNameCache != null && this.propertyNameCache.TryGetValue(name, out duplicationRecord))
			{
				this.CheckNavigationLinkDuplicateNameForExistingDuplicationRecord(name, duplicationRecord);
			}
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00042428 File Offset: 0x00040628
		internal ODataAssociationLink CheckForDuplicatePropertyNames(ODataNavigationLink navigationLink, bool isExpanded, bool? isCollection)
		{
			string name = navigationLink.Name;
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(name, out duplicationRecord))
			{
				DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord2 = new DuplicatePropertyNamesChecker.DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty);
				DuplicatePropertyNamesChecker.ApplyNavigationLinkToDuplicationRecord(duplicationRecord2, navigationLink, isExpanded, isCollection);
				this.propertyNameCache.Add(name, duplicationRecord2);
				return null;
			}
			this.CheckNavigationLinkDuplicateNameForExistingDuplicationRecord(name, duplicationRecord);
			if (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen || (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && duplicationRecord.AssociationLink != null && duplicationRecord.NavigationLink == null))
			{
				DuplicatePropertyNamesChecker.ApplyNavigationLinkToDuplicationRecord(duplicationRecord, navigationLink, isExpanded, isCollection);
			}
			else if (this.allowDuplicateProperties)
			{
				duplicationRecord.DuplicationKind = DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty;
				DuplicatePropertyNamesChecker.ApplyNavigationLinkToDuplicationRecord(duplicationRecord, navigationLink, isExpanded, isCollection);
			}
			else
			{
				bool? isCollectionEffectiveValue = DuplicatePropertyNamesChecker.GetIsCollectionEffectiveValue(isExpanded, isCollection);
				if (isCollectionEffectiveValue == false || duplicationRecord.NavigationPropertyIsCollection == false)
				{
					throw new ODataException(Strings.DuplicatePropertyNamesChecker_MultipleLinksForSingleton(name));
				}
				if (isCollectionEffectiveValue != null)
				{
					duplicationRecord.NavigationPropertyIsCollection = isCollectionEffectiveValue;
				}
			}
			return duplicationRecord.AssociationLink;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00042514 File Offset: 0x00040714
		internal ODataNavigationLink CheckForDuplicateAssociationLinkNames(ODataAssociationLink associationLink)
		{
			string name = associationLink.Name;
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(name, out duplicationRecord))
			{
				this.propertyNameCache.Add(name, new DuplicatePropertyNamesChecker.DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty)
				{
					AssociationLink = associationLink
				});
				return null;
			}
			if (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen || (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && duplicationRecord.AssociationLink == null))
			{
				duplicationRecord.DuplicationKind = DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty;
				duplicationRecord.AssociationLink = associationLink;
				return duplicationRecord.NavigationLink;
			}
			throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(name));
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0004258B File Offset: 0x0004078B
		internal void Clear()
		{
			if (this.propertyNameCache != null)
			{
				this.propertyNameCache.Clear();
			}
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000425A0 File Offset: 0x000407A0
		internal void AddODataPropertyAnnotation(string propertyName, string annotationName, object annotationValue)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecordToAddPropertyAnnotation = this.GetDuplicationRecordToAddPropertyAnnotation(propertyName, annotationName);
			Dictionary<string, object> dictionary = duplicationRecordToAddPropertyAnnotation.PropertyODataAnnotations;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
				duplicationRecordToAddPropertyAnnotation.PropertyODataAnnotations = dictionary;
			}
			else if (dictionary.ContainsKey(annotationName))
			{
				if (ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName))
				{
					throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicateAnnotationForInstanceAnnotationNotAllowed(annotationName, propertyName));
				}
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicateAnnotationForPropertyNotAllowed(annotationName, propertyName));
			}
			dictionary.Add(annotationName, annotationValue);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00042608 File Offset: 0x00040808
		internal void AddCustomPropertyAnnotation(string propertyName, string annotationName)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecordToAddPropertyAnnotation = this.GetDuplicationRecordToAddPropertyAnnotation(propertyName, annotationName);
			HashSet<string> hashSet = duplicationRecordToAddPropertyAnnotation.PropertyCustomAnnotations;
			if (hashSet == null)
			{
				hashSet = new HashSet<string>(StringComparer.Ordinal);
				duplicationRecordToAddPropertyAnnotation.PropertyCustomAnnotations = hashSet;
			}
			else if (hashSet.Contains(annotationName))
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicateAnnotationForPropertyNotAllowed(annotationName, propertyName));
			}
			hashSet.Add(annotationName);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0004265C File Offset: 0x0004085C
		internal Dictionary<string, object> GetODataPropertyAnnotations(string propertyName)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(propertyName, out duplicationRecord))
			{
				return null;
			}
			DuplicatePropertyNamesChecker.ThrowIfPropertyIsProcessed(propertyName, duplicationRecord);
			return duplicationRecord.PropertyODataAnnotations;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00042684 File Offset: 0x00040884
		internal void MarkPropertyAsProcessed(string propertyName)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(propertyName, out duplicationRecord))
			{
				duplicationRecord = new DuplicatePropertyNamesChecker.DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen);
				this.propertyNameCache.Add(propertyName, duplicationRecord);
			}
			DuplicatePropertyNamesChecker.ThrowIfPropertyIsProcessed(propertyName, duplicationRecord);
			duplicationRecord.PropertyODataAnnotations = DuplicatePropertyNamesChecker.propertyAnnotationsProcessedToken;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000426CC File Offset: 0x000408CC
		internal IEnumerable<string> GetAllUnprocessedProperties()
		{
			if (this.propertyNameCache == null)
			{
				return Enumerable.Empty<string>();
			}
			return from property in this.propertyNameCache.Where(new Func<KeyValuePair<string, DuplicatePropertyNamesChecker.DuplicationRecord>, bool>(DuplicatePropertyNamesChecker.IsPropertyUnprocessed))
				select property.Key;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00042720 File Offset: 0x00040920
		private static void ThrowIfPropertyIsProcessed(string propertyName, DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord)
		{
			if (!object.ReferenceEquals(duplicationRecord.PropertyODataAnnotations, DuplicatePropertyNamesChecker.propertyAnnotationsProcessedToken))
			{
				return;
			}
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName) && !ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName))
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicateAnnotationNotAllowed(propertyName));
			}
			throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(propertyName));
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0004275C File Offset: 0x0004095C
		private static bool IsPropertyUnprocessed(KeyValuePair<string, DuplicatePropertyNamesChecker.DuplicationRecord> property)
		{
			return !string.IsNullOrEmpty(property.Key) && !object.ReferenceEquals(property.Value.PropertyODataAnnotations, DuplicatePropertyNamesChecker.propertyAnnotationsProcessedToken);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00042788 File Offset: 0x00040988
		private static DuplicatePropertyNamesChecker.DuplicationKind GetDuplicationKind(ODataProperty property)
		{
			object value = property.Value;
			if (value == null || (!(value is ODataStreamReferenceValue) && !(value is ODataCollectionValue)))
			{
				return DuplicatePropertyNamesChecker.DuplicationKind.PotentiallyAllowed;
			}
			return DuplicatePropertyNamesChecker.DuplicationKind.Prohibited;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000427B4 File Offset: 0x000409B4
		private static bool? GetIsCollectionEffectiveValue(bool isExpanded, bool? isCollection)
		{
			if (isExpanded)
			{
				return isCollection;
			}
			if (!(isCollection == true))
			{
				return null;
			}
			return new bool?(true);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000427ED File Offset: 0x000409ED
		private static void ApplyNavigationLinkToDuplicationRecord(DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord, ODataNavigationLink navigationLink, bool isExpanded, bool? isCollection)
		{
			duplicationRecord.DuplicationKind = DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty;
			duplicationRecord.NavigationLink = navigationLink;
			duplicationRecord.NavigationPropertyIsCollection = DuplicatePropertyNamesChecker.GetIsCollectionEffectiveValue(isExpanded, isCollection);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0004280A File Offset: 0x00040A0A
		private bool TryGetDuplicationRecord(string propertyName, out DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord)
		{
			if (this.propertyNameCache == null)
			{
				this.propertyNameCache = new Dictionary<string, DuplicatePropertyNamesChecker.DuplicationRecord>(StringComparer.Ordinal);
				duplicationRecord = null;
				return false;
			}
			return this.propertyNameCache.TryGetValue(propertyName, out duplicationRecord);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00042838 File Offset: 0x00040A38
		private void CheckNavigationLinkDuplicateNameForExistingDuplicationRecord(string propertyName, DuplicatePropertyNamesChecker.DuplicationRecord existingDuplicationRecord)
		{
			if (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && existingDuplicationRecord.AssociationLink != null && existingDuplicationRecord.NavigationLink == null)
			{
				return;
			}
			if (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.Prohibited || (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.PotentiallyAllowed && !this.allowDuplicateProperties) || (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && this.isResponse && !this.allowDuplicateProperties))
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(propertyName));
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000428A0 File Offset: 0x00040AA0
		private DuplicatePropertyNamesChecker.DuplicationRecord GetDuplicationRecordToAddPropertyAnnotation(string propertyName, string annotationName)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(propertyName, out duplicationRecord))
			{
				duplicationRecord = new DuplicatePropertyNamesChecker.DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen);
				this.propertyNameCache.Add(propertyName, duplicationRecord);
			}
			if (object.ReferenceEquals(duplicationRecord.PropertyODataAnnotations, DuplicatePropertyNamesChecker.propertyAnnotationsProcessedToken))
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_PropertyAnnotationAfterTheProperty(annotationName, propertyName));
			}
			return duplicationRecord;
		}

		// Token: 0x0400067C RID: 1660
		private static readonly Dictionary<string, object> propertyAnnotationsProcessedToken = new Dictionary<string, object>(0, StringComparer.Ordinal);

		// Token: 0x0400067D RID: 1661
		private readonly bool allowDuplicateProperties;

		// Token: 0x0400067E RID: 1662
		private readonly bool isResponse;

		// Token: 0x0400067F RID: 1663
		private Dictionary<string, DuplicatePropertyNamesChecker.DuplicationRecord> propertyNameCache;

		// Token: 0x04000680 RID: 1664
		private DuplicatePropertyNamesChecker.PropertyAnnotationCollector annotationCollector = new DuplicatePropertyNamesChecker.PropertyAnnotationCollector();

		// Token: 0x0200022F RID: 559
		private enum DuplicationKind
		{
			// Token: 0x04000683 RID: 1667
			PropertyAnnotationSeen,
			// Token: 0x04000684 RID: 1668
			Prohibited,
			// Token: 0x04000685 RID: 1669
			PotentiallyAllowed,
			// Token: 0x04000686 RID: 1670
			NavigationProperty
		}

		// Token: 0x02000230 RID: 560
		internal sealed class PropertyAnnotationCollector
		{
			// Token: 0x170003D7 RID: 983
			// (get) Token: 0x060011D6 RID: 4566 RVA: 0x000428FE File Offset: 0x00040AFE
			// (set) Token: 0x060011D7 RID: 4567 RVA: 0x00042906 File Offset: 0x00040B06
			public bool ShouldCollectAnnotation
			{
				get
				{
					return this.shouldCollectAnnotation;
				}
				set
				{
					this.shouldCollectAnnotation = value;
				}
			}

			// Token: 0x060011D8 RID: 4568 RVA: 0x0004290F File Offset: 0x00040B0F
			public void TryPeekAndCollectAnnotationRawJson(BufferingJsonReader jsonReader, string propertyName, string annotationName)
			{
				if (this.shouldCollectAnnotation)
				{
					this.PeekAndCollectAnnotationRawJson(jsonReader, propertyName, annotationName);
				}
			}

			// Token: 0x060011D9 RID: 4569 RVA: 0x00042922 File Offset: 0x00040B22
			public void TryAddPropertyAnnotationRawJson(string propertyName, string annotationName, string rawJson)
			{
				if (this.shouldCollectAnnotation)
				{
					this.AddPropertyAnnotationRawJson(propertyName, annotationName, rawJson);
				}
			}

			// Token: 0x060011DA RID: 4570 RVA: 0x00042938 File Offset: 0x00040B38
			public ODataJsonLightRawAnnotationSet GetPropertyRawAnnotationSet(string propertyName)
			{
				Dictionary<string, string> dictionary = null;
				if (!this.propertyAnnotations.TryGetValue(propertyName, out dictionary))
				{
					return null;
				}
				return new ODataJsonLightRawAnnotationSet
				{
					Annotations = dictionary
				};
			}

			// Token: 0x060011DB RID: 4571 RVA: 0x00042968 File Offset: 0x00040B68
			private void PeekAndCollectAnnotationRawJson(BufferingJsonReader jsonReader, string propertyName, string annotationName)
			{
				if (jsonReader.IsBuffering)
				{
					return;
				}
				try
				{
					jsonReader.StartBuffering();
					if (jsonReader.NodeType == JsonNodeType.Property)
					{
						jsonReader.Read();
					}
					StringBuilder stringBuilder = new StringBuilder();
					jsonReader.SkipValue(stringBuilder);
					string text = stringBuilder.ToString();
					this.AddPropertyAnnotationRawJson(propertyName, annotationName, text);
				}
				finally
				{
					jsonReader.StopBuffering();
				}
			}

			// Token: 0x060011DC RID: 4572 RVA: 0x000429CC File Offset: 0x00040BCC
			private void AddPropertyAnnotationRawJson(string propertyName, string annotationName, string rawJson)
			{
				Dictionary<string, string> dictionary = null;
				if (!this.propertyAnnotations.TryGetValue(propertyName, out dictionary))
				{
					dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
					this.propertyAnnotations[propertyName] = dictionary;
				}
				dictionary[annotationName] = rawJson;
			}

			// Token: 0x04000687 RID: 1671
			private Dictionary<string, Dictionary<string, string>> propertyAnnotations = new Dictionary<string, Dictionary<string, string>>(StringComparer.Ordinal);

			// Token: 0x04000688 RID: 1672
			private bool shouldCollectAnnotation;
		}

		// Token: 0x02000231 RID: 561
		private sealed class DuplicationRecord
		{
			// Token: 0x060011DE RID: 4574 RVA: 0x00042A23 File Offset: 0x00040C23
			public DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind duplicationKind)
			{
				this.DuplicationKind = duplicationKind;
			}

			// Token: 0x170003D8 RID: 984
			// (get) Token: 0x060011DF RID: 4575 RVA: 0x00042A32 File Offset: 0x00040C32
			// (set) Token: 0x060011E0 RID: 4576 RVA: 0x00042A3A File Offset: 0x00040C3A
			public DuplicatePropertyNamesChecker.DuplicationKind DuplicationKind { get; set; }

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00042A43 File Offset: 0x00040C43
			// (set) Token: 0x060011E2 RID: 4578 RVA: 0x00042A4B File Offset: 0x00040C4B
			public ODataNavigationLink NavigationLink { get; set; }

			// Token: 0x170003DA RID: 986
			// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00042A54 File Offset: 0x00040C54
			// (set) Token: 0x060011E4 RID: 4580 RVA: 0x00042A5C File Offset: 0x00040C5C
			public ODataAssociationLink AssociationLink { get; set; }

			// Token: 0x170003DB RID: 987
			// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00042A65 File Offset: 0x00040C65
			// (set) Token: 0x060011E6 RID: 4582 RVA: 0x00042A6D File Offset: 0x00040C6D
			public bool? NavigationPropertyIsCollection { get; set; }

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00042A76 File Offset: 0x00040C76
			// (set) Token: 0x060011E8 RID: 4584 RVA: 0x00042A7E File Offset: 0x00040C7E
			public Dictionary<string, object> PropertyODataAnnotations { get; set; }

			// Token: 0x170003DD RID: 989
			// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00042A87 File Offset: 0x00040C87
			// (set) Token: 0x060011EA RID: 4586 RVA: 0x00042A8F File Offset: 0x00040C8F
			public HashSet<string> PropertyCustomAnnotations { get; set; }
		}
	}
}
