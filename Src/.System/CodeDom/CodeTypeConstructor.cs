using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a static constructor for a class.</summary>
	// Token: 0x0200065A RID: 1626
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeConstructor" /> class.</summary>
		// Token: 0x06003AD6 RID: 15062 RVA: 0x000F4094 File Offset: 0x000F2294
		public CodeTypeConstructor()
		{
			base.Name = ".cctor";
		}
	}
}
