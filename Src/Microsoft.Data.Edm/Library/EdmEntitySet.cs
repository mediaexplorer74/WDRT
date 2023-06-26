using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Internal;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001CF RID: 463
	public class EdmEntitySet : EdmNamedElement, IEdmEntitySet, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000AF1 RID: 2801 RVA: 0x000202D4 File Offset: 0x0001E4D4
		public EdmEntitySet(IEdmEntityContainer container, string name, IEdmEntityType elementType)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(elementType, "elementType");
			this.container = container;
			this.elementType = elementType;
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00020324 File Offset: 0x0001E524
		public IEdmEntityType ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0002032C File Offset: 0x0001E52C
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0002032F File Offset: 0x0001E52F
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00020337 File Offset: 0x0001E537
		public IEnumerable<IEdmNavigationTargetMapping> NavigationTargets
		{
			get
			{
				return this.navigationTargetsCache.GetValue(this, EdmEntitySet.ComputeNavigationTargetsFunc, null);
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002034B File Offset: 0x0001E54B
		public void AddNavigationTarget(IEdmNavigationProperty property, IEdmEntitySet target)
		{
			this.navigationPropertyMappings[property] = target;
			this.navigationTargetsCache.Clear(null);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00020368 File Offset: 0x0001E568
		public IEdmEntitySet FindNavigationTarget(IEdmNavigationProperty property)
		{
			IEdmEntitySet edmEntitySet;
			if (property != null && this.navigationPropertyMappings.TryGetValue(property, out edmEntitySet))
			{
				return edmEntitySet;
			}
			return null;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00020390 File Offset: 0x0001E590
		private IEnumerable<IEdmNavigationTargetMapping> ComputeNavigationTargets()
		{
			List<IEdmNavigationTargetMapping> list = new List<IEdmNavigationTargetMapping>();
			foreach (KeyValuePair<IEdmNavigationProperty, IEdmEntitySet> keyValuePair in this.navigationPropertyMappings)
			{
				list.Add(new EdmNavigationTargetMapping(keyValuePair.Key, keyValuePair.Value));
			}
			return list;
		}

		// Token: 0x04000528 RID: 1320
		private readonly IEdmEntityContainer container;

		// Token: 0x04000529 RID: 1321
		private readonly IEdmEntityType elementType;

		// Token: 0x0400052A RID: 1322
		private readonly Dictionary<IEdmNavigationProperty, IEdmEntitySet> navigationPropertyMappings = new Dictionary<IEdmNavigationProperty, IEdmEntitySet>();

		// Token: 0x0400052B RID: 1323
		private readonly Cache<EdmEntitySet, IEnumerable<IEdmNavigationTargetMapping>> navigationTargetsCache = new Cache<EdmEntitySet, IEnumerable<IEdmNavigationTargetMapping>>();

		// Token: 0x0400052C RID: 1324
		private static readonly Func<EdmEntitySet, IEnumerable<IEdmNavigationTargetMapping>> ComputeNavigationTargetsFunc = (EdmEntitySet me) => me.ComputeNavigationTargets();
	}
}
