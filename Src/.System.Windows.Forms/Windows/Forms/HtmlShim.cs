using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000282 RID: 642
	internal abstract class HtmlShim : IDisposable
	{
		// Token: 0x06002926 RID: 10534 RVA: 0x000BCD48 File Offset: 0x000BAF48
		~HtmlShim()
		{
			this.Dispose(false);
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x000BCD78 File Offset: 0x000BAF78
		private EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		// Token: 0x06002928 RID: 10536
		public abstract void AttachEventHandler(string eventName, EventHandler eventHandler);

		// Token: 0x06002929 RID: 10537 RVA: 0x000BCD93 File Offset: 0x000BAF93
		public void AddHandler(object key, Delegate value)
		{
			this.eventCount++;
			this.Events.AddHandler(key, value);
			this.OnEventHandlerAdded();
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000BCDB8 File Offset: 0x000BAFB8
		protected HtmlToClrEventProxy AddEventProxy(string eventName, EventHandler eventHandler)
		{
			if (this.attachedEventList == null)
			{
				this.attachedEventList = new Dictionary<EventHandler, HtmlToClrEventProxy>();
			}
			HtmlToClrEventProxy htmlToClrEventProxy = new HtmlToClrEventProxy(this, eventName, eventHandler);
			this.attachedEventList[eventHandler] = htmlToClrEventProxy;
			return htmlToClrEventProxy;
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600292B RID: 10539
		public abstract UnsafeNativeMethods.IHTMLWindow2 AssociatedWindow { get; }

		// Token: 0x0600292C RID: 10540
		public abstract void ConnectToEvents();

		// Token: 0x0600292D RID: 10541
		public abstract void DetachEventHandler(string eventName, EventHandler eventHandler);

		// Token: 0x0600292E RID: 10542 RVA: 0x000BCDF0 File Offset: 0x000BAFF0
		public virtual void DisconnectFromEvents()
		{
			if (this.attachedEventList != null)
			{
				EventHandler[] array = new EventHandler[this.attachedEventList.Count];
				this.attachedEventList.Keys.CopyTo(array, 0);
				foreach (EventHandler eventHandler in array)
				{
					HtmlToClrEventProxy htmlToClrEventProxy = this.attachedEventList[eventHandler];
					this.DetachEventHandler(htmlToClrEventProxy.EventName, eventHandler);
				}
			}
		}

		// Token: 0x0600292F RID: 10543
		protected abstract object GetEventSender();

		// Token: 0x06002930 RID: 10544 RVA: 0x000BCE58 File Offset: 0x000BB058
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000BCE67 File Offset: 0x000BB067
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisconnectFromEvents();
				if (this.events != null)
				{
					this.events.Dispose();
					this.events = null;
				}
			}
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x000BCE8C File Offset: 0x000BB08C
		public void FireEvent(object key, EventArgs e)
		{
			Delegate @delegate = this.Events[key];
			if (@delegate != null)
			{
				try
				{
					@delegate.DynamicInvoke(new object[]
					{
						this.GetEventSender(),
						e
					});
				}
				catch (Exception ex)
				{
					if (NativeWindow.WndProcShouldBeDebuggable)
					{
						throw;
					}
					Application.OnThreadException(ex);
				}
			}
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000BCEE8 File Offset: 0x000BB0E8
		protected virtual void OnEventHandlerAdded()
		{
			this.ConnectToEvents();
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x000BCEF0 File Offset: 0x000BB0F0
		protected virtual void OnEventHandlerRemoved()
		{
			if (this.eventCount <= 0)
			{
				this.DisconnectFromEvents();
				this.eventCount = 0;
			}
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000BCF08 File Offset: 0x000BB108
		public void RemoveHandler(object key, Delegate value)
		{
			this.eventCount--;
			this.Events.RemoveHandler(key, value);
			this.OnEventHandlerRemoved();
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000BCF2C File Offset: 0x000BB12C
		protected HtmlToClrEventProxy RemoveEventProxy(EventHandler eventHandler)
		{
			if (this.attachedEventList == null)
			{
				return null;
			}
			if (this.attachedEventList.ContainsKey(eventHandler))
			{
				HtmlToClrEventProxy htmlToClrEventProxy = this.attachedEventList[eventHandler];
				this.attachedEventList.Remove(eventHandler);
				return htmlToClrEventProxy;
			}
			return null;
		}

		// Token: 0x040010D0 RID: 4304
		private EventHandlerList events;

		// Token: 0x040010D1 RID: 4305
		private int eventCount;

		// Token: 0x040010D2 RID: 4306
		private Dictionary<EventHandler, HtmlToClrEventProxy> attachedEventList;
	}
}
