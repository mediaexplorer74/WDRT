using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Allows the user to specify the ProgID of a class.</summary>
	// Token: 0x0200091B RID: 2331
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ProgIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="ProgIdAttribute" /> with the specified ProgID.</summary>
		/// <param name="progId">The ProgID to be assigned to the class.</param>
		// Token: 0x0600601E RID: 24606 RVA: 0x0014CE87 File Offset: 0x0014B087
		public ProgIdAttribute(string progId)
		{
			this._val = progId;
		}

		/// <summary>Gets the ProgID of the class.</summary>
		/// <returns>The ProgID of the class.</returns>
		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x0600601F RID: 24607 RVA: 0x0014CE96 File Offset: 0x0014B096
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A7C RID: 10876
		internal string _val;
	}
}
