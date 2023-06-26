using System;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Implements a Windows message.</summary>
	// Token: 0x020002F7 RID: 759
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public struct Message
	{
		/// <summary>Gets or sets the window handle of the message.</summary>
		/// <returns>The window handle of the message.</returns>
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06003039 RID: 12345 RVA: 0x000D906B File Offset: 0x000D726B
		// (set) Token: 0x0600303A RID: 12346 RVA: 0x000D9073 File Offset: 0x000D7273
		public IntPtr HWnd
		{
			get
			{
				return this.hWnd;
			}
			set
			{
				this.hWnd = value;
			}
		}

		/// <summary>Gets or sets the ID number for the message.</summary>
		/// <returns>The ID number for the message.</returns>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x000D907C File Offset: 0x000D727C
		// (set) Token: 0x0600303C RID: 12348 RVA: 0x000D9084 File Offset: 0x000D7284
		public int Msg
		{
			get
			{
				return this.msg;
			}
			set
			{
				this.msg = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Windows.Forms.Message.WParam" /> field of the message.</summary>
		/// <returns>The <see cref="P:System.Windows.Forms.Message.WParam" /> field of the message.</returns>
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x000D908D File Offset: 0x000D728D
		// (set) Token: 0x0600303E RID: 12350 RVA: 0x000D9095 File Offset: 0x000D7295
		public IntPtr WParam
		{
			get
			{
				return this.wparam;
			}
			set
			{
				this.wparam = value;
			}
		}

		/// <summary>Specifies the <see cref="P:System.Windows.Forms.Message.LParam" /> field of the message.</summary>
		/// <returns>The <see cref="P:System.Windows.Forms.Message.LParam" /> field of the message.</returns>
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x000D909E File Offset: 0x000D729E
		// (set) Token: 0x06003040 RID: 12352 RVA: 0x000D90A6 File Offset: 0x000D72A6
		public IntPtr LParam
		{
			get
			{
				return this.lparam;
			}
			set
			{
				this.lparam = value;
			}
		}

		/// <summary>Specifies the value that is returned to Windows in response to handling the message.</summary>
		/// <returns>The return value of the message.</returns>
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x000D90AF File Offset: 0x000D72AF
		// (set) Token: 0x06003042 RID: 12354 RVA: 0x000D90B7 File Offset: 0x000D72B7
		public IntPtr Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		/// <summary>Gets the <see cref="P:System.Windows.Forms.Message.LParam" /> value and converts the value to an object.</summary>
		/// <param name="cls">The type to use to create an instance. This type must be declared as a structure type.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents an instance of the class specified by the <paramref name="cls" /> parameter, with the data from the <see cref="P:System.Windows.Forms.Message.LParam" /> field of the message.</returns>
		// Token: 0x06003043 RID: 12355 RVA: 0x000D90C0 File Offset: 0x000D72C0
		public object GetLParam(Type cls)
		{
			return UnsafeNativeMethods.PtrToStructure(this.lparam, cls);
		}

		/// <summary>Creates a new <see cref="T:System.Windows.Forms.Message" />.</summary>
		/// <param name="hWnd">The window handle that the message is for.</param>
		/// <param name="msg">The message ID.</param>
		/// <param name="wparam">The message <paramref name="wparam" /> field.</param>
		/// <param name="lparam">The message <paramref name="lparam" /> field.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.Message" /> that represents the message that was created.</returns>
		// Token: 0x06003044 RID: 12356 RVA: 0x000D90D0 File Offset: 0x000D72D0
		public static Message Create(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			return new Message
			{
				hWnd = hWnd,
				msg = msg,
				wparam = wparam,
				lparam = lparam,
				result = IntPtr.Zero
			};
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <param name="o">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003045 RID: 12357 RVA: 0x000D9114 File Offset: 0x000D7314
		public override bool Equals(object o)
		{
			if (!(o is Message))
			{
				return false;
			}
			Message message = (Message)o;
			return this.hWnd == message.hWnd && this.msg == message.msg && this.wparam == message.wparam && this.lparam == message.lparam && this.result == message.result;
		}

		/// <summary>Determines whether two instances of <see cref="T:System.Windows.Forms.Message" /> are not equal.</summary>
		/// <param name="a">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> and <paramref name="b" /> do not represent the same <see cref="T:System.Windows.Forms.Message" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003046 RID: 12358 RVA: 0x000D918C File Offset: 0x000D738C
		public static bool operator !=(Message a, Message b)
		{
			return !a.Equals(b);
		}

		/// <summary>Determines whether two instances of <see cref="T:System.Windows.Forms.Message" /> are equal.</summary>
		/// <param name="a">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> and <paramref name="b" /> represent the same <see cref="T:System.Windows.Forms.Message" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003047 RID: 12359 RVA: 0x000D91A4 File Offset: 0x000D73A4
		public static bool operator ==(Message a, Message b)
		{
			return a.Equals(b);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x06003048 RID: 12360 RVA: 0x000D91B9 File Offset: 0x000D73B9
		public override int GetHashCode()
		{
			return ((int)this.hWnd << 4) | this.msg;
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Message" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Message" />.</returns>
		// Token: 0x06003049 RID: 12361 RVA: 0x000D91D0 File Offset: 0x000D73D0
		public override string ToString()
		{
			bool flag = false;
			try
			{
				IntSecurity.UnmanagedCode.Demand();
				flag = true;
			}
			catch (SecurityException)
			{
			}
			if (flag)
			{
				return MessageDecoder.ToString(this);
			}
			return base.ToString();
		}

		// Token: 0x040013DB RID: 5083
		private IntPtr hWnd;

		// Token: 0x040013DC RID: 5084
		private int msg;

		// Token: 0x040013DD RID: 5085
		private IntPtr wparam;

		// Token: 0x040013DE RID: 5086
		private IntPtr lparam;

		// Token: 0x040013DF RID: 5087
		private IntPtr result;
	}
}
