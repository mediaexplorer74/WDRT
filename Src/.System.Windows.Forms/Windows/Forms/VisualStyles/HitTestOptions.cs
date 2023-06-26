using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies the options that can be used when performing a hit test on the background specified by a visual style.</summary>
	// Token: 0x02000481 RID: 1153
	[Flags]
	public enum HitTestOptions
	{
		/// <summary>The hit test option for the background segment.</summary>
		// Token: 0x040033AA RID: 13226
		BackgroundSegment = 0,
		/// <summary>The hit test option for the fixed border.</summary>
		// Token: 0x040033AB RID: 13227
		FixedBorder = 2,
		/// <summary>The hit test option for the caption.</summary>
		// Token: 0x040033AC RID: 13228
		Caption = 4,
		/// <summary>The hit test option for the left resizing border.</summary>
		// Token: 0x040033AD RID: 13229
		ResizingBorderLeft = 16,
		/// <summary>The hit test option for the top resizing border.</summary>
		// Token: 0x040033AE RID: 13230
		ResizingBorderTop = 32,
		/// <summary>The hit test option for the right resizing border.</summary>
		// Token: 0x040033AF RID: 13231
		ResizingBorderRight = 64,
		/// <summary>The hit test option for the bottom resizing border.</summary>
		// Token: 0x040033B0 RID: 13232
		ResizingBorderBottom = 128,
		/// <summary>The hit test option for the resizing border.</summary>
		// Token: 0x040033B1 RID: 13233
		ResizingBorder = 240,
		/// <summary>The resizing border is specified as a template, not just window edges. This option is mutually exclusive with <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SystemSizingMargins" />; <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SizingTemplate" /> takes precedence.</summary>
		// Token: 0x040033B2 RID: 13234
		SizingTemplate = 256,
		/// <summary>The system resizing border width is used instead of visual style content margins. This option is mutually exclusive with <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SizingTemplate" />; <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SizingTemplate" /> takes precedence.</summary>
		// Token: 0x040033B3 RID: 13235
		SystemSizingMargins = 512
	}
}
