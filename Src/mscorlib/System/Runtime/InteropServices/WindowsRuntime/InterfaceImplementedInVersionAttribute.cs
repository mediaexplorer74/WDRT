using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Specifies the version of the target type that first implemented the specified interface.</summary>
	// Token: 0x020009C6 RID: 2502
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
	[__DynamicallyInvokable]
	public sealed class InterfaceImplementedInVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.InterfaceImplementedInVersionAttribute" /> class, specifying the interface that the target type implements and the version in which that interface was first implemented.</summary>
		/// <param name="interfaceType">The interface that was first implemented in the specified version of the target type.</param>
		/// <param name="majorVersion">The major component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		/// <param name="minorVersion">The minor component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		/// <param name="buildVersion">The build component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		/// <param name="revisionVersion">The revision component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		// Token: 0x060063D9 RID: 25561 RVA: 0x00156040 File Offset: 0x00154240
		[__DynamicallyInvokable]
		public InterfaceImplementedInVersionAttribute(Type interfaceType, byte majorVersion, byte minorVersion, byte buildVersion, byte revisionVersion)
		{
			this.m_interfaceType = interfaceType;
			this.m_majorVersion = majorVersion;
			this.m_minorVersion = minorVersion;
			this.m_buildVersion = buildVersion;
			this.m_revisionVersion = revisionVersion;
		}

		/// <summary>Gets the type of the interface that the target type implements.</summary>
		/// <returns>The type of the interface.</returns>
		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x060063DA RID: 25562 RVA: 0x0015606D File Offset: 0x0015426D
		[__DynamicallyInvokable]
		public Type InterfaceType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_interfaceType;
			}
		}

		/// <summary>Gets the major component of the version of the target type that first implemented the interface.</summary>
		/// <returns>The major component of the version.</returns>
		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x060063DB RID: 25563 RVA: 0x00156075 File Offset: 0x00154275
		[__DynamicallyInvokable]
		public byte MajorVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_majorVersion;
			}
		}

		/// <summary>Gets the minor component of the version of the target type that first implemented the interface.</summary>
		/// <returns>The minor component of the version.</returns>
		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x060063DC RID: 25564 RVA: 0x0015607D File Offset: 0x0015427D
		[__DynamicallyInvokable]
		public byte MinorVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_minorVersion;
			}
		}

		/// <summary>Gets the build component of the version of the target type that first implemented the interface.</summary>
		/// <returns>The build component of the version.</returns>
		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x060063DD RID: 25565 RVA: 0x00156085 File Offset: 0x00154285
		[__DynamicallyInvokable]
		public byte BuildVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_buildVersion;
			}
		}

		/// <summary>Gets the revision component of the version of the target type that first implemented the interface.</summary>
		/// <returns>The revision component of the version.</returns>
		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x060063DE RID: 25566 RVA: 0x0015608D File Offset: 0x0015428D
		[__DynamicallyInvokable]
		public byte RevisionVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_revisionVersion;
			}
		}

		// Token: 0x04002CE3 RID: 11491
		private Type m_interfaceType;

		// Token: 0x04002CE4 RID: 11492
		private byte m_majorVersion;

		// Token: 0x04002CE5 RID: 11493
		private byte m_minorVersion;

		// Token: 0x04002CE6 RID: 11494
		private byte m_buildVersion;

		// Token: 0x04002CE7 RID: 11495
		private byte m_revisionVersion;
	}
}
