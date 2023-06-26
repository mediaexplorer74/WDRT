using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Internal
{
	// Token: 0x020000FB RID: 251
	internal class DebugHandleTracker
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000C758 File Offset: 0x0000A958
		static DebugHandleTracker()
		{
			if (CompModSwitches.HandleLeak.Level > TraceLevel.Off || CompModSwitches.TraceCollect.Enabled)
			{
				HandleCollector.HandleAdded += DebugHandleTracker.tracker.OnHandleAdd;
				HandleCollector.HandleRemoved += DebugHandleTracker.tracker.OnHandleRemove;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00002843 File Offset: 0x00000A43
		private DebugHandleTracker()
		{
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		public static void IgnoreCurrentHandlesAsLeaks()
		{
			object obj = DebugHandleTracker.internalSyncObject;
			lock (obj)
			{
				if (CompModSwitches.HandleLeak.Level >= TraceLevel.Warning)
				{
					DebugHandleTracker.HandleType[] array = new DebugHandleTracker.HandleType[DebugHandleTracker.handleTypes.Values.Count];
					DebugHandleTracker.handleTypes.Values.CopyTo(array, 0);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null)
						{
							array[i].IgnoreCurrentHandlesAsLeaks();
						}
					}
				}
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000C850 File Offset: 0x0000AA50
		public static void CheckLeaks()
		{
			object obj = DebugHandleTracker.internalSyncObject;
			lock (obj)
			{
				if (CompModSwitches.HandleLeak.Level >= TraceLevel.Warning)
				{
					GC.Collect();
					GC.WaitForPendingFinalizers();
					DebugHandleTracker.HandleType[] array = new DebugHandleTracker.HandleType[DebugHandleTracker.handleTypes.Values.Count];
					DebugHandleTracker.handleTypes.Values.CopyTo(array, 0);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null)
						{
							array[i].CheckLeaks();
						}
					}
				}
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000070A6 File Offset: 0x000052A6
		public static void Initialize()
		{
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		private void OnHandleAdd(string handleName, IntPtr handle, int handleCount)
		{
			DebugHandleTracker.HandleType handleType = (DebugHandleTracker.HandleType)DebugHandleTracker.handleTypes[handleName];
			if (handleType == null)
			{
				handleType = new DebugHandleTracker.HandleType(handleName);
				DebugHandleTracker.handleTypes[handleName] = handleType;
			}
			handleType.Add(handle);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000C920 File Offset: 0x0000AB20
		private void OnHandleRemove(string handleName, IntPtr handle, int HandleCount)
		{
			DebugHandleTracker.HandleType handleType = (DebugHandleTracker.HandleType)DebugHandleTracker.handleTypes[handleName];
			bool flag = false;
			if (handleType != null)
			{
				flag = handleType.Remove(handle);
			}
			if (!flag)
			{
				TraceLevel level = CompModSwitches.HandleLeak.Level;
			}
		}

		// Token: 0x0400042E RID: 1070
		private static Hashtable handleTypes = new Hashtable();

		// Token: 0x0400042F RID: 1071
		private static DebugHandleTracker tracker = new DebugHandleTracker();

		// Token: 0x04000430 RID: 1072
		private static object internalSyncObject = new object();

		// Token: 0x02000546 RID: 1350
		private class HandleType
		{
			// Token: 0x06005565 RID: 21861 RVA: 0x00166294 File Offset: 0x00164494
			public HandleType(string name)
			{
				this.name = name;
				this.buckets = new DebugHandleTracker.HandleType.HandleEntry[10];
			}

			// Token: 0x06005566 RID: 21862 RVA: 0x001662B0 File Offset: 0x001644B0
			public void Add(IntPtr handle)
			{
				lock (this)
				{
					int num = this.ComputeHash(handle);
					if (CompModSwitches.HandleLeak.Level >= TraceLevel.Info)
					{
						TraceLevel level = CompModSwitches.HandleLeak.Level;
					}
					for (DebugHandleTracker.HandleType.HandleEntry handleEntry = this.buckets[num]; handleEntry != null; handleEntry = handleEntry.next)
					{
					}
					this.buckets[num] = new DebugHandleTracker.HandleType.HandleEntry(this.buckets[num], handle);
					this.handleCount++;
				}
			}

			// Token: 0x06005567 RID: 21863 RVA: 0x00166340 File Offset: 0x00164540
			public void CheckLeaks()
			{
				lock (this)
				{
					bool flag2 = false;
					if (this.handleCount > 0)
					{
						for (int i = 0; i < 10; i++)
						{
							for (DebugHandleTracker.HandleType.HandleEntry handleEntry = this.buckets[i]; handleEntry != null; handleEntry = handleEntry.next)
							{
								if (!handleEntry.ignorableAsLeak && !flag2)
								{
									flag2 = true;
								}
							}
						}
					}
				}
			}

			// Token: 0x06005568 RID: 21864 RVA: 0x001663B4 File Offset: 0x001645B4
			public void IgnoreCurrentHandlesAsLeaks()
			{
				lock (this)
				{
					if (this.handleCount > 0)
					{
						for (int i = 0; i < 10; i++)
						{
							for (DebugHandleTracker.HandleType.HandleEntry handleEntry = this.buckets[i]; handleEntry != null; handleEntry = handleEntry.next)
							{
								handleEntry.ignorableAsLeak = true;
							}
						}
					}
				}
			}

			// Token: 0x06005569 RID: 21865 RVA: 0x0016641C File Offset: 0x0016461C
			private int ComputeHash(IntPtr handle)
			{
				return ((int)handle & 65535) % 10;
			}

			// Token: 0x0600556A RID: 21866 RVA: 0x00166430 File Offset: 0x00164630
			public bool Remove(IntPtr handle)
			{
				bool flag2;
				lock (this)
				{
					int num = this.ComputeHash(handle);
					if (CompModSwitches.HandleLeak.Level >= TraceLevel.Info)
					{
						TraceLevel level = CompModSwitches.HandleLeak.Level;
					}
					DebugHandleTracker.HandleType.HandleEntry handleEntry = this.buckets[num];
					DebugHandleTracker.HandleType.HandleEntry handleEntry2 = null;
					while (handleEntry != null && handleEntry.handle != handle)
					{
						handleEntry2 = handleEntry;
						handleEntry = handleEntry.next;
					}
					if (handleEntry != null)
					{
						if (handleEntry2 == null)
						{
							this.buckets[num] = handleEntry.next;
						}
						else
						{
							handleEntry2.next = handleEntry.next;
						}
						this.handleCount--;
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
				return flag2;
			}

			// Token: 0x04003804 RID: 14340
			public readonly string name;

			// Token: 0x04003805 RID: 14341
			private int handleCount;

			// Token: 0x04003806 RID: 14342
			private DebugHandleTracker.HandleType.HandleEntry[] buckets;

			// Token: 0x04003807 RID: 14343
			private const int BUCKETS = 10;

			// Token: 0x020008A3 RID: 2211
			private class HandleEntry
			{
				// Token: 0x06007222 RID: 29218 RVA: 0x001A22A3 File Offset: 0x001A04A3
				public HandleEntry(DebugHandleTracker.HandleType.HandleEntry next, IntPtr handle)
				{
					this.handle = handle;
					this.next = next;
					if (CompModSwitches.HandleLeak.Level > TraceLevel.Off)
					{
						this.callStack = Environment.StackTrace;
						return;
					}
					this.callStack = null;
				}

				// Token: 0x06007223 RID: 29219 RVA: 0x001A22DC File Offset: 0x001A04DC
				public string ToString(DebugHandleTracker.HandleType type)
				{
					DebugHandleTracker.HandleType.HandleEntry.StackParser stackParser = new DebugHandleTracker.HandleType.HandleEntry.StackParser(this.callStack);
					stackParser.DiscardTo("HandleCollector.Add");
					stackParser.DiscardNext();
					stackParser.Truncate(40);
					string text = "";
					return Convert.ToString((int)this.handle, 16) + text + ": " + stackParser.ToString();
				}

				// Token: 0x040044D2 RID: 17618
				public readonly IntPtr handle;

				// Token: 0x040044D3 RID: 17619
				public DebugHandleTracker.HandleType.HandleEntry next;

				// Token: 0x040044D4 RID: 17620
				public readonly string callStack;

				// Token: 0x040044D5 RID: 17621
				public bool ignorableAsLeak;

				// Token: 0x02000980 RID: 2432
				private class StackParser
				{
					// Token: 0x06007561 RID: 30049 RVA: 0x001A7E90 File Offset: 0x001A6090
					public StackParser(string callStack)
					{
						this.releventStack = callStack;
						this.length = this.releventStack.Length;
					}

					// Token: 0x06007562 RID: 30050 RVA: 0x001A7EB0 File Offset: 0x001A60B0
					private static bool ContainsString(string str, string token)
					{
						int num = str.Length;
						int num2 = token.Length;
						for (int i = 0; i < num; i++)
						{
							int num3 = 0;
							while (num3 < num2 && str[i + num3] == token[num3])
							{
								num3++;
							}
							if (num3 == num2)
							{
								return true;
							}
						}
						return false;
					}

					// Token: 0x06007563 RID: 30051 RVA: 0x001A7EFC File Offset: 0x001A60FC
					public void DiscardNext()
					{
						this.GetLine();
					}

					// Token: 0x06007564 RID: 30052 RVA: 0x001A7F08 File Offset: 0x001A6108
					public void DiscardTo(string discardText)
					{
						while (this.startIndex < this.length)
						{
							string line = this.GetLine();
							if (line == null || DebugHandleTracker.HandleType.HandleEntry.StackParser.ContainsString(line, discardText))
							{
								break;
							}
						}
					}

					// Token: 0x06007565 RID: 30053 RVA: 0x001A7F38 File Offset: 0x001A6138
					private string GetLine()
					{
						this.endIndex = this.releventStack.IndexOf('\r', this.startIndex);
						if (this.endIndex < 0)
						{
							this.endIndex = this.length - 1;
						}
						string text = this.releventStack.Substring(this.startIndex, this.endIndex - this.startIndex);
						char c;
						while (this.endIndex < this.length && ((c = this.releventStack[this.endIndex]) == '\r' || c == '\n'))
						{
							this.endIndex++;
						}
						if (this.startIndex == this.endIndex)
						{
							return null;
						}
						this.startIndex = this.endIndex;
						return text.Replace('\t', ' ');
					}

					// Token: 0x06007566 RID: 30054 RVA: 0x001A7FF6 File Offset: 0x001A61F6
					public override string ToString()
					{
						return this.releventStack.Substring(this.startIndex);
					}

					// Token: 0x06007567 RID: 30055 RVA: 0x001A800C File Offset: 0x001A620C
					public void Truncate(int lines)
					{
						string text = "";
						while (lines-- > 0 && this.startIndex < this.length)
						{
							if (text == null)
							{
								text = this.GetLine();
							}
							else
							{
								text = text + ": " + this.GetLine();
							}
							text += Environment.NewLine;
						}
						this.releventStack = text;
						this.startIndex = 0;
						this.endIndex = 0;
						this.length = this.releventStack.Length;
					}

					// Token: 0x040047D0 RID: 18384
					internal string releventStack;

					// Token: 0x040047D1 RID: 18385
					internal int startIndex;

					// Token: 0x040047D2 RID: 18386
					internal int endIndex;

					// Token: 0x040047D3 RID: 18387
					internal int length;
				}
			}
		}
	}
}
