using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Marks the program elements that are no longer in use. This class cannot be inherited.</summary>
	// Token: 0x0200011A RID: 282
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ObsoleteAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with default properties.</summary>
		// Token: 0x060010BB RID: 4283 RVA: 0x000327B1 File Offset: 0x000309B1
		[__DynamicallyInvokable]
		public ObsoleteAttribute()
		{
			this._message = null;
			this._error = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a specified workaround message.</summary>
		/// <param name="message">The text string that describes alternative workarounds.</param>
		// Token: 0x060010BC RID: 4284 RVA: 0x000327C7 File Offset: 0x000309C7
		[__DynamicallyInvokable]
		public ObsoleteAttribute(string message)
		{
			this._message = message;
			this._error = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a workaround message and a Boolean value indicating whether the obsolete element usage is considered an error.</summary>
		/// <param name="message">The text string that describes alternative workarounds.</param>
		/// <param name="error">
		///   <see langword="true" /> if the obsolete element usage generates a compiler error; <see langword="false" /> if it generates a compiler warning.</param>
		// Token: 0x060010BD RID: 4285 RVA: 0x000327DD File Offset: 0x000309DD
		[__DynamicallyInvokable]
		public ObsoleteAttribute(string message, bool error)
		{
			this._message = message;
			this._error = error;
		}

		/// <summary>Gets the workaround message, including a description of the alternative program elements.</summary>
		/// <returns>The workaround text string.</returns>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x000327F3 File Offset: 0x000309F3
		[__DynamicallyInvokable]
		public string Message
		{
			[__DynamicallyInvokable]
			get
			{
				return this._message;
			}
		}

		/// <summary>Gets a Boolean value indicating whether the compiler will treat usage of the obsolete program element as an error.</summary>
		/// <returns>
		///   <see langword="true" /> if the obsolete element usage is considered an error; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x000327FB File Offset: 0x000309FB
		[__DynamicallyInvokable]
		public bool IsError
		{
			[__DynamicallyInvokable]
			get
			{
				return this._error;
			}
		}

		// Token: 0x040005CD RID: 1485
		private string _message;

		// Token: 0x040005CE RID: 1486
		private bool _error;
	}
}
