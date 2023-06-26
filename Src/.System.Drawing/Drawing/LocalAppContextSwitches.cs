using System;
using System.Runtime.CompilerServices;

namespace System.Drawing
{
	// Token: 0x02000026 RID: 38
	internal static class LocalAppContextSwitches
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600037B RID: 891 RVA: 0x000114D4 File Offset: 0x0000F6D4
		public static bool DontSupportPngFramesInIcons
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Drawing.DontSupportPngFramesInIcons", ref LocalAppContextSwitches.dontSupportPngFramesInIcons);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600037C RID: 892 RVA: 0x000114E5 File Offset: 0x0000F6E5
		public static bool OptimizePrintPreview
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Drawing.Printing.OptimizePrintPreview", ref LocalAppContextSwitches.optimizePrintPreview);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000114F6 File Offset: 0x0000F6F6
		public static bool DoNotRemoveGdiFontsResourcesFromFontCollection
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Drawing.Text.DoNotRemoveGdiFontsResourcesFromFontCollection", ref LocalAppContextSwitches.doNotRemoveGdiFontsResourcesFromFontCollection);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00011507 File Offset: 0x0000F707
		public static bool FreeCopyToDevModeOnFailure
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Drawing.Printing.CopyToDevModeFreeOnFailure", ref LocalAppContextSwitches.freeCopyToDevModeOnFailure);
			}
		}

		// Token: 0x04000259 RID: 601
		private static int dontSupportPngFramesInIcons;

		// Token: 0x0400025A RID: 602
		private static int optimizePrintPreview;

		// Token: 0x0400025B RID: 603
		private static int doNotRemoveGdiFontsResourcesFromFontCollection;

		// Token: 0x0400025C RID: 604
		private static int freeCopyToDevModeOnFailure;
	}
}
