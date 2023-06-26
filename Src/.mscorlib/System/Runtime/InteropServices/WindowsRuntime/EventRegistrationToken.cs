using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>A token that is returned when an event handler is added to a Windows Runtime event. The token is used to remove the event handler from the event at a later time.</summary>
	// Token: 0x020009DE RID: 2526
	[__DynamicallyInvokable]
	public struct EventRegistrationToken
	{
		// Token: 0x06006487 RID: 25735 RVA: 0x00157DC7 File Offset: 0x00155FC7
		internal EventRegistrationToken(ulong value)
		{
			this.m_value = value;
		}

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06006488 RID: 25736 RVA: 0x00157DD0 File Offset: 0x00155FD0
		internal ulong Value
		{
			get
			{
				return this.m_value;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken" /> instances are equal.</summary>
		/// <param name="left">The first instance to compare.</param>
		/// <param name="right">The second instance to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the two objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006489 RID: 25737 RVA: 0x00157DD8 File Offset: 0x00155FD8
		[__DynamicallyInvokable]
		public static bool operator ==(EventRegistrationToken left, EventRegistrationToken right)
		{
			return left.Equals(right);
		}

		/// <summary>Indicates whether two <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken" /> instances are not equal.</summary>
		/// <param name="left">The first instance to compare.</param>
		/// <param name="right">The second instance to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the two instances are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600648A RID: 25738 RVA: 0x00157DED File Offset: 0x00155FED
		[__DynamicallyInvokable]
		public static bool operator !=(EventRegistrationToken left, EventRegistrationToken right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether the current object is equal to the specified object.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to <paramref name="obj" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600648B RID: 25739 RVA: 0x00157E08 File Offset: 0x00156008
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is EventRegistrationToken && ((EventRegistrationToken)obj).Value == this.Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x0600648C RID: 25740 RVA: 0x00157E35 File Offset: 0x00156035
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		// Token: 0x04002CF7 RID: 11511
		internal ulong m_value;
	}
}
