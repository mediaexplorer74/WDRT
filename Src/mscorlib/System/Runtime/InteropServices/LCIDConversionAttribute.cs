using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that a method's unmanaged signature expects a locale identifier (LCID) parameter.</summary>
	// Token: 0x02000918 RID: 2328
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class LCIDConversionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="LCIDConversionAttribute" /> class with the position of the LCID in the unmanaged signature.</summary>
		/// <param name="lcid">Indicates the position of the LCID argument in the unmanaged signature, where 0 is the first argument.</param>
		// Token: 0x0600601A RID: 24602 RVA: 0x0014CE60 File Offset: 0x0014B060
		public LCIDConversionAttribute(int lcid)
		{
			this._val = lcid;
		}

		/// <summary>Gets the position of the LCID argument in the unmanaged signature.</summary>
		/// <returns>The position of the LCID argument in the unmanaged signature, where 0 is the first argument.</returns>
		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x0600601B RID: 24603 RVA: 0x0014CE6F File Offset: 0x0014B06F
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A7B RID: 10875
		internal int _val;
	}
}
