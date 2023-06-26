using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Layout
{
	// Token: 0x02000031 RID: 49
	public sealed class FluidMoveSetTagBehavior : FluidMoveBehaviorBase
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00005CF8 File Offset: 0x00003EF8
		internal override void UpdateLayoutTransitionCore(FrameworkElement child, FrameworkElement root, object tag, FluidMoveBehaviorBase.TagData newTagData)
		{
			FluidMoveBehaviorBase.TagData tagData;
			if (!FluidMoveBehaviorBase.TagDictionary.TryGetValue(tag, out tagData))
			{
				tagData = new FluidMoveBehaviorBase.TagData();
				FluidMoveBehaviorBase.TagDictionary.Add(tag, tagData);
			}
			tagData.ParentRect = newTagData.ParentRect;
			tagData.AppRect = newTagData.AppRect;
			tagData.Parent = newTagData.Parent;
			tagData.Child = newTagData.Child;
			tagData.Timestamp = newTagData.Timestamp;
		}
	}
}
