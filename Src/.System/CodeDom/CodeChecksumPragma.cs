using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a code checksum pragma code entity.</summary>
	// Token: 0x02000624 RID: 1572
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeChecksumPragma : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class.</summary>
		// Token: 0x0600395F RID: 14687 RVA: 0x000F2344 File Offset: 0x000F0544
		public CodeChecksumPragma()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class using a file name, a GUID representing the checksum algorithm, and a byte stream representing the checksum data.</summary>
		/// <param name="fileName">The path to the checksum file.</param>
		/// <param name="checksumAlgorithmId">A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</param>
		/// <param name="checksumData">A byte array that contains the checksum data.</param>
		// Token: 0x06003960 RID: 14688 RVA: 0x000F234C File Offset: 0x000F054C
		public CodeChecksumPragma(string fileName, Guid checksumAlgorithmId, byte[] checksumData)
		{
			this.fileName = fileName;
			this.checksumAlgorithmId = checksumAlgorithmId;
			this.checksumData = checksumData;
		}

		/// <summary>Gets or sets the path to the checksum file.</summary>
		/// <returns>The path to the checksum file.</returns>
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06003961 RID: 14689 RVA: 0x000F2369 File Offset: 0x000F0569
		// (set) Token: 0x06003962 RID: 14690 RVA: 0x000F237F File Offset: 0x000F057F
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

		/// <summary>Gets or sets a GUID that identifies the checksum algorithm to use.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06003963 RID: 14691 RVA: 0x000F2388 File Offset: 0x000F0588
		// (set) Token: 0x06003964 RID: 14692 RVA: 0x000F2390 File Offset: 0x000F0590
		public Guid ChecksumAlgorithmId
		{
			get
			{
				return this.checksumAlgorithmId;
			}
			set
			{
				this.checksumAlgorithmId = value;
			}
		}

		/// <summary>Gets or sets the value of the data for the checksum calculation.</summary>
		/// <returns>A byte array that contains the data for the checksum calculation.</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06003965 RID: 14693 RVA: 0x000F2399 File Offset: 0x000F0599
		// (set) Token: 0x06003966 RID: 14694 RVA: 0x000F23A1 File Offset: 0x000F05A1
		public byte[] ChecksumData
		{
			get
			{
				return this.checksumData;
			}
			set
			{
				this.checksumData = value;
			}
		}

		// Token: 0x04002B95 RID: 11157
		private string fileName;

		// Token: 0x04002B96 RID: 11158
		private byte[] checksumData;

		// Token: 0x04002B97 RID: 11159
		private Guid checksumAlgorithmId;
	}
}
