using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000214 RID: 532
	internal sealed class EpmTargetTree
	{
		// Token: 0x0600106A RID: 4202 RVA: 0x0003C269 File Offset: 0x0003A469
		internal EpmTargetTree()
		{
			this.syndicationRoot = new EpmTargetPathSegment();
			this.nonSyndicationRoot = new EpmTargetPathSegment();
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0003C287 File Offset: 0x0003A487
		internal EpmTargetPathSegment SyndicationRoot
		{
			get
			{
				return this.syndicationRoot;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0003C28F File Offset: 0x0003A48F
		internal EpmTargetPathSegment NonSyndicationRoot
		{
			get
			{
				return this.nonSyndicationRoot;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x0003C297 File Offset: 0x0003A497
		internal ODataVersion MinimumODataProtocolVersion
		{
			get
			{
				if (this.countOfNonContentV2Mappings > 0)
				{
					return ODataVersion.V2;
				}
				return ODataVersion.V1;
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0003C304 File Offset: 0x0003A504
		internal void Add(EntityPropertyMappingInfo epmInfo)
		{
			string targetPath = epmInfo.Attribute.TargetPath;
			string namespaceUri = epmInfo.Attribute.TargetNamespaceUri;
			string targetNamespacePrefix = epmInfo.Attribute.TargetNamespacePrefix;
			EpmTargetPathSegment epmTargetPathSegment = (epmInfo.IsSyndicationMapping ? this.SyndicationRoot : this.NonSyndicationRoot);
			IList<EpmTargetPathSegment> list = epmTargetPathSegment.SubSegments;
			string[] array = targetPath.Split(new char[] { '/' });
			for (int i = 0; i < array.Length; i++)
			{
				string targetSegment = array[i];
				if (targetSegment.Length == 0)
				{
					throw new ODataException(Strings.EpmTargetTree_InvalidTargetPath_EmptySegment(targetPath));
				}
				if (targetSegment[0] == '@' && i != array.Length - 1)
				{
					throw new ODataException(Strings.EpmTargetTree_AttributeInMiddle(targetSegment));
				}
				EpmTargetPathSegment epmTargetPathSegment2 = list.SingleOrDefault((EpmTargetPathSegment segment) => segment.SegmentName == targetSegment && (epmInfo.IsSyndicationMapping || segment.SegmentNamespaceUri == namespaceUri));
				if (epmTargetPathSegment2 != null)
				{
					epmTargetPathSegment = epmTargetPathSegment2;
				}
				else
				{
					epmTargetPathSegment = new EpmTargetPathSegment(targetSegment, namespaceUri, targetNamespacePrefix, epmTargetPathSegment);
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
				throw new ODataException(Strings.EpmTargetTree_DuplicateEpmAttributesWithSameTargetName(epmTargetPathSegment.EpmInfo.DefiningType.ODataFullName(), EpmTargetTree.GetPropertyNameFromEpmInfo(epmTargetPathSegment.EpmInfo), epmTargetPathSegment.EpmInfo.Attribute.SourcePath, epmInfo.Attribute.SourcePath));
			}
			if (!epmInfo.Attribute.KeepInContent)
			{
				this.countOfNonContentV2Mappings++;
			}
			epmTargetPathSegment.EpmInfo = epmInfo;
			List<EntityPropertyMappingAttribute> list2 = new List<EntityPropertyMappingAttribute>(2);
			if (EpmTargetTree.HasMixedContent(this.NonSyndicationRoot, list2))
			{
				throw new ODataException(Strings.EpmTargetTree_InvalidTargetPath_MixedContent(list2[0].TargetPath, list2[1].TargetPath));
			}
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0003C58C File Offset: 0x0003A78C
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
					this.countOfNonContentV2Mappings--;
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

		// Token: 0x06001070 RID: 4208 RVA: 0x0003C6C1 File Offset: 0x0003A8C1
		[Conditional("DEBUG")]
		internal void Validate()
		{
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0003C6D0 File Offset: 0x0003A8D0
		private static bool HasMixedContent(EpmTargetPathSegment currentSegment, List<EntityPropertyMappingAttribute> ancestorsWithContent)
		{
			foreach (EpmTargetPathSegment epmTargetPathSegment in currentSegment.SubSegments.Where((EpmTargetPathSegment s) => !s.IsAttribute))
			{
				if (epmTargetPathSegment.HasContent && ancestorsWithContent.Count == 1)
				{
					ancestorsWithContent.Add(epmTargetPathSegment.EpmInfo.Attribute);
					return true;
				}
				if (epmTargetPathSegment.HasContent)
				{
					ancestorsWithContent.Add(epmTargetPathSegment.EpmInfo.Attribute);
				}
				if (EpmTargetTree.HasMixedContent(epmTargetPathSegment, ancestorsWithContent))
				{
					return true;
				}
				if (epmTargetPathSegment.HasContent)
				{
					ancestorsWithContent.Clear();
				}
			}
			return false;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0003C798 File Offset: 0x0003A998
		private static string GetPropertyNameFromEpmInfo(EntityPropertyMappingInfo epmInfo)
		{
			if (epmInfo.Attribute.TargetSyndicationItem == SyndicationItemProperty.CustomProperty)
			{
				return epmInfo.Attribute.TargetPath;
			}
			return epmInfo.Attribute.TargetSyndicationItem.ToString();
		}

		// Token: 0x04000604 RID: 1540
		private readonly EpmTargetPathSegment syndicationRoot;

		// Token: 0x04000605 RID: 1541
		private readonly EpmTargetPathSegment nonSyndicationRoot;

		// Token: 0x04000606 RID: 1542
		private int countOfNonContentV2Mappings;
	}
}
