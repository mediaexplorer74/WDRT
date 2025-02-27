﻿using System;
using System.Collections.Generic;

namespace System.Data.Services.Client
{
	// Token: 0x02000052 RID: 82
	internal abstract class EntityTrackerBase
	{
		// Token: 0x060002A5 RID: 677
		internal abstract object TryGetEntity(string resourceUri, out EntityStates state);

		// Token: 0x060002A6 RID: 678
		internal abstract IEnumerable<LinkDescriptor> GetLinks(object source, string sourceProperty);

		// Token: 0x060002A7 RID: 679
		internal abstract EntityDescriptor InternalAttachEntityDescriptor(EntityDescriptor entityDescriptorFromMaterializer, bool failIfDuplicated);

		// Token: 0x060002A8 RID: 680
		internal abstract EntityDescriptor GetEntityDescriptor(object resource);

		// Token: 0x060002A9 RID: 681
		internal abstract void DetachExistingLink(LinkDescriptor existingLink, bool targetDelete);

		// Token: 0x060002AA RID: 682
		internal abstract void AttachLink(object source, string sourceProperty, object target, MergeOption linkMerge);

		// Token: 0x060002AB RID: 683
		internal abstract void AttachIdentity(EntityDescriptor entityDescriptorFromMaterializer, MergeOption metadataMergeOption);
	}
}
