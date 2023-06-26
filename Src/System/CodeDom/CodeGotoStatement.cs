using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a <see langword="goto" /> statement.</summary>
	// Token: 0x02000637 RID: 1591
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeGotoStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeGotoStatement" /> class.</summary>
		// Token: 0x060039D6 RID: 14806 RVA: 0x000F2B3A File Offset: 0x000F0D3A
		public CodeGotoStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeGotoStatement" /> class using the specified label name.</summary>
		/// <param name="label">The name of the label at which to continue program execution.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="Label" /> is <see langword="null" />.</exception>
		// Token: 0x060039D7 RID: 14807 RVA: 0x000F2B42 File Offset: 0x000F0D42
		public CodeGotoStatement(string label)
		{
			this.Label = label;
		}

		/// <summary>Gets or sets the name of the label at which to continue program execution.</summary>
		/// <returns>A string that indicates the name of the label at which to continue program execution.</returns>
		/// <exception cref="T:System.ArgumentNullException">The label cannot be set because <paramref name="value" /> is <see langword="null" /> or an empty string.</exception>
		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x000F2B51 File Offset: 0x000F0D51
		// (set) Token: 0x060039D9 RID: 14809 RVA: 0x000F2B59 File Offset: 0x000F0D59
		public string Label
		{
			get
			{
				return this.label;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				this.label = value;
			}
		}

		// Token: 0x04002BB2 RID: 11186
		private string label;
	}
}
