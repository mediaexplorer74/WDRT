using System;
using System.Collections.Generic;
using System.Data.Services.Client.Serializers;
using System.Data.Services.Common;
using System.Diagnostics;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData;
using Microsoft.Data.OData.Metadata;

namespace System.Data.Services.Client.Metadata
{
	// Token: 0x02000106 RID: 262
	[DebuggerDisplay("{ElementTypeName}")]
	internal sealed class ClientTypeAnnotation
	{
		// Token: 0x06000887 RID: 2183 RVA: 0x000238C8 File Offset: 0x00021AC8
		internal ClientTypeAnnotation(IEdmType edmType, Type type, string qualifiedName, ClientEdmModel model)
		{
			this.EdmType = edmType;
			this.EdmTypeReference = this.EdmType.ToEdmTypeReference(Util.IsNullableType(type));
			this.ElementTypeName = qualifiedName;
			this.ElementType = Nullable.GetUnderlyingType(type) ?? type;
			this.model = model;
			this.epmLazyLoader = new ClientTypeAnnotation.EpmLazyLoader(this);
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00023925 File Offset: 0x00021B25
		internal bool IsEntityType
		{
			get
			{
				return this.EdmType.TypeKind == EdmTypeKind.Entity;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00023935 File Offset: 0x00021B35
		internal ClientPropertyAnnotation MediaDataMember
		{
			get
			{
				if (this.isMediaLinkEntry == null)
				{
					this.CheckMediaLinkEntry();
				}
				return this.mediaDataMember;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00023950 File Offset: 0x00021B50
		internal bool IsMediaLinkEntry
		{
			get
			{
				if (this.isMediaLinkEntry == null)
				{
					this.CheckMediaLinkEntry();
				}
				return this.isMediaLinkEntry.Value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00023970 File Offset: 0x00021B70
		internal EpmTargetTree EpmTargetTree
		{
			get
			{
				return this.epmLazyLoader.EpmTargetTree;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0002397D File Offset: 0x00021B7D
		internal bool HasEntityPropertyMappings
		{
			get
			{
				return this.epmLazyLoader.EpmSourceTree.Root.SubProperties.Count > 0;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0002399C File Offset: 0x00021B9C
		internal DataServiceProtocolVersion EpmMinimumDataServiceProtocolVersion
		{
			get
			{
				if (!this.HasEntityPropertyMappings)
				{
					return DataServiceProtocolVersion.V1;
				}
				return this.EpmTargetTree.MinimumDataServiceProtocolVersion;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x000239B3 File Offset: 0x00021BB3
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x000239BB File Offset: 0x00021BBB
		internal IEdmTypeReference EdmTypeReference { get; private set; }

		// Token: 0x06000890 RID: 2192 RVA: 0x000239C4 File Offset: 0x00021BC4
		internal void EnsureEPMLoaded()
		{
			this.epmLazyLoader.EnsureEPMLoaded();
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000239D1 File Offset: 0x00021BD1
		internal IEnumerable<IEdmProperty> EdmProperties()
		{
			if (this.edmPropertyCache == null)
			{
				this.edmPropertyCache = this.DiscoverEdmProperties().ToArray<IEdmProperty>();
			}
			return this.edmPropertyCache;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000239F2 File Offset: 0x00021BF2
		internal IEnumerable<ClientPropertyAnnotation> Properties()
		{
			if (this.clientPropertyCache == null)
			{
				this.BuildPropertyCache();
			}
			return this.clientPropertyCache.Values;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00023A1E File Offset: 0x00021C1E
		internal IEnumerable<ClientPropertyAnnotation> PropertiesToSerialize()
		{
			return from p in this.Properties()
				where ClientTypeAnnotation.ShouldSerializeProperty(this, p)
				orderby p.PropertyName
				select p;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00023A5C File Offset: 0x00021C5C
		internal ClientPropertyAnnotation GetProperty(string propertyName, bool ignoreMissingProperties)
		{
			if (this.clientPropertyCache == null)
			{
				this.BuildPropertyCache();
			}
			ClientPropertyAnnotation clientPropertyAnnotation;
			if (!this.clientPropertyCache.TryGetValue(propertyName, out clientPropertyAnnotation) && !ignoreMissingProperties)
			{
				throw Error.InvalidOperation(Strings.ClientType_MissingProperty(this.ElementTypeName, propertyName));
			}
			return clientPropertyAnnotation;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00023AA0 File Offset: 0x00021CA0
		internal Version GetMetadataVersion()
		{
			if (this.metadataVersion == null)
			{
				Version dataServiceVersion = Util.DataServiceVersion1;
				WebUtil.RaiseVersion(ref dataServiceVersion, this.ComputeVersionForPropertyCollection(this.EdmProperties(), null));
				this.metadataVersion = dataServiceVersion;
			}
			return this.metadataVersion;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00023AE2 File Offset: 0x00021CE2
		private static bool ShouldSerializeProperty(ClientTypeAnnotation type, ClientPropertyAnnotation property)
		{
			return !property.IsDictionary && property != type.MediaDataMember && !property.IsStreamLinkProperty && (type.MediaDataMember == null || type.MediaDataMember.MimeTypeProperty != property) && !property.IsEntityCollection;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00023B20 File Offset: 0x00021D20
		private void BuildPropertyCache()
		{
			lock (this)
			{
				if (this.clientPropertyCache == null)
				{
					Dictionary<string, ClientPropertyAnnotation> dictionary = new Dictionary<string, ClientPropertyAnnotation>(StringComparer.Ordinal);
					foreach (IEdmProperty edmProperty in this.EdmProperties())
					{
						dictionary.Add(edmProperty.Name, this.model.GetClientPropertyAnnotation(edmProperty));
					}
					this.clientPropertyCache = dictionary;
				}
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00023BE4 File Offset: 0x00021DE4
		private void CheckMediaLinkEntry()
		{
			this.isMediaLinkEntry = new bool?(false);
			MediaEntryAttribute mediaEntryAttribute = (MediaEntryAttribute)this.ElementType.GetCustomAttributes(typeof(MediaEntryAttribute), true).SingleOrDefault<object>();
			if (mediaEntryAttribute != null)
			{
				this.isMediaLinkEntry = new bool?(true);
				ClientPropertyAnnotation clientPropertyAnnotation = this.Properties().SingleOrDefault((ClientPropertyAnnotation p) => p.PropertyName == mediaEntryAttribute.MediaMemberName);
				if (clientPropertyAnnotation == null)
				{
					throw Error.InvalidOperation(Strings.ClientType_MissingMediaEntryProperty(this.ElementTypeName, mediaEntryAttribute.MediaMemberName));
				}
				this.mediaDataMember = clientPropertyAnnotation;
			}
			bool flag = this.ElementType.GetCustomAttributes(typeof(HasStreamAttribute), true).Any<object>();
			if (flag)
			{
				this.isMediaLinkEntry = new bool?(true);
			}
			if (this.isMediaLinkEntry != null && this.isMediaLinkEntry.Value)
			{
				this.SetMediaLinkEntryAnnotation();
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00023CCD File Offset: 0x00021ECD
		private void SetMediaLinkEntryAnnotation()
		{
			this.model.SetHasDefaultStream((IEdmEntityType)this.EdmType, true);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00023CE8 File Offset: 0x00021EE8
		private Version ComputeVersionForPropertyCollection(IEnumerable<IEdmProperty> propertyCollection, HashSet<IEdmType> visitedComplexTypes)
		{
			Version dataServiceVersion = Util.DataServiceVersion1;
			foreach (IEdmProperty edmProperty in propertyCollection)
			{
				ClientPropertyAnnotation clientPropertyAnnotation = this.model.GetClientPropertyAnnotation(edmProperty);
				if (clientPropertyAnnotation.IsPrimitiveOrComplexCollection || clientPropertyAnnotation.IsSpatialType)
				{
					WebUtil.RaiseVersion(ref dataServiceVersion, Util.DataServiceVersion3);
				}
				else if (edmProperty.Type.TypeKind() == EdmTypeKind.Complex && !clientPropertyAnnotation.IsDictionary)
				{
					if (visitedComplexTypes == null)
					{
						visitedComplexTypes = new HashSet<IEdmType>(EqualityComparer<IEdmType>.Default);
					}
					else if (visitedComplexTypes.Contains(edmProperty.Type.Definition))
					{
						continue;
					}
					visitedComplexTypes.Add(edmProperty.Type.Definition);
					WebUtil.RaiseVersion(ref dataServiceVersion, this.ComputeVersionForPropertyCollection(this.model.GetClientTypeAnnotation(edmProperty).EdmProperties(), visitedComplexTypes));
				}
			}
			return dataServiceVersion;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00023FF0 File Offset: 0x000221F0
		private IEnumerable<IEdmProperty> DiscoverEdmProperties()
		{
			IEdmStructuredType edmStructuredType = this.EdmType as IEdmStructuredType;
			if (edmStructuredType != null)
			{
				HashSet<string> propertyNames = new HashSet<string>(StringComparer.Ordinal);
				do
				{
					foreach (IEdmProperty property in edmStructuredType.DeclaredProperties)
					{
						string propertyName = property.Name;
						if (!propertyNames.Contains(propertyName))
						{
							propertyNames.Add(propertyName);
							yield return property;
						}
					}
					edmStructuredType = edmStructuredType.BaseType;
				}
				while (edmStructuredType != null);
			}
			yield break;
		}

		// Token: 0x040004FD RID: 1277
		internal readonly IEdmType EdmType;

		// Token: 0x040004FE RID: 1278
		internal readonly string ElementTypeName;

		// Token: 0x040004FF RID: 1279
		internal readonly Type ElementType;

		// Token: 0x04000500 RID: 1280
		private readonly ClientEdmModel model;

		// Token: 0x04000501 RID: 1281
		private bool? isMediaLinkEntry;

		// Token: 0x04000502 RID: 1282
		private ClientPropertyAnnotation mediaDataMember;

		// Token: 0x04000503 RID: 1283
		private Version metadataVersion;

		// Token: 0x04000504 RID: 1284
		private ClientTypeAnnotation.EpmLazyLoader epmLazyLoader;

		// Token: 0x04000505 RID: 1285
		private Dictionary<string, ClientPropertyAnnotation> clientPropertyCache;

		// Token: 0x04000506 RID: 1286
		private IEdmProperty[] edmPropertyCache;

		// Token: 0x02000107 RID: 263
		private class EpmLazyLoader
		{
			// Token: 0x0600089E RID: 2206 RVA: 0x0002400D File Offset: 0x0002220D
			internal EpmLazyLoader(ClientTypeAnnotation clientTypeAnnotation)
			{
				this.clientTypeAnnotation = clientTypeAnnotation;
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x0600089F RID: 2207 RVA: 0x00024027 File Offset: 0x00022227
			internal EpmTargetTree EpmTargetTree
			{
				get
				{
					if (this.EpmNeedsInitializing)
					{
						this.InitializeAndBuildTree();
					}
					return this.epmTargetTree;
				}
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0002403D File Offset: 0x0002223D
			internal EpmSourceTree EpmSourceTree
			{
				get
				{
					if (this.EpmNeedsInitializing)
					{
						this.InitializeAndBuildTree();
					}
					return this.epmSourceTree;
				}
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00024053 File Offset: 0x00022253
			private bool EpmNeedsInitializing
			{
				get
				{
					return this.epmSourceTree == null || this.epmTargetTree == null;
				}
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x00024068 File Offset: 0x00022268
			internal void EnsureEPMLoaded()
			{
				if (this.EpmNeedsInitializing)
				{
					this.InitializeAndBuildTree();
				}
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x00024078 File Offset: 0x00022278
			private static void BuildEpmInfo(ClientTypeAnnotation clientTypeAnnotation, EpmSourceTree sourceTree)
			{
				ClientTypeAnnotation.EpmLazyLoader.BuildEpmInfo(clientTypeAnnotation.ElementType, clientTypeAnnotation, sourceTree);
			}

			// Token: 0x060008A4 RID: 2212 RVA: 0x000240EC File Offset: 0x000222EC
			private static void BuildEpmInfo(Type type, ClientTypeAnnotation clientTypeAnnotation, EpmSourceTree sourceTree)
			{
				if (clientTypeAnnotation.IsEntityType)
				{
					Type baseType = type.GetBaseType();
					ClientEdmModel model = clientTypeAnnotation.model;
					ODataEntityPropertyMappingCollection mappings = null;
					if (baseType != null && baseType != typeof(object))
					{
						if (((EdmStructuredType)clientTypeAnnotation.EdmType).BaseType == null)
						{
							ClientTypeAnnotation.EpmLazyLoader.BuildEpmInfo(baseType, clientTypeAnnotation, sourceTree);
							mappings = model.GetAnnotationValue(clientTypeAnnotation.EdmType);
						}
						else
						{
							ClientTypeAnnotation clientTypeAnnotation2 = model.GetClientTypeAnnotation(baseType);
							ClientTypeAnnotation.EpmLazyLoader.BuildEpmInfo(baseType, clientTypeAnnotation2, sourceTree);
						}
					}
					foreach (EntityPropertyMappingAttribute entityPropertyMappingAttribute in type.GetCustomAttributes(typeof(EntityPropertyMappingAttribute), false))
					{
						ClientTypeAnnotation.EpmLazyLoader.BuildEpmInfo(entityPropertyMappingAttribute, type, clientTypeAnnotation, sourceTree);
						if (mappings == null)
						{
							mappings = new ODataEntityPropertyMappingCollection();
						}
						mappings.Add(entityPropertyMappingAttribute);
					}
					if (mappings != null)
					{
						ODataEntityPropertyMappingCollection annotationValue = model.GetAnnotationValue(clientTypeAnnotation.EdmType);
						if (annotationValue != null)
						{
							List<EntityPropertyMappingAttribute> list = annotationValue.Where((EntityPropertyMappingAttribute oldM) => !mappings.Any((EntityPropertyMappingAttribute newM) => oldM.SourcePath == newM.SourcePath)).ToList<EntityPropertyMappingAttribute>();
							foreach (EntityPropertyMappingAttribute entityPropertyMappingAttribute2 in list)
							{
								mappings.Add(entityPropertyMappingAttribute2);
							}
						}
						model.SetAnnotationValue(clientTypeAnnotation.EdmType, mappings);
					}
				}
			}

			// Token: 0x060008A5 RID: 2213 RVA: 0x0002427C File Offset: 0x0002247C
			private static void BuildEpmInfo(EntityPropertyMappingAttribute epmAttr, Type definingType, ClientTypeAnnotation clientTypeAnnotation, EpmSourceTree sourceTree)
			{
				sourceTree.Add(new EntityPropertyMappingInfo(epmAttr, definingType, clientTypeAnnotation));
			}

			// Token: 0x060008A6 RID: 2214 RVA: 0x0002428C File Offset: 0x0002248C
			private void InitializeAndBuildTree()
			{
				lock (this.epmDataLock)
				{
					if (this.EpmNeedsInitializing)
					{
						EpmTargetTree epmTargetTree = new EpmTargetTree();
						EpmSourceTree epmSourceTree = new EpmSourceTree(epmTargetTree);
						ClientTypeAnnotation.EpmLazyLoader.BuildEpmInfo(this.clientTypeAnnotation, epmSourceTree);
						epmSourceTree.Validate(this.clientTypeAnnotation);
						this.epmTargetTree = epmTargetTree;
						this.epmSourceTree = epmSourceTree;
					}
				}
			}

			// Token: 0x04000509 RID: 1289
			private EpmSourceTree epmSourceTree;

			// Token: 0x0400050A RID: 1290
			private EpmTargetTree epmTargetTree;

			// Token: 0x0400050B RID: 1291
			private object epmDataLock = new object();

			// Token: 0x0400050C RID: 1292
			private ClientTypeAnnotation clientTypeAnnotation;
		}
	}
}
