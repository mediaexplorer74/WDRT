using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x020002B0 RID: 688
	internal sealed class KeyboardToolTipStateMachine
	{
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x000BF98E File Offset: 0x000BDB8E
		public static KeyboardToolTipStateMachine Instance
		{
			get
			{
				if (KeyboardToolTipStateMachine.instance == null)
				{
					KeyboardToolTipStateMachine.instance = new KeyboardToolTipStateMachine();
				}
				return KeyboardToolTipStateMachine.instance;
			}
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000BF9A8 File Offset: 0x000BDBA8
		private KeyboardToolTipStateMachine()
		{
			Dictionary<KeyboardToolTipStateMachine.SmTransition, Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>> dictionary = new Dictionary<KeyboardToolTipStateMachine.SmTransition, Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>>();
			KeyboardToolTipStateMachine.SmTransition smTransition = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.Hidden, KeyboardToolTipStateMachine.SmEvent.FocusedTool);
			dictionary[smTransition] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.SetupInitShowTimer);
			KeyboardToolTipStateMachine.SmTransition smTransition2 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.Hidden, KeyboardToolTipStateMachine.SmEvent.LeftTool);
			dictionary[smTransition2] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.DoNothing);
			KeyboardToolTipStateMachine.SmTransition smTransition3 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForInitShow, KeyboardToolTipStateMachine.SmEvent.FocusedTool);
			dictionary[smTransition3] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.DoNothing);
			KeyboardToolTipStateMachine.SmTransition smTransition4 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForInitShow, KeyboardToolTipStateMachine.SmEvent.LeftTool);
			dictionary[smTransition4] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.ResetFsmToHidden);
			KeyboardToolTipStateMachine.SmTransition smTransition5 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForInitShow, KeyboardToolTipStateMachine.SmEvent.InitialDelayTimerExpired);
			dictionary[smTransition5] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.ShowToolTip);
			KeyboardToolTipStateMachine.SmTransition smTransition6 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.Shown, KeyboardToolTipStateMachine.SmEvent.FocusedTool);
			dictionary[smTransition6] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.DoNothing);
			KeyboardToolTipStateMachine.SmTransition smTransition7 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.Shown, KeyboardToolTipStateMachine.SmEvent.LeftTool);
			dictionary[smTransition7] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.HideAndStartWaitingForRefocus);
			KeyboardToolTipStateMachine.SmTransition smTransition8 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.Shown, KeyboardToolTipStateMachine.SmEvent.DismissTooltips);
			dictionary[smTransition8] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.ResetFsmToHidden);
			KeyboardToolTipStateMachine.SmTransition smTransition9 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.WaitForRefocus, KeyboardToolTipStateMachine.SmEvent.FocusedTool);
			dictionary[smTransition9] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.SetupReshowTimer);
			KeyboardToolTipStateMachine.SmTransition smTransition10 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.WaitForRefocus, KeyboardToolTipStateMachine.SmEvent.LeftTool);
			dictionary[smTransition10] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.DoNothing);
			KeyboardToolTipStateMachine.SmTransition smTransition11 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.WaitForRefocus, KeyboardToolTipStateMachine.SmEvent.RefocusWaitDelayExpired);
			dictionary[smTransition11] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.ResetFsmToHidden);
			KeyboardToolTipStateMachine.SmTransition smTransition12 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForReshow, KeyboardToolTipStateMachine.SmEvent.FocusedTool);
			dictionary[smTransition12] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.DoNothing);
			KeyboardToolTipStateMachine.SmTransition smTransition13 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForReshow, KeyboardToolTipStateMachine.SmEvent.LeftTool);
			dictionary[smTransition13] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.StartWaitingForRefocus);
			KeyboardToolTipStateMachine.SmTransition smTransition14 = new KeyboardToolTipStateMachine.SmTransition(KeyboardToolTipStateMachine.SmState.ReadyForReshow, KeyboardToolTipStateMachine.SmEvent.ReshowDelayTimerExpired);
			dictionary[smTransition14] = new Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>(this.ShowToolTip);
			this.transitions = dictionary;
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000BFB7A File Offset: 0x000BDD7A
		public void ResetStateMachine(ToolTip toolTip)
		{
			this.Reset(toolTip);
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000BFB83 File Offset: 0x000BDD83
		public void Hook(IKeyboardToolTip tool, ToolTip toolTip)
		{
			if (tool.AllowsToolTip())
			{
				this.StartTracking(tool, toolTip);
				tool.OnHooked(toolTip);
			}
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000BFB9C File Offset: 0x000BDD9C
		public void NotifyAboutMouseEnter(IKeyboardToolTip sender)
		{
			if (this.IsToolTracked(sender) && sender.ShowsOwnToolTip())
			{
				this.Reset(null);
			}
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000BFBB6 File Offset: 0x000BDDB6
		private bool IsToolTracked(IKeyboardToolTip sender)
		{
			return this.toolToTip[sender] != null;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x000BFBC7 File Offset: 0x000BDDC7
		public void NotifyAboutLostFocus(IKeyboardToolTip sender)
		{
			if (this.IsToolTracked(sender) && sender.ShowsOwnToolTip())
			{
				this.Transit(KeyboardToolTipStateMachine.SmEvent.LeftTool, sender);
				if (this.currentTool == null)
				{
					this.lastFocusedTool.SetTarget(null);
				}
			}
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x000BFBF6 File Offset: 0x000BDDF6
		public void NotifyAboutGotFocus(IKeyboardToolTip sender)
		{
			if (this.IsToolTracked(sender) && sender.ShowsOwnToolTip() && sender.IsBeingTabbedTo())
			{
				this.Transit(KeyboardToolTipStateMachine.SmEvent.FocusedTool, sender);
				if (this.currentTool == sender)
				{
					this.lastFocusedTool.SetTarget(sender);
				}
			}
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000BFC2E File Offset: 0x000BDE2E
		public void Unhook(IKeyboardToolTip tool, ToolTip toolTip)
		{
			if (tool.AllowsToolTip())
			{
				this.StopTracking(tool, toolTip);
				tool.OnUnhooked(toolTip);
			}
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000BFC47 File Offset: 0x000BDE47
		public void NotifyAboutFormDeactivation(ToolTip sender)
		{
			this.OnFormDeactivation(sender);
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x000BFC50 File Offset: 0x000BDE50
		internal IKeyboardToolTip LastFocusedTool
		{
			get
			{
				IKeyboardToolTip keyboardToolTip;
				if (this.lastFocusedTool.TryGetTarget(out keyboardToolTip))
				{
					return keyboardToolTip;
				}
				return Control.FromHandleInternal(UnsafeNativeMethods.GetFocus());
			}
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000BFC78 File Offset: 0x000BDE78
		private KeyboardToolTipStateMachine.SmState HideAndStartWaitingForRefocus(IKeyboardToolTip tool, ToolTip toolTip)
		{
			toolTip.HideToolTip(this.currentTool);
			return this.StartWaitingForRefocus(tool, toolTip);
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000BFC90 File Offset: 0x000BDE90
		private KeyboardToolTipStateMachine.SmState StartWaitingForRefocus(IKeyboardToolTip tool, ToolTip toolTip)
		{
			this.ResetTimer();
			this.currentTool = null;
			SendOrPostCallback expirationCallback = null;
			this.refocusDelayExpirationCallback = (expirationCallback = delegate(object toolObject)
			{
				if (this.currentState == KeyboardToolTipStateMachine.SmState.WaitForRefocus && this.refocusDelayExpirationCallback == expirationCallback)
				{
					this.Transit(KeyboardToolTipStateMachine.SmEvent.RefocusWaitDelayExpired, (IKeyboardToolTip)toolObject);
				}
			});
			SynchronizationContext.Current.Post(expirationCallback, tool);
			return KeyboardToolTipStateMachine.SmState.WaitForRefocus;
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000BFCEC File Offset: 0x000BDEEC
		private KeyboardToolTipStateMachine.SmState SetupReshowTimer(IKeyboardToolTip tool, ToolTip toolTip)
		{
			this.currentTool = tool;
			this.ResetTimer();
			this.StartTimer(toolTip.GetDelayTime(1), this.GetOneRunTickHandler(delegate(Timer sender)
			{
				this.Transit(KeyboardToolTipStateMachine.SmEvent.ReshowDelayTimerExpired, tool);
			}));
			return KeyboardToolTipStateMachine.SmState.ReadyForReshow;
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000BFD40 File Offset: 0x000BDF40
		private KeyboardToolTipStateMachine.SmState ShowToolTip(IKeyboardToolTip tool, ToolTip toolTip)
		{
			string captionForTool = tool.GetCaptionForTool(toolTip);
			int num = (toolTip.IsPersistent ? 0 : toolTip.GetDelayTime(2));
			if (!this.currentTool.IsHoveredWithMouse())
			{
				toolTip.ShowKeyboardToolTip(captionForTool, this.currentTool, num);
			}
			if (!toolTip.IsPersistent)
			{
				this.StartTimer(num, this.GetOneRunTickHandler(delegate(Timer sender)
				{
					this.Transit(KeyboardToolTipStateMachine.SmEvent.DismissTooltips, this.currentTool);
				}));
			}
			return KeyboardToolTipStateMachine.SmState.Shown;
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000BFDA5 File Offset: 0x000BDFA5
		private KeyboardToolTipStateMachine.SmState ResetFsmToHidden(IKeyboardToolTip tool, ToolTip toolTip)
		{
			return this.FullFsmReset();
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000BFDAD File Offset: 0x000BDFAD
		private KeyboardToolTipStateMachine.SmState DoNothing(IKeyboardToolTip tool, ToolTip toolTip)
		{
			return this.currentState;
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000BFDB5 File Offset: 0x000BDFB5
		private KeyboardToolTipStateMachine.SmState SetupInitShowTimer(IKeyboardToolTip tool, ToolTip toolTip)
		{
			this.currentTool = tool;
			this.ResetTimer();
			this.StartTimer(toolTip.GetDelayTime(3), this.GetOneRunTickHandler(delegate(Timer sender)
			{
				this.Transit(KeyboardToolTipStateMachine.SmEvent.InitialDelayTimerExpired, this.currentTool);
			}));
			return KeyboardToolTipStateMachine.SmState.ReadyForInitShow;
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000BFDE4 File Offset: 0x000BDFE4
		private void StartTimer(int interval, EventHandler eventHandler)
		{
			this.timer.Interval = interval;
			this.timer.Tick += eventHandler;
			this.timer.Start();
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000BFE0C File Offset: 0x000BE00C
		private EventHandler GetOneRunTickHandler(Action<Timer> handler)
		{
			EventHandler wrapper = null;
			wrapper = delegate(object sender, EventArgs eventArgs)
			{
				this.timer.Stop();
				this.timer.Tick -= wrapper;
				handler(this.timer);
			};
			return wrapper;
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000BFE4C File Offset: 0x000BE04C
		private void Transit(KeyboardToolTipStateMachine.SmEvent @event, IKeyboardToolTip source)
		{
			bool flag = false;
			try
			{
				ToolTip toolTip = this.toolToTip[source];
				if ((this.currentTool == null || this.currentTool.CanShowToolTipsNow()) && toolTip != null)
				{
					Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState> func = this.transitions[new KeyboardToolTipStateMachine.SmTransition(this.currentState, @event)];
					this.currentState = func(source, toolTip);
				}
				else
				{
					flag = true;
				}
			}
			catch
			{
				flag = true;
				throw;
			}
			finally
			{
				if (flag)
				{
					this.FullFsmReset();
				}
			}
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000BFED8 File Offset: 0x000BE0D8
		internal static void HidePersistentTooltip()
		{
			KeyboardToolTipStateMachine keyboardToolTipStateMachine = KeyboardToolTipStateMachine.instance;
			if (keyboardToolTipStateMachine == null)
			{
				return;
			}
			keyboardToolTipStateMachine.HidePersistent();
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000BFEEC File Offset: 0x000BE0EC
		private void HidePersistent()
		{
			if (this.currentState != KeyboardToolTipStateMachine.SmState.Shown || this.currentTool == null)
			{
				return;
			}
			ToolTip toolTip = this.toolToTip[this.currentTool];
			if (toolTip != null && toolTip.IsPersistent)
			{
				toolTip.HideToolTip(this.currentTool);
				this.currentTool = null;
				this.currentState = KeyboardToolTipStateMachine.SmState.Hidden;
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000BFF44 File Offset: 0x000BE144
		private KeyboardToolTipStateMachine.SmState FullFsmReset()
		{
			if (this.currentState == KeyboardToolTipStateMachine.SmState.Shown && this.currentTool != null)
			{
				ToolTip toolTip = this.toolToTip[this.currentTool];
				if (toolTip != null)
				{
					toolTip.HideToolTip(this.currentTool);
				}
			}
			this.ResetTimer();
			this.currentTool = null;
			return this.currentState = KeyboardToolTipStateMachine.SmState.Hidden;
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000BFF9A File Offset: 0x000BE19A
		private void ResetTimer()
		{
			this.timer.ClearTimerTickHandlers();
			this.timer.Stop();
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000BFFB2 File Offset: 0x000BE1B2
		private void Reset(ToolTip toolTipToReset)
		{
			if (toolTipToReset == null || (this.currentTool != null && this.toolToTip[this.currentTool] == toolTipToReset))
			{
				this.FullFsmReset();
			}
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000BFFDA File Offset: 0x000BE1DA
		private void StartTracking(IKeyboardToolTip tool, ToolTip toolTip)
		{
			this.toolToTip[tool] = toolTip;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000BFFE9 File Offset: 0x000BE1E9
		private void StopTracking(IKeyboardToolTip tool, ToolTip toolTip)
		{
			this.toolToTip.Remove(tool, toolTip);
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000BFFF8 File Offset: 0x000BE1F8
		private void OnFormDeactivation(ToolTip sender)
		{
			if (this.currentTool != null && this.toolToTip[this.currentTool] == sender)
			{
				this.FullFsmReset();
			}
		}

		// Token: 0x04001126 RID: 4390
		[ThreadStatic]
		private static KeyboardToolTipStateMachine instance;

		// Token: 0x04001127 RID: 4391
		private readonly Dictionary<KeyboardToolTipStateMachine.SmTransition, Func<IKeyboardToolTip, ToolTip, KeyboardToolTipStateMachine.SmState>> transitions;

		// Token: 0x04001128 RID: 4392
		private readonly KeyboardToolTipStateMachine.ToolToTipDictionary toolToTip = new KeyboardToolTipStateMachine.ToolToTipDictionary();

		// Token: 0x04001129 RID: 4393
		private KeyboardToolTipStateMachine.SmState currentState;

		// Token: 0x0400112A RID: 4394
		private IKeyboardToolTip currentTool;

		// Token: 0x0400112B RID: 4395
		private readonly KeyboardToolTipStateMachine.InternalStateMachineTimer timer = new KeyboardToolTipStateMachine.InternalStateMachineTimer();

		// Token: 0x0400112C RID: 4396
		private SendOrPostCallback refocusDelayExpirationCallback;

		// Token: 0x0400112D RID: 4397
		private readonly WeakReference<IKeyboardToolTip> lastFocusedTool = new WeakReference<IKeyboardToolTip>(null);

		// Token: 0x020006AF RID: 1711
		private enum SmEvent : byte
		{
			// Token: 0x04003AF9 RID: 15097
			FocusedTool,
			// Token: 0x04003AFA RID: 15098
			LeftTool,
			// Token: 0x04003AFB RID: 15099
			InitialDelayTimerExpired,
			// Token: 0x04003AFC RID: 15100
			ReshowDelayTimerExpired,
			// Token: 0x04003AFD RID: 15101
			DismissTooltips,
			// Token: 0x04003AFE RID: 15102
			RefocusWaitDelayExpired
		}

		// Token: 0x020006B0 RID: 1712
		internal enum SmState : byte
		{
			// Token: 0x04003B00 RID: 15104
			Hidden,
			// Token: 0x04003B01 RID: 15105
			ReadyForInitShow,
			// Token: 0x04003B02 RID: 15106
			Shown,
			// Token: 0x04003B03 RID: 15107
			ReadyForReshow,
			// Token: 0x04003B04 RID: 15108
			WaitForRefocus
		}

		// Token: 0x020006B1 RID: 1713
		private struct SmTransition : IEquatable<KeyboardToolTipStateMachine.SmTransition>
		{
			// Token: 0x060068A5 RID: 26789 RVA: 0x00184EF2 File Offset: 0x001830F2
			public SmTransition(KeyboardToolTipStateMachine.SmState currentState, KeyboardToolTipStateMachine.SmEvent @event)
			{
				this.currentState = currentState;
				this.@event = @event;
			}

			// Token: 0x060068A6 RID: 26790 RVA: 0x00184F02 File Offset: 0x00183102
			public bool Equals(KeyboardToolTipStateMachine.SmTransition other)
			{
				return this.currentState == other.currentState && this.@event == other.@event;
			}

			// Token: 0x060068A7 RID: 26791 RVA: 0x00184F22 File Offset: 0x00183122
			public override bool Equals(object obj)
			{
				return obj is KeyboardToolTipStateMachine.SmTransition && this.Equals((KeyboardToolTipStateMachine.SmTransition)obj);
			}

			// Token: 0x060068A8 RID: 26792 RVA: 0x00184F3A File Offset: 0x0018313A
			public override int GetHashCode()
			{
				return (int)(((int)this.currentState << 16) | (KeyboardToolTipStateMachine.SmState)this.@event);
			}

			// Token: 0x04003B05 RID: 15109
			private readonly KeyboardToolTipStateMachine.SmState currentState;

			// Token: 0x04003B06 RID: 15110
			private readonly KeyboardToolTipStateMachine.SmEvent @event;
		}

		// Token: 0x020006B2 RID: 1714
		private sealed class InternalStateMachineTimer : Timer
		{
			// Token: 0x060068A9 RID: 26793 RVA: 0x00184F4C File Offset: 0x0018314C
			public void ClearTimerTickHandlers()
			{
				this.onTimer = null;
			}
		}

		// Token: 0x020006B3 RID: 1715
		private sealed class ToolToTipDictionary
		{
			// Token: 0x170016A1 RID: 5793
			public ToolTip this[IKeyboardToolTip tool]
			{
				get
				{
					ToolTip toolTip = null;
					WeakReference<ToolTip> weakReference;
					if (this.table.TryGetValue(tool, out weakReference) && !weakReference.TryGetTarget(out toolTip))
					{
						this.table.Remove(tool);
					}
					return toolTip;
				}
				set
				{
					WeakReference<ToolTip> weakReference;
					if (this.table.TryGetValue(tool, out weakReference))
					{
						weakReference.SetTarget(value);
						return;
					}
					this.table.Add(tool, new WeakReference<ToolTip>(value));
				}
			}

			// Token: 0x060068AD RID: 26797 RVA: 0x00184FD0 File Offset: 0x001831D0
			public void Remove(IKeyboardToolTip tool, ToolTip toolTip)
			{
				WeakReference<ToolTip> weakReference;
				if (this.table.TryGetValue(tool, out weakReference))
				{
					ToolTip toolTip2;
					if (weakReference.TryGetTarget(out toolTip2))
					{
						if (toolTip2 == toolTip)
						{
							this.table.Remove(tool);
							return;
						}
					}
					else
					{
						this.table.Remove(tool);
					}
				}
			}

			// Token: 0x04003B07 RID: 15111
			private ConditionalWeakTable<IKeyboardToolTip, WeakReference<ToolTip>> table = new ConditionalWeakTable<IKeyboardToolTip, WeakReference<ToolTip>>();
		}
	}
}
