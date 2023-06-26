using System;
using System.ComponentModel;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the paper tray from which the printer gets paper.</summary>
	// Token: 0x0200005B RID: 91
	[Serializable]
	public class PaperSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PaperSource" /> class.</summary>
		// Token: 0x06000758 RID: 1880 RVA: 0x0001DF68 File Offset: 0x0001C168
		public PaperSource()
		{
			this.kind = PaperSourceKind.Custom;
			this.name = string.Empty;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001DF86 File Offset: 0x0001C186
		internal PaperSource(PaperSourceKind kind, string name)
		{
			this.kind = kind;
			this.name = name;
		}

		/// <summary>Gets the paper source.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PaperSourceKind" /> values.</returns>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001DF9C File Offset: 0x0001C19C
		public PaperSourceKind Kind
		{
			get
			{
				if (this.kind >= (PaperSourceKind)256)
				{
					return PaperSourceKind.Custom;
				}
				return this.kind;
			}
		}

		/// <summary>Gets or sets the integer representing one of the <see cref="T:System.Drawing.Printing.PaperSourceKind" /> values or a custom value.</summary>
		/// <returns>The integer value representing one of the <see cref="T:System.Drawing.Printing.PaperSourceKind" /> values or a custom value.</returns>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001DFB7 File Offset: 0x0001C1B7
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x0001DFBF File Offset: 0x0001C1BF
		public int RawKind
		{
			get
			{
				return (int)this.kind;
			}
			set
			{
				this.kind = (PaperSourceKind)value;
			}
		}

		/// <summary>Gets or sets the name of the paper source.</summary>
		/// <returns>The name of the paper source.</returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001DFC8 File Offset: 0x0001C1C8
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
		public string SourceName
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Provides information about the <see cref="T:System.Drawing.Printing.PaperSource" /> in string form.</summary>
		/// <returns>A string.</returns>
		// Token: 0x0600075F RID: 1887 RVA: 0x0001DFDC File Offset: 0x0001C1DC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[PaperSource ",
				this.SourceName,
				" Kind=",
				TypeDescriptor.GetConverter(typeof(PaperSourceKind)).ConvertToString(this.Kind),
				"]"
			});
		}

		// Token: 0x040006A3 RID: 1699
		private string name;

		// Token: 0x040006A4 RID: 1700
		private PaperSourceKind kind;
	}
}
