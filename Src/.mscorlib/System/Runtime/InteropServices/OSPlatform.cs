using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents an operating system platform.</summary>
	// Token: 0x020009A8 RID: 2472
	public struct OSPlatform : IEquatable<OSPlatform>
	{
		/// <summary>Gets an object that represents the Linux operating system.</summary>
		/// <returns>An object that represents the Linux operating system.</returns>
		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x00152EF4 File Offset: 0x001510F4
		public static OSPlatform Linux { get; } = new OSPlatform("LINUX");

		/// <summary>Gets an object that represents the OSX operating system.</summary>
		/// <returns>An object that represents the OSX operating system.</returns>
		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06006313 RID: 25363 RVA: 0x00152EFB File Offset: 0x001510FB
		public static OSPlatform OSX { get; } = new OSPlatform("OSX");

		/// <summary>Gets an object that represents the Windows operating system.</summary>
		/// <returns>An object that represents the Windows operating system.</returns>
		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06006314 RID: 25364 RVA: 0x00152F02 File Offset: 0x00151102
		public static OSPlatform Windows { get; } = new OSPlatform("WINDOWS");

		// Token: 0x06006315 RID: 25365 RVA: 0x00152F09 File Offset: 0x00151109
		private OSPlatform(string osPlatform)
		{
			if (osPlatform == null)
			{
				throw new ArgumentNullException("osPlatform");
			}
			if (osPlatform.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyValue"), "osPlatform");
			}
			this._osPlatform = osPlatform;
		}

		/// <summary>Creates a new <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance.</summary>
		/// <param name="osPlatform">The name of the platform that this instance represents.</param>
		/// <returns>An object that represents the <paramref name="osPlatform" /> operating system.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="osPlatform" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="osPlatform" /> is <see langword="null" />.</exception>
		// Token: 0x06006316 RID: 25366 RVA: 0x00152F3D File Offset: 0x0015113D
		public static OSPlatform Create(string osPlatform)
		{
			return new OSPlatform(osPlatform);
		}

		/// <summary>Determines whether the current instance and the specified <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance are equal.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="other" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006317 RID: 25367 RVA: 0x00152F45 File Offset: 0x00151145
		public bool Equals(OSPlatform other)
		{
			return this.Equals(other._osPlatform);
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x00152F53 File Offset: 0x00151153
		internal bool Equals(string other)
		{
			return string.Equals(this._osPlatform, other, StringComparison.Ordinal);
		}

		/// <summary>Determines whether the current <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance is equal to the specified object.</summary>
		/// <param name="obj">
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance and its name is the same as the current object; otherwise, false.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance and its name is the same as the current object.</returns>
		// Token: 0x06006319 RID: 25369 RVA: 0x00152F62 File Offset: 0x00151162
		public override bool Equals(object obj)
		{
			return obj is OSPlatform && this.Equals((OSPlatform)obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x0600631A RID: 25370 RVA: 0x00152F7A File Offset: 0x0015117A
		public override int GetHashCode()
		{
			if (this._osPlatform != null)
			{
				return this._osPlatform.GetHashCode();
			}
			return 0;
		}

		/// <summary>Returns the string representation of this <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance.</summary>
		/// <returns>A string that represents this <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance.</returns>
		// Token: 0x0600631B RID: 25371 RVA: 0x00152F91 File Offset: 0x00151191
		public override string ToString()
		{
			return this._osPlatform ?? string.Empty;
		}

		/// <summary>Determines whether two <see cref="T:System.Runtime.InteropServices.OSPlatform" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600631C RID: 25372 RVA: 0x00152FA2 File Offset: 0x001511A2
		public static bool operator ==(OSPlatform left, OSPlatform right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instances are unequal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are unequal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600631D RID: 25373 RVA: 0x00152FAC File Offset: 0x001511AC
		public static bool operator !=(OSPlatform left, OSPlatform right)
		{
			return !(left == right);
		}

		// Token: 0x04002CB2 RID: 11442
		private readonly string _osPlatform;
	}
}
