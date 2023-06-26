using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Contains information about a windows-format (WMF) metafile.</summary>
	// Token: 0x020000AB RID: 171
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public sealed class MetaHeader
	{
		/// <summary>Gets or sets the type of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
		/// <returns>The type of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x000256B4 File Offset: 0x000238B4
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x000256BC File Offset: 0x000238BC
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

		/// <summary>Gets or sets the size, in bytes, of the header file.</summary>
		/// <returns>The size, in bytes, of the header file.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x000256C5 File Offset: 0x000238C5
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x000256CD File Offset: 0x000238CD
		public short HeaderSize
		{
			get
			{
				return this.headerSize;
			}
			set
			{
				this.headerSize = value;
			}
		}

		/// <summary>Gets or sets the version number of the header format.</summary>
		/// <returns>The version number of the header format.</returns>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x000256D6 File Offset: 0x000238D6
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x000256DE File Offset: 0x000238DE
		public short Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		/// <summary>Gets or sets the size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
		/// <returns>The size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x000256E7 File Offset: 0x000238E7
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x000256EF File Offset: 0x000238EF
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		/// <summary>Gets or sets the maximum number of objects that exist in the <see cref="T:System.Drawing.Imaging.Metafile" /> object at the same time.</summary>
		/// <returns>The maximum number of objects that exist in the <see cref="T:System.Drawing.Imaging.Metafile" /> object at the same time.</returns>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x000256F8 File Offset: 0x000238F8
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x00025700 File Offset: 0x00023900
		public short NoObjects
		{
			get
			{
				return this.noObjects;
			}
			set
			{
				this.noObjects = value;
			}
		}

		/// <summary>Gets or sets the size, in bytes, of the largest record in the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
		/// <returns>The size, in bytes, of the largest record in the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00025709 File Offset: 0x00023909
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x00025711 File Offset: 0x00023911
		public int MaxRecord
		{
			get
			{
				return this.maxRecord;
			}
			set
			{
				this.maxRecord = value;
			}
		}

		/// <summary>Not used. Always returns 0.</summary>
		/// <returns>Always 0.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002571A File Offset: 0x0002391A
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x00025722 File Offset: 0x00023922
		public short NoParameters
		{
			get
			{
				return this.noParameters;
			}
			set
			{
				this.noParameters = value;
			}
		}

		// Token: 0x0400092A RID: 2346
		private short type;

		// Token: 0x0400092B RID: 2347
		private short headerSize;

		// Token: 0x0400092C RID: 2348
		private short version;

		// Token: 0x0400092D RID: 2349
		private int size;

		// Token: 0x0400092E RID: 2350
		private short noObjects;

		// Token: 0x0400092F RID: 2351
		private int maxRecord;

		// Token: 0x04000930 RID: 2352
		private short noParameters;
	}
}
