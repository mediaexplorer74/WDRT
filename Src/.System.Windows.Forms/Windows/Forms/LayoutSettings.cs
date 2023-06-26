using System;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Provides a base class for collecting layout scheme characteristics.</summary>
	// Token: 0x02000446 RID: 1094
	public abstract class LayoutSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LayoutSettings" /> class.</summary>
		// Token: 0x06004BFE RID: 19454 RVA: 0x00002843 File Offset: 0x00000A43
		protected LayoutSettings()
		{
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x0013B899 File Offset: 0x00139A99
		internal LayoutSettings(IArrangedElement owner)
		{
			this._owner = owner;
		}

		/// <summary>Gets the current table layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> currently being used.</returns>
		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06004C00 RID: 19456 RVA: 0x00015C90 File Offset: 0x00013E90
		public virtual LayoutEngine LayoutEngine
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06004C01 RID: 19457 RVA: 0x0013B8A8 File Offset: 0x00139AA8
		internal IArrangedElement Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x04002862 RID: 10338
		private IArrangedElement _owner;
	}
}
