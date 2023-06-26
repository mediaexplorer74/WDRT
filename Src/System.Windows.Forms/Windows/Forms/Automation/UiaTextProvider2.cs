using System;

namespace System.Windows.Forms.Automation
{
	// Token: 0x020004F2 RID: 1266
	internal abstract class UiaTextProvider2 : UiaTextProvider, UnsafeNativeMethods.UiaCore.ITextProvider2, UnsafeNativeMethods.UiaCore.ITextProvider
	{
		// Token: 0x06005264 RID: 21092
		public abstract UnsafeNativeMethods.UiaCore.ITextRangeProvider GetCaretRange(out UnsafeNativeMethods.BOOL isActive);

		// Token: 0x06005265 RID: 21093
		public abstract UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement);
	}
}
