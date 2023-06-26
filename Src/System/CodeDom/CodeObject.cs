using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Provides a common base class for most Code Document Object Model (CodeDOM) objects.</summary>
	// Token: 0x02000647 RID: 1607
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeObject
	{
		/// <summary>Gets the user-definable data for the current object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing user data for the current object.</returns>
		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x000F391A File Offset: 0x000F1B1A
		public IDictionary UserData
		{
			get
			{
				if (this.userData == null)
				{
					this.userData = new ListDictionary();
				}
				return this.userData;
			}
		}

		// Token: 0x04002BEE RID: 11246
		private IDictionary userData;
	}
}
