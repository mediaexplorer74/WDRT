using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	/// <summary>Represents the <see langword="abstract" /> base class from which all code statements derive.</summary>
	// Token: 0x02000655 RID: 1621
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeStatement : CodeObject
	{
		/// <summary>Gets or sets the line on which the code statement occurs.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> object that indicates the context of the code statement.</returns>
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x000F3E0E File Offset: 0x000F200E
		// (set) Token: 0x06003AB9 RID: 15033 RVA: 0x000F3E16 File Offset: 0x000F2016
		public CodeLinePragma LinePragma
		{
			get
			{
				return this.linePragma;
			}
			set
			{
				this.linePragma = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object that contains start directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003ABA RID: 15034 RVA: 0x000F3E1F File Offset: 0x000F201F
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				if (this.startDirectives == null)
				{
					this.startDirectives = new CodeDirectiveCollection();
				}
				return this.startDirectives;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object that contains end directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x000F3E3A File Offset: 0x000F203A
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				if (this.endDirectives == null)
				{
					this.endDirectives = new CodeDirectiveCollection();
				}
				return this.endDirectives;
			}
		}

		// Token: 0x04002C06 RID: 11270
		private CodeLinePragma linePragma;

		// Token: 0x04002C07 RID: 11271
		[OptionalField]
		private CodeDirectiveCollection startDirectives;

		// Token: 0x04002C08 RID: 11272
		[OptionalField]
		private CodeDirectiveCollection endDirectives;
	}
}
