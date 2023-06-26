using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Specifies the name and mode for a code region.</summary>
	// Token: 0x0200064E RID: 1614
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeRegionDirective : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class with default values.</summary>
		// Token: 0x06003A99 RID: 15001 RVA: 0x000F3C68 File Offset: 0x000F1E68
		public CodeRegionDirective()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class, specifying its mode and name.</summary>
		/// <param name="regionMode">One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values.</param>
		/// <param name="regionText">The name for the region.</param>
		// Token: 0x06003A9A RID: 15002 RVA: 0x000F3C70 File Offset: 0x000F1E70
		public CodeRegionDirective(CodeRegionMode regionMode, string regionText)
		{
			this.RegionText = regionText;
			this.regionMode = regionMode;
		}

		/// <summary>Gets or sets the name of the region.</summary>
		/// <returns>The name of the region.</returns>
		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06003A9B RID: 15003 RVA: 0x000F3C86 File Offset: 0x000F1E86
		// (set) Token: 0x06003A9C RID: 15004 RVA: 0x000F3C9C File Offset: 0x000F1E9C
		public string RegionText
		{
			get
			{
				if (this.regionText != null)
				{
					return this.regionText;
				}
				return string.Empty;
			}
			set
			{
				this.regionText = value;
			}
		}

		/// <summary>Gets or sets the mode for the region directive.</summary>
		/// <returns>One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values. The default is <see cref="F:System.CodeDom.CodeRegionMode.None" />.</returns>
		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06003A9D RID: 15005 RVA: 0x000F3CA5 File Offset: 0x000F1EA5
		// (set) Token: 0x06003A9E RID: 15006 RVA: 0x000F3CAD File Offset: 0x000F1EAD
		public CodeRegionMode RegionMode
		{
			get
			{
				return this.regionMode;
			}
			set
			{
				this.regionMode = value;
			}
		}

		// Token: 0x04002BF9 RID: 11257
		private string regionText;

		// Token: 0x04002BFA RID: 11258
		private CodeRegionMode regionMode;
	}
}
