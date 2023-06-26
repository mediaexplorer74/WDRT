using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020003B6 RID: 950
	internal static class EncodingTable
	{
		// Token: 0x06002F69 RID: 12137 RVA: 0x000B72FC File Offset: 0x000B54FC
		[SecuritySafeCritical]
		private unsafe static int internalGetCodePageFromName(string name)
		{
			int i = 0;
			int num = EncodingTable.lastEncodingItem;
			while (num - i > 3)
			{
				int num2 = (num - i) / 2 + i;
				int num3 = string.nativeCompareOrdinalIgnoreCaseWC(name, EncodingTable.encodingDataPtr[num2].webName);
				if (num3 == 0)
				{
					return (int)EncodingTable.encodingDataPtr[num2].codePage;
				}
				if (num3 < 0)
				{
					num = num2;
				}
				else
				{
					i = num2;
				}
			}
			while (i <= num)
			{
				if (string.nativeCompareOrdinalIgnoreCaseWC(name, EncodingTable.encodingDataPtr[i].webName) == 0)
				{
					return (int)EncodingTable.encodingDataPtr[i].codePage;
				}
				i++;
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_EncodingNotSupported"), name), "name");
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x000B73B8 File Offset: 0x000B55B8
		[SecuritySafeCritical]
		internal unsafe static EncodingInfo[] GetEncodings()
		{
			if (EncodingTable.lastCodePageItem == 0)
			{
				int num = 0;
				while (EncodingTable.codePageDataPtr[num].codePage != 0)
				{
					num++;
				}
				EncodingTable.lastCodePageItem = num;
			}
			EncodingInfo[] array = new EncodingInfo[EncodingTable.lastCodePageItem];
			for (int i = 0; i < EncodingTable.lastCodePageItem; i++)
			{
				array[i] = new EncodingInfo((int)EncodingTable.codePageDataPtr[i].codePage, CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[i].Names, 0U), Environment.GetResourceString("Globalization.cp_" + EncodingTable.codePageDataPtr[i].codePage.ToString()));
			}
			return array;
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x000B7474 File Offset: 0x000B5674
		internal static int GetCodePageFromName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = EncodingTable.hashByName[name];
			if (obj != null)
			{
				return (int)obj;
			}
			int num = EncodingTable.internalGetCodePageFromName(name);
			EncodingTable.hashByName[name] = num;
			return num;
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x000B74C0 File Offset: 0x000B56C0
		[SecuritySafeCritical]
		internal unsafe static CodePageDataItem GetCodePageDataItem(int codepage)
		{
			CodePageDataItem codePageDataItem = (CodePageDataItem)EncodingTable.hashByCodePage[codepage];
			if (codePageDataItem != null)
			{
				return codePageDataItem;
			}
			int num = 0;
			int codePage;
			while ((codePage = (int)EncodingTable.codePageDataPtr[num].codePage) != 0)
			{
				if (codePage == codepage)
				{
					codePageDataItem = new CodePageDataItem(num);
					EncodingTable.hashByCodePage[codepage] = codePageDataItem;
					return codePageDataItem;
				}
				num++;
			}
			return null;
		}

		// Token: 0x06002F6D RID: 12141
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern InternalEncodingDataItem* GetEncodingData();

		// Token: 0x06002F6E RID: 12142
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNumEncodingItems();

		// Token: 0x06002F6F RID: 12143
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern InternalCodePageDataItem* GetCodePageData();

		// Token: 0x06002F70 RID: 12144
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern byte* nativeCreateOpenFileMapping(string inSectionName, int inBytesToAllocate, out IntPtr mappedFileHandle);

		// Token: 0x0400141D RID: 5149
		private static int lastEncodingItem = EncodingTable.GetNumEncodingItems() - 1;

		// Token: 0x0400141E RID: 5150
		private static volatile int lastCodePageItem;

		// Token: 0x0400141F RID: 5151
		[SecurityCritical]
		internal unsafe static InternalEncodingDataItem* encodingDataPtr = EncodingTable.GetEncodingData();

		// Token: 0x04001420 RID: 5152
		[SecurityCritical]
		internal unsafe static InternalCodePageDataItem* codePageDataPtr = EncodingTable.GetCodePageData();

		// Token: 0x04001421 RID: 5153
		private static Hashtable hashByName = Hashtable.Synchronized(new Hashtable(StringComparer.OrdinalIgnoreCase));

		// Token: 0x04001422 RID: 5154
		private static Hashtable hashByCodePage = Hashtable.Synchronized(new Hashtable());
	}
}
