using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates the data that makes up a <see cref="T:System.Drawing.Region" /> object. This class cannot be inherited.</summary>
	// Token: 0x020000D3 RID: 211
	public sealed class RegionData
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x00029D35 File Offset: 0x00027F35
		internal RegionData(byte[] data)
		{
			this.data = data;
		}

		/// <summary>Gets or sets an array of bytes that specify the <see cref="T:System.Drawing.Region" /> object.</summary>
		/// <returns>An array of bytes that specify the <see cref="T:System.Drawing.Region" /> object.</returns>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00029D44 File Offset: 0x00027F44
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x00029D4C File Offset: 0x00027F4C
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x04000A14 RID: 2580
		private byte[] data;
	}
}
