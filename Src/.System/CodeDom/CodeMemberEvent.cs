using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an event of a type.</summary>
	// Token: 0x0200063C RID: 1596
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMemberEvent : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the delegate type that handles the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the delegate type that handles the event.</returns>
		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060039F6 RID: 14838 RVA: 0x000F2D05 File Offset: 0x000F0F05
		// (set) Token: 0x060039F7 RID: 14839 RVA: 0x000F2D25 File Offset: 0x000F0F25
		public CodeTypeReference Type
		{
			get
			{
				if (this.type == null)
				{
					this.type = new CodeTypeReference("");
				}
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets the privately implemented data type, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type that the event privately implements.</returns>
		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060039F8 RID: 14840 RVA: 0x000F2D2E File Offset: 0x000F0F2E
		// (set) Token: 0x060039F9 RID: 14841 RVA: 0x000F2D36 File Offset: 0x000F0F36
		public CodeTypeReference PrivateImplementationType
		{
			get
			{
				return this.privateImplements;
			}
			set
			{
				this.privateImplements = value;
			}
		}

		/// <summary>Gets or sets the data type that the member event implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data type or types that the member event implements.</returns>
		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060039FA RID: 14842 RVA: 0x000F2D3F File Offset: 0x000F0F3F
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				if (this.implementationTypes == null)
				{
					this.implementationTypes = new CodeTypeReferenceCollection();
				}
				return this.implementationTypes;
			}
		}

		// Token: 0x04002BBD RID: 11197
		private CodeTypeReference type;

		// Token: 0x04002BBE RID: 11198
		private CodeTypeReference privateImplements;

		// Token: 0x04002BBF RID: 11199
		private CodeTypeReferenceCollection implementationTypes;
	}
}
