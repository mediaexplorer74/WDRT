using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the types defined within an assembly were originally defined in a type library.</summary>
	// Token: 0x0200091C RID: 2332
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class ImportedFromTypeLibAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ImportedFromTypeLibAttribute" /> class with the name of the original type library file.</summary>
		/// <param name="tlbFile">The location of the original type library file.</param>
		// Token: 0x06006020 RID: 24608 RVA: 0x0014CE9E File Offset: 0x0014B09E
		public ImportedFromTypeLibAttribute(string tlbFile)
		{
			this._val = tlbFile;
		}

		/// <summary>Gets the name of the original type library file.</summary>
		/// <returns>The name of the original type library file.</returns>
		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06006021 RID: 24609 RVA: 0x0014CEAD File Offset: 0x0014B0AD
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A7D RID: 10877
		internal string _val;
	}
}
