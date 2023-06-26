using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a Windows menu or toolbar command item.</summary>
	// Token: 0x020005FB RID: 1531
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MenuCommand" /> class.</summary>
		/// <param name="handler">The event to raise when the user selects the menu item or toolbar button.</param>
		/// <param name="command">The unique command ID that links this menu command to the environment's menu.</param>
		// Token: 0x06003852 RID: 14418 RVA: 0x000F0732 File Offset: 0x000EE932
		public MenuCommand(EventHandler handler, CommandID command)
		{
			this.execHandler = handler;
			this.commandID = command;
			this.status = 3;
		}

		/// <summary>Gets or sets a value indicating whether this menu item is checked.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is checked; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06003853 RID: 14419 RVA: 0x000F074F File Offset: 0x000EE94F
		// (set) Token: 0x06003854 RID: 14420 RVA: 0x000F075C File Offset: 0x000EE95C
		public virtual bool Checked
		{
			get
			{
				return (this.status & 4) != 0;
			}
			set
			{
				this.SetStatus(4, value);
			}
		}

		/// <summary>Gets a value indicating whether this menu item is available.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06003855 RID: 14421 RVA: 0x000F0766 File Offset: 0x000EE966
		// (set) Token: 0x06003856 RID: 14422 RVA: 0x000F0773 File Offset: 0x000EE973
		public virtual bool Enabled
		{
			get
			{
				return (this.status & 2) != 0;
			}
			set
			{
				this.SetStatus(2, value);
			}
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000F0780 File Offset: 0x000EE980
		private void SetStatus(int mask, bool value)
		{
			int num = this.status;
			if (value)
			{
				num |= mask;
			}
			else
			{
				num &= ~mask;
			}
			if (num != this.status)
			{
				this.status = num;
				this.OnCommandChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the public properties associated with the <see cref="T:System.ComponentModel.Design.MenuCommand" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the public properties of the <see cref="T:System.ComponentModel.Design.MenuCommand" />.</returns>
		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x000F07BD File Offset: 0x000EE9BD
		public virtual IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new HybridDictionary();
				}
				return this.properties;
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is supported.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is supported, which is the default; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x000F07D8 File Offset: 0x000EE9D8
		// (set) Token: 0x0600385A RID: 14426 RVA: 0x000F07E5 File Offset: 0x000EE9E5
		public virtual bool Supported
		{
			get
			{
				return (this.status & 1) != 0;
			}
			set
			{
				this.SetStatus(1, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is visible; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x000F07EF File Offset: 0x000EE9EF
		// (set) Token: 0x0600385C RID: 14428 RVA: 0x000F07FD File Offset: 0x000EE9FD
		public virtual bool Visible
		{
			get
			{
				return (this.status & 16) == 0;
			}
			set
			{
				this.SetStatus(16, !value);
			}
		}

		/// <summary>Occurs when the menu command changes.</summary>
		// Token: 0x14000067 RID: 103
		// (add) Token: 0x0600385D RID: 14429 RVA: 0x000F080B File Offset: 0x000EEA0B
		// (remove) Token: 0x0600385E RID: 14430 RVA: 0x000F0824 File Offset: 0x000EEA24
		public event EventHandler CommandChanged
		{
			add
			{
				this.statusHandler = (EventHandler)Delegate.Combine(this.statusHandler, value);
			}
			remove
			{
				this.statusHandler = (EventHandler)Delegate.Remove(this.statusHandler, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> associated with this menu command.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.CommandID" /> associated with the menu command.</returns>
		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x000F083D File Offset: 0x000EEA3D
		public virtual CommandID CommandID
		{
			get
			{
				return this.commandID;
			}
		}

		/// <summary>Invokes the command.</summary>
		// Token: 0x06003860 RID: 14432 RVA: 0x000F0848 File Offset: 0x000EEA48
		public virtual void Invoke()
		{
			if (this.execHandler != null)
			{
				try
				{
					this.execHandler(this, EventArgs.Empty);
				}
				catch (CheckoutException ex)
				{
					if (ex != CheckoutException.Canceled)
					{
						throw;
					}
				}
			}
		}

		/// <summary>Invokes the command with the given parameter.</summary>
		/// <param name="arg">An optional argument for use by the command.</param>
		// Token: 0x06003861 RID: 14433 RVA: 0x000F0890 File Offset: 0x000EEA90
		public virtual void Invoke(object arg)
		{
			this.Invoke();
		}

		/// <summary>Gets the OLE command status code for this menu item.</summary>
		/// <returns>An integer containing a mixture of status flags that reflect the state of this menu item.</returns>
		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000F0898 File Offset: 0x000EEA98
		public virtual int OleStatus
		{
			get
			{
				return this.status;
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.MenuCommand.CommandChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003863 RID: 14435 RVA: 0x000F08A0 File Offset: 0x000EEAA0
		protected virtual void OnCommandChanged(EventArgs e)
		{
			if (this.statusHandler != null)
			{
				this.statusHandler(this, e);
			}
		}

		/// <summary>Returns a string representation of this menu command.</summary>
		/// <returns>A string containing the value of the <see cref="P:System.ComponentModel.Design.MenuCommand.CommandID" /> property appended with the names of any flags that are set, separated by pipe bars (|). These flag properties include <see cref="P:System.ComponentModel.Design.MenuCommand.Checked" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Enabled" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Supported" />, and <see cref="P:System.ComponentModel.Design.MenuCommand.Visible" />.</returns>
		// Token: 0x06003864 RID: 14436 RVA: 0x000F08B8 File Offset: 0x000EEAB8
		public override string ToString()
		{
			string text = this.CommandID.ToString() + " : ";
			if ((this.status & 1) != 0)
			{
				text += "Supported";
			}
			if ((this.status & 2) != 0)
			{
				text += "|Enabled";
			}
			if ((this.status & 16) == 0)
			{
				text += "|Visible";
			}
			if ((this.status & 4) != 0)
			{
				text += "|Checked";
			}
			return text;
		}

		// Token: 0x04002AF6 RID: 10998
		private EventHandler execHandler;

		// Token: 0x04002AF7 RID: 10999
		private EventHandler statusHandler;

		// Token: 0x04002AF8 RID: 11000
		private CommandID commandID;

		// Token: 0x04002AF9 RID: 11001
		private int status;

		// Token: 0x04002AFA RID: 11002
		private IDictionary properties;

		// Token: 0x04002AFB RID: 11003
		private const int ENABLED = 2;

		// Token: 0x04002AFC RID: 11004
		private const int INVISIBLE = 16;

		// Token: 0x04002AFD RID: 11005
		private const int CHECKED = 4;

		// Token: 0x04002AFE RID: 11006
		private const int SUPPORTED = 1;
	}
}
