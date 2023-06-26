using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Contains static methods that interact directly with the IME API.</summary>
	// Token: 0x02000169 RID: 361
	public static class ImeContext
	{
		/// <summary>Disables the specified IME.</summary>
		/// <param name="handle">A pointer to the IME to disable.</param>
		// Token: 0x060012F5 RID: 4853 RVA: 0x0003C910 File Offset: 0x0003AB10
		public static void Disable(IntPtr handle)
		{
			if (ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable)
			{
				if (ImeContext.IsOpen(handle))
				{
					ImeContext.SetOpenStatus(false, handle);
				}
				IntPtr intPtr = UnsafeNativeMethods.ImmAssociateContext(new HandleRef(null, handle), NativeMethods.NullHandleRef);
				if (intPtr != IntPtr.Zero)
				{
					ImeContext.originalImeContext = intPtr;
				}
			}
		}

		/// <summary>Enables the specified IME.</summary>
		/// <param name="handle">A pointer to the IME to enable.</param>
		// Token: 0x060012F6 RID: 4854 RVA: 0x0003C960 File Offset: 0x0003AB60
		public static void Enable(IntPtr handle)
		{
			if (ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable)
			{
				IntPtr intPtr = UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
				if (intPtr == IntPtr.Zero)
				{
					if (ImeContext.originalImeContext == IntPtr.Zero)
					{
						intPtr = UnsafeNativeMethods.ImmCreateContext();
						if (intPtr != IntPtr.Zero)
						{
							UnsafeNativeMethods.ImmAssociateContext(new HandleRef(null, handle), new HandleRef(null, intPtr));
						}
					}
					else
					{
						UnsafeNativeMethods.ImmAssociateContext(new HandleRef(null, handle), new HandleRef(null, ImeContext.originalImeContext));
					}
				}
				else
				{
					UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, intPtr));
				}
				if (!ImeContext.IsOpen(handle))
				{
					ImeContext.SetOpenStatus(true, handle);
				}
			}
		}

		/// <summary>Returns the <see cref="T:System.Windows.Forms.ImeMode" /> of the specified IME.</summary>
		/// <param name="handle">A pointer to the IME to query.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ImeMode" /> of the specified IME.</returns>
		// Token: 0x060012F7 RID: 4855 RVA: 0x0003CA10 File Offset: 0x0003AC10
		public static ImeMode GetImeMode(IntPtr handle)
		{
			IntPtr intPtr = IntPtr.Zero;
			ImeMode[] inputLanguageTable = ImeModeConversion.InputLanguageTable;
			ImeMode imeMode;
			if (inputLanguageTable == ImeModeConversion.UnsupportedTable)
			{
				imeMode = ImeMode.Inherit;
			}
			else
			{
				intPtr = UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
				if (intPtr == IntPtr.Zero)
				{
					imeMode = ImeMode.Disable;
				}
				else if (!ImeContext.IsOpen(handle))
				{
					imeMode = inputLanguageTable[3];
				}
				else
				{
					int num = 0;
					int num2 = 0;
					UnsafeNativeMethods.ImmGetConversionStatus(new HandleRef(null, intPtr), ref num, ref num2);
					if ((num & 1) != 0)
					{
						if ((num & 2) != 0)
						{
							imeMode = (((num & 8) != 0) ? inputLanguageTable[6] : inputLanguageTable[7]);
						}
						else
						{
							imeMode = (((num & 8) != 0) ? inputLanguageTable[4] : inputLanguageTable[5]);
						}
					}
					else
					{
						imeMode = (((num & 8) != 0) ? inputLanguageTable[8] : inputLanguageTable[9]);
					}
				}
			}
			if (intPtr != IntPtr.Zero)
			{
				UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, intPtr));
			}
			return imeMode;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		internal static void TraceImeStatus(Control ctl)
		{
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		private static void TraceImeStatus(IntPtr handle)
		{
		}

		/// <summary>Returns a value that indicates whether the specified IME is open.</summary>
		/// <param name="handle">A pointer to the IME to query.</param>
		/// <returns>
		///   <see langword="true" /> if the specified IME is open; otherwise, <see langword="false" />.</returns>
		// Token: 0x060012FA RID: 4858 RVA: 0x0003CAD4 File Offset: 0x0003ACD4
		public static bool IsOpen(IntPtr handle)
		{
			IntPtr intPtr = UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
			bool flag = false;
			if (intPtr != IntPtr.Zero)
			{
				flag = UnsafeNativeMethods.ImmGetOpenStatus(new HandleRef(null, intPtr));
				UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, intPtr));
			}
			return flag;
		}

		/// <summary>Sets the status of the specified IME.</summary>
		/// <param name="imeMode">The status to set.</param>
		/// <param name="handle">A pointer to the IME to set status of.</param>
		// Token: 0x060012FB RID: 4859 RVA: 0x0003CB20 File Offset: 0x0003AD20
		public static void SetImeStatus(ImeMode imeMode, IntPtr handle)
		{
			if (imeMode != ImeMode.Inherit && imeMode != ImeMode.NoControl)
			{
				ImeMode[] inputLanguageTable = ImeModeConversion.InputLanguageTable;
				if (inputLanguageTable != ImeModeConversion.UnsupportedTable)
				{
					int num = 0;
					int num2 = 0;
					if (imeMode == ImeMode.Disable)
					{
						ImeContext.Disable(handle);
					}
					else
					{
						ImeContext.Enable(handle);
					}
					switch (imeMode)
					{
					case ImeMode.NoControl:
					case ImeMode.Disable:
						return;
					case ImeMode.On:
						imeMode = ImeMode.Hiragana;
						goto IL_78;
					case ImeMode.Off:
						if (inputLanguageTable != ImeModeConversion.JapaneseTable)
						{
							imeMode = ImeMode.Alpha;
							goto IL_78;
						}
						break;
					default:
						if (imeMode != ImeMode.Close)
						{
							goto IL_78;
						}
						break;
					}
					if (inputLanguageTable != ImeModeConversion.KoreanTable)
					{
						ImeContext.SetOpenStatus(false, handle);
						return;
					}
					imeMode = ImeMode.Alpha;
					IL_78:
					if (ImeModeConversion.ImeModeConversionBits.ContainsKey(imeMode))
					{
						ImeModeConversion imeModeConversion = ImeModeConversion.ImeModeConversionBits[imeMode];
						IntPtr intPtr = UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
						UnsafeNativeMethods.ImmGetConversionStatus(new HandleRef(null, intPtr), ref num, ref num2);
						num |= imeModeConversion.setBits;
						num &= ~imeModeConversion.clearBits;
						bool flag = UnsafeNativeMethods.ImmSetConversionStatus(new HandleRef(null, intPtr), num, num2);
						UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, intPtr));
					}
				}
			}
		}

		/// <summary>Opens or closes the IME context.</summary>
		/// <param name="open">
		///   <see langword="true" /> to open the IME or <see langword="false" /> to close it.</param>
		/// <param name="handle">A pointer to the IME to open or close.</param>
		// Token: 0x060012FC RID: 4860 RVA: 0x0003CC18 File Offset: 0x0003AE18
		public static void SetOpenStatus(bool open, IntPtr handle)
		{
			if (ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable)
			{
				IntPtr intPtr = UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
				if (intPtr != IntPtr.Zero)
				{
					bool flag = UnsafeNativeMethods.ImmSetOpenStatus(new HandleRef(null, intPtr), open);
					if (flag)
					{
						flag = UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, intPtr));
					}
				}
			}
		}

		// Token: 0x040008EC RID: 2284
		private static IntPtr originalImeContext;
	}
}
