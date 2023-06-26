using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Drawing.Design
{
	/// <summary>Provides a base implementation of a toolbox item.</summary>
	// Token: 0x0200007F RID: 127
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[Serializable]
	public class ToolboxItem : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItem" /> class.</summary>
		// Token: 0x06000875 RID: 2165 RVA: 0x00020E39 File Offset: 0x0001F039
		public ToolboxItem()
		{
			if (!ToolboxItem.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					ToolboxItem.iconWidth = DpiHelper.LogicalToDeviceUnitsX(16);
					ToolboxItem.iconHeight = DpiHelper.LogicalToDeviceUnitsY(16);
				}
				ToolboxItem.isScalingInitialized = true;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItem" /> class that creates the specified type of component.</summary>
		/// <param name="toolType">The type of <see cref="T:System.ComponentModel.IComponent" /> that the toolbox item creates.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> was locked.</exception>
		// Token: 0x06000876 RID: 2166 RVA: 0x00020E6D File Offset: 0x0001F06D
		public ToolboxItem(Type toolType)
			: this()
		{
			this.Initialize(toolType);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00020E7C File Offset: 0x0001F07C
		private ToolboxItem(SerializationInfo info, StreamingContext context)
			: this()
		{
			this.Deserialize(info, context);
		}

		/// <summary>Gets or sets the name of the assembly that contains the type or types that the toolbox item creates.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly containing the type or types to create.</returns>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x00020E8C File Offset: 0x0001F08C
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x00020EA3 File Offset: 0x0001F0A3
		public AssemblyName AssemblyName
		{
			get
			{
				return (AssemblyName)this.Properties["AssemblyName"];
			}
			set
			{
				this.Properties["AssemblyName"] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Reflection.AssemblyName" /> for the toolbox item.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.AssemblyName" /> objects.</returns>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x00020EB8 File Offset: 0x0001F0B8
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x00020EEB File Offset: 0x0001F0EB
		public AssemblyName[] DependentAssemblies
		{
			get
			{
				AssemblyName[] array = (AssemblyName[])this.Properties["DependentAssemblies"];
				if (array != null)
				{
					return (AssemblyName[])array.Clone();
				}
				return null;
			}
			set
			{
				this.Properties["DependentAssemblies"] = value.Clone();
			}
		}

		/// <summary>Gets or sets a bitmap to represent the toolbox item in the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Bitmap" /> that represents the toolbox item in the toolbox.</returns>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00020F03 File Offset: 0x0001F103
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x00020F1A File Offset: 0x0001F11A
		public Bitmap Bitmap
		{
			get
			{
				return (Bitmap)this.Properties["Bitmap"];
			}
			set
			{
				this.Properties["Bitmap"] = value;
			}
		}

		/// <summary>Gets or sets the original bitmap that will be used in the toolbox for this item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Bitmap" /> that represents the toolbox item in the toolbox.</returns>
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00020F2D File Offset: 0x0001F12D
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x00020F44 File Offset: 0x0001F144
		public Bitmap OriginalBitmap
		{
			get
			{
				return (Bitmap)this.Properties["OriginalBitmap"];
			}
			set
			{
				this.Properties["OriginalBitmap"] = value;
			}
		}

		/// <summary>Gets or sets the company name for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the company for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00020F57 File Offset: 0x0001F157
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x00020F6E File Offset: 0x0001F16E
		public string Company
		{
			get
			{
				return (string)this.Properties["Company"];
			}
			set
			{
				this.Properties["Company"] = value;
			}
		}

		/// <summary>Gets the component type for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the component type for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00020F81 File Offset: 0x0001F181
		public virtual string ComponentType
		{
			get
			{
				return SR.GetString("DotNET_ComponentType");
			}
		}

		/// <summary>Gets or sets the description for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the description for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00020F8D File Offset: 0x0001F18D
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		public string Description
		{
			get
			{
				return (string)this.Properties["Description"];
			}
			set
			{
				this.Properties["Description"] = value;
			}
		}

		/// <summary>Gets or sets the display name for the toolbox item.</summary>
		/// <returns>The display name for the toolbox item.</returns>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00020FB7 File Offset: 0x0001F1B7
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x00020FCE File Offset: 0x0001F1CE
		public string DisplayName
		{
			get
			{
				return (string)this.Properties["DisplayName"];
			}
			set
			{
				this.Properties["DisplayName"] = value;
			}
		}

		/// <summary>Gets or sets the filter that determines whether the toolbox item can be used on a destination component.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects.</returns>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00020FE1 File Offset: 0x0001F1E1
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x00020FF8 File Offset: 0x0001F1F8
		public ICollection Filter
		{
			get
			{
				return (ICollection)this.Properties["Filter"];
			}
			set
			{
				this.Properties["Filter"] = value;
			}
		}

		/// <summary>Gets a value indicating whether the toolbox item is transient.</summary>
		/// <returns>
		///   <see langword="true" />, if this toolbox item should not be stored in any toolbox database when an application that is providing a toolbox closes; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0002100B File Offset: 0x0001F20B
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x00021022 File Offset: 0x0001F222
		public bool IsTransient
		{
			get
			{
				return (bool)this.Properties["IsTransient"];
			}
			set
			{
				this.Properties["IsTransient"] = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Drawing.Design.ToolboxItem" /> is currently locked.</summary>
		/// <returns>
		///   <see langword="true" /> if the toolbox item is locked; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0002103A File Offset: 0x0001F23A
		public virtual bool Locked
		{
			get
			{
				return this.locked;
			}
		}

		/// <summary>Gets a dictionary of properties.</summary>
		/// <returns>A dictionary of name/value pairs (the names are property names and the values are property values).</returns>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00021042 File Offset: 0x0001F242
		public IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ToolboxItem.LockableDictionary(this, 8);
				}
				return this.properties;
			}
		}

		/// <summary>Gets or sets the fully qualified name of the type of <see cref="T:System.ComponentModel.IComponent" /> that the toolbox item creates when invoked.</summary>
		/// <returns>The fully qualified type name of the type of component that this toolbox item creates.</returns>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0002105F File Offset: 0x0001F25F
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x00021076 File Offset: 0x0001F276
		public string TypeName
		{
			get
			{
				return (string)this.Properties["TypeName"];
			}
			set
			{
				this.Properties["TypeName"] = value;
			}
		}

		/// <summary>Gets the version for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the version for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00021089 File Offset: 0x0001F289
		public virtual string Version
		{
			get
			{
				if (this.AssemblyName != null)
				{
					return this.AssemblyName.Version.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Occurs immediately after components are created.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000890 RID: 2192 RVA: 0x000210A9 File Offset: 0x0001F2A9
		// (remove) Token: 0x06000891 RID: 2193 RVA: 0x000210C2 File Offset: 0x0001F2C2
		public event ToolboxComponentsCreatedEventHandler ComponentsCreated
		{
			add
			{
				this.componentsCreatedEvent = (ToolboxComponentsCreatedEventHandler)Delegate.Combine(this.componentsCreatedEvent, value);
			}
			remove
			{
				this.componentsCreatedEvent = (ToolboxComponentsCreatedEventHandler)Delegate.Remove(this.componentsCreatedEvent, value);
			}
		}

		/// <summary>Occurs when components are about to be created.</summary>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000892 RID: 2194 RVA: 0x000210DB File Offset: 0x0001F2DB
		// (remove) Token: 0x06000893 RID: 2195 RVA: 0x000210F4 File Offset: 0x0001F2F4
		public event ToolboxComponentsCreatingEventHandler ComponentsCreating
		{
			add
			{
				this.componentsCreatingEvent = (ToolboxComponentsCreatingEventHandler)Delegate.Combine(this.componentsCreatingEvent, value);
			}
			remove
			{
				this.componentsCreatingEvent = (ToolboxComponentsCreatingEventHandler)Delegate.Remove(this.componentsCreatingEvent, value);
			}
		}

		/// <summary>Throws an exception if the toolbox item is currently locked.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> is locked.</exception>
		// Token: 0x06000894 RID: 2196 RVA: 0x0002110D File Offset: 0x0001F30D
		protected void CheckUnlocked()
		{
			if (this.Locked)
			{
				throw new InvalidOperationException(SR.GetString("ToolboxItemLocked"));
			}
		}

		/// <summary>Creates the components that the toolbox item is configured to create.</summary>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		// Token: 0x06000895 RID: 2197 RVA: 0x00021127 File Offset: 0x0001F327
		public IComponent[] CreateComponents()
		{
			return this.CreateComponents(null);
		}

		/// <summary>Creates the components that the toolbox item is configured to create, using the specified designer host.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to use when creating the components.</param>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		// Token: 0x06000896 RID: 2198 RVA: 0x00021130 File Offset: 0x0001F330
		public IComponent[] CreateComponents(IDesignerHost host)
		{
			this.OnComponentsCreating(new ToolboxComponentsCreatingEventArgs(host));
			IComponent[] array = this.CreateComponentsCore(host, new Hashtable());
			if (array != null && array.Length != 0)
			{
				this.OnComponentsCreated(new ToolboxComponentsCreatedEventArgs(array));
			}
			return array;
		}

		/// <summary>Creates the components that the toolbox item is configured to create, using the specified designer host and default values.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to use when creating the components.</param>
		/// <param name="defaultValues">A dictionary of property name/value pairs of default values with which to initialize the component.</param>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		// Token: 0x06000897 RID: 2199 RVA: 0x0002116C File Offset: 0x0001F36C
		public IComponent[] CreateComponents(IDesignerHost host, IDictionary defaultValues)
		{
			this.OnComponentsCreating(new ToolboxComponentsCreatingEventArgs(host));
			IComponent[] array = this.CreateComponentsCore(host, defaultValues);
			if (array != null && array.Length != 0)
			{
				this.OnComponentsCreated(new ToolboxComponentsCreatedEventArgs(array));
			}
			return array;
		}

		/// <summary>Creates a component or an array of components when the toolbox item is invoked.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to host the toolbox item.</param>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		// Token: 0x06000898 RID: 2200 RVA: 0x000211A4 File Offset: 0x0001F3A4
		protected virtual IComponent[] CreateComponentsCore(IDesignerHost host)
		{
			ArrayList arrayList = new ArrayList();
			Type type = this.GetType(host, this.AssemblyName, this.TypeName, true);
			if (type != null)
			{
				if (host != null)
				{
					arrayList.Add(host.CreateComponent(type));
				}
				else if (typeof(IComponent).IsAssignableFrom(type))
				{
					arrayList.Add(TypeDescriptor.CreateInstance(null, type, null, null));
				}
			}
			IComponent[] array = new IComponent[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		/// <summary>Creates an array of components when the toolbox item is invoked.</summary>
		/// <param name="host">The designer host to use when creating components.</param>
		/// <param name="defaultValues">A dictionary of property name/value pairs of default values with which to initialize the component.</param>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		// Token: 0x06000899 RID: 2201 RVA: 0x00021220 File Offset: 0x0001F420
		protected virtual IComponent[] CreateComponentsCore(IDesignerHost host, IDictionary defaultValues)
		{
			IComponent[] array = this.CreateComponentsCore(host);
			if (host != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					IComponentInitializer componentInitializer = host.GetDesigner(array[i]) as IComponentInitializer;
					if (componentInitializer != null)
					{
						bool flag = true;
						try
						{
							componentInitializer.InitializeNewComponent(defaultValues);
							flag = false;
						}
						finally
						{
							if (flag)
							{
								for (int j = 0; j < array.Length; j++)
								{
									host.DestroyComponent(array[j]);
								}
							}
						}
					}
				}
			}
			return array;
		}

		/// <summary>Loads the state of the toolbox item from the specified serialization information object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to load from.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the stream characteristics.</param>
		// Token: 0x0600089A RID: 2202 RVA: 0x00021298 File Offset: 0x0001F498
		protected virtual void Deserialize(SerializationInfo info, StreamingContext context)
		{
			string[] array = null;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name.Equals("PropertyNames"))
				{
					array = serializationEntry.Value as string[];
					break;
				}
			}
			if (array == null)
			{
				array = new string[] { "AssemblyName", "Bitmap", "DisplayName", "Filter", "IsTransient", "TypeName" };
			}
			foreach (SerializationEntry serializationEntry2 in info)
			{
				foreach (string text in array)
				{
					if (text.Equals(serializationEntry2.Name))
					{
						this.Properties[serializationEntry2.Name] = serializationEntry2.Value;
						break;
					}
				}
			}
			bool boolean = info.GetBoolean("Locked");
			if (boolean)
			{
				this.Lock();
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00021394 File Offset: 0x0001F594
		private static bool AreAssemblyNamesEqual(AssemblyName name1, AssemblyName name2)
		{
			return name1 == name2 || (name1 != null && name2 != null && name1.FullName == name2.FullName);
		}

		/// <summary>Determines whether two <see cref="T:System.Drawing.Design.ToolboxItem" /> instances are equal.</summary>
		/// <param name="obj">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to compare with the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Drawing.Design.ToolboxItem" /> is equal to the current <see cref="T:System.Drawing.Design.ToolboxItem" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600089C RID: 2204 RVA: 0x000213B8 File Offset: 0x0001F5B8
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (!(obj.GetType() == base.GetType()))
			{
				return false;
			}
			ToolboxItem toolboxItem = (ToolboxItem)obj;
			return this.TypeName == toolboxItem.TypeName && ToolboxItem.AreAssemblyNamesEqual(this.AssemblyName, toolboxItem.AssemblyName) && this.DisplayName == toolboxItem.DisplayName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x0600089D RID: 2205 RVA: 0x00021428 File Offset: 0x0001F628
		public override int GetHashCode()
		{
			string typeName = this.TypeName;
			int num = ((typeName != null) ? typeName.GetHashCode() : 0);
			return num ^ this.DisplayName.GetHashCode();
		}

		/// <summary>Filters a property value before returning it.</summary>
		/// <param name="propertyName">The name of the property to filter.</param>
		/// <param name="value">The value against which to filter the property.</param>
		/// <returns>A filtered property value.</returns>
		// Token: 0x0600089E RID: 2206 RVA: 0x00021458 File Offset: 0x0001F658
		protected virtual object FilterPropertyValue(string propertyName, object value)
		{
			if (!(propertyName == "AssemblyName"))
			{
				if (!(propertyName == "DisplayName") && !(propertyName == "TypeName"))
				{
					if (!(propertyName == "Filter"))
					{
						if (propertyName == "IsTransient")
						{
							if (value == null)
							{
								value = false;
							}
						}
					}
					else if (value == null)
					{
						value = new ToolboxItemFilterAttribute[0];
					}
				}
				else if (value == null)
				{
					value = string.Empty;
				}
			}
			else if (value != null)
			{
				value = ((AssemblyName)value).Clone();
			}
			return value;
		}

		/// <summary>Enables access to the type associated with the toolbox item.</summary>
		/// <param name="host">The designer host to query for <see cref="T:System.ComponentModel.Design.ITypeResolutionService" />.</param>
		/// <returns>The type associated with the toolbox item.</returns>
		// Token: 0x0600089F RID: 2207 RVA: 0x000214DF File Offset: 0x0001F6DF
		public Type GetType(IDesignerHost host)
		{
			return this.GetType(host, this.AssemblyName, this.TypeName, false);
		}

		/// <summary>Creates an instance of the specified type, optionally using a specified designer host and assembly name.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current document. This can be <see langword="null" />.</param>
		/// <param name="assemblyName">An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly that contains the type to load. This can be <see langword="null" />.</param>
		/// <param name="typeName">The name of the type to create an instance of.</param>
		/// <param name="reference">A value indicating whether or not to add a reference to the assembly that contains the specified type to the designer host's set of references.</param>
		/// <returns>An instance of the specified type, if it can be located.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is not specified.</exception>
		// Token: 0x060008A0 RID: 2208 RVA: 0x000214F8 File Offset: 0x0001F6F8
		protected virtual Type GetType(IDesignerHost host, AssemblyName assemblyName, string typeName, bool reference)
		{
			ITypeResolutionService typeResolutionService = null;
			Type type = null;
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (host != null)
			{
				typeResolutionService = (ITypeResolutionService)host.GetService(typeof(ITypeResolutionService));
			}
			if (typeResolutionService != null)
			{
				if (reference)
				{
					if (assemblyName != null)
					{
						typeResolutionService.ReferenceAssembly(assemblyName);
						type = typeResolutionService.GetType(typeName);
					}
					else
					{
						type = typeResolutionService.GetType(typeName);
						if (type == null)
						{
							type = Type.GetType(typeName);
						}
						if (type != null)
						{
							typeResolutionService.ReferenceAssembly(type.Assembly.GetName());
						}
					}
				}
				else
				{
					if (assemblyName != null)
					{
						Assembly assembly = typeResolutionService.GetAssembly(assemblyName);
						if (assembly != null)
						{
							type = assembly.GetType(typeName);
						}
					}
					if (type == null)
					{
						type = typeResolutionService.GetType(typeName);
					}
				}
			}
			else if (!string.IsNullOrEmpty(typeName))
			{
				if (assemblyName != null)
				{
					Assembly assembly2 = null;
					try
					{
						assembly2 = Assembly.Load(assemblyName);
					}
					catch (FileNotFoundException)
					{
					}
					catch (BadImageFormatException)
					{
					}
					catch (IOException)
					{
					}
					if (assembly2 == null && assemblyName.CodeBase != null && assemblyName.CodeBase.Length > 0)
					{
						try
						{
							assembly2 = Assembly.LoadFrom(assemblyName.CodeBase);
						}
						catch (FileNotFoundException)
						{
						}
						catch (BadImageFormatException)
						{
						}
						catch (IOException)
						{
						}
					}
					if (assembly2 != null)
					{
						type = assembly2.GetType(typeName);
					}
				}
				if (type == null)
				{
					type = Type.GetType(typeName, false);
				}
			}
			return type;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00021684 File Offset: 0x0001F884
		private AssemblyName GetNonRetargetedAssemblyName(Type type, AssemblyName policiedAssemblyName)
		{
			if (type == null || policiedAssemblyName == null)
			{
				return null;
			}
			if (type.Assembly.FullName == policiedAssemblyName.FullName)
			{
				return policiedAssemblyName;
			}
			foreach (AssemblyName assemblyName in type.Assembly.GetReferencedAssemblies())
			{
				if (assemblyName.FullName == policiedAssemblyName.FullName)
				{
					return assemblyName;
				}
			}
			foreach (AssemblyName assemblyName2 in type.Assembly.GetReferencedAssemblies())
			{
				if (assemblyName2.Name == policiedAssemblyName.Name)
				{
					return assemblyName2;
				}
			}
			foreach (AssemblyName assemblyName3 in type.Assembly.GetReferencedAssemblies())
			{
				try
				{
					Assembly assembly = Assembly.Load(assemblyName3);
					if (assembly != null && assembly.FullName == policiedAssemblyName.FullName)
					{
						return assemblyName3;
					}
				}
				catch
				{
				}
			}
			return null;
		}

		/// <summary>Initializes the current toolbox item with the specified type to create.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> that the toolbox item creates.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> was locked.</exception>
		// Token: 0x060008A2 RID: 2210 RVA: 0x00021798 File Offset: 0x0001F998
		public virtual void Initialize(Type type)
		{
			this.CheckUnlocked();
			if (type != null)
			{
				this.TypeName = type.FullName;
				AssemblyName name = type.Assembly.GetName(true);
				if (type.Assembly.GlobalAssemblyCache)
				{
					name.CodeBase = null;
				}
				Dictionary<string, AssemblyName> dictionary = new Dictionary<string, AssemblyName>();
				Type type2 = type;
				while (type2 != null)
				{
					AssemblyName name2 = type2.Assembly.GetName(true);
					AssemblyName nonRetargetedAssemblyName = this.GetNonRetargetedAssemblyName(type, name2);
					if (nonRetargetedAssemblyName != null && !dictionary.ContainsKey(nonRetargetedAssemblyName.FullName))
					{
						dictionary[nonRetargetedAssemblyName.FullName] = nonRetargetedAssemblyName;
					}
					type2 = type2.BaseType;
				}
				AssemblyName[] array = new AssemblyName[dictionary.Count];
				int num = 0;
				foreach (AssemblyName assemblyName in dictionary.Values)
				{
					array[num++] = assemblyName;
				}
				this.DependentAssemblies = array;
				this.AssemblyName = name;
				this.DisplayName = type.Name;
				if (!type.Assembly.ReflectionOnly)
				{
					object[] customAttributes = type.Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
					if (customAttributes != null && customAttributes.Length != 0)
					{
						AssemblyCompanyAttribute assemblyCompanyAttribute = customAttributes[0] as AssemblyCompanyAttribute;
						if (assemblyCompanyAttribute != null && assemblyCompanyAttribute.Company != null)
						{
							this.Company = assemblyCompanyAttribute.Company;
						}
					}
					DescriptionAttribute descriptionAttribute = (DescriptionAttribute)TypeDescriptor.GetAttributes(type)[typeof(DescriptionAttribute)];
					if (descriptionAttribute != null)
					{
						this.Description = descriptionAttribute.Description;
					}
					ToolboxBitmapAttribute toolboxBitmapAttribute = (ToolboxBitmapAttribute)TypeDescriptor.GetAttributes(type)[typeof(ToolboxBitmapAttribute)];
					if (toolboxBitmapAttribute != null)
					{
						Bitmap bitmap = toolboxBitmapAttribute.GetImage(type, false) as Bitmap;
						if (bitmap != null)
						{
							this.OriginalBitmap = toolboxBitmapAttribute.GetOriginalBitmap();
							if (bitmap.Width != ToolboxItem.iconWidth || bitmap.Height != ToolboxItem.iconHeight)
							{
								bitmap = new Bitmap(bitmap, new Size(ToolboxItem.iconWidth, ToolboxItem.iconHeight));
							}
						}
						this.Bitmap = bitmap;
					}
					bool flag = false;
					ArrayList arrayList = new ArrayList();
					foreach (object obj in TypeDescriptor.GetAttributes(type))
					{
						Attribute attribute = (Attribute)obj;
						ToolboxItemFilterAttribute toolboxItemFilterAttribute = attribute as ToolboxItemFilterAttribute;
						if (toolboxItemFilterAttribute != null)
						{
							if (toolboxItemFilterAttribute.FilterString.Equals(this.TypeName))
							{
								flag = true;
							}
							arrayList.Add(toolboxItemFilterAttribute);
						}
					}
					if (!flag)
					{
						arrayList.Add(new ToolboxItemFilterAttribute(this.TypeName));
					}
					this.Filter = (ToolboxItemFilterAttribute[])arrayList.ToArray(typeof(ToolboxItemFilterAttribute));
				}
			}
		}

		/// <summary>Locks the toolbox item and prevents changes to its properties.</summary>
		// Token: 0x060008A3 RID: 2211 RVA: 0x00021A64 File Offset: 0x0001FC64
		public virtual void Lock()
		{
			this.locked = true;
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreated" /> event.</summary>
		/// <param name="args">A <see cref="T:System.Drawing.Design.ToolboxComponentsCreatedEventArgs" /> that provides data for the event.</param>
		// Token: 0x060008A4 RID: 2212 RVA: 0x00021A6D File Offset: 0x0001FC6D
		protected virtual void OnComponentsCreated(ToolboxComponentsCreatedEventArgs args)
		{
			if (this.componentsCreatedEvent != null)
			{
				this.componentsCreatedEvent(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreating" /> event.</summary>
		/// <param name="args">A <see cref="T:System.Drawing.Design.ToolboxComponentsCreatingEventArgs" /> that provides data for the event.</param>
		// Token: 0x060008A5 RID: 2213 RVA: 0x00021A84 File Offset: 0x0001FC84
		protected virtual void OnComponentsCreating(ToolboxComponentsCreatingEventArgs args)
		{
			if (this.componentsCreatingEvent != null)
			{
				this.componentsCreatingEvent(this, args);
			}
		}

		/// <summary>Saves the state of the toolbox item to the specified serialization information object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to save to.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the stream characteristics.</param>
		// Token: 0x060008A6 RID: 2214 RVA: 0x00021A9C File Offset: 0x0001FC9C
		protected virtual void Serialize(SerializationInfo info, StreamingContext context)
		{
			bool traceVerbose = ToolboxItem.ToolboxItemPersist.TraceVerbose;
			info.AddValue("Locked", this.Locked);
			ArrayList arrayList = new ArrayList(this.Properties.Count);
			foreach (object obj in this.Properties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				arrayList.Add(dictionaryEntry.Key);
				info.AddValue((string)dictionaryEntry.Key, dictionaryEntry.Value);
			}
			info.AddValue("PropertyNames", (string[])arrayList.ToArray(typeof(string)));
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x060008A7 RID: 2215 RVA: 0x00021B64 File Offset: 0x0001FD64
		public override string ToString()
		{
			return this.DisplayName;
		}

		/// <summary>Validates that an object is of a given type.</summary>
		/// <param name="propertyName">The name of the property to validate.</param>
		/// <param name="value">Optional value against which to validate.</param>
		/// <param name="expectedType">The expected type of the property.</param>
		/// <param name="allowNull">
		///   <see langword="true" /> to allow <see langword="null" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />, and <paramref name="allowNull" /> is <see langword="false" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not the type specified by <paramref name="expectedType" />.</exception>
		// Token: 0x060008A8 RID: 2216 RVA: 0x00021B6C File Offset: 0x0001FD6C
		protected void ValidatePropertyType(string propertyName, object value, Type expectedType, bool allowNull)
		{
			if (value == null)
			{
				if (!allowNull)
				{
					throw new ArgumentNullException("value");
				}
			}
			else if (!expectedType.IsInstanceOfType(value))
			{
				throw new ArgumentException(SR.GetString("ToolboxItemInvalidPropertyType", new object[] { propertyName, expectedType.FullName }), "value");
			}
		}

		/// <summary>Validates a property before it is assigned to the property dictionary.</summary>
		/// <param name="propertyName">The name of the property to validate.</param>
		/// <param name="value">The value against which to validate.</param>
		/// <returns>The value used to perform validation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />, and <paramref name="propertyName" /> is "IsTransient".</exception>
		// Token: 0x060008A9 RID: 2217 RVA: 0x00021BBC File Offset: 0x0001FDBC
		protected virtual object ValidatePropertyValue(string propertyName, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(propertyName);
			if (num <= 1629252038U)
			{
				if (num <= 278446637U)
				{
					if (num != 81274633U)
					{
						if (num != 278446637U)
						{
							return value;
						}
						if (!(propertyName == "IsTransient"))
						{
							return value;
						}
						this.ValidatePropertyType(propertyName, value, typeof(bool), false);
						return value;
					}
					else
					{
						if (!(propertyName == "OriginalBitmap"))
						{
							return value;
						}
						this.ValidatePropertyType(propertyName, value, typeof(Bitmap), true);
						return value;
					}
				}
				else if (num != 982935374U)
				{
					if (num != 1629252038U)
					{
						return value;
					}
					if (!(propertyName == "AssemblyName"))
					{
						return value;
					}
					this.ValidatePropertyType(propertyName, value, typeof(AssemblyName), true);
					return value;
				}
				else if (!(propertyName == "TypeName"))
				{
					return value;
				}
			}
			else if (num <= 1725856265U)
			{
				if (num != 1651150918U)
				{
					if (num != 1725856265U)
					{
						return value;
					}
					if (!(propertyName == "Description"))
					{
						return value;
					}
				}
				else
				{
					if (!(propertyName == "Bitmap"))
					{
						return value;
					}
					this.ValidatePropertyType(propertyName, value, typeof(Bitmap), true);
					return value;
				}
			}
			else if (num != 3250523996U)
			{
				if (num != 4104765591U)
				{
					if (num != 4176258230U)
					{
						return value;
					}
					if (!(propertyName == "DisplayName"))
					{
						return value;
					}
				}
				else
				{
					if (!(propertyName == "Filter"))
					{
						return value;
					}
					this.ValidatePropertyType(propertyName, value, typeof(ICollection), true);
					int num2 = 0;
					ICollection collection = (ICollection)value;
					if (collection != null)
					{
						foreach (object obj in collection)
						{
							if (obj is ToolboxItemFilterAttribute)
							{
								num2++;
							}
						}
					}
					ToolboxItemFilterAttribute[] array = new ToolboxItemFilterAttribute[num2];
					if (collection != null)
					{
						num2 = 0;
						foreach (object obj2 in collection)
						{
							ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj2 as ToolboxItemFilterAttribute;
							if (toolboxItemFilterAttribute != null)
							{
								array[num2++] = toolboxItemFilterAttribute;
							}
						}
					}
					return array;
				}
			}
			else if (!(propertyName == "Company"))
			{
				return value;
			}
			this.ValidatePropertyType(propertyName, value, typeof(string), true);
			if (value == null)
			{
				value = string.Empty;
			}
			return value;
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060008AA RID: 2218 RVA: 0x00021E60 File Offset: 0x00020060
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			IntSecurity.UnmanagedCode.Demand();
			this.Serialize(info, context);
		}

		// Token: 0x0400070D RID: 1805
		private static TraceSwitch ToolboxItemPersist = new TraceSwitch("ToolboxPersisting", "ToolboxItem: write data");

		// Token: 0x0400070E RID: 1806
		private static object EventComponentsCreated = new object();

		// Token: 0x0400070F RID: 1807
		private static object EventComponentsCreating = new object();

		// Token: 0x04000710 RID: 1808
		private static bool isScalingInitialized = false;

		// Token: 0x04000711 RID: 1809
		private const int ICON_DIMENSION = 16;

		// Token: 0x04000712 RID: 1810
		private static int iconWidth = 16;

		// Token: 0x04000713 RID: 1811
		private static int iconHeight = 16;

		// Token: 0x04000714 RID: 1812
		private bool locked;

		// Token: 0x04000715 RID: 1813
		private ToolboxItem.LockableDictionary properties;

		// Token: 0x04000716 RID: 1814
		private ToolboxComponentsCreatedEventHandler componentsCreatedEvent;

		// Token: 0x04000717 RID: 1815
		private ToolboxComponentsCreatingEventHandler componentsCreatingEvent;

		// Token: 0x02000124 RID: 292
		private class LockableDictionary : Hashtable
		{
			// Token: 0x06000F85 RID: 3973 RVA: 0x0002DDB5 File Offset: 0x0002BFB5
			internal LockableDictionary(ToolboxItem item, int capacity)
				: base(capacity)
			{
				this._item = item;
			}

			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
			public override bool IsFixedSize
			{
				get
				{
					return this._item.Locked;
				}
			}

			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
			public override bool IsReadOnly
			{
				get
				{
					return this._item.Locked;
				}
			}

			// Token: 0x170003FF RID: 1023
			public override object this[object key]
			{
				get
				{
					string propertyName = this.GetPropertyName(key);
					object obj = base[propertyName];
					return this._item.FilterPropertyValue(propertyName, obj);
				}
				set
				{
					string propertyName = this.GetPropertyName(key);
					value = this._item.ValidatePropertyValue(propertyName, value);
					this.CheckSerializable(value);
					this._item.CheckUnlocked();
					base[propertyName] = value;
				}
			}

			// Token: 0x06000F8A RID: 3978 RVA: 0x0002DE40 File Offset: 0x0002C040
			public override void Add(object key, object value)
			{
				string propertyName = this.GetPropertyName(key);
				value = this._item.ValidatePropertyValue(propertyName, value);
				this.CheckSerializable(value);
				this._item.CheckUnlocked();
				base.Add(propertyName, value);
			}

			// Token: 0x06000F8B RID: 3979 RVA: 0x0002DE7E File Offset: 0x0002C07E
			private void CheckSerializable(object value)
			{
				if (value != null && !value.GetType().IsSerializable)
				{
					throw new ArgumentException(SR.GetString("ToolboxItemValueNotSerializable", new object[] { value.GetType().FullName }));
				}
			}

			// Token: 0x06000F8C RID: 3980 RVA: 0x0002DEB4 File Offset: 0x0002C0B4
			public override void Clear()
			{
				this._item.CheckUnlocked();
				base.Clear();
			}

			// Token: 0x06000F8D RID: 3981 RVA: 0x0002DEC8 File Offset: 0x0002C0C8
			private string GetPropertyName(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				string text = key as string;
				if (text == null || text.Length == 0)
				{
					throw new ArgumentException(SR.GetString("ToolboxItemInvalidKey"), "key");
				}
				return text;
			}

			// Token: 0x06000F8E RID: 3982 RVA: 0x0002DF0B File Offset: 0x0002C10B
			public override void Remove(object key)
			{
				this._item.CheckUnlocked();
				base.Remove(key);
			}

			// Token: 0x04000C71 RID: 3185
			private ToolboxItem _item;
		}
	}
}
