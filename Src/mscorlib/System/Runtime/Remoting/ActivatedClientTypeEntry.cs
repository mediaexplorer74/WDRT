using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the client end as a type that can be activated on the server.</summary>
	// Token: 0x020007BF RID: 1983
	[ComVisible(true)]
	public class ActivatedClientTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class with the given type name, assembly name, and application URL.</summary>
		/// <param name="typeName">The type name of the client activated type.</param>
		/// <param name="assemblyName">The assembly name of the client activated type.</param>
		/// <param name="appUrl">The URL of the application to activate the type in.</param>
		// Token: 0x0600560C RID: 22028 RVA: 0x00132754 File Offset: 0x00130954
		public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._appUrl = appUrl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class with the given <see cref="T:System.Type" /> and application URL.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the client activated type.</param>
		/// <param name="appUrl">The URL of the application to activate the type in.</param>
		// Token: 0x0600560D RID: 22029 RVA: 0x001327A8 File Offset: 0x001309A8
		public ActivatedClientTypeEntry(Type type, string appUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
			}
			base.TypeName = type.FullName;
			base.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
			this._appUrl = appUrl;
		}

		/// <summary>Gets the URL of the application to activate the type in.</summary>
		/// <returns>The URL of the application to activate the type in.</returns>
		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x0600560E RID: 22030 RVA: 0x00132821 File Offset: 0x00130A21
		public string ApplicationUrl
		{
			get
			{
				return this._appUrl;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the client-activated type.</summary>
		/// <returns>Gets the <see cref="T:System.Type" /> of the client-activated type.</returns>
		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x0600560F RID: 22031 RVA: 0x0013282C File Offset: 0x00130A2C
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeTypeHandle.GetTypeByName(base.TypeName + ", " + base.AssemblyName, ref stackCrawlMark);
			}
		}

		/// <summary>Gets or sets the context attributes for the client-activated type.</summary>
		/// <returns>The context attributes for the client activated type.</returns>
		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06005610 RID: 22032 RVA: 0x00132858 File Offset: 0x00130A58
		// (set) Token: 0x06005611 RID: 22033 RVA: 0x00132860 File Offset: 0x00130A60
		public IContextAttribute[] ContextAttributes
		{
			get
			{
				return this._contextAttributes;
			}
			set
			{
				this._contextAttributes = value;
			}
		}

		/// <summary>Returns the type name, assembly name, and application URL of the client-activated type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type name, assembly name, and application URL of the client-activated type as a <see cref="T:System.String" />.</returns>
		// Token: 0x06005612 RID: 22034 RVA: 0x00132869 File Offset: 0x00130A69
		public override string ToString()
		{
			return string.Concat(new string[] { "type='", base.TypeName, ", ", base.AssemblyName, "'; appUrl=", this._appUrl });
		}

		// Token: 0x04002788 RID: 10120
		private string _appUrl;

		// Token: 0x04002789 RID: 10121
		private IContextAttribute[] _contextAttributes;
	}
}
