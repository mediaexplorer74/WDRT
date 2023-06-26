using System;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Encapsulates the information needed when creating a control.</summary>
	// Token: 0x02000173 RID: 371
	public class CreateParams
	{
		/// <summary>Gets or sets the name of the Windows class to derive the control from.</summary>
		/// <returns>The name of the Windows class to derive the control from.</returns>
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0004137D File Offset: 0x0003F57D
		// (set) Token: 0x06001388 RID: 5000 RVA: 0x00041385 File Offset: 0x0003F585
		public string ClassName
		{
			get
			{
				return this.className;
			}
			set
			{
				this.className = value;
			}
		}

		/// <summary>Gets or sets the control's initial text.</summary>
		/// <returns>The control's initial text.</returns>
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0004138E File Offset: 0x0003F58E
		// (set) Token: 0x0600138A RID: 5002 RVA: 0x00041396 File Offset: 0x0003F596
		public string Caption
		{
			get
			{
				return this.caption;
			}
			set
			{
				this.caption = value;
			}
		}

		/// <summary>Gets or sets a bitwise combination of window style values.</summary>
		/// <returns>A bitwise combination of the window style values.</returns>
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x0004139F File Offset: 0x0003F59F
		// (set) Token: 0x0600138C RID: 5004 RVA: 0x000413A7 File Offset: 0x0003F5A7
		public int Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
			}
		}

		/// <summary>Gets or sets a bitwise combination of extended window style values.</summary>
		/// <returns>A bitwise combination of the extended window style values.</returns>
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x000413B0 File Offset: 0x0003F5B0
		// (set) Token: 0x0600138E RID: 5006 RVA: 0x000413B8 File Offset: 0x0003F5B8
		public int ExStyle
		{
			get
			{
				return this.exStyle;
			}
			set
			{
				this.exStyle = value;
			}
		}

		/// <summary>Gets or sets a bitwise combination of class style values.</summary>
		/// <returns>A bitwise combination of the class style values.</returns>
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x000413C1 File Offset: 0x0003F5C1
		// (set) Token: 0x06001390 RID: 5008 RVA: 0x000413C9 File Offset: 0x0003F5C9
		public int ClassStyle
		{
			get
			{
				return this.classStyle;
			}
			set
			{
				this.classStyle = value;
			}
		}

		/// <summary>Gets or sets the initial left position of the control.</summary>
		/// <returns>The numeric value that represents the initial left position of the control.</returns>
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x000413D2 File Offset: 0x0003F5D2
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x000413DA File Offset: 0x0003F5DA
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		/// <summary>Gets or sets the top position of the initial location of the control.</summary>
		/// <returns>The numeric value that represents the top position of the initial location of the control.</returns>
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x000413E3 File Offset: 0x0003F5E3
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x000413EB File Offset: 0x0003F5EB
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		/// <summary>Gets or sets the initial width of the control.</summary>
		/// <returns>The numeric value that represents the initial width of the control.</returns>
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x000413F4 File Offset: 0x0003F5F4
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x000413FC File Offset: 0x0003F5FC
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		/// <summary>Gets or sets the initial height of the control.</summary>
		/// <returns>The numeric value that represents the initial height of the control.</returns>
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x00041405 File Offset: 0x0003F605
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x0004140D File Offset: 0x0003F60D
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>Gets or sets the control's parent.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that contains the window handle of the control's parent.</returns>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x00041416 File Offset: 0x0003F616
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x0004141E File Offset: 0x0003F61E
		public IntPtr Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		/// <summary>Gets or sets additional parameter information needed to create the control.</summary>
		/// <returns>The <see cref="T:System.Object" /> that holds additional parameter information needed to create the control.</returns>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x00041427 File Offset: 0x0003F627
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x0004142F File Offset: 0x0003F62F
		public object Param
		{
			get
			{
				return this.param;
			}
			set
			{
				this.param = value;
			}
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x0600139D RID: 5021 RVA: 0x00041438 File Offset: 0x0003F638
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("CreateParams {'");
			stringBuilder.Append(this.className);
			stringBuilder.Append("', '");
			stringBuilder.Append(this.caption);
			stringBuilder.Append("', 0x");
			stringBuilder.Append(Convert.ToString(this.style, 16));
			stringBuilder.Append(", 0x");
			stringBuilder.Append(Convert.ToString(this.exStyle, 16));
			stringBuilder.Append(", {");
			stringBuilder.Append(this.x);
			stringBuilder.Append(", ");
			stringBuilder.Append(this.y);
			stringBuilder.Append(", ");
			stringBuilder.Append(this.width);
			stringBuilder.Append(", ");
			stringBuilder.Append(this.height);
			stringBuilder.Append("}");
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0400092D RID: 2349
		private string className;

		// Token: 0x0400092E RID: 2350
		private string caption;

		// Token: 0x0400092F RID: 2351
		private int style;

		// Token: 0x04000930 RID: 2352
		private int exStyle;

		// Token: 0x04000931 RID: 2353
		private int classStyle;

		// Token: 0x04000932 RID: 2354
		private int x;

		// Token: 0x04000933 RID: 2355
		private int y;

		// Token: 0x04000934 RID: 2356
		private int width;

		// Token: 0x04000935 RID: 2357
		private int height;

		// Token: 0x04000936 RID: 2358
		private IntPtr parent;

		// Token: 0x04000937 RID: 2359
		private object param;
	}
}
