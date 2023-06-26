using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Diagnostics
{
	/// <summary>Provides a set of utility functions for interpreting performance counter data.</summary>
	// Token: 0x020004C2 RID: 1218
	public static class CounterSampleCalculator
	{
		// Token: 0x06002D83 RID: 11651 RVA: 0x000CC7AC File Offset: 0x000CA9AC
		private static float GetElapsedTime(CounterSample oldSample, CounterSample newSample)
		{
			if (newSample.RawValue == 0L)
			{
				return 0f;
			}
			float num = oldSample.CounterFrequency;
			if (oldSample.UnsignedRawValue >= (ulong)newSample.CounterTimeStamp || num <= 0f)
			{
				return 0f;
			}
			float num2 = newSample.CounterTimeStamp - (long)oldSample.UnsignedRawValue;
			return num2 / num;
		}

		/// <summary>Computes the calculated value of a single raw counter sample.</summary>
		/// <param name="newSample">A <see cref="T:System.Diagnostics.CounterSample" /> that indicates the most recent sample the system has taken.</param>
		/// <returns>A floating-point representation of the performance counter's calculated value.</returns>
		// Token: 0x06002D84 RID: 11652 RVA: 0x000CC807 File Offset: 0x000CAA07
		public static float ComputeCounterValue(CounterSample newSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(CounterSample.Empty, newSample);
		}

		/// <summary>Computes the calculated value of two raw counter samples.</summary>
		/// <param name="oldSample">A <see cref="T:System.Diagnostics.CounterSample" /> that indicates a previous sample the system has taken.</param>
		/// <param name="newSample">A <see cref="T:System.Diagnostics.CounterSample" /> that indicates the most recent sample the system has taken.</param>
		/// <returns>A floating-point representation of the performance counter's calculated value.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="oldSample" /> uses a counter type that is different from <paramref name="newSample" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">
		///   <paramref name="newSample" /> counter type has a Performance Data Helper (PDH) error. For more information, see "Checking PDH Interface Return Values" in the Win32 and COM Development section of this documentation.</exception>
		// Token: 0x06002D85 RID: 11653 RVA: 0x000CC814 File Offset: 0x000CAA14
		public static float ComputeCounterValue(CounterSample oldSample, CounterSample newSample)
		{
			int counterType = (int)newSample.CounterType;
			if (oldSample.SystemFrequency == 0L)
			{
				if (counterType != 537003008 && counterType != 65536 && counterType != 0 && counterType != 65792 && counterType != 256 && counterType != 1107494144)
				{
					return 0f;
				}
			}
			else if (oldSample.CounterType != newSample.CounterType)
			{
				throw new InvalidOperationException(SR.GetString("MismatchedCounterTypes"));
			}
			if (counterType == 807666944)
			{
				return CounterSampleCalculator.GetElapsedTime(oldSample, newSample);
			}
			Microsoft.Win32.NativeMethods.PDH_RAW_COUNTER pdh_RAW_COUNTER = new Microsoft.Win32.NativeMethods.PDH_RAW_COUNTER();
			Microsoft.Win32.NativeMethods.PDH_RAW_COUNTER pdh_RAW_COUNTER2 = new Microsoft.Win32.NativeMethods.PDH_RAW_COUNTER();
			CounterSampleCalculator.FillInValues(oldSample, newSample, pdh_RAW_COUNTER2, pdh_RAW_COUNTER);
			CounterSampleCalculator.LoadPerfCounterDll();
			Microsoft.Win32.NativeMethods.PDH_FMT_COUNTERVALUE pdh_FMT_COUNTERVALUE = new Microsoft.Win32.NativeMethods.PDH_FMT_COUNTERVALUE();
			long systemFrequency = newSample.SystemFrequency;
			int num = SafeNativeMethods.FormatFromRawValue((uint)counterType, 37376U, ref systemFrequency, pdh_RAW_COUNTER, pdh_RAW_COUNTER2, pdh_FMT_COUNTERVALUE);
			if (num == 0)
			{
				return (float)pdh_FMT_COUNTERVALUE.data;
			}
			if (num == -2147481640 || num == -2147481642 || num == -2147481643)
			{
				return 0f;
			}
			throw new Win32Exception(num, SR.GetString("PerfCounterPdhError", new object[] { num.ToString("x", CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000CC928 File Offset: 0x000CAB28
		private static void FillInValues(CounterSample oldSample, CounterSample newSample, Microsoft.Win32.NativeMethods.PDH_RAW_COUNTER oldPdhValue, Microsoft.Win32.NativeMethods.PDH_RAW_COUNTER newPdhValue)
		{
			int counterType = (int)newSample.CounterType;
			if (counterType <= 537003264)
			{
				if (counterType <= 4260864)
				{
					if (counterType <= 65536)
					{
						if (counterType != 0 && counterType != 256 && counterType != 65536)
						{
							goto IL_3CE;
						}
					}
					else if (counterType <= 4195328)
					{
						if (counterType != 65792 && counterType != 4195328)
						{
							goto IL_3CE;
						}
					}
					else if (counterType != 4195584)
					{
						if (counterType != 4260864)
						{
							goto IL_3CE;
						}
						goto IL_1FE;
					}
					newPdhValue.FirstValue = newSample.RawValue;
					newPdhValue.SecondValue = 0L;
					oldPdhValue.FirstValue = oldSample.RawValue;
					oldPdhValue.SecondValue = 0L;
					return;
				}
				if (counterType <= 6620416)
				{
					if (counterType <= 4523264)
					{
						if (counterType != 4523008)
						{
							if (counterType != 4523264)
							{
								goto IL_3CE;
							}
							goto IL_268;
						}
					}
					else
					{
						if (counterType == 5571840)
						{
							newPdhValue.FirstValue = newSample.RawValue;
							newPdhValue.SecondValue = newSample.TimeStamp100nSec;
							oldPdhValue.FirstValue = oldSample.RawValue;
							oldPdhValue.SecondValue = oldSample.TimeStamp100nSec;
							return;
						}
						if (counterType != 6620416)
						{
							goto IL_3CE;
						}
					}
				}
				else if (counterType <= 272696576)
				{
					if (counterType != 272696320)
					{
						if (counterType != 272696576)
						{
							goto IL_3CE;
						}
						goto IL_268;
					}
				}
				else
				{
					if (counterType != 537003008 && counterType != 537003264)
					{
						goto IL_3CE;
					}
					goto IL_399;
				}
			}
			else
			{
				if (counterType <= 549585920)
				{
					if (counterType <= 542180608)
					{
						if (counterType == 541132032)
						{
							goto IL_268;
						}
						if (counterType == 541525248)
						{
							goto IL_399;
						}
						if (counterType != 542180608)
						{
							goto IL_3CE;
						}
					}
					else if (counterType <= 543229184)
					{
						if (counterType == 542573824)
						{
							goto IL_399;
						}
						if (counterType != 543229184)
						{
							goto IL_3CE;
						}
						goto IL_1FE;
					}
					else
					{
						if (counterType != 543622400 && counterType != 549585920)
						{
							goto IL_3CE;
						}
						goto IL_399;
					}
				}
				else if (counterType <= 575735040)
				{
					if (counterType <= 558957824)
					{
						if (counterType == 557909248)
						{
							goto IL_268;
						}
						if (counterType != 558957824)
						{
							goto IL_3CE;
						}
					}
					else
					{
						if (counterType == 574686464)
						{
							goto IL_268;
						}
						if (counterType != 575735040)
						{
							goto IL_3CE;
						}
					}
				}
				else if (counterType <= 592512256)
				{
					if (counterType == 591463680)
					{
						goto IL_268;
					}
					if (counterType != 592512256)
					{
						goto IL_3CE;
					}
				}
				else
				{
					if (counterType != 805438464 && counterType != 1073874176)
					{
						goto IL_3CE;
					}
					goto IL_399;
				}
				newPdhValue.FirstValue = newSample.RawValue;
				newPdhValue.SecondValue = newSample.TimeStamp100nSec;
				oldPdhValue.FirstValue = oldSample.RawValue;
				oldPdhValue.SecondValue = oldSample.TimeStamp100nSec;
				if ((counterType & 33554432) == 33554432)
				{
					newPdhValue.MultiCount = (int)newSample.BaseValue;
					oldPdhValue.MultiCount = (int)oldSample.BaseValue;
					return;
				}
				return;
			}
			IL_1FE:
			newPdhValue.FirstValue = newSample.RawValue;
			newPdhValue.SecondValue = newSample.TimeStamp;
			oldPdhValue.FirstValue = oldSample.RawValue;
			oldPdhValue.SecondValue = oldSample.TimeStamp;
			return;
			IL_268:
			newPdhValue.FirstValue = newSample.RawValue;
			newPdhValue.SecondValue = newSample.TimeStamp;
			oldPdhValue.FirstValue = oldSample.RawValue;
			oldPdhValue.SecondValue = oldSample.TimeStamp;
			if (counterType == 574686464 || counterType == 591463680)
			{
				newPdhValue.FirstValue *= (long)((ulong)((uint)newSample.CounterFrequency));
				if (oldSample.CounterFrequency != 0L)
				{
					oldPdhValue.FirstValue *= (long)((ulong)((uint)oldSample.CounterFrequency));
				}
			}
			if ((counterType & 33554432) == 33554432)
			{
				newPdhValue.MultiCount = (int)newSample.BaseValue;
				oldPdhValue.MultiCount = (int)oldSample.BaseValue;
				return;
			}
			return;
			IL_399:
			newPdhValue.FirstValue = newSample.RawValue;
			newPdhValue.SecondValue = newSample.BaseValue;
			oldPdhValue.FirstValue = oldSample.RawValue;
			oldPdhValue.SecondValue = oldSample.BaseValue;
			return;
			IL_3CE:
			newPdhValue.FirstValue = 0L;
			newPdhValue.SecondValue = 0L;
			oldPdhValue.FirstValue = 0L;
			oldPdhValue.SecondValue = 0L;
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000CCD24 File Offset: 0x000CAF24
		private static void LoadPerfCounterDll()
		{
			if (CounterSampleCalculator.perfCounterDllLoaded)
			{
				return;
			}
			new FileIOPermission(PermissionState.Unrestricted).Assert();
			string runtimeDirectory = RuntimeEnvironment.GetRuntimeDirectory();
			string text = Path.Combine(runtimeDirectory, "perfcounter.dll");
			if (SafeNativeMethods.LoadLibrary(text) == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			CounterSampleCalculator.perfCounterDllLoaded = true;
		}

		// Token: 0x0400271A RID: 10010
		private static volatile bool perfCounterDllLoaded;
	}
}
