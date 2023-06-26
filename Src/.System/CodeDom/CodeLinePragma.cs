using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a specific location within a specific file.</summary>
	// Token: 0x0200063B RID: 1595
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeLinePragma
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLinePragma" /> class.</summary>
		// Token: 0x060039EF RID: 14831 RVA: 0x000F2CAF File Offset: 0x000F0EAF
		public CodeLinePragma()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLinePragma" /> class.</summary>
		/// <param name="fileName">The file name of the associated file.</param>
		/// <param name="lineNumber">The line number to store a reference to.</param>
		// Token: 0x060039F0 RID: 14832 RVA: 0x000F2CB7 File Offset: 0x000F0EB7
		public CodeLinePragma(string fileName, int lineNumber)
		{
			this.FileName = fileName;
			this.LineNumber = lineNumber;
		}

		/// <summary>Gets or sets the name of the associated file.</summary>
		/// <returns>The file name of the associated file.</returns>
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x060039F1 RID: 14833 RVA: 0x000F2CCD File Offset: 0x000F0ECD
		// (set) Token: 0x060039F2 RID: 14834 RVA: 0x000F2CE3 File Offset: 0x000F0EE3
		public string FileName
		{
			get
			{
				if (this.fileName != null)
				{
					return this.fileName;
				}
				return string.Empty;
			}
			set
			{
				this.fileName = value;
			}
		}

		/// <summary>Gets or sets the line number of the associated reference.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x060039F3 RID: 14835 RVA: 0x000F2CEC File Offset: 0x000F0EEC
		// (set) Token: 0x060039F4 RID: 14836 RVA: 0x000F2CF4 File Offset: 0x000F0EF4
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x04002BBB RID: 11195
		private string fileName;

		// Token: 0x04002BBC RID: 11196
		private int lineNumber;
	}
}
