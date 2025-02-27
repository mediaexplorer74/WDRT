﻿using System;

namespace System.Drawing.Imaging
{
	/// <summary>Encapsulates a metadata property to be included in an image file. Not inheritable.</summary>
	// Token: 0x020000AF RID: 175
	public sealed class PropertyItem
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x00003800 File Offset: 0x00001A00
		internal PropertyItem()
		{
		}

		/// <summary>Gets or sets the ID of the property.</summary>
		/// <returns>The integer that represents the ID of the property.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002572B File Offset: 0x0002392B
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00025733 File Offset: 0x00023933
		public int Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>Gets or sets the length (in bytes) of the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> property.</summary>
		/// <returns>An integer that represents the length (in bytes) of the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> byte array.</returns>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0002573C File Offset: 0x0002393C
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x00025744 File Offset: 0x00023944
		public int Len
		{
			get
			{
				return this.len;
			}
			set
			{
				this.len = value;
			}
		}

		/// <summary>Gets or sets an integer that defines the type of data contained in the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> property.</summary>
		/// <returns>An integer that defines the type of data contained in <see cref="P:System.Drawing.Imaging.PropertyItem.Value" />.</returns>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0002574D File Offset: 0x0002394D
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x00025755 File Offset: 0x00023955
		public short Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets the value of the property item.</summary>
		/// <returns>A byte array that represents the value of the property item.</returns>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0002575E File Offset: 0x0002395E
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x00025766 File Offset: 0x00023966
		public byte[] Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0400094D RID: 2381
		private int id;

		// Token: 0x0400094E RID: 2382
		private int len;

		// Token: 0x0400094F RID: 2383
		private short type;

		// Token: 0x04000950 RID: 2384
		private byte[] value;
	}
}
