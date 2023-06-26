using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Internal
{
	// Token: 0x020000F0 RID: 240
	internal class DebugHandleTracker
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x0002BF14 File Offset: 0x0002A114
		static DebugHandleTracker()
		{
			if (CompModSwitches.HandleLeak.Level > TraceLevel.Off || CompModSwitches.TraceCollect.Enabled)
			{
				HandleCollector.HandleAdded += DebugHandleTracker.tracker.OnHandleAdd;
				HandleCollector.HandleRemoved += DebugHandleTracker.tracker.OnHandleRemove;
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00003800 File Offset: 0x00001A00
		private DebugHandleTracker()
		{
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002BF84 File Offset: 0x0002A184
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

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002C00C File Offset: 0x0002A20C
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

		// Token: 0x06000C6D RID: 3181 RVA: 0x00015259 File Offset: 0x00013459
		public static void Initialize()
		{
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002C0A0 File Offset: 0x0002A2A0
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

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002C0DC File Offset: 0x0002A2DC
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

		// Token: 0x04000ADA RID: 2778
		private static Hashtable handleTypes = new Hashtable();

		// Token: 0x04000ADB RID: 2779
		private static DebugHandleTracker tracker = new DebugHandleTracker();

		// Token: 0x04000ADC RID: 2780
		private static object internalSyncObject = new object();

		// Token: 0x02000132 RID: 306
		private class HandleType
		{
			// Token: 0x06000FA9 RID: 4009 RVA: 0x0002E424 File Offset: 0x0002C624
			public HandleType(string name)
			{
				this.name = name;
				this.buckets = new DebugHandleTracker.HandleType.HandleEntry[10];
			}

			// Token: 0x06000FAA RID: 4010 RVA: 0x0002E440 File Offset: 0x0002C640
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

			// Token: 0x06000FAB RID: 4011 RVA: 0x0002E4D0 File Offset: 0x0002C6D0
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

			// Token: 0x06000FAC RID: 4012 RVA: 0x0002E544 File Offset: 0x0002C744
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

			// Token: 0x06000FAD RID: 4013 RVA: 0x0002E5AC File Offset: 0x0002C7AC
			private int ComputeHash(IntPtr handle)
			{
				return ((int)handle & 65535) % 10;
			}

			// Token: 0x06000FAE RID: 4014 RVA: 0x0002E5C0 File Offset: 0x0002C7C0
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

			// Token: 0x04000CDE RID: 3294
			public readonly string name;

			// Token: 0x04000CDF RID: 3295
			private int handleCount;

			// Token: 0x04000CE0 RID: 3296
			private DebugHandleTracker.HandleType.HandleEntry[] buckets;

			// Token: 0x04000CE1 RID: 3297
			private const int BUCKETS = 10;

			// Token: 0x02000137 RID: 311
			private class HandleEntry
			{
				// Token: 0x06000FB5 RID: 4021 RVA: 0x0002E73A File Offset: 0x0002C93A
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

				// Token: 0x06000FB6 RID: 4022 RVA: 0x0002E770 File Offset: 0x0002C970
				public string ToString(DebugHandleTracker.HandleType type)
				{
					DebugHandleTracker.HandleType.HandleEntry.StackParser stackParser = new DebugHandleTracker.HandleType.HandleEntry.StackParser(this.callStack);
					stackParser.DiscardTo("HandleCollector.Add");
					stackParser.DiscardNext();
					stackParser.Truncate(40);
					string text = "";
					return Convert.ToString((int)this.handle, 16) + text + ": " + stackParser.ToString();
				}

				// Token: 0x04000CED RID: 3309
				public readonly IntPtr handle;

				// Token: 0x04000CEE RID: 3310
				public DebugHandleTracker.HandleType.HandleEntry next;

				// Token: 0x04000CEF RID: 3311
				public readonly string callStack;

				// Token: 0x04000CF0 RID: 3312
				public bool ignorableAsLeak;

				// Token: 0x02000138 RID: 312
				private class StackParser
				{
					// Token: 0x06000FB7 RID: 4023 RVA: 0x0002E7CB File Offset: 0x0002C9CB
					public StackParser(string callStack)
					{
						this.releventStack = callStack;
						this.length = this.releventStack.Length;
					}

					// Token: 0x06000FB8 RID: 4024 RVA: 0x0002E7EC File Offset: 0x0002C9EC
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

					// Token: 0x06000FB9 RID: 4025 RVA: 0x0002E838 File Offset: 0x0002CA38
					public void DiscardNext()
					{
						this.GetLine();
					}

					// Token: 0x06000FBA RID: 4026 RVA: 0x0002E844 File Offset: 0x0002CA44
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

					// Token: 0x06000FBB RID: 4027 RVA: 0x0002E874 File Offset: 0x0002CA74
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

					// Token: 0x06000FBC RID: 4028 RVA: 0x0002E932 File Offset: 0x0002CB32
					public override string ToString()
					{
						return this.releventStack.Substring(this.startIndex);
					}

					// Token: 0x06000FBD RID: 4029 RVA: 0x0002E948 File Offset: 0x0002CB48
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

					// Token: 0x04000CF1 RID: 3313
					internal string releventStack;

					// Token: 0x04000CF2 RID: 3314
					internal int startIndex;

					// Token: 0x04000CF3 RID: 3315
					internal int endIndex;

					// Token: 0x04000CF4 RID: 3316
					internal int length;
				}
			}
		}
	}
}
