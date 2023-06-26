using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the service end as a server-activated type object (single call or singleton).</summary>
	// Token: 0x020007C2 RID: 1986
	[ComVisible(true)]
	public class WellKnownServiceTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> class with the given type name, assembly name, object URI, and <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.</summary>
		/// <param name="typeName">The full type name of the server-activated service type.</param>
		/// <param name="assemblyName">The assembly name of the server-activated service type.</param>
		/// <param name="objectUri">The URI of the server-activated object.</param>
		/// <param name="mode">The <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the type, which defines how the object is activated.</param>
		// Token: 0x06005620 RID: 22048 RVA: 0x00132B34 File Offset: 0x00130D34
		public WellKnownServiceTypeEntry(string typeName, string assemblyName, string objectUri, WellKnownObjectMode mode)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (objectUri == null)
			{
				throw new ArgumentNullException("objectUri");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._objectUri = objectUri;
			this._mode = mode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> class with the given <see cref="T:System.Type" />, object URI, and <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the server-activated service type object.</param>
		/// <param name="objectUri">The URI of the server-activated type.</param>
		/// <param name="mode">The <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the type, which defines how the object is activated.</param>
		// Token: 0x06005621 RID: 22049 RVA: 0x00132B90 File Offset: 0x00130D90
		public WellKnownServiceTypeEntry(Type type, string objectUri, WellKnownObjectMode mode)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUri == null)
			{
				throw new ArgumentNullException("objectUri");
			}
			if (!(type is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			base.TypeName = type.FullName;
			base.AssemblyName = type.Module.Assembly.FullName;
			this._objectUri = objectUri;
			this._mode = mode;
		}

		/// <summary>Gets the URI of the well-known service type.</summary>
		/// <returns>The URI of the server-activated service type.</returns>
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06005622 RID: 22050 RVA: 0x00132C0D File Offset: 0x00130E0D
		public string ObjectUri
		{
			get
			{
				return this._objectUri;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated service type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated service type.</returns>
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06005623 RID: 22051 RVA: 0x00132C15 File Offset: 0x00130E15
		public WellKnownObjectMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the server-activated service type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the server-activated service type.</returns>
		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06005624 RID: 22052 RVA: 0x00132C20 File Offset: 0x00130E20
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeTypeHandle.GetTypeByName(base.TypeName + ", " + base.AssemblyName, ref stackCrawlMark);
			}
		}

		/// <summary>Gets or sets the context attributes for the server-activated service type.</summary>
		/// <returns>The context attributes for the server-activated service type.</returns>
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06005625 RID: 22053 RVA: 0x00132C4C File Offset: 0x00130E4C
		// (set) Token: 0x06005626 RID: 22054 RVA: 0x00132C54 File Offset: 0x00130E54
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

		/// <summary>Returns the type name, assembly name, object URI and the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type name, assembly name, object URI, and the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated type as a <see cref="T:System.String" />.</returns>
		// Token: 0x06005627 RID: 22055 RVA: 0x00132C60 File Offset: 0x00130E60
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"type='",
				base.TypeName,
				", ",
				base.AssemblyName,
				"'; objectUri=",
				this._objectUri,
				"; mode=",
				this._mode.ToString()
			});
		}

		// Token: 0x0400278D RID: 10125
		private string _objectUri;

		// Token: 0x0400278E RID: 10126
		private WellKnownObjectMode _mode;

		// Token: 0x0400278F RID: 10127
		private IContextAttribute[] _contextAttributes;
	}
}
