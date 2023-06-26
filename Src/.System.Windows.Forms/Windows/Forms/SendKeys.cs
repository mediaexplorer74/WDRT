using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Provides methods for sending keystrokes to an application.</summary>
	// Token: 0x02000367 RID: 871
	public class SendKeys
	{
		// Token: 0x06003894 RID: 14484 RVA: 0x000FAD34 File Offset: 0x000F8F34
		static SendKeys()
		{
			Application.ThreadExit += SendKeys.OnThreadExit;
			SendKeys.messageWindow = new SendKeys.SKWindow();
			SendKeys.messageWindow.CreateControl();
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x00002843 File Offset: 0x00000A43
		private SendKeys()
		{
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000FB097 File Offset: 0x000F9297
		private static void AddEvent(SendKeys.SKEvent skevent)
		{
			if (SendKeys.events == null)
			{
				SendKeys.events = new Queue();
			}
			SendKeys.events.Enqueue(skevent);
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000FB0B8 File Offset: 0x000F92B8
		private static bool AddSimpleKey(char character, int repeat, IntPtr hwnd, int[] haveKeys, bool fStartNewChar, int cGrp)
		{
			int num = (int)UnsafeNativeMethods.VkKeyScan(character);
			if (num != -1)
			{
				if (haveKeys[0] == 0 && (num & 256) != 0)
				{
					SendKeys.AddEvent(new SendKeys.SKEvent(256, 16, fStartNewChar, hwnd));
					fStartNewChar = false;
					haveKeys[0] = 10;
				}
				if (haveKeys[1] == 0 && (num & 512) != 0)
				{
					SendKeys.AddEvent(new SendKeys.SKEvent(256, 17, fStartNewChar, hwnd));
					fStartNewChar = false;
					haveKeys[1] = 10;
				}
				if (haveKeys[2] == 0 && (num & 1024) != 0)
				{
					SendKeys.AddEvent(new SendKeys.SKEvent(256, 18, fStartNewChar, hwnd));
					fStartNewChar = false;
					haveKeys[2] = 10;
				}
				SendKeys.AddMsgsForVK(num & 255, repeat, haveKeys[2] > 0 && haveKeys[1] == 0, hwnd);
				SendKeys.CancelMods(haveKeys, 10, hwnd);
			}
			else
			{
				int num2 = SafeNativeMethods.OemKeyScan((short)('ÿ' & character));
				for (int i = 0; i < repeat; i++)
				{
					SendKeys.AddEvent(new SendKeys.SKEvent(258, (int)character, num2 & 65535, hwnd));
				}
			}
			if (cGrp != 0)
			{
				fStartNewChar = true;
			}
			return fStartNewChar;
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000FB1B4 File Offset: 0x000F93B4
		private static void AddMsgsForVK(int vk, int repeat, bool altnoctrldown, IntPtr hwnd)
		{
			for (int i = 0; i < repeat; i++)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(altnoctrldown ? 260 : 256, vk, SendKeys.fStartNewChar, hwnd));
				SendKeys.AddEvent(new SendKeys.SKEvent(altnoctrldown ? 261 : 257, vk, SendKeys.fStartNewChar, hwnd));
			}
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000FB210 File Offset: 0x000F9410
		private static void CancelMods(int[] haveKeys, int level, IntPtr hwnd)
		{
			if (haveKeys[0] == level)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(257, 16, false, hwnd));
				haveKeys[0] = 0;
			}
			if (haveKeys[1] == level)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(257, 17, false, hwnd));
				haveKeys[1] = 0;
			}
			if (haveKeys[2] == level)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(261, 18, false, hwnd));
				haveKeys[2] = 0;
			}
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000FB274 File Offset: 0x000F9474
		private static void InstallHook()
		{
			if (SendKeys.hhook == IntPtr.Zero)
			{
				SendKeys.hook = new NativeMethods.HookProc(new SendKeys.SendKeysHookProc().Callback);
				SendKeys.stopHook = false;
				SendKeys.hhook = UnsafeNativeMethods.SetWindowsHookEx(1, SendKeys.hook, new HandleRef(null, UnsafeNativeMethods.GetModuleHandle(null)), 0);
				if (SendKeys.hhook == IntPtr.Zero)
				{
					throw new SecurityException(SR.GetString("SendKeysHookFailed"));
				}
			}
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000FB2EC File Offset: 0x000F94EC
		private static void TestHook()
		{
			SendKeys.hookSupported = new bool?(false);
			try
			{
				NativeMethods.HookProc hookProc = new NativeMethods.HookProc(SendKeys.EmptyHookCallback);
				IntPtr intPtr = UnsafeNativeMethods.SetWindowsHookEx(1, hookProc, new HandleRef(null, UnsafeNativeMethods.GetModuleHandle(null)), 0);
				SendKeys.hookSupported = new bool?(intPtr != IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.UnhookWindowsHookEx(new HandleRef(null, intPtr));
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000F9C41 File Offset: 0x000F7E41
		private static IntPtr EmptyHookCallback(int code, IntPtr wparam, IntPtr lparam)
		{
			return IntPtr.Zero;
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000FB36C File Offset: 0x000F956C
		private static void LoadSendMethodFromConfig()
		{
			if (SendKeys.sendMethod == null)
			{
				SendKeys.sendMethod = new SendKeys.SendMethodTypes?(SendKeys.SendMethodTypes.Default);
				try
				{
					string text = ConfigurationManager.AppSettings.Get("SendKeys");
					if (!string.IsNullOrEmpty(text))
					{
						if (text.Equals("JournalHook", StringComparison.OrdinalIgnoreCase))
						{
							SendKeys.sendMethod = new SendKeys.SendMethodTypes?(SendKeys.SendMethodTypes.JournalHook);
						}
						else if (text.Equals("SendInput", StringComparison.OrdinalIgnoreCase))
						{
							SendKeys.sendMethod = new SendKeys.SendMethodTypes?(SendKeys.SendMethodTypes.SendInput);
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000FB3F4 File Offset: 0x000F95F4
		private static void JournalCancel()
		{
			if (SendKeys.hhook != IntPtr.Zero)
			{
				SendKeys.stopHook = false;
				if (SendKeys.events != null)
				{
					SendKeys.events.Clear();
				}
				SendKeys.hhook = IntPtr.Zero;
			}
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000FB428 File Offset: 0x000F9628
		private static byte[] GetKeyboardState()
		{
			byte[] array = new byte[256];
			UnsafeNativeMethods.GetKeyboardState(array);
			return array;
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000FB448 File Offset: 0x000F9648
		private static void SetKeyboardState(byte[] keystate)
		{
			UnsafeNativeMethods.SetKeyboardState(keystate);
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000FB454 File Offset: 0x000F9654
		private static void ClearKeyboardState()
		{
			byte[] keyboardState = SendKeys.GetKeyboardState();
			keyboardState[20] = 0;
			keyboardState[144] = 0;
			keyboardState[145] = 0;
			SendKeys.SetKeyboardState(keyboardState);
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000FB484 File Offset: 0x000F9684
		private static int MatchKeyword(string keyword)
		{
			for (int i = 0; i < SendKeys.keywords.Length; i++)
			{
				if (string.Equals(SendKeys.keywords[i].keyword, keyword, StringComparison.OrdinalIgnoreCase))
				{
					return SendKeys.keywords[i].vk;
				}
			}
			return -1;
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000FB4C8 File Offset: 0x000F96C8
		private static void OnThreadExit(object sender, EventArgs e)
		{
			try
			{
				SendKeys.UninstallJournalingHook();
			}
			catch
			{
			}
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000FB4F0 File Offset: 0x000F96F0
		private static void ParseKeys(string keys, IntPtr hwnd)
		{
			int i = 0;
			int[] array = new int[3];
			int num = 0;
			SendKeys.fStartNewChar = true;
			int length = keys.Length;
			while (i < length)
			{
				int num2 = 1;
				char c = keys[i];
				switch (c)
				{
				case '%':
					if (array[2] != 0)
					{
						throw new ArgumentException(SR.GetString("InvalidSendKeysString", new object[] { keys }));
					}
					SendKeys.AddEvent(new SendKeys.SKEvent((array[1] != 0) ? 256 : 260, 18, SendKeys.fStartNewChar, hwnd));
					SendKeys.fStartNewChar = false;
					array[2] = 10;
					break;
				case '&':
				case '\'':
				case '*':
					goto IL_46A;
				case '(':
					num++;
					if (num > 3)
					{
						throw new ArgumentException(SR.GetString("SendKeysNestingError"));
					}
					if (array[0] == 10)
					{
						array[0] = num;
					}
					if (array[1] == 10)
					{
						array[1] = num;
					}
					if (array[2] == 10)
					{
						array[2] = num;
					}
					break;
				case ')':
					if (num < 1)
					{
						throw new ArgumentException(SR.GetString("InvalidSendKeysString", new object[] { keys }));
					}
					SendKeys.CancelMods(array, num, hwnd);
					num--;
					if (num == 0)
					{
						SendKeys.fStartNewChar = true;
					}
					break;
				case '+':
					if (array[0] != 0)
					{
						throw new ArgumentException(SR.GetString("InvalidSendKeysString", new object[] { keys }));
					}
					SendKeys.AddEvent(new SendKeys.SKEvent(256, 16, SendKeys.fStartNewChar, hwnd));
					SendKeys.fStartNewChar = false;
					array[0] = 10;
					break;
				default:
					if (c != '^')
					{
						switch (c)
						{
						case '{':
						{
							int num3 = i + 1;
							if (num3 + 1 < length && keys[num3] == '}')
							{
								int num4 = num3 + 1;
								while (num4 < length && keys[num4] != '}')
								{
									num4++;
								}
								if (num4 < length)
								{
									num3++;
								}
							}
							while (num3 < length && keys[num3] != '}' && !char.IsWhiteSpace(keys[num3]))
							{
								num3++;
							}
							if (num3 >= length)
							{
								throw new ArgumentException(SR.GetString("SendKeysKeywordDelimError"));
							}
							string text = keys.Substring(i + 1, num3 - (i + 1));
							if (char.IsWhiteSpace(keys[num3]))
							{
								while (num3 < length && char.IsWhiteSpace(keys[num3]))
								{
									num3++;
								}
								if (num3 >= length)
								{
									throw new ArgumentException(SR.GetString("SendKeysKeywordDelimError"));
								}
								if (char.IsDigit(keys[num3]))
								{
									int num5 = num3;
									while (num3 < length && char.IsDigit(keys[num3]))
									{
										num3++;
									}
									num2 = int.Parse(keys.Substring(num5, num3 - num5), CultureInfo.InvariantCulture);
								}
							}
							if (num3 >= length)
							{
								throw new ArgumentException(SR.GetString("SendKeysKeywordDelimError"));
							}
							if (keys[num3] != '}')
							{
								throw new ArgumentException(SR.GetString("InvalidSendKeysRepeat"));
							}
							int num6 = SendKeys.MatchKeyword(text);
							if (num6 != -1)
							{
								if (array[0] == 0 && (num6 & 65536) != 0)
								{
									SendKeys.AddEvent(new SendKeys.SKEvent(256, 16, SendKeys.fStartNewChar, hwnd));
									SendKeys.fStartNewChar = false;
									array[0] = 10;
								}
								if (array[1] == 0 && (num6 & 131072) != 0)
								{
									SendKeys.AddEvent(new SendKeys.SKEvent(256, 17, SendKeys.fStartNewChar, hwnd));
									SendKeys.fStartNewChar = false;
									array[1] = 10;
								}
								if (array[2] == 0 && (num6 & 262144) != 0)
								{
									SendKeys.AddEvent(new SendKeys.SKEvent(256, 18, SendKeys.fStartNewChar, hwnd));
									SendKeys.fStartNewChar = false;
									array[2] = 10;
								}
								SendKeys.AddMsgsForVK(num6, num2, array[2] > 0 && array[1] == 0, hwnd);
								SendKeys.CancelMods(array, 10, hwnd);
							}
							else
							{
								if (text.Length != 1)
								{
									throw new ArgumentException(SR.GetString("InvalidSendKeysKeyword", new object[] { keys.Substring(i + 1, num3 - (i + 1)) }));
								}
								SendKeys.fStartNewChar = SendKeys.AddSimpleKey(text[0], num2, hwnd, array, SendKeys.fStartNewChar, num);
							}
							i = num3;
							break;
						}
						case '|':
							goto IL_46A;
						case '}':
							throw new ArgumentException(SR.GetString("InvalidSendKeysString", new object[] { keys }));
						case '~':
						{
							int num6 = 13;
							SendKeys.AddMsgsForVK(num6, num2, array[2] > 0 && array[1] == 0, hwnd);
							break;
						}
						default:
							goto IL_46A;
						}
					}
					else
					{
						if (array[1] != 0)
						{
							throw new ArgumentException(SR.GetString("InvalidSendKeysString", new object[] { keys }));
						}
						SendKeys.AddEvent(new SendKeys.SKEvent(256, 17, SendKeys.fStartNewChar, hwnd));
						SendKeys.fStartNewChar = false;
						array[1] = 10;
					}
					break;
				}
				IL_485:
				i++;
				continue;
				IL_46A:
				SendKeys.fStartNewChar = SendKeys.AddSimpleKey(keys[i], num2, hwnd, array, SendKeys.fStartNewChar, num);
				goto IL_485;
			}
			if (num != 0)
			{
				throw new ArgumentException(SR.GetString("SendKeysGroupDelimError"));
			}
			SendKeys.CancelMods(array, 10, hwnd);
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000FB9AC File Offset: 0x000F9BAC
		private static void SendInput(byte[] oldKeyboardState, Queue previousEvents)
		{
			SendKeys.AddCancelModifiersForPreviousEvents(previousEvents);
			NativeMethods.INPUT[] array = new NativeMethods.INPUT[2];
			array[0].type = 1;
			array[1].type = 1;
			array[1].inputUnion.ki.wVk = 0;
			array[1].inputUnion.ki.dwFlags = 6;
			array[0].inputUnion.ki.dwExtraInfo = IntPtr.Zero;
			array[0].inputUnion.ki.time = 0;
			array[1].inputUnion.ki.dwExtraInfo = IntPtr.Zero;
			array[1].inputUnion.ki.time = 0;
			int num = Marshal.SizeOf(typeof(NativeMethods.INPUT));
			uint num2 = 0U;
			object syncRoot = SendKeys.events.SyncRoot;
			int count;
			lock (syncRoot)
			{
				bool flag2 = UnsafeNativeMethods.BlockInput(true);
				try
				{
					count = SendKeys.events.Count;
					SendKeys.ClearGlobalKeys();
					for (int i = 0; i < count; i++)
					{
						SendKeys.SKEvent skevent = (SendKeys.SKEvent)SendKeys.events.Dequeue();
						array[0].inputUnion.ki.dwFlags = 0;
						if (skevent.wm == 258)
						{
							array[0].inputUnion.ki.wVk = 0;
							array[0].inputUnion.ki.wScan = (short)skevent.paramL;
							array[0].inputUnion.ki.dwFlags = 4;
							array[1].inputUnion.ki.wScan = (short)skevent.paramL;
							num2 += UnsafeNativeMethods.SendInput(2U, array, num) - 1U;
						}
						else
						{
							array[0].inputUnion.ki.wScan = 0;
							if (skevent.wm == 257 || skevent.wm == 261)
							{
								NativeMethods.INPUT[] array2 = array;
								int num3 = 0;
								array2[num3].inputUnion.ki.dwFlags = array2[num3].inputUnion.ki.dwFlags | 2;
							}
							if (SendKeys.IsExtendedKey(skevent))
							{
								NativeMethods.INPUT[] array3 = array;
								int num4 = 0;
								array3[num4].inputUnion.ki.dwFlags = array3[num4].inputUnion.ki.dwFlags | 1;
							}
							array[0].inputUnion.ki.wVk = (short)skevent.paramL;
							num2 += UnsafeNativeMethods.SendInput(1U, array, num);
							SendKeys.CheckGlobalKeys(skevent);
						}
						Thread.Sleep(1);
					}
					SendKeys.ResetKeyboardUsingSendInput(num);
				}
				finally
				{
					SendKeys.SetKeyboardState(oldKeyboardState);
					if (flag2)
					{
						UnsafeNativeMethods.BlockInput(false);
					}
				}
			}
			if ((ulong)num2 != (ulong)((long)count))
			{
				throw new Win32Exception();
			}
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000FBC8C File Offset: 0x000F9E8C
		private static void AddCancelModifiersForPreviousEvents(Queue previousEvents)
		{
			if (previousEvents == null)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			while (previousEvents.Count > 0)
			{
				SendKeys.SKEvent skevent = (SendKeys.SKEvent)previousEvents.Dequeue();
				bool flag4;
				if (skevent.wm == 257 || skevent.wm == 261)
				{
					flag4 = false;
				}
				else
				{
					if (skevent.wm != 256 && skevent.wm != 260)
					{
						continue;
					}
					flag4 = true;
				}
				if (skevent.paramL == 16)
				{
					flag = flag4;
				}
				else if (skevent.paramL == 17)
				{
					flag2 = flag4;
				}
				else if (skevent.paramL == 18)
				{
					flag3 = flag4;
				}
			}
			if (flag)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(257, 16, false, IntPtr.Zero));
				return;
			}
			if (flag2)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(257, 17, false, IntPtr.Zero));
				return;
			}
			if (flag3)
			{
				SendKeys.AddEvent(new SendKeys.SKEvent(261, 18, false, IntPtr.Zero));
			}
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000FBD74 File Offset: 0x000F9F74
		private static bool IsExtendedKey(SendKeys.SKEvent skEvent)
		{
			return skEvent.paramL == 38 || skEvent.paramL == 40 || skEvent.paramL == 37 || skEvent.paramL == 39 || skEvent.paramL == 33 || skEvent.paramL == 34 || skEvent.paramL == 36 || skEvent.paramL == 35 || skEvent.paramL == 45 || skEvent.paramL == 46;
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000FBDE7 File Offset: 0x000F9FE7
		private static void ClearGlobalKeys()
		{
			SendKeys.capslockChanged = false;
			SendKeys.numlockChanged = false;
			SendKeys.scrollLockChanged = false;
			SendKeys.kanaChanged = false;
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000FBE04 File Offset: 0x000FA004
		private static void CheckGlobalKeys(SendKeys.SKEvent skEvent)
		{
			if (skEvent.wm == 256)
			{
				int paramL = skEvent.paramL;
				if (paramL <= 21)
				{
					if (paramL == 20)
					{
						SendKeys.capslockChanged = !SendKeys.capslockChanged;
						return;
					}
					if (paramL != 21)
					{
						return;
					}
					SendKeys.kanaChanged = !SendKeys.kanaChanged;
				}
				else
				{
					if (paramL == 144)
					{
						SendKeys.numlockChanged = !SendKeys.numlockChanged;
						return;
					}
					if (paramL != 145)
					{
						return;
					}
					SendKeys.scrollLockChanged = !SendKeys.scrollLockChanged;
					return;
				}
			}
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000FBE80 File Offset: 0x000FA080
		private static void ResetKeyboardUsingSendInput(int INPUTSize)
		{
			if (!SendKeys.capslockChanged && !SendKeys.numlockChanged && !SendKeys.scrollLockChanged && !SendKeys.kanaChanged)
			{
				return;
			}
			NativeMethods.INPUT[] array = new NativeMethods.INPUT[2];
			array[0].type = 1;
			array[0].inputUnion.ki.dwFlags = 0;
			array[1].type = 1;
			array[1].inputUnion.ki.dwFlags = 2;
			if (SendKeys.capslockChanged)
			{
				array[0].inputUnion.ki.wVk = 20;
				array[1].inputUnion.ki.wVk = 20;
				UnsafeNativeMethods.SendInput(2U, array, INPUTSize);
			}
			if (SendKeys.numlockChanged)
			{
				array[0].inputUnion.ki.wVk = 144;
				array[1].inputUnion.ki.wVk = 144;
				UnsafeNativeMethods.SendInput(2U, array, INPUTSize);
			}
			if (SendKeys.scrollLockChanged)
			{
				array[0].inputUnion.ki.wVk = 145;
				array[1].inputUnion.ki.wVk = 145;
				UnsafeNativeMethods.SendInput(2U, array, INPUTSize);
			}
			if (SendKeys.kanaChanged)
			{
				array[0].inputUnion.ki.wVk = 21;
				array[1].inputUnion.ki.wVk = 21;
				UnsafeNativeMethods.SendInput(2U, array, INPUTSize);
			}
		}

		/// <summary>Sends keystrokes to the active application.</summary>
		/// <param name="keys">The string of keystrokes to send.</param>
		/// <exception cref="T:System.InvalidOperationException">There is not an active application to send keystrokes to.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keys" /> does not represent valid keystrokes</exception>
		// Token: 0x060038AB RID: 14507 RVA: 0x000FC005 File Offset: 0x000FA205
		public static void Send(string keys)
		{
			SendKeys.Send(keys, null, false);
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000FC010 File Offset: 0x000FA210
		private static void Send(string keys, Control control, bool wait)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (keys == null || keys.Length == 0)
			{
				return;
			}
			if (!wait && !Application.MessageLoop)
			{
				throw new InvalidOperationException(SR.GetString("SendKeysNoMessageLoop"));
			}
			Queue queue = null;
			if (SendKeys.events != null && SendKeys.events.Count != 0)
			{
				queue = (Queue)SendKeys.events.Clone();
			}
			SendKeys.ParseKeys(keys, (control != null) ? control.Handle : IntPtr.Zero);
			if (SendKeys.events == null)
			{
				return;
			}
			SendKeys.LoadSendMethodFromConfig();
			byte[] keyboardState = SendKeys.GetKeyboardState();
			if (SendKeys.sendMethod.Value != SendKeys.SendMethodTypes.SendInput)
			{
				if (SendKeys.hookSupported == null && SendKeys.sendMethod.Value == SendKeys.SendMethodTypes.Default)
				{
					SendKeys.TestHook();
				}
				if (SendKeys.sendMethod.Value == SendKeys.SendMethodTypes.JournalHook || SendKeys.hookSupported.Value)
				{
					SendKeys.ClearKeyboardState();
					SendKeys.InstallHook();
					SendKeys.SetKeyboardState(keyboardState);
				}
			}
			if (SendKeys.sendMethod.Value == SendKeys.SendMethodTypes.SendInput || (SendKeys.sendMethod.Value == SendKeys.SendMethodTypes.Default && !SendKeys.hookSupported.Value))
			{
				SendKeys.SendInput(keyboardState, queue);
			}
			if (wait)
			{
				SendKeys.Flush();
			}
		}

		/// <summary>Sends the given keys to the active application, and then waits for the messages to be processed.</summary>
		/// <param name="keys">The string of keystrokes to send.</param>
		// Token: 0x060038AD RID: 14509 RVA: 0x000FC124 File Offset: 0x000FA324
		public static void SendWait(string keys)
		{
			SendKeys.SendWait(keys, null);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000FC12D File Offset: 0x000FA32D
		private static void SendWait(string keys, Control control)
		{
			SendKeys.Send(keys, control, true);
		}

		/// <summary>Processes all the Windows messages currently in the message queue.</summary>
		// Token: 0x060038AF RID: 14511 RVA: 0x000FC137 File Offset: 0x000FA337
		public static void Flush()
		{
			Application.DoEvents();
			while (SendKeys.events != null && SendKeys.events.Count > 0)
			{
				Application.DoEvents();
			}
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000FC15C File Offset: 0x000FA35C
		private static void UninstallJournalingHook()
		{
			if (SendKeys.hhook != IntPtr.Zero)
			{
				SendKeys.stopHook = false;
				if (SendKeys.events != null)
				{
					SendKeys.events.Clear();
				}
				UnsafeNativeMethods.UnhookWindowsHookEx(new HandleRef(null, SendKeys.hhook));
				SendKeys.hhook = IntPtr.Zero;
			}
		}

		// Token: 0x040021CC RID: 8652
		private const int HAVESHIFT = 0;

		// Token: 0x040021CD RID: 8653
		private const int HAVECTRL = 1;

		// Token: 0x040021CE RID: 8654
		private const int HAVEALT = 2;

		// Token: 0x040021CF RID: 8655
		private const int UNKNOWN_GROUPING = 10;

		// Token: 0x040021D0 RID: 8656
		private static SendKeys.KeywordVk[] keywords = new SendKeys.KeywordVk[]
		{
			new SendKeys.KeywordVk("ENTER", 13),
			new SendKeys.KeywordVk("TAB", 9),
			new SendKeys.KeywordVk("ESC", 27),
			new SendKeys.KeywordVk("ESCAPE", 27),
			new SendKeys.KeywordVk("HOME", 36),
			new SendKeys.KeywordVk("END", 35),
			new SendKeys.KeywordVk("LEFT", 37),
			new SendKeys.KeywordVk("RIGHT", 39),
			new SendKeys.KeywordVk("UP", 38),
			new SendKeys.KeywordVk("DOWN", 40),
			new SendKeys.KeywordVk("PGUP", 33),
			new SendKeys.KeywordVk("PGDN", 34),
			new SendKeys.KeywordVk("NUMLOCK", 144),
			new SendKeys.KeywordVk("SCROLLLOCK", 145),
			new SendKeys.KeywordVk("PRTSC", 44),
			new SendKeys.KeywordVk("BREAK", 3),
			new SendKeys.KeywordVk("BACKSPACE", 8),
			new SendKeys.KeywordVk("BKSP", 8),
			new SendKeys.KeywordVk("BS", 8),
			new SendKeys.KeywordVk("CLEAR", 12),
			new SendKeys.KeywordVk("CAPSLOCK", 20),
			new SendKeys.KeywordVk("INS", 45),
			new SendKeys.KeywordVk("INSERT", 45),
			new SendKeys.KeywordVk("DEL", 46),
			new SendKeys.KeywordVk("DELETE", 46),
			new SendKeys.KeywordVk("HELP", 47),
			new SendKeys.KeywordVk("F1", 112),
			new SendKeys.KeywordVk("F2", 113),
			new SendKeys.KeywordVk("F3", 114),
			new SendKeys.KeywordVk("F4", 115),
			new SendKeys.KeywordVk("F5", 116),
			new SendKeys.KeywordVk("F6", 117),
			new SendKeys.KeywordVk("F7", 118),
			new SendKeys.KeywordVk("F8", 119),
			new SendKeys.KeywordVk("F9", 120),
			new SendKeys.KeywordVk("F10", 121),
			new SendKeys.KeywordVk("F11", 122),
			new SendKeys.KeywordVk("F12", 123),
			new SendKeys.KeywordVk("F13", 124),
			new SendKeys.KeywordVk("F14", 125),
			new SendKeys.KeywordVk("F15", 126),
			new SendKeys.KeywordVk("F16", 127),
			new SendKeys.KeywordVk("MULTIPLY", 106),
			new SendKeys.KeywordVk("ADD", 107),
			new SendKeys.KeywordVk("SUBTRACT", 109),
			new SendKeys.KeywordVk("DIVIDE", 111),
			new SendKeys.KeywordVk("+", 107),
			new SendKeys.KeywordVk("%", 65589),
			new SendKeys.KeywordVk("^", 65590)
		};

		// Token: 0x040021D1 RID: 8657
		private const int SHIFTKEYSCAN = 256;

		// Token: 0x040021D2 RID: 8658
		private const int CTRLKEYSCAN = 512;

		// Token: 0x040021D3 RID: 8659
		private const int ALTKEYSCAN = 1024;

		// Token: 0x040021D4 RID: 8660
		private static bool stopHook;

		// Token: 0x040021D5 RID: 8661
		private static IntPtr hhook;

		// Token: 0x040021D6 RID: 8662
		private static NativeMethods.HookProc hook;

		// Token: 0x040021D7 RID: 8663
		private static Queue events;

		// Token: 0x040021D8 RID: 8664
		private static bool fStartNewChar;

		// Token: 0x040021D9 RID: 8665
		private static SendKeys.SKWindow messageWindow;

		// Token: 0x040021DA RID: 8666
		private static SendKeys.SendMethodTypes? sendMethod = null;

		// Token: 0x040021DB RID: 8667
		private static bool? hookSupported = null;

		// Token: 0x040021DC RID: 8668
		private static bool capslockChanged;

		// Token: 0x040021DD RID: 8669
		private static bool numlockChanged;

		// Token: 0x040021DE RID: 8670
		private static bool scrollLockChanged;

		// Token: 0x040021DF RID: 8671
		private static bool kanaChanged;

		// Token: 0x020007DF RID: 2015
		private enum SendMethodTypes
		{
			// Token: 0x040042B5 RID: 17077
			Default = 1,
			// Token: 0x040042B6 RID: 17078
			JournalHook,
			// Token: 0x040042B7 RID: 17079
			SendInput
		}

		// Token: 0x020007E0 RID: 2016
		private class SKWindow : Control
		{
			// Token: 0x06006DC1 RID: 28097 RVA: 0x001921BA File Offset: 0x001903BA
			public SKWindow()
			{
				base.SetState(524288, true);
				base.SetState2(8, false);
				base.SetBounds(-1, -1, 0, 0);
				base.Visible = false;
			}

			// Token: 0x06006DC2 RID: 28098 RVA: 0x001921E8 File Offset: 0x001903E8
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 75)
				{
					try
					{
						SendKeys.JournalCancel();
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x020007E1 RID: 2017
		private class SKEvent
		{
			// Token: 0x06006DC3 RID: 28099 RVA: 0x0019221C File Offset: 0x0019041C
			public SKEvent(int a, int b, bool c, IntPtr hwnd)
			{
				this.wm = a;
				this.paramL = b;
				this.paramH = (c ? 1 : 0);
				this.hwnd = hwnd;
			}

			// Token: 0x06006DC4 RID: 28100 RVA: 0x00192247 File Offset: 0x00190447
			public SKEvent(int a, int b, int c, IntPtr hwnd)
			{
				this.wm = a;
				this.paramL = b;
				this.paramH = c;
				this.hwnd = hwnd;
			}

			// Token: 0x040042B8 RID: 17080
			internal int wm;

			// Token: 0x040042B9 RID: 17081
			internal int paramL;

			// Token: 0x040042BA RID: 17082
			internal int paramH;

			// Token: 0x040042BB RID: 17083
			internal IntPtr hwnd;
		}

		// Token: 0x020007E2 RID: 2018
		private class KeywordVk
		{
			// Token: 0x06006DC5 RID: 28101 RVA: 0x0019226C File Offset: 0x0019046C
			public KeywordVk(string key, int v)
			{
				this.keyword = key;
				this.vk = v;
			}

			// Token: 0x040042BC RID: 17084
			internal string keyword;

			// Token: 0x040042BD RID: 17085
			internal int vk;
		}

		// Token: 0x020007E3 RID: 2019
		private class SendKeysHookProc
		{
			// Token: 0x06006DC6 RID: 28102 RVA: 0x00192284 File Offset: 0x00190484
			public virtual IntPtr Callback(int code, IntPtr wparam, IntPtr lparam)
			{
				NativeMethods.EVENTMSG eventmsg = (NativeMethods.EVENTMSG)UnsafeNativeMethods.PtrToStructure(lparam, typeof(NativeMethods.EVENTMSG));
				if (UnsafeNativeMethods.GetAsyncKeyState(19) != 0)
				{
					SendKeys.stopHook = true;
				}
				if (code != 1)
				{
					if (code == 2)
					{
						if (this.gotNextEvent)
						{
							if (SendKeys.events != null && SendKeys.events.Count > 0)
							{
								SendKeys.events.Dequeue();
							}
							SendKeys.stopHook = SendKeys.events == null || SendKeys.events.Count == 0;
						}
					}
					else if (code < 0)
					{
						UnsafeNativeMethods.CallNextHookEx(new HandleRef(null, SendKeys.hhook), code, wparam, lparam);
					}
				}
				else
				{
					this.gotNextEvent = true;
					SendKeys.SKEvent skevent = (SendKeys.SKEvent)SendKeys.events.Peek();
					eventmsg.message = skevent.wm;
					eventmsg.paramL = skevent.paramL;
					eventmsg.paramH = skevent.paramH;
					eventmsg.hwnd = skevent.hwnd;
					eventmsg.time = SafeNativeMethods.GetTickCount();
					Marshal.StructureToPtr(eventmsg, lparam, true);
				}
				if (SendKeys.stopHook)
				{
					SendKeys.UninstallJournalingHook();
					this.gotNextEvent = false;
				}
				return IntPtr.Zero;
			}

			// Token: 0x040042BE RID: 17086
			private bool gotNextEvent;
		}
	}
}
