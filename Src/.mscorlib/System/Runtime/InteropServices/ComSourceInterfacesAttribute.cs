using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies a list of interfaces that are exposed as COM event sources for the attributed class.</summary>
	// Token: 0x0200091F RID: 2335
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ComSourceInterfacesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the name of the event source interface.</summary>
		/// <param name="sourceInterfaces">A null-delimited list of fully qualified event source interface names.</param>
		// Token: 0x06006025 RID: 24613 RVA: 0x0014CEDB File Offset: 0x0014B0DB
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(string sourceInterfaces)
		{
			this._val = sourceInterfaces;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the type to use as a source interface.</summary>
		/// <param name="sourceInterface">The <see cref="T:System.Type" /> of the source interface.</param>
		// Token: 0x06006026 RID: 24614 RVA: 0x0014CEEA File Offset: 0x0014B0EA
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface)
		{
			this._val = sourceInterface.FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface.</param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface.</param>
		// Token: 0x06006027 RID: 24615 RVA: 0x0014CEFE File Offset: 0x0014B0FE
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2)
		{
			this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName;
		}

		/// <summary>Initializes a new instance of the <see langword="ComSourceInterfacesAttribute" /> class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface.</param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface.</param>
		/// <param name="sourceInterface3">The <see cref="T:System.Type" /> of a source interface.</param>
		// Token: 0x06006028 RID: 24616 RVA: 0x0014CF24 File Offset: 0x0014B124
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3)
		{
			this._val = string.Concat(new string[] { sourceInterface1.FullName, "\0", sourceInterface2.FullName, "\0", sourceInterface3.FullName });
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface.</param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface.</param>
		/// <param name="sourceInterface3">The <see cref="T:System.Type" /> of a source interface.</param>
		/// <param name="sourceInterface4">The <see cref="T:System.Type" /> of a source interface.</param>
		// Token: 0x06006029 RID: 24617 RVA: 0x0014CF74 File Offset: 0x0014B174
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3, Type sourceInterface4)
		{
			this._val = string.Concat(new string[] { sourceInterface1.FullName, "\0", sourceInterface2.FullName, "\0", sourceInterface3.FullName, "\0", sourceInterface4.FullName });
		}

		/// <summary>Gets the fully qualified name of the event source interface.</summary>
		/// <returns>The fully qualified name of the event source interface.</returns>
		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x0600602A RID: 24618 RVA: 0x0014CFD5 File Offset: 0x0014B1D5
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A83 RID: 10883
		internal string _val;
	}
}
