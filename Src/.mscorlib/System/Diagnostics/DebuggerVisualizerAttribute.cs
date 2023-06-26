using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies that the type has a visualizer. This class cannot be inherited.</summary>
	// Token: 0x020003EE RID: 1006
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerVisualizerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		// Token: 0x0600333B RID: 13115 RVA: 0x000C656E File Offset: 0x000C476E
		public DebuggerVisualizerAttribute(string visualizerTypeName)
		{
			this.visualizerName = visualizerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer and the type name of the visualizer object source.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		/// <param name="visualizerObjectSourceTypeName">The fully qualified type name of the visualizer object source.</param>
		// Token: 0x0600333C RID: 13116 RVA: 0x000C657D File Offset: 0x000C477D
		public DebuggerVisualizerAttribute(string visualizerTypeName, string visualizerObjectSourceTypeName)
		{
			this.visualizerName = visualizerTypeName;
			this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer and the type of the visualizer object source.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		/// <param name="visualizerObjectSource">The type of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visualizerObjectSource" /> is <see langword="null" />.</exception>
		// Token: 0x0600333D RID: 13117 RVA: 0x000C6593 File Offset: 0x000C4793
		public DebuggerVisualizerAttribute(string visualizerTypeName, Type visualizerObjectSource)
		{
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizerTypeName;
			this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visualizer" /> is <see langword="null" />.</exception>
		// Token: 0x0600333E RID: 13118 RVA: 0x000C65C2 File Offset: 0x000C47C2
		public DebuggerVisualizerAttribute(Type visualizer)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer and the type of the visualizer object source.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <param name="visualizerObjectSource">The type of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visualizerObjectSource" /> is <see langword="null" />.</exception>
		// Token: 0x0600333F RID: 13119 RVA: 0x000C65EC File Offset: 0x000C47EC
		public DebuggerVisualizerAttribute(Type visualizer, Type visualizerObjectSource)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer and the type name of the visualizer object source.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <param name="visualizerObjectSourceTypeName">The fully qualified type name of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visualizer" /> is <see langword="null" />.</exception>
		// Token: 0x06003340 RID: 13120 RVA: 0x000C663F File Offset: 0x000C483F
		public DebuggerVisualizerAttribute(Type visualizer, string visualizerObjectSourceTypeName)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
		}

		/// <summary>Gets the fully qualified type name of the visualizer object source.</summary>
		/// <returns>The fully qualified type name of the visualizer object source.</returns>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000C666E File Offset: 0x000C486E
		public string VisualizerObjectSourceTypeName
		{
			get
			{
				return this.visualizerObjectSourceName;
			}
		}

		/// <summary>Gets the fully qualified type name of the visualizer.</summary>
		/// <returns>The fully qualified visualizer type name.</returns>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000C6676 File Offset: 0x000C4876
		public string VisualizerTypeName
		{
			get
			{
				return this.visualizerName;
			}
		}

		/// <summary>Gets or sets the description of the visualizer.</summary>
		/// <returns>The description of the visualizer.</returns>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000C667E File Offset: 0x000C487E
		// (set) Token: 0x06003344 RID: 13124 RVA: 0x000C6686 File Offset: 0x000C4886
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets the target type when the attribute is applied at the assembly level.</summary>
		/// <returns>The type that is the target of the visualizer.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value cannot be set because it is <see langword="null" />.</exception>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x000C66B8 File Offset: 0x000C48B8
		// (set) Token: 0x06003345 RID: 13125 RVA: 0x000C668F File Offset: 0x000C488F
		public Type Target
		{
			get
			{
				return this.target;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.targetName = value.AssemblyQualifiedName;
				this.target = value;
			}
		}

		/// <summary>Gets or sets the fully qualified type name when the attribute is applied at the assembly level.</summary>
		/// <returns>The fully qualified type name of the target type.</returns>
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x000C66C9 File Offset: 0x000C48C9
		// (set) Token: 0x06003347 RID: 13127 RVA: 0x000C66C0 File Offset: 0x000C48C0
		public string TargetTypeName
		{
			get
			{
				return this.targetName;
			}
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x040016B8 RID: 5816
		private string visualizerObjectSourceName;

		// Token: 0x040016B9 RID: 5817
		private string visualizerName;

		// Token: 0x040016BA RID: 5818
		private string description;

		// Token: 0x040016BB RID: 5819
		private string targetName;

		// Token: 0x040016BC RID: 5820
		private Type target;
	}
}
