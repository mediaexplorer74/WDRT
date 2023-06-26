using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Services.Client.Serializers
{
	// Token: 0x02000019 RID: 25
	internal sealed class EpmTargetTree
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003C53 File Offset: 0x00001E53
		internal EpmTargetTree()
		{
			this.SyndicationRoot = new EpmTargetPathSegment();
			this.NonSyndicationRoot = new EpmTargetPathSegment();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003C71 File Offset: 0x00001E71
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003C79 File Offset: 0x00001E79
		internal EpmTargetPathSegment SyndicationRoot { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003C82 File Offset: 0x00001E82
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003C8A File Offset: 0x00001E8A
		internal EpmTargetPathSegment NonSyndicationRoot { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003C93 File Offset: 0x00001E93
		internal DataServiceProtocolVersion MinimumDataServiceProtocolVersion
		{
			get
			{
				if (this.countOfNonContentV2mappings > 0)
				{
					return DataServiceProtocolVersion.V2;
				}
				return DataServiceProtocolVersion.V1;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003D00 File Offset: 0x00001F00
		internal void Add(EntityPropertyMappingInfo epmInfo)
		{
			string targetPath = epmInfo.Attribute.TargetPath;
			string namespaceUri = epmInfo.Attribute.TargetNamespaceUri;
			EpmTargetPathSegment epmTargetPathSegment = (epmInfo.IsSyndicationMapping ? this.SyndicationRoot : this.NonSyndicationRoot);
			IList<EpmTargetPathSegment> list = epmTargetPathSegment.SubSegments;
			string[] array = targetPath.Split(new char[] { '/' });
			for (int i = 0; i < array.Length; i++)
			{
				string targetSegment = array[i];
				if (targetSegment.Length == 0)
				{
					throw new InvalidOperationException(Strings.EpmTargetTree_InvalidTargetPath(targetPath));
				}
				if (targetSegment[0] == '@' && i != array.Length - 1)
				{
					throw new InvalidOperationException(Strings.EpmTargetTree_AttributeInMiddle(targetSegment));
				}
				EpmTargetPathSegment epmTargetPathSegment2 = list.SingleOrDefault((EpmTargetPathSegment segment) => segment.SegmentName == targetSegment && (epmInfo.IsSyndicationMapping || segment.SegmentNamespaceUri == namespaceUri));
				if (epmTargetPathSegment2 != null)
				{
					epmTargetPathSegment = epmTargetPathSegment2;
				}
				else
				{
					epmTargetPathSegment = new EpmTargetPathSegment(targetSegment, namespaceUri, epmTargetPathSegment);
					if (targetSegment[0] == '@')
					{
						list.Insert(0, epmTargetPathSegment);
					}
					else
					{
						list.Add(epmTargetPathSegment);
					}
				}
				list = epmTargetPathSegment.SubSegments;
			}
			if (epmTargetPathSegment.EpmInfo != null)
			{
				throw new ArgumentException(Strings.EpmTargetTree_DuplicateEpmAttrsWithSameTargetName(EpmTargetTree.GetPropertyNameFromEpmInfo(epmTargetPathSegment.EpmInfo), epmTargetPathSegment.EpmInfo.DefiningType.Name, epmTargetPathSegment.EpmInfo.Attribute.SourcePath, epmInfo.Attribute.SourcePath));
			}
			if (!epmInfo.Attribute.KeepInContent)
			{
				this.countOfNonContentV2mappings++;
			}
			epmTargetPathSegment.EpmInfo = epmInfo;
			if (EpmTargetTree.HasMixedContent(this.NonSyndicationRoot, false))
			{
				throw new InvalidOperationException(Strings.EpmTargetTree_InvalidTargetPath(targetPath));
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003F4C File Offset: 0x0000214C
		internal void Remove(EntityPropertyMappingInfo epmInfo)
		{
			string targetPath = epmInfo.Attribute.TargetPath;
			string namespaceUri = epmInfo.Attribute.TargetNamespaceUri;
			EpmTargetPathSegment epmTargetPathSegment = (epmInfo.IsSyndicationMapping ? this.SyndicationRoot : this.NonSyndicationRoot);
			List<EpmTargetPathSegment> list = epmTargetPathSegment.SubSegments;
			string[] array = targetPath.Split(new char[] { '/' });
			for (int i = 0; i < array.Length; i++)
			{
				string targetSegment = array[i];
				EpmTargetPathSegment epmTargetPathSegment2 = list.FirstOrDefault((EpmTargetPathSegment segment) => segment.SegmentName == targetSegment && (epmInfo.IsSyndicationMapping || segment.SegmentNamespaceUri == namespaceUri));
				if (epmTargetPathSegment2 == null)
				{
					return;
				}
				epmTargetPathSegment = epmTargetPathSegment2;
				list = epmTargetPathSegment.SubSegments;
			}
			if (epmTargetPathSegment.EpmInfo != null)
			{
				if (!epmTargetPathSegment.EpmInfo.Attribute.KeepInContent)
				{
					this.countOfNonContentV2mappings--;
				}
				do
				{
					EpmTargetPathSegment parentSegment = epmTargetPathSegment.ParentSegment;
					parentSegment.SubSegments.Remove(epmTargetPathSegment);
					epmTargetPathSegment = parentSegment;
				}
				while (epmTargetPathSegment.ParentSegment != null && !epmTargetPathSegment.HasContent && epmTargetPathSegment.SubSegments.Count == 0);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004081 File Offset: 0x00002281
		[Conditional("DEBUG")]
		internal void Validate()
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004090 File Offset: 0x00002290
		private static bool HasMixedContent(EpmTargetPathSegment currentSegment, bool ancestorHasContent)
		{
			foreach (EpmTargetPathSegment epmTargetPathSegment in currentSegment.SubSegments.Where((EpmTargetPathSegment s) => !s.IsAttribute))
			{
				if (epmTargetPathSegment.HasContent && ancestorHasContent)
				{
					return true;
				}
				if (EpmTargetTree.HasMixedContent(epmTargetPathSegment, epmTargetPathSegment.HasContent || ancestorHasContent))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004124 File Offset: 0x00002324
		private static string GetPropertyNameFromEpmInfo(EntityPropertyMappingInfo epmInfo)
		{
			if (epmInfo.Attribute.TargetSyndicationItem == SyndicationItemProperty.CustomProperty)
			{
				return epmInfo.Attribute.TargetPath;
			}
			return epmInfo.Attribute.TargetSyndicationItem.ToString();
		}

		// Token: 0x0400016E RID: 366
		private int countOfNonContentV2mappings;
	}
}
