using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A7A RID: 2682
	internal class Normalization
	{
		// Token: 0x060068CD RID: 26829 RVA: 0x001639C0 File Offset: 0x00161BC0
		[SecurityCritical]
		private unsafe static void InitializeForm(NormalizationForm form, string strDataFile)
		{
			byte* ptr = null;
			if (!Environment.IsWindows8OrAbove)
			{
				if (strDataFile == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNormalizationForm"));
				}
				ptr = GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof(Normalization).Assembly, strDataFile);
				if (ptr == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNormalizationForm"));
				}
			}
			Normalization.nativeNormalizationInitNormalization(form, ptr);
		}

		// Token: 0x060068CE RID: 26830 RVA: 0x00163A1C File Offset: 0x00161C1C
		[SecurityCritical]
		private static void EnsureInitialized(NormalizationForm form)
		{
			if (form <= (NormalizationForm)13)
			{
				switch (form)
				{
				case NormalizationForm.FormC:
					if (Normalization.NFC)
					{
						return;
					}
					Normalization.InitializeForm(form, "normnfc.nlp");
					Normalization.NFC = true;
					return;
				case NormalizationForm.FormD:
					if (Normalization.NFD)
					{
						return;
					}
					Normalization.InitializeForm(form, "normnfd.nlp");
					Normalization.NFD = true;
					return;
				case (NormalizationForm)3:
				case (NormalizationForm)4:
					break;
				case NormalizationForm.FormKC:
					if (Normalization.NFKC)
					{
						return;
					}
					Normalization.InitializeForm(form, "normnfkc.nlp");
					Normalization.NFKC = true;
					return;
				case NormalizationForm.FormKD:
					if (Normalization.NFKD)
					{
						return;
					}
					Normalization.InitializeForm(form, "normnfkd.nlp");
					Normalization.NFKD = true;
					return;
				default:
					if (form == (NormalizationForm)13)
					{
						if (Normalization.IDNA)
						{
							return;
						}
						Normalization.InitializeForm(form, "normidna.nlp");
						Normalization.IDNA = true;
						return;
					}
					break;
				}
			}
			else
			{
				switch (form)
				{
				case (NormalizationForm)257:
					if (Normalization.NFCDisallowUnassigned)
					{
						return;
					}
					Normalization.InitializeForm(form, "normnfc.nlp");
					Normalization.NFCDisallowUnassigned = true;
					return;
				case (NormalizationForm)258:
					if (Normalization.NFDDisallowUnassigned)
					{
						return;
					}
					Normalization.InitializeForm(form, "normnfd.nlp");
					Normalization.NFDDisallowUnassigned = true;
					return;
				case (NormalizationForm)259:
				case (NormalizationForm)260:
					break;
				case (NormalizationForm)261:
					if (Normalization.NFKCDisallowUnassigned)
					{
						return;
					}
					Normalization.InitializeForm(form, "normnfkc.nlp");
					Normalization.NFKCDisallowUnassigned = true;
					return;
				case (NormalizationForm)262:
					if (Normalization.NFKDDisallowUnassigned)
					{
						return;
					}
					Normalization.InitializeForm(form, "normnfkd.nlp");
					Normalization.NFKDDisallowUnassigned = true;
					return;
				default:
					if (form == (NormalizationForm)269)
					{
						if (Normalization.IDNADisallowUnassigned)
						{
							return;
						}
						Normalization.InitializeForm(form, "normidna.nlp");
						Normalization.IDNADisallowUnassigned = true;
						return;
					}
					break;
				}
			}
			if (Normalization.Other)
			{
				return;
			}
			Normalization.InitializeForm(form, null);
			Normalization.Other = true;
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x00163BD8 File Offset: 0x00161DD8
		[SecurityCritical]
		internal static bool IsNormalized(string strInput, NormalizationForm normForm)
		{
			Normalization.EnsureInitialized(normForm);
			int num = 0;
			bool flag = Normalization.nativeNormalizationIsNormalizedString(normForm, ref num, strInput, strInput.Length);
			if (num <= 8)
			{
				if (num == 0)
				{
					return flag;
				}
				if (num == 8)
				{
					throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
				}
			}
			else if (num == 87 || num == 1113)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), "strInput");
			}
			throw new InvalidOperationException(Environment.GetResourceString("UnknownError_Num", new object[] { num }));
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x00163C5C File Offset: 0x00161E5C
		[SecurityCritical]
		internal static string Normalize(string strInput, NormalizationForm normForm)
		{
			Normalization.EnsureInitialized(normForm);
			int num = 0;
			int num2 = Normalization.nativeNormalizationNormalizeString(normForm, ref num, strInput, strInput.Length, null, 0);
			if (num != 0)
			{
				if (num == 87)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), "strInput");
				}
				if (num == 8)
				{
					throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
				}
				throw new InvalidOperationException(Environment.GetResourceString("UnknownError_Num", new object[] { num }));
			}
			else
			{
				if (num2 == 0)
				{
					return string.Empty;
				}
				char[] array;
				for (;;)
				{
					array = new char[num2];
					num2 = Normalization.nativeNormalizationNormalizeString(normForm, ref num, strInput, strInput.Length, array, array.Length);
					if (num == 0)
					{
						goto IL_103;
					}
					if (num <= 87)
					{
						break;
					}
					if (num != 122)
					{
						goto Block_9;
					}
				}
				if (num == 8)
				{
					throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
				}
				if (num != 87)
				{
					goto IL_E4;
				}
				goto IL_B0;
				Block_9:
				if (num != 1113)
				{
					goto IL_E4;
				}
				IL_B0:
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", new object[] { num2 }), "strInput");
				IL_E4:
				throw new InvalidOperationException(Environment.GetResourceString("UnknownError_Num", new object[] { num }));
				IL_103:
				return new string(array, 0, num2);
			}
		}

		// Token: 0x060068D1 RID: 26833
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int nativeNormalizationNormalizeString(NormalizationForm normForm, ref int iError, string lpSrcString, int cwSrcLength, char[] lpDstString, int cwDstLength);

		// Token: 0x060068D2 RID: 26834
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool nativeNormalizationIsNormalizedString(NormalizationForm normForm, ref int iError, string lpString, int cwLength);

		// Token: 0x060068D3 RID: 26835
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void nativeNormalizationInitNormalization(NormalizationForm normForm, byte* pTableData);

		// Token: 0x04002EF4 RID: 12020
		private static volatile bool NFC;

		// Token: 0x04002EF5 RID: 12021
		private static volatile bool NFD;

		// Token: 0x04002EF6 RID: 12022
		private static volatile bool NFKC;

		// Token: 0x04002EF7 RID: 12023
		private static volatile bool NFKD;

		// Token: 0x04002EF8 RID: 12024
		private static volatile bool IDNA;

		// Token: 0x04002EF9 RID: 12025
		private static volatile bool NFCDisallowUnassigned;

		// Token: 0x04002EFA RID: 12026
		private static volatile bool NFDDisallowUnassigned;

		// Token: 0x04002EFB RID: 12027
		private static volatile bool NFKCDisallowUnassigned;

		// Token: 0x04002EFC RID: 12028
		private static volatile bool NFKDDisallowUnassigned;

		// Token: 0x04002EFD RID: 12029
		private static volatile bool IDNADisallowUnassigned;

		// Token: 0x04002EFE RID: 12030
		private static volatile bool Other;

		// Token: 0x04002EFF RID: 12031
		private const int ERROR_SUCCESS = 0;

		// Token: 0x04002F00 RID: 12032
		private const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x04002F01 RID: 12033
		private const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x04002F02 RID: 12034
		private const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x04002F03 RID: 12035
		private const int ERROR_NO_UNICODE_TRANSLATION = 1113;
	}
}
