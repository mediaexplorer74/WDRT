using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Text
{
	// Token: 0x02000A59 RID: 2649
	[Serializable]
	internal abstract class BaseCodePageEncoding : EncodingNLS, ISerializable
	{
		// Token: 0x06006767 RID: 26471 RVA: 0x0015E6C9 File Offset: 0x0015C8C9
		[SecurityCritical]
		internal BaseCodePageEncoding(int codepage)
			: this(codepage, codepage)
		{
		}

		// Token: 0x06006768 RID: 26472 RVA: 0x0015E6D3 File Offset: 0x0015C8D3
		[SecurityCritical]
		internal BaseCodePageEncoding(int codepage, int dataCodePage)
		{
			this.bFlagDataTable = true;
			this.pCodePage = null;
			base..ctor((codepage == 0) ? Win32Native.GetACP() : codepage);
			this.dataTableCodePage = dataCodePage;
			this.LoadCodePageTables();
		}

		// Token: 0x06006769 RID: 26473 RVA: 0x0015E702 File Offset: 0x0015C902
		[SecurityCritical]
		internal BaseCodePageEncoding(SerializationInfo info, StreamingContext context)
		{
			this.bFlagDataTable = true;
			this.pCodePage = null;
			base..ctor(0);
			throw new ArgumentNullException("this");
		}

		// Token: 0x0600676A RID: 26474 RVA: 0x0015E724 File Offset: 0x0015C924
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.SerializeEncoding(info, context);
			info.AddValue(this.m_bUseMlangTypeForSerialization ? "m_maxByteSize" : "maxCharSize", this.IsSingleByte ? 1 : 2);
			info.SetType(this.m_bUseMlangTypeForSerialization ? typeof(MLangCodePageEncoding) : typeof(CodePageEncoding));
		}

		// Token: 0x0600676B RID: 26475 RVA: 0x0015E784 File Offset: 0x0015C984
		[SecurityCritical]
		private unsafe void LoadCodePageTables()
		{
			BaseCodePageEncoding.CodePageHeader* ptr = BaseCodePageEncoding.FindCodePage(this.dataTableCodePage);
			if (ptr == null)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", new object[] { this.CodePage }));
			}
			this.pCodePage = ptr;
			this.LoadManagedCodePage();
		}

		// Token: 0x0600676C RID: 26476 RVA: 0x0015E7D4 File Offset: 0x0015C9D4
		[SecurityCritical]
		private unsafe static BaseCodePageEncoding.CodePageHeader* FindCodePage(int codePage)
		{
			for (int i = 0; i < (int)BaseCodePageEncoding.m_pCodePageFileHeader->CodePageCount; i++)
			{
				BaseCodePageEncoding.CodePageIndex* ptr = &BaseCodePageEncoding.m_pCodePageFileHeader->CodePages + i;
				if ((int)ptr->CodePage == codePage)
				{
					return (BaseCodePageEncoding.CodePageHeader*)(BaseCodePageEncoding.m_pCodePageFileHeader + ptr->Offset / sizeof(BaseCodePageEncoding.CodePageDataFileHeader));
				}
			}
			return null;
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x0015E828 File Offset: 0x0015CA28
		[SecurityCritical]
		internal unsafe static int GetCodePageByteSize(int codePage)
		{
			BaseCodePageEncoding.CodePageHeader* ptr = BaseCodePageEncoding.FindCodePage(codePage);
			if (ptr == null)
			{
				return 0;
			}
			return (int)ptr->ByteCount;
		}

		// Token: 0x0600676E RID: 26478
		[SecurityCritical]
		protected abstract void LoadManagedCodePage();

		// Token: 0x0600676F RID: 26479 RVA: 0x0015E84C File Offset: 0x0015CA4C
		[SecurityCritical]
		protected unsafe byte* GetSharedMemory(int iSize)
		{
			string memorySectionName = this.GetMemorySectionName();
			IntPtr intPtr;
			byte* ptr = EncodingTable.nativeCreateOpenFileMapping(memorySectionName, iSize, out intPtr);
			if (ptr == null)
			{
				throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
			}
			if (intPtr != IntPtr.Zero)
			{
				this.safeMemorySectionHandle = new SafeViewOfFileHandle((IntPtr)((void*)ptr), true);
				this.safeFileMappingHandle = new SafeFileMappingHandle(intPtr, true);
			}
			return ptr;
		}

		// Token: 0x06006770 RID: 26480 RVA: 0x0015E8AC File Offset: 0x0015CAAC
		[SecurityCritical]
		protected unsafe virtual string GetMemorySectionName()
		{
			int num = (this.bFlagDataTable ? this.dataTableCodePage : this.CodePage);
			return string.Format(CultureInfo.InvariantCulture, "NLS_CodePage_{0}_{1}_{2}_{3}_{4}", new object[]
			{
				num,
				this.pCodePage->VersionMajor,
				this.pCodePage->VersionMinor,
				this.pCodePage->VersionRevision,
				this.pCodePage->VersionBuild
			});
		}

		// Token: 0x06006771 RID: 26481
		[SecurityCritical]
		protected abstract void ReadBestFitTable();

		// Token: 0x06006772 RID: 26482 RVA: 0x0015E93C File Offset: 0x0015CB3C
		[SecuritySafeCritical]
		internal override char[] GetBestFitUnicodeToBytesData()
		{
			if (this.arrayUnicodeBestFit == null)
			{
				this.ReadBestFitTable();
			}
			return this.arrayUnicodeBestFit;
		}

		// Token: 0x06006773 RID: 26483 RVA: 0x0015E952 File Offset: 0x0015CB52
		[SecuritySafeCritical]
		internal override char[] GetBestFitBytesToUnicodeData()
		{
			if (this.arrayBytesBestFit == null)
			{
				this.ReadBestFitTable();
			}
			return this.arrayBytesBestFit;
		}

		// Token: 0x06006774 RID: 26484 RVA: 0x0015E968 File Offset: 0x0015CB68
		[SecurityCritical]
		internal void CheckMemorySection()
		{
			if (this.safeMemorySectionHandle != null && this.safeMemorySectionHandle.DangerousGetHandle() == IntPtr.Zero)
			{
				this.LoadManagedCodePage();
			}
		}

		// Token: 0x04002E31 RID: 11825
		internal const string CODE_PAGE_DATA_FILE_NAME = "codepages.nlp";

		// Token: 0x04002E32 RID: 11826
		[NonSerialized]
		protected int dataTableCodePage;

		// Token: 0x04002E33 RID: 11827
		[NonSerialized]
		protected bool bFlagDataTable;

		// Token: 0x04002E34 RID: 11828
		[NonSerialized]
		protected int iExtraBytes;

		// Token: 0x04002E35 RID: 11829
		[NonSerialized]
		protected char[] arrayUnicodeBestFit;

		// Token: 0x04002E36 RID: 11830
		[NonSerialized]
		protected char[] arrayBytesBestFit;

		// Token: 0x04002E37 RID: 11831
		[NonSerialized]
		protected bool m_bUseMlangTypeForSerialization;

		// Token: 0x04002E38 RID: 11832
		[SecurityCritical]
		private unsafe static BaseCodePageEncoding.CodePageDataFileHeader* m_pCodePageFileHeader = (BaseCodePageEncoding.CodePageDataFileHeader*)GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof(CharUnicodeInfo).Assembly, "codepages.nlp");

		// Token: 0x04002E39 RID: 11833
		[SecurityCritical]
		[NonSerialized]
		protected unsafe BaseCodePageEncoding.CodePageHeader* pCodePage;

		// Token: 0x04002E3A RID: 11834
		[SecurityCritical]
		[NonSerialized]
		protected SafeViewOfFileHandle safeMemorySectionHandle;

		// Token: 0x04002E3B RID: 11835
		[SecurityCritical]
		[NonSerialized]
		protected SafeFileMappingHandle safeFileMappingHandle;

		// Token: 0x02000CA6 RID: 3238
		[StructLayout(LayoutKind.Explicit)]
		internal struct CodePageDataFileHeader
		{
			// Token: 0x0400388C RID: 14476
			[FieldOffset(0)]
			internal char TableName;

			// Token: 0x0400388D RID: 14477
			[FieldOffset(32)]
			internal ushort Version;

			// Token: 0x0400388E RID: 14478
			[FieldOffset(40)]
			internal short CodePageCount;

			// Token: 0x0400388F RID: 14479
			[FieldOffset(42)]
			internal short unused1;

			// Token: 0x04003890 RID: 14480
			[FieldOffset(44)]
			internal BaseCodePageEncoding.CodePageIndex CodePages;
		}

		// Token: 0x02000CA7 RID: 3239
		[StructLayout(LayoutKind.Explicit, Pack = 2)]
		internal struct CodePageIndex
		{
			// Token: 0x04003891 RID: 14481
			[FieldOffset(0)]
			internal char CodePageName;

			// Token: 0x04003892 RID: 14482
			[FieldOffset(32)]
			internal short CodePage;

			// Token: 0x04003893 RID: 14483
			[FieldOffset(34)]
			internal short ByteCount;

			// Token: 0x04003894 RID: 14484
			[FieldOffset(36)]
			internal int Offset;
		}

		// Token: 0x02000CA8 RID: 3240
		[StructLayout(LayoutKind.Explicit)]
		internal struct CodePageHeader
		{
			// Token: 0x04003895 RID: 14485
			[FieldOffset(0)]
			internal char CodePageName;

			// Token: 0x04003896 RID: 14486
			[FieldOffset(32)]
			internal ushort VersionMajor;

			// Token: 0x04003897 RID: 14487
			[FieldOffset(34)]
			internal ushort VersionMinor;

			// Token: 0x04003898 RID: 14488
			[FieldOffset(36)]
			internal ushort VersionRevision;

			// Token: 0x04003899 RID: 14489
			[FieldOffset(38)]
			internal ushort VersionBuild;

			// Token: 0x0400389A RID: 14490
			[FieldOffset(40)]
			internal short CodePage;

			// Token: 0x0400389B RID: 14491
			[FieldOffset(42)]
			internal short ByteCount;

			// Token: 0x0400389C RID: 14492
			[FieldOffset(44)]
			internal char UnicodeReplace;

			// Token: 0x0400389D RID: 14493
			[FieldOffset(46)]
			internal ushort ByteReplace;

			// Token: 0x0400389E RID: 14494
			[FieldOffset(48)]
			internal short FirstDataWord;
		}
	}
}
