using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a shortcut menu.</summary>
	// Token: 0x02000167 RID: 359
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("Opening")]
	[SRDescription("DescriptionContextMenuStrip")]
	public class ContextMenuStrip : ToolStripDropDownMenu
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> class and associates it with the specified container.</summary>
		/// <param name="container">A component that implements <see cref="T:System.ComponentModel.IContainer" /> that is the container of the <see cref="T:System.Windows.Forms.ContextMenuStrip" />.</param>
		// Token: 0x06000F4E RID: 3918 RVA: 0x0002E959 File Offset: 0x0002CB59
		public ContextMenuStrip(IContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> class.</summary>
		// Token: 0x06000F4F RID: 3919 RVA: 0x0002E976 File Offset: 0x0002CB76
		public ContextMenuStrip()
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000F50 RID: 3920 RVA: 0x0002E97E File Offset: 0x0002CB7E
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Gets the last control that caused this <see cref="T:System.Windows.Forms.ContextMenuStrip" /> to be displayed.</summary>
		/// <returns>The control that caused this <see cref="T:System.Windows.Forms.ContextMenuStrip" /> to be displayed.</returns>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0002E987 File Offset: 0x0002CB87
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ContextMenuStripSourceControlDescr")]
		public Control SourceControl
		{
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				return base.SourceControlInternal;
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002E990 File Offset: 0x0002CB90
		internal ContextMenuStrip Clone()
		{
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			contextMenuStrip.Events.AddHandlers(base.Events);
			contextMenuStrip.AutoClose = base.AutoClose;
			contextMenuStrip.AutoSize = this.AutoSize;
			contextMenuStrip.Bounds = base.Bounds;
			contextMenuStrip.ImageList = base.ImageList;
			contextMenuStrip.ShowCheckMargin = base.ShowCheckMargin;
			contextMenuStrip.ShowImageMargin = base.ShowImageMargin;
			for (int i = 0; i < this.Items.Count; i++)
			{
				ToolStripItem toolStripItem = this.Items[i];
				if (toolStripItem is ToolStripSeparator)
				{
					contextMenuStrip.Items.Add(new ToolStripSeparator());
				}
				else if (toolStripItem is ToolStripMenuItem)
				{
					ToolStripMenuItem toolStripMenuItem = toolStripItem as ToolStripMenuItem;
					contextMenuStrip.Items.Add(toolStripMenuItem.Clone());
				}
			}
			return contextMenuStrip;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0002EA5C File Offset: 0x0002CC5C
		internal void ShowInternal(Control source, Point location, bool isKeyboardActivated)
		{
			base.Show(source, location);
			if (isKeyboardActivated)
			{
				ToolStripManager.ModalMenuFilter.Instance.ShowUnderlines = true;
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0002EA74 File Offset: 0x0002CC74
		internal void ShowInTaskbar(int x, int y)
		{
			base.WorkingAreaConstrained = false;
			Rectangle rectangle = base.CalculateDropDownLocation(new Point(x, y), ToolStripDropDownDirection.AboveLeft);
			Rectangle bounds = Screen.FromRectangle(rectangle).Bounds;
			if (rectangle.Y < bounds.Y)
			{
				rectangle = base.CalculateDropDownLocation(new Point(x, y), ToolStripDropDownDirection.BelowLeft);
			}
			else if (rectangle.X < bounds.X)
			{
				rectangle = base.CalculateDropDownLocation(new Point(x, y), ToolStripDropDownDirection.AboveRight);
			}
			rectangle = WindowsFormsUtils.ConstrainToBounds(bounds, rectangle);
			base.Show(rectangle.X, rectangle.Y);
		}

		/// <summary>Sets the control to the specified visible state.</summary>
		/// <param name="visible">
		///   <see langword="true" /> to make the control visible; otherwise, <see langword="false" />.</param>
		// Token: 0x06000F55 RID: 3925 RVA: 0x0002EAFF File Offset: 0x0002CCFF
		protected override void SetVisibleCore(bool visible)
		{
			if (!visible)
			{
				base.WorkingAreaConstrained = true;
			}
			base.SetVisibleCore(visible);
		}
	}
}
