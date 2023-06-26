using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Management
{
	/// <summary>Represents a WMI instance.</summary>
	// Token: 0x0200001E RID: 30
	[Serializable]
	public class ManagementObject : ManagementBaseObject, ICloneable
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000AE RID: 174 RVA: 0x00004FC8 File Offset: 0x000031C8
		// (remove) Token: 0x060000AF RID: 175 RVA: 0x00005000 File Offset: 0x00003200
		internal event IdentifierChangedEventHandler IdentifierChanged;

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the field represented by this instance.</summary>
		/// <param name="info">The object to be populated with serialization information.</param>
		/// <param name="context">The location where serialized data will be stored and retrieved.</param>
		// Token: 0x060000B0 RID: 176 RVA: 0x00005035 File Offset: 0x00003235
		protected override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Releases all resources used by the Component.</summary>
		// Token: 0x060000B1 RID: 177 RVA: 0x0000503F File Offset: 0x0000323F
		public new void Dispose()
		{
			if (this.wmiClass != null)
			{
				this.wmiClass.Dispose();
				this.wmiClass = null;
			}
			base.Dispose();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005067 File Offset: 0x00003267
		internal void FireIdentifierChanged()
		{
			if (this.IdentifierChanged != null)
			{
				this.IdentifierChanged(this, null);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000507E File Offset: 0x0000327E
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00005086 File Offset: 0x00003286
		internal bool PutButNotGot
		{
			get
			{
				return this.putButNotGot;
			}
			set
			{
				this.putButNotGot = value;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000508F File Offset: 0x0000328F
		private void HandleIdentifierChange(object sender, IdentifierChangedEventArgs e)
		{
			base.wbemObject = null;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00005098 File Offset: 0x00003298
		internal bool IsBound
		{
			get
			{
				return this._wbemObject != null;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000050A4 File Offset: 0x000032A4
		internal static ManagementObject GetManagementObject(IWbemClassObjectFreeThreaded wbemObject, ManagementObject mgObj)
		{
			ManagementObject managementObject = new ManagementObject();
			managementObject.wbemObject = wbemObject;
			if (mgObj != null)
			{
				managementObject.scope = ManagementScope._Clone(mgObj.scope);
				if (mgObj.path != null)
				{
					managementObject.path = ManagementPath._Clone(mgObj.path);
				}
				if (mgObj.options != null)
				{
					managementObject.options = ObjectGetOptions._Clone(mgObj.options);
				}
			}
			return managementObject;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005108 File Offset: 0x00003308
		internal static ManagementObject GetManagementObject(IWbemClassObjectFreeThreaded wbemObject, ManagementScope scope)
		{
			ManagementObject managementObject = new ManagementObject();
			managementObject.wbemObject = wbemObject;
			managementObject.path = new ManagementPath(ManagementPath.GetManagementPath(wbemObject));
			managementObject.path.IdentifierChanged += managementObject.HandleIdentifierChange;
			managementObject.scope = ManagementScope._Clone(scope, new IdentifierChangedEventHandler(managementObject.HandleIdentifierChange));
			return managementObject;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementObject" /> class. This is the default constructor.</summary>
		// Token: 0x060000B9 RID: 185 RVA: 0x00005163 File Offset: 0x00003363
		public ManagementObject()
			: this(null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementObject" /> class for the specified WMI object path. The path is provided as a <see cref="T:System.Management.ManagementPath" />.</summary>
		/// <param name="path">A <see cref="T:System.Management.ManagementPath" /> that contains a path to a WMI object.</param>
		// Token: 0x060000BA RID: 186 RVA: 0x0000516E File Offset: 0x0000336E
		public ManagementObject(ManagementPath path)
			: this(null, path, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementObject" /> class for the specified WMI object path. The path is provided as a string.</summary>
		/// <param name="path">A WMI path.</param>
		// Token: 0x060000BB RID: 187 RVA: 0x00005179 File Offset: 0x00003379
		public ManagementObject(string path)
			: this(null, new ManagementPath(path), null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementObject" /> class bound to the specified WMI path, including the specified additional options.</summary>
		/// <param name="path">A <see cref="T:System.Management.ManagementPath" /> containing the WMI path.</param>
		/// <param name="options">An <see cref="T:System.Management.ObjectGetOptions" /> containing additional options for binding to the WMI object. This parameter could be null if default options are to be used.</param>
		// Token: 0x060000BC RID: 188 RVA: 0x00005189 File Offset: 0x00003389
		public ManagementObject(ManagementPath path, ObjectGetOptions options)
			: this(null, path, options)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementObject" /> class bound to the specified WMI path, including the specified additional options. In this variant, the path can be specified as a string.</summary>
		/// <param name="path">The WMI path to the object.</param>
		/// <param name="options">An <see cref="T:System.Management.ObjectGetOptions" /> representing options to get the specified WMI object.</param>
		// Token: 0x060000BD RID: 189 RVA: 0x00005194 File Offset: 0x00003394
		public ManagementObject(string path, ObjectGetOptions options)
			: this(new ManagementPath(path), options)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementObject" /> class bound to the specified WMI path that includes the specified options.</summary>
		/// <param name="scope">A <see cref="T:System.Management.ManagementScope" /> representing the scope in which the WMI object resides. In this version, scopes can only be WMI namespaces.</param>
		/// <param name="path">A <see cref="T:System.Management.ManagementPath" /> representing the WMI path to the manageable object.</param>
		/// <param name="options">An <see cref="T:System.Management.ObjectGetOptions" /> specifying additional options for getting the object.</param>
		// Token: 0x060000BE RID: 190 RVA: 0x000051A3 File Offset: 0x000033A3
		public ManagementObject(ManagementScope scope, ManagementPath path, ObjectGetOptions options)
			: base(null)
		{
			this.ManagementObjectCTOR(scope, path, options);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000051B8 File Offset: 0x000033B8
		private void ManagementObjectCTOR(ManagementScope scope, ManagementPath path, ObjectGetOptions options)
		{
			string text = string.Empty;
			if (path != null && !path.IsEmpty)
			{
				if (base.GetType() == typeof(ManagementObject) && path.IsClass)
				{
					throw new ArgumentOutOfRangeException("path");
				}
				if (base.GetType() == typeof(ManagementClass) && path.IsInstance)
				{
					throw new ArgumentOutOfRangeException("path");
				}
				text = path.GetNamespacePath(8);
				if (scope != null && scope.Path.NamespacePath.Length > 0)
				{
					path = new ManagementPath(path.RelativePath);
					path.NamespacePath = scope.Path.GetNamespacePath(8);
				}
				if (path.IsClass || path.IsInstance)
				{
					this.path = ManagementPath._Clone(path, new IdentifierChangedEventHandler(this.HandleIdentifierChange));
				}
				else
				{
					this.path = ManagementPath._Clone(null, new IdentifierChangedEventHandler(this.HandleIdentifierChange));
				}
			}
			if (options != null)
			{
				this.options = ObjectGetOptions._Clone(options, new IdentifierChangedEventHandler(this.HandleIdentifierChange));
			}
			if (scope != null)
			{
				this.scope = ManagementScope._Clone(scope, new IdentifierChangedEventHandler(this.HandleIdentifierChange));
			}
			else if (text.Length > 0)
			{
				this.scope = new ManagementScope(text);
				this.scope.IdentifierChanged += this.HandleIdentifierChange;
			}
			this.IdentifierChanged += this.HandleIdentifierChange;
			this.putButNotGot = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementObject" /> class bound to the specified WMI path, and includes the specified options. The scope and the path are specified as strings.</summary>
		/// <param name="scopeString">The scope for the WMI object.</param>
		/// <param name="pathString">The WMI object path.</param>
		/// <param name="options">An <see cref="T:System.Management.ObjectGetOptions" /> representing additional options for getting the WMI object.</param>
		// Token: 0x060000C0 RID: 192 RVA: 0x000029A3 File Offset: 0x00000BA3
		public ManagementObject(string scopeString, string pathString, ObjectGetOptions options)
			: this(new ManagementScope(scopeString), new ManagementPath(pathString), options)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementObject" /> class that is serializable.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060000C1 RID: 193 RVA: 0x0000532C File Offset: 0x0000352C
		protected ManagementObject(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ManagementObjectCTOR(null, null, null);
		}

		/// <summary>Gets or sets the scope in which this object resides.</summary>
		/// <returns>The scope in which this object resides.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005340 File Offset: 0x00003540
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000536C File Offset: 0x0000356C
		public ManagementScope Scope
		{
			get
			{
				if (this.scope == null)
				{
					return this.scope = ManagementScope._Clone(null);
				}
				return this.scope;
			}
			set
			{
				if (value != null)
				{
					if (this.scope != null)
					{
						this.scope.IdentifierChanged -= this.HandleIdentifierChange;
					}
					this.scope = ManagementScope._Clone(value, new IdentifierChangedEventHandler(this.HandleIdentifierChange));
					this.FireIdentifierChanged();
					return;
				}
				throw new ArgumentNullException("value");
			}
		}

		/// <summary>Gets or sets the object's WMI path.</summary>
		/// <returns>A <see cref="T:System.Management.ManagementPath" /> representing the object's path.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000053C4 File Offset: 0x000035C4
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000053F0 File Offset: 0x000035F0
		public virtual ManagementPath Path
		{
			get
			{
				if (this.path == null)
				{
					return this.path = ManagementPath._Clone(null);
				}
				return this.path;
			}
			set
			{
				ManagementPath managementPath = ((value != null) ? value : new ManagementPath());
				string namespacePath = managementPath.GetNamespacePath(8);
				if (namespacePath.Length > 0 && this.scope != null && this.scope.IsDefaulted)
				{
					this.Scope = new ManagementScope(namespacePath);
				}
				if ((base.GetType() == typeof(ManagementObject) && managementPath.IsInstance) || (base.GetType() == typeof(ManagementClass) && managementPath.IsClass) || managementPath.IsEmpty)
				{
					if (this.path != null)
					{
						this.path.IdentifierChanged -= this.HandleIdentifierChange;
					}
					this.path = ManagementPath._Clone(value, new IdentifierChangedEventHandler(this.HandleIdentifierChange));
					this.FireIdentifierChanged();
					return;
				}
				throw new ArgumentOutOfRangeException("value");
			}
		}

		/// <summary>Gets or sets additional information to use when retrieving the object.</summary>
		/// <returns>An <see cref="T:System.Management.ObjectGetOptions" /> to use when retrieving the object.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000054CC File Offset: 0x000036CC
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000054F8 File Offset: 0x000036F8
		public ObjectGetOptions Options
		{
			get
			{
				if (this.options == null)
				{
					return this.options = ObjectGetOptions._Clone(null);
				}
				return this.options;
			}
			set
			{
				if (value != null)
				{
					if (this.options != null)
					{
						this.options.IdentifierChanged -= this.HandleIdentifierChange;
					}
					this.options = ObjectGetOptions._Clone(value, new IdentifierChangedEventHandler(this.HandleIdentifierChange));
					this.FireIdentifierChanged();
					return;
				}
				throw new ArgumentNullException("value");
			}
		}

		/// <summary>Gets or sets the path to the object's class.</summary>
		/// <returns>A <see cref="T:System.Management.ManagementPath" /> representing the path to the object's class.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005550 File Offset: 0x00003750
		public override ManagementPath ClassPath
		{
			get
			{
				object obj = null;
				object obj2 = null;
				object obj3 = null;
				int num = 0;
				int num2 = 0;
				if (this.PutButNotGot)
				{
					this.Get();
					this.PutButNotGot = false;
				}
				int num3 = base.wbemObject.Get_("__SERVER", 0, ref obj, ref num, ref num2);
				if (num3 >= 0)
				{
					num3 = base.wbemObject.Get_("__NAMESPACE", 0, ref obj2, ref num, ref num2);
					if (num3 >= 0)
					{
						num3 = base.wbemObject.Get_("__CLASS", 0, ref obj3, ref num, ref num2);
					}
				}
				if (num3 < 0)
				{
					if (((long)num3 & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
					{
						ManagementException.ThrowWithExtendedInfo((ManagementStatus)num3);
					}
					else
					{
						Marshal.ThrowExceptionForHR(num3, WmiNetUtilsHelper.GetErrorInfo_f());
					}
				}
				ManagementPath managementPath = new ManagementPath();
				managementPath.Server = string.Empty;
				managementPath.NamespacePath = string.Empty;
				managementPath.ClassName = string.Empty;
				try
				{
					managementPath.Server = (string)((obj is DBNull) ? "" : obj);
					managementPath.NamespacePath = (string)((obj2 is DBNull) ? "" : obj2);
					managementPath.ClassName = (string)((obj3 is DBNull) ? "" : obj3);
				}
				catch
				{
				}
				return managementPath;
			}
		}

		/// <summary>Binds WMI class information to the management object.</summary>
		// Token: 0x060000C9 RID: 201 RVA: 0x0000569C File Offset: 0x0000389C
		public void Get()
		{
			IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded = null;
			this.Initialize(false);
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			ObjectGetOptions objectGetOptions = ((this.options == null) ? new ObjectGetOptions() : this.options);
			SecurityHandler securityHandler = null;
			try
			{
				securityHandler = this.scope.GetSecurityHandler();
				int object_ = this.scope.GetSecuredIWbemServicesHandler(this.scope.GetIWbemServices()).GetObject_(this.path.RelativePath, objectGetOptions.Flags, objectGetOptions.GetContext(), ref wbemClassObjectFreeThreaded, IntPtr.Zero);
				if (object_ < 0)
				{
					if (((long)object_ & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
					{
						ManagementException.ThrowWithExtendedInfo((ManagementStatus)object_);
					}
					else
					{
						Marshal.ThrowExceptionForHR(object_, WmiNetUtilsHelper.GetErrorInfo_f());
					}
				}
				base.wbemObject = wbemClassObjectFreeThreaded;
			}
			finally
			{
				if (securityHandler != null)
				{
					securityHandler.Reset();
				}
			}
		}

		/// <summary>Binds to the management object asynchronously.</summary>
		/// <param name="watcher">The object to receive the results of the operation as events.</param>
		// Token: 0x060000CA RID: 202 RVA: 0x00005784 File Offset: 0x00003984
		public void Get(ManagementOperationObserver watcher)
		{
			this.Initialize(false);
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			if (watcher == null)
			{
				throw new ArgumentNullException("watcher");
			}
			IWbemServices iwbemServices = this.scope.GetIWbemServices();
			ObjectGetOptions objectGetOptions = ObjectGetOptions._Clone(this.options);
			WmiGetEventSink newGetSink = watcher.GetNewGetSink(this.scope, objectGetOptions.Context, this);
			if (watcher.HaveListenersForProgress)
			{
				objectGetOptions.SendStatus = true;
			}
			SecurityHandler securityHandler = this.scope.GetSecurityHandler();
			int objectAsync_ = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).GetObjectAsync_(this.path.RelativePath, objectGetOptions.Flags, objectGetOptions.GetContext(), newGetSink.Stub);
			if (securityHandler != null)
			{
				securityHandler.Reset();
			}
			if (objectAsync_ < 0)
			{
				watcher.RemoveSink(newGetSink);
				if (((long)objectAsync_ & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
				{
					ManagementException.ThrowWithExtendedInfo((ManagementStatus)objectAsync_);
					return;
				}
				Marshal.ThrowExceptionForHR(objectAsync_, WmiNetUtilsHelper.GetErrorInfo_f());
			}
		}

		/// <summary>Gets a collection of objects related to the object (associators).</summary>
		/// <returns>A <see cref="T:System.Management.ManagementObjectCollection" /> containing the related objects.</returns>
		// Token: 0x060000CB RID: 203 RVA: 0x00005882 File Offset: 0x00003A82
		public ManagementObjectCollection GetRelated()
		{
			return this.GetRelated(null);
		}

		/// <summary>Gets a collection of objects related to the object (associators).</summary>
		/// <param name="relatedClass">A class of related objects.</param>
		/// <returns>A <see cref="T:System.Management.ManagementObjectCollection" /> containing the related objects.</returns>
		// Token: 0x060000CC RID: 204 RVA: 0x0000588C File Offset: 0x00003A8C
		public ManagementObjectCollection GetRelated(string relatedClass)
		{
			return this.GetRelated(relatedClass, null, null, null, null, null, false, null);
		}

		/// <summary>Gets a collection of objects related to the object (associators).</summary>
		/// <param name="relatedClass">The class of the related objects.</param>
		/// <param name="relationshipClass">The relationship class of interest.</param>
		/// <param name="relationshipQualifier">The qualifier required to be present on the relationship class.</param>
		/// <param name="relatedQualifier">The qualifier required to be present on the related class.</param>
		/// <param name="relatedRole">The role that the related class is playing in the relationship.</param>
		/// <param name="thisRole">The role that this class is playing in the relationship.</param>
		/// <param name="classDefinitionsOnly">When this method returns, it contains only class definitions for the instances that match the query.</param>
		/// <param name="options">Extended options for how to execute the query.</param>
		/// <returns>A <see cref="T:System.Management.ManagementObjectCollection" /> containing the related objects.</returns>
		// Token: 0x060000CD RID: 205 RVA: 0x000058A8 File Offset: 0x00003AA8
		public ManagementObjectCollection GetRelated(string relatedClass, string relationshipClass, string relationshipQualifier, string relatedQualifier, string relatedRole, string thisRole, bool classDefinitionsOnly, EnumerationOptions options)
		{
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			this.Initialize(false);
			IEnumWbemClassObject enumWbemClassObject = null;
			EnumerationOptions enumerationOptions = ((options != null) ? options : new EnumerationOptions());
			RelatedObjectQuery relatedObjectQuery = new RelatedObjectQuery(this.path.Path, relatedClass, relationshipClass, relationshipQualifier, relatedQualifier, relatedRole, thisRole, classDefinitionsOnly);
			enumerationOptions.EnumerateDeep = true;
			SecurityHandler securityHandler = null;
			try
			{
				securityHandler = this.scope.GetSecurityHandler();
				int num = this.scope.GetSecuredIWbemServicesHandler(this.scope.GetIWbemServices()).ExecQuery_(relatedObjectQuery.QueryLanguage, relatedObjectQuery.QueryString, enumerationOptions.Flags, enumerationOptions.GetContext(), ref enumWbemClassObject);
				if (num < 0)
				{
					if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
					{
						ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					}
					else
					{
						Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
					}
				}
			}
			finally
			{
				if (securityHandler != null)
				{
					securityHandler.Reset();
				}
			}
			return new ManagementObjectCollection(this.scope, enumerationOptions, enumWbemClassObject);
		}

		/// <summary>Gets a collection of objects related to the object (associators) asynchronously. This call returns immediately, and a delegate is called when the results are available.</summary>
		/// <param name="watcher">The object to use to return results.</param>
		// Token: 0x060000CE RID: 206 RVA: 0x000059B4 File Offset: 0x00003BB4
		public void GetRelated(ManagementOperationObserver watcher)
		{
			this.GetRelated(watcher, null);
		}

		/// <summary>Gets a collection of objects related to the object (associators).</summary>
		/// <param name="watcher">The object to use to return results.</param>
		/// <param name="relatedClass">The class of related objects.</param>
		// Token: 0x060000CF RID: 207 RVA: 0x000059C0 File Offset: 0x00003BC0
		public void GetRelated(ManagementOperationObserver watcher, string relatedClass)
		{
			this.GetRelated(watcher, relatedClass, null, null, null, null, null, false, null);
		}

		/// <summary>Gets a collection of objects related to the object (associators).</summary>
		/// <param name="watcher">The object to use to return results.</param>
		/// <param name="relatedClass">The class of the related objects.</param>
		/// <param name="relationshipClass">The relationship class of interest.</param>
		/// <param name="relationshipQualifier">The qualifier required to be present on the relationship class.</param>
		/// <param name="relatedQualifier">The qualifier required to be present on the related class.</param>
		/// <param name="relatedRole">The role that the related class is playing in the relationship.</param>
		/// <param name="thisRole">The role that this class is playing in the relationship.</param>
		/// <param name="classDefinitionsOnly">Return only class definitions for the instances that match the query.</param>
		/// <param name="options">Extended options for how to execute the query.</param>
		// Token: 0x060000D0 RID: 208 RVA: 0x000059DC File Offset: 0x00003BDC
		public void GetRelated(ManagementOperationObserver watcher, string relatedClass, string relationshipClass, string relationshipQualifier, string relatedQualifier, string relatedRole, string thisRole, bool classDefinitionsOnly, EnumerationOptions options)
		{
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			this.Initialize(true);
			if (watcher == null)
			{
				throw new ArgumentNullException("watcher");
			}
			EnumerationOptions enumerationOptions = ((options != null) ? ((EnumerationOptions)options.Clone()) : new EnumerationOptions());
			enumerationOptions.ReturnImmediately = false;
			if (watcher.HaveListenersForProgress)
			{
				enumerationOptions.SendStatus = true;
			}
			WmiEventSink newSink = watcher.GetNewSink(this.scope, enumerationOptions.Context);
			RelatedObjectQuery relatedObjectQuery = new RelatedObjectQuery(this.path.Path, relatedClass, relationshipClass, relationshipQualifier, relatedQualifier, relatedRole, thisRole, classDefinitionsOnly);
			enumerationOptions.EnumerateDeep = true;
			SecurityHandler securityHandler = this.scope.GetSecurityHandler();
			int num = this.scope.GetSecuredIWbemServicesHandler(this.scope.GetIWbemServices()).ExecQueryAsync_(relatedObjectQuery.QueryLanguage, relatedObjectQuery.QueryString, enumerationOptions.Flags, enumerationOptions.GetContext(), newSink.Stub);
			securityHandler.Reset();
			if (num < 0)
			{
				watcher.RemoveSink(newSink);
				if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
				{
					ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					return;
				}
				Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
			}
		}

		/// <summary>Gets a collection of associations to the object.</summary>
		/// <returns>A <see cref="T:System.Management.ManagementObjectCollection" /> containing the association objects.</returns>
		// Token: 0x060000D1 RID: 209 RVA: 0x00005B0C File Offset: 0x00003D0C
		public ManagementObjectCollection GetRelationships()
		{
			return this.GetRelationships(null);
		}

		/// <summary>Gets a collection of associations to the object.</summary>
		/// <param name="relationshipClass">The associations to include.</param>
		/// <returns>A <see cref="T:System.Management.ManagementObjectCollection" /> containing the association objects.</returns>
		// Token: 0x060000D2 RID: 210 RVA: 0x00005B15 File Offset: 0x00003D15
		public ManagementObjectCollection GetRelationships(string relationshipClass)
		{
			return this.GetRelationships(relationshipClass, null, null, false, null);
		}

		/// <summary>Gets a collection of associations to the object.</summary>
		/// <param name="relationshipClass">The type of relationship of interest.</param>
		/// <param name="relationshipQualifier">The qualifier to be present on the relationship.</param>
		/// <param name="thisRole">The role of this object in the relationship.</param>
		/// <param name="classDefinitionsOnly">When this method returns, it contains only the class definitions for the result set.</param>
		/// <param name="options">The extended options for the query execution.</param>
		/// <returns>A <see cref="T:System.Management.ManagementObjectCollection" /> containing the association objects.</returns>
		// Token: 0x060000D3 RID: 211 RVA: 0x00005B24 File Offset: 0x00003D24
		public ManagementObjectCollection GetRelationships(string relationshipClass, string relationshipQualifier, string thisRole, bool classDefinitionsOnly, EnumerationOptions options)
		{
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			this.Initialize(false);
			IEnumWbemClassObject enumWbemClassObject = null;
			EnumerationOptions enumerationOptions = ((options != null) ? options : new EnumerationOptions());
			RelationshipQuery relationshipQuery = new RelationshipQuery(this.path.Path, relationshipClass, relationshipQualifier, thisRole, classDefinitionsOnly);
			enumerationOptions.EnumerateDeep = true;
			SecurityHandler securityHandler = null;
			try
			{
				securityHandler = this.scope.GetSecurityHandler();
				int num = this.scope.GetSecuredIWbemServicesHandler(this.scope.GetIWbemServices()).ExecQuery_(relationshipQuery.QueryLanguage, relationshipQuery.QueryString, enumerationOptions.Flags, enumerationOptions.GetContext(), ref enumWbemClassObject);
				if (num < 0)
				{
					if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
					{
						ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					}
					else
					{
						Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
					}
				}
			}
			finally
			{
				if (securityHandler != null)
				{
					securityHandler.Reset();
				}
			}
			return new ManagementObjectCollection(this.scope, enumerationOptions, enumWbemClassObject);
		}

		/// <summary>Gets a collection of associations to the object.</summary>
		/// <param name="watcher">The object to use to return results.</param>
		// Token: 0x060000D4 RID: 212 RVA: 0x00005C28 File Offset: 0x00003E28
		public void GetRelationships(ManagementOperationObserver watcher)
		{
			this.GetRelationships(watcher, null);
		}

		/// <summary>Gets a collection of associations to the object.</summary>
		/// <param name="watcher">The object to use to return results.</param>
		/// <param name="relationshipClass">The associations to include.</param>
		// Token: 0x060000D5 RID: 213 RVA: 0x00005C32 File Offset: 0x00003E32
		public void GetRelationships(ManagementOperationObserver watcher, string relationshipClass)
		{
			this.GetRelationships(watcher, relationshipClass, null, null, false, null);
		}

		/// <summary>Gets a collection of associations to the object.</summary>
		/// <param name="watcher">The object to use to return results.</param>
		/// <param name="relationshipClass">The type of relationship of interest.</param>
		/// <param name="relationshipQualifier">The qualifier to be present on the relationship.</param>
		/// <param name="thisRole">The role of this object in the relationship.</param>
		/// <param name="classDefinitionsOnly">When this method returns, it contains only the class definitions for the result set.</param>
		/// <param name="options">The extended options for the query execution.</param>
		// Token: 0x060000D6 RID: 214 RVA: 0x00005C40 File Offset: 0x00003E40
		public void GetRelationships(ManagementOperationObserver watcher, string relationshipClass, string relationshipQualifier, string thisRole, bool classDefinitionsOnly, EnumerationOptions options)
		{
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			if (watcher == null)
			{
				throw new ArgumentNullException("watcher");
			}
			this.Initialize(false);
			EnumerationOptions enumerationOptions = ((options != null) ? ((EnumerationOptions)options.Clone()) : new EnumerationOptions());
			enumerationOptions.ReturnImmediately = false;
			if (watcher.HaveListenersForProgress)
			{
				enumerationOptions.SendStatus = true;
			}
			WmiEventSink newSink = watcher.GetNewSink(this.scope, enumerationOptions.Context);
			RelationshipQuery relationshipQuery = new RelationshipQuery(this.path.Path, relationshipClass, relationshipQualifier, thisRole, classDefinitionsOnly);
			enumerationOptions.EnumerateDeep = true;
			SecurityHandler securityHandler = this.scope.GetSecurityHandler();
			int num = this.scope.GetSecuredIWbemServicesHandler(this.scope.GetIWbemServices()).ExecQueryAsync_(relationshipQuery.QueryLanguage, relationshipQuery.QueryString, enumerationOptions.Flags, enumerationOptions.GetContext(), newSink.Stub);
			if (securityHandler != null)
			{
				securityHandler.Reset();
			}
			if (num < 0)
			{
				watcher.RemoveSink(newSink);
				if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
				{
					ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					return;
				}
				Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
			}
		}

		/// <summary>Commits the changes to the object.</summary>
		/// <returns>A <see cref="T:System.Management.ManagementPath" /> containing the path to the committed object.</returns>
		// Token: 0x060000D7 RID: 215 RVA: 0x00005D6D File Offset: 0x00003F6D
		public ManagementPath Put()
		{
			return this.Put(null);
		}

		/// <summary>Commits the changes to the object.</summary>
		/// <param name="options">The options for how to commit the changes.</param>
		/// <returns>A <see cref="T:System.Management.ManagementPath" /> containing the path to the committed object.</returns>
		// Token: 0x060000D8 RID: 216 RVA: 0x00005D78 File Offset: 0x00003F78
		public ManagementPath Put(PutOptions options)
		{
			ManagementPath managementPath = null;
			this.Initialize(true);
			PutOptions putOptions = ((options != null) ? options : new PutOptions());
			IWbemServices iwbemServices = this.scope.GetIWbemServices();
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IWbemCallResult wbemCallResult = null;
			SecurityHandler securityHandler = null;
			try
			{
				securityHandler = this.scope.GetSecurityHandler();
				intPtr = Marshal.AllocHGlobal(IntPtr.Size);
				Marshal.WriteIntPtr(intPtr, IntPtr.Zero);
				int num;
				if (base.IsClass)
				{
					num = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).PutClass_(base.wbemObject, putOptions.Flags | 16, putOptions.GetContext(), intPtr);
				}
				else
				{
					num = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).PutInstance_(base.wbemObject, putOptions.Flags | 16, putOptions.GetContext(), intPtr);
				}
				intPtr2 = Marshal.ReadIntPtr(intPtr);
				wbemCallResult = (IWbemCallResult)Marshal.GetObjectForIUnknown(intPtr2);
				int num2;
				num = wbemCallResult.GetCallStatus_(-1, out num2);
				if (num >= 0)
				{
					num = num2;
				}
				if (num < 0)
				{
					if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
					{
						ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					}
					else
					{
						Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
					}
				}
				managementPath = this.GetPath(wbemCallResult);
			}
			finally
			{
				if (securityHandler != null)
				{
					securityHandler.Reset();
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.Release(intPtr2);
				}
				if (wbemCallResult != null)
				{
					Marshal.ReleaseComObject(wbemCallResult);
				}
			}
			this.putButNotGot = true;
			this.path.SetRelativePath(managementPath.RelativePath);
			return managementPath;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005F0C File Offset: 0x0000410C
		private ManagementPath GetPath(IWbemCallResult callResult)
		{
			ManagementPath managementPath = null;
			try
			{
				string text = null;
				int resultString_ = callResult.GetResultString_(-1, out text);
				if (resultString_ >= 0)
				{
					managementPath = new ManagementPath(this.scope.Path.Path);
					managementPath.RelativePath = text;
				}
				else
				{
					object obj = base.GetPropertyValue("__PATH");
					if (obj != null)
					{
						managementPath = new ManagementPath((string)obj);
					}
					else
					{
						obj = base.GetPropertyValue("__RELPATH");
						if (obj != null)
						{
							managementPath = new ManagementPath(this.scope.Path.Path);
							managementPath.RelativePath = (string)obj;
						}
					}
				}
			}
			catch
			{
			}
			if (managementPath == null)
			{
				managementPath = new ManagementPath();
			}
			return managementPath;
		}

		/// <summary>Commits the changes to the object, asynchronously.</summary>
		/// <param name="watcher">A <see cref="T:System.Management.ManagementOperationObserver" /> used to handle the progress and results of the asynchronous operation.</param>
		// Token: 0x060000DA RID: 218 RVA: 0x00005FBC File Offset: 0x000041BC
		public void Put(ManagementOperationObserver watcher)
		{
			this.Put(watcher, null);
		}

		/// <summary>Commits the changes to the object asynchronously and using the specified options.</summary>
		/// <param name="watcher">A <see cref="T:System.Management.ManagementOperationObserver" /> used to handle the progress and results of the asynchronous operation.</param>
		/// <param name="options">A <see cref="T:System.Management.PutOptions" /> used to specify additional options for the commit operation.</param>
		// Token: 0x060000DB RID: 219 RVA: 0x00005FC8 File Offset: 0x000041C8
		public void Put(ManagementOperationObserver watcher, PutOptions options)
		{
			if (watcher == null)
			{
				throw new ArgumentNullException("watcher");
			}
			this.Initialize(false);
			PutOptions putOptions = ((options == null) ? new PutOptions() : ((PutOptions)options.Clone()));
			if (watcher.HaveListenersForProgress)
			{
				putOptions.SendStatus = true;
			}
			IWbemServices iwbemServices = this.scope.GetIWbemServices();
			WmiEventSink newPutSink = watcher.GetNewPutSink(this.scope, putOptions.Context, this.scope.Path.GetNamespacePath(8), base.ClassName);
			newPutSink.InternalObjectPut += this.HandleObjectPut;
			SecurityHandler securityHandler = this.scope.GetSecurityHandler();
			int num;
			if (base.IsClass)
			{
				num = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).PutClassAsync_(base.wbemObject, putOptions.Flags, putOptions.GetContext(), newPutSink.Stub);
			}
			else
			{
				num = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).PutInstanceAsync_(base.wbemObject, putOptions.Flags, putOptions.GetContext(), newPutSink.Stub);
			}
			if (securityHandler != null)
			{
				securityHandler.Reset();
			}
			if (num < 0)
			{
				newPutSink.InternalObjectPut -= this.HandleObjectPut;
				watcher.RemoveSink(newPutSink);
				if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
				{
					ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					return;
				}
				Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006120 File Offset: 0x00004320
		internal void HandleObjectPut(object sender, InternalObjectPutEventArgs e)
		{
			try
			{
				if (sender is WmiEventSink)
				{
					((WmiEventSink)sender).InternalObjectPut -= this.HandleObjectPut;
					this.putButNotGot = true;
					this.path.SetRelativePath(e.Path.RelativePath);
				}
			}
			catch
			{
			}
		}

		/// <summary>Copies the object to a different location.</summary>
		/// <param name="path">The <see cref="T:System.Management.ManagementPath" /> to which the object should be copied.</param>
		/// <returns>The new path of the copied object.</returns>
		// Token: 0x060000DD RID: 221 RVA: 0x00006180 File Offset: 0x00004380
		public ManagementPath CopyTo(ManagementPath path)
		{
			return this.CopyTo(path, null);
		}

		/// <summary>Copies the object to a different location.</summary>
		/// <param name="path">The path to which the object should be copied.</param>
		/// <returns>The new path of the copied object.</returns>
		// Token: 0x060000DE RID: 222 RVA: 0x0000618A File Offset: 0x0000438A
		public ManagementPath CopyTo(string path)
		{
			return this.CopyTo(new ManagementPath(path), null);
		}

		/// <summary>Copies the object to a different location.</summary>
		/// <param name="path">The path to which the object should be copied.</param>
		/// <param name="options">The options for how the object should be put.</param>
		/// <returns>The new path of the copied object.</returns>
		// Token: 0x060000DF RID: 223 RVA: 0x00006199 File Offset: 0x00004399
		public ManagementPath CopyTo(string path, PutOptions options)
		{
			return this.CopyTo(new ManagementPath(path), options);
		}

		/// <summary>Copies the object to a different location.</summary>
		/// <param name="path">The <see cref="T:System.Management.ManagementPath" /> to which the object should be copied.</param>
		/// <param name="options">The options for how the object should be put.</param>
		/// <returns>The new path of the copied object.</returns>
		// Token: 0x060000E0 RID: 224 RVA: 0x000061A8 File Offset: 0x000043A8
		public ManagementPath CopyTo(ManagementPath path, PutOptions options)
		{
			this.Initialize(false);
			ManagementScope managementScope = new ManagementScope(path, this.scope);
			managementScope.Initialize();
			PutOptions putOptions = ((options != null) ? options : new PutOptions());
			IWbemServices iwbemServices = managementScope.GetIWbemServices();
			ManagementPath managementPath = null;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IWbemCallResult wbemCallResult = null;
			SecurityHandler securityHandler = null;
			try
			{
				securityHandler = managementScope.GetSecurityHandler();
				intPtr = Marshal.AllocHGlobal(IntPtr.Size);
				Marshal.WriteIntPtr(intPtr, IntPtr.Zero);
				int num;
				if (base.IsClass)
				{
					num = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).PutClass_(base.wbemObject, putOptions.Flags | 16, putOptions.GetContext(), intPtr);
				}
				else
				{
					num = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).PutInstance_(base.wbemObject, putOptions.Flags | 16, putOptions.GetContext(), intPtr);
				}
				intPtr2 = Marshal.ReadIntPtr(intPtr);
				wbemCallResult = (IWbemCallResult)Marshal.GetObjectForIUnknown(intPtr2);
				int num2;
				num = wbemCallResult.GetCallStatus_(-1, out num2);
				if (num >= 0)
				{
					num = num2;
				}
				if (num < 0)
				{
					if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
					{
						ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					}
					else
					{
						Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
					}
				}
				managementPath = this.GetPath(wbemCallResult);
				managementPath.NamespacePath = path.GetNamespacePath(8);
			}
			finally
			{
				if (securityHandler != null)
				{
					securityHandler.Reset();
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.Release(intPtr2);
				}
				if (wbemCallResult != null)
				{
					Marshal.ReleaseComObject(wbemCallResult);
				}
			}
			return managementPath;
		}

		/// <summary>Copies the object to a different location, asynchronously.</summary>
		/// <param name="watcher">The object that will receive the results of the operation.</param>
		/// <param name="path">A <see cref="T:System.Management.ManagementPath" /> specifying the path to which the object should be copied.</param>
		// Token: 0x060000E1 RID: 225 RVA: 0x00006344 File Offset: 0x00004544
		public void CopyTo(ManagementOperationObserver watcher, ManagementPath path)
		{
			this.CopyTo(watcher, path, null);
		}

		/// <summary>Copies the object to a different location, asynchronously.</summary>
		/// <param name="watcher">The object that will receive the results of the operation.</param>
		/// <param name="path">The path to which the object should be copied.</param>
		// Token: 0x060000E2 RID: 226 RVA: 0x0000634F File Offset: 0x0000454F
		public void CopyTo(ManagementOperationObserver watcher, string path)
		{
			this.CopyTo(watcher, new ManagementPath(path), null);
		}

		/// <summary>Copies the object to a different location, asynchronously.</summary>
		/// <param name="watcher">The object that will receive the results of the operation.</param>
		/// <param name="path">The path to which the object should be copied.</param>
		/// <param name="options">The options for how the object should be put.</param>
		// Token: 0x060000E3 RID: 227 RVA: 0x0000635F File Offset: 0x0000455F
		public void CopyTo(ManagementOperationObserver watcher, string path, PutOptions options)
		{
			this.CopyTo(watcher, new ManagementPath(path), options);
		}

		/// <summary>Copies the object to a different location, asynchronously.</summary>
		/// <param name="watcher">The object that will receive the results of the operation.</param>
		/// <param name="path">The path to which the object should be copied.</param>
		/// <param name="options">The options for how the object should be put.</param>
		// Token: 0x060000E4 RID: 228 RVA: 0x00006370 File Offset: 0x00004570
		public void CopyTo(ManagementOperationObserver watcher, ManagementPath path, PutOptions options)
		{
			if (watcher == null)
			{
				throw new ArgumentNullException("watcher");
			}
			this.Initialize(false);
			ManagementScope managementScope = new ManagementScope(path, this.scope);
			managementScope.Initialize();
			PutOptions putOptions = ((options != null) ? ((PutOptions)options.Clone()) : new PutOptions());
			if (watcher.HaveListenersForProgress)
			{
				putOptions.SendStatus = true;
			}
			WmiEventSink newPutSink = watcher.GetNewPutSink(managementScope, putOptions.Context, path.GetNamespacePath(8), base.ClassName);
			IWbemServices iwbemServices = managementScope.GetIWbemServices();
			SecurityHandler securityHandler = managementScope.GetSecurityHandler();
			int num;
			if (base.IsClass)
			{
				num = managementScope.GetSecuredIWbemServicesHandler(iwbemServices).PutClassAsync_(base.wbemObject, putOptions.Flags, putOptions.GetContext(), newPutSink.Stub);
			}
			else
			{
				num = managementScope.GetSecuredIWbemServicesHandler(iwbemServices).PutInstanceAsync_(base.wbemObject, putOptions.Flags, putOptions.GetContext(), newPutSink.Stub);
			}
			if (securityHandler != null)
			{
				securityHandler.Reset();
			}
			if (num < 0)
			{
				watcher.RemoveSink(newPutSink);
				if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
				{
					ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					return;
				}
				Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
			}
		}

		/// <summary>Deletes the object.</summary>
		// Token: 0x060000E5 RID: 229 RVA: 0x00006494 File Offset: 0x00004694
		public void Delete()
		{
			this.Delete(null);
		}

		/// <summary>Deletes the object.</summary>
		/// <param name="options">The options for how to delete the object.</param>
		// Token: 0x060000E6 RID: 230 RVA: 0x000064A0 File Offset: 0x000046A0
		public void Delete(DeleteOptions options)
		{
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			this.Initialize(false);
			DeleteOptions deleteOptions = ((options != null) ? options : new DeleteOptions());
			IWbemServices iwbemServices = this.scope.GetIWbemServices();
			SecurityHandler securityHandler = null;
			try
			{
				securityHandler = this.scope.GetSecurityHandler();
				int num;
				if (base.IsClass)
				{
					num = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).DeleteClass_(this.path.RelativePath, deleteOptions.Flags, deleteOptions.GetContext(), IntPtr.Zero);
				}
				else
				{
					num = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).DeleteInstance_(this.path.RelativePath, deleteOptions.Flags, deleteOptions.GetContext(), IntPtr.Zero);
				}
				if (num < 0)
				{
					if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
					{
						ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					}
					else
					{
						Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
					}
				}
			}
			finally
			{
				if (securityHandler != null)
				{
					securityHandler.Reset();
				}
			}
		}

		/// <summary>Deletes the object.</summary>
		/// <param name="watcher">The object that will receive the results of the operation.</param>
		// Token: 0x060000E7 RID: 231 RVA: 0x000065AC File Offset: 0x000047AC
		public void Delete(ManagementOperationObserver watcher)
		{
			this.Delete(watcher, null);
		}

		/// <summary>Deletes the object.</summary>
		/// <param name="watcher">The object that will receive the results of the operation.</param>
		/// <param name="options">The options for how to delete the object.</param>
		// Token: 0x060000E8 RID: 232 RVA: 0x000065B8 File Offset: 0x000047B8
		public void Delete(ManagementOperationObserver watcher, DeleteOptions options)
		{
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			if (watcher == null)
			{
				throw new ArgumentNullException("watcher");
			}
			this.Initialize(false);
			DeleteOptions deleteOptions = ((options != null) ? ((DeleteOptions)options.Clone()) : new DeleteOptions());
			if (watcher.HaveListenersForProgress)
			{
				deleteOptions.SendStatus = true;
			}
			IWbemServices iwbemServices = this.scope.GetIWbemServices();
			WmiEventSink newSink = watcher.GetNewSink(this.scope, deleteOptions.Context);
			SecurityHandler securityHandler = this.scope.GetSecurityHandler();
			int num;
			if (base.IsClass)
			{
				num = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).DeleteClassAsync_(this.path.RelativePath, deleteOptions.Flags, deleteOptions.GetContext(), newSink.Stub);
			}
			else
			{
				num = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).DeleteInstanceAsync_(this.path.RelativePath, deleteOptions.Flags, deleteOptions.GetContext(), newSink.Stub);
			}
			if (securityHandler != null)
			{
				securityHandler.Reset();
			}
			if (num < 0)
			{
				watcher.RemoveSink(newSink);
				if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
				{
					ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					return;
				}
				Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
			}
		}

		/// <summary>Invokes a method on the object.</summary>
		/// <param name="methodName">The name of the method to execute.</param>
		/// <param name="args">An array containing parameter values.</param>
		/// <returns>The object value returned by the method.</returns>
		// Token: 0x060000E9 RID: 233 RVA: 0x000066FC File Offset: 0x000048FC
		public object InvokeMethod(string methodName, object[] args)
		{
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			if (methodName == null)
			{
				throw new ArgumentNullException("methodName");
			}
			this.Initialize(false);
			ManagementBaseObject managementBaseObject;
			IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded;
			IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded2;
			this.GetMethodParameters(methodName, out managementBaseObject, out wbemClassObjectFreeThreaded, out wbemClassObjectFreeThreaded2);
			ManagementObject.MapInParameters(args, managementBaseObject, wbemClassObjectFreeThreaded);
			ManagementBaseObject managementBaseObject2 = this.InvokeMethod(methodName, managementBaseObject, null);
			return ManagementObject.MapOutParameters(args, managementBaseObject2, wbemClassObjectFreeThreaded2);
		}

		/// <summary>Invokes a method on the object, asynchronously.</summary>
		/// <param name="watcher">The object to receive the results of the operation.</param>
		/// <param name="methodName">The name of the method to execute.</param>
		/// <param name="args">An array containing parameter values.</param>
		// Token: 0x060000EA RID: 234 RVA: 0x0000676C File Offset: 0x0000496C
		public void InvokeMethod(ManagementOperationObserver watcher, string methodName, object[] args)
		{
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			if (watcher == null)
			{
				throw new ArgumentNullException("watcher");
			}
			if (methodName == null)
			{
				throw new ArgumentNullException("methodName");
			}
			this.Initialize(false);
			ManagementBaseObject managementBaseObject;
			IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded;
			IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded2;
			this.GetMethodParameters(methodName, out managementBaseObject, out wbemClassObjectFreeThreaded, out wbemClassObjectFreeThreaded2);
			ManagementObject.MapInParameters(args, managementBaseObject, wbemClassObjectFreeThreaded);
			this.InvokeMethod(watcher, methodName, managementBaseObject, null);
		}

		/// <summary>Invokes a method on the WMI object. The input and output parameters are represented as <see cref="T:System.Management.ManagementBaseObject" /> objects.</summary>
		/// <param name="methodName">The name of the method to execute.</param>
		/// <param name="inParameters">A <see cref="T:System.Management.ManagementBaseObject" /> holding the input parameters to the method.</param>
		/// <param name="options">An <see cref="T:System.Management.InvokeMethodOptions" /> containing additional options for the execution of the method.</param>
		/// <returns>A <see cref="T:System.Management.ManagementBaseObject" /> containing the output parameters and return value of the executed method.</returns>
		// Token: 0x060000EB RID: 235 RVA: 0x000067DC File Offset: 0x000049DC
		public ManagementBaseObject InvokeMethod(string methodName, ManagementBaseObject inParameters, InvokeMethodOptions options)
		{
			ManagementBaseObject managementBaseObject = null;
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			if (methodName == null)
			{
				throw new ArgumentNullException("methodName");
			}
			this.Initialize(false);
			InvokeMethodOptions invokeMethodOptions = ((options != null) ? options : new InvokeMethodOptions());
			IWbemServices iwbemServices = this.scope.GetIWbemServices();
			SecurityHandler securityHandler = null;
			try
			{
				securityHandler = this.scope.GetSecurityHandler();
				IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded = ((inParameters == null) ? null : inParameters.wbemObject);
				IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded2 = null;
				int num = this.scope.GetSecuredIWbemServicesHandler(this.scope.GetIWbemServices()).ExecMethod_(this.path.RelativePath, methodName, invokeMethodOptions.Flags, invokeMethodOptions.GetContext(), wbemClassObjectFreeThreaded, ref wbemClassObjectFreeThreaded2, IntPtr.Zero);
				if (num < 0)
				{
					if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
					{
						ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					}
					else
					{
						Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
					}
				}
				if (wbemClassObjectFreeThreaded2 != null)
				{
					managementBaseObject = new ManagementBaseObject(wbemClassObjectFreeThreaded2);
				}
			}
			finally
			{
				if (securityHandler != null)
				{
					securityHandler.Reset();
				}
			}
			return managementBaseObject;
		}

		/// <summary>Invokes a method on the object, asynchronously.</summary>
		/// <param name="watcher">A <see cref="T:System.Management.ManagementOperationObserver" /> used to handle the asynchronous execution's progress and results.</param>
		/// <param name="methodName">The name of the method to be executed.</param>
		/// <param name="inParameters">A <see cref="T:System.Management.ManagementBaseObject" /> containing the input parameters for the method.</param>
		/// <param name="options">An <see cref="T:System.Management.InvokeMethodOptions" /> containing additional options used to execute the method.</param>
		// Token: 0x060000EC RID: 236 RVA: 0x000068F4 File Offset: 0x00004AF4
		public void InvokeMethod(ManagementOperationObserver watcher, string methodName, ManagementBaseObject inParameters, InvokeMethodOptions options)
		{
			if (this.path == null || this.path.Path.Length == 0)
			{
				throw new InvalidOperationException();
			}
			if (watcher == null)
			{
				throw new ArgumentNullException("watcher");
			}
			if (methodName == null)
			{
				throw new ArgumentNullException("methodName");
			}
			this.Initialize(false);
			InvokeMethodOptions invokeMethodOptions = ((options != null) ? ((InvokeMethodOptions)options.Clone()) : new InvokeMethodOptions());
			if (watcher.HaveListenersForProgress)
			{
				invokeMethodOptions.SendStatus = true;
			}
			WmiEventSink newSink = watcher.GetNewSink(this.scope, invokeMethodOptions.Context);
			SecurityHandler securityHandler = this.scope.GetSecurityHandler();
			IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded = null;
			if (inParameters != null)
			{
				wbemClassObjectFreeThreaded = inParameters.wbemObject;
			}
			int num = this.scope.GetSecuredIWbemServicesHandler(this.scope.GetIWbemServices()).ExecMethodAsync_(this.path.RelativePath, methodName, invokeMethodOptions.Flags, invokeMethodOptions.GetContext(), wbemClassObjectFreeThreaded, newSink.Stub);
			if (securityHandler != null)
			{
				securityHandler.Reset();
			}
			if (num < 0)
			{
				watcher.RemoveSink(newSink);
				if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
				{
					ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					return;
				}
				Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
			}
		}

		/// <summary>Returns a <see cref="T:System.Management.ManagementBaseObject" /> representing the list of input parameters for a method.</summary>
		/// <param name="methodName">The name of the method.</param>
		/// <returns>A <see cref="T:System.Management.ManagementBaseObject" /> containing the input parameters to the method.</returns>
		// Token: 0x060000ED RID: 237 RVA: 0x00006A14 File Offset: 0x00004C14
		public ManagementBaseObject GetMethodParameters(string methodName)
		{
			ManagementBaseObject managementBaseObject;
			IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded;
			IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded2;
			this.GetMethodParameters(methodName, out managementBaseObject, out wbemClassObjectFreeThreaded, out wbemClassObjectFreeThreaded2);
			return managementBaseObject;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006A30 File Offset: 0x00004C30
		private void GetMethodParameters(string methodName, out ManagementBaseObject inParameters, out IWbemClassObjectFreeThreaded inParametersClass, out IWbemClassObjectFreeThreaded outParametersClass)
		{
			inParameters = null;
			inParametersClass = null;
			outParametersClass = null;
			if (methodName == null)
			{
				throw new ArgumentNullException("methodName");
			}
			this.Initialize(false);
			if (this.wmiClass == null)
			{
				ManagementPath classPath = this.ClassPath;
				if (classPath == null || !classPath.IsClass)
				{
					throw new InvalidOperationException();
				}
				ManagementClass managementClass = new ManagementClass(this.scope, classPath, null);
				managementClass.Get();
				this.wmiClass = managementClass.wbemObject;
			}
			int num = this.wmiClass.GetMethod_(methodName, 0, out inParametersClass, out outParametersClass);
			if (num == -2147217406)
			{
				num = -2147217323;
			}
			if (num >= 0 && inParametersClass != null)
			{
				IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded = null;
				num = inParametersClass.SpawnInstance_(0, out wbemClassObjectFreeThreaded);
				if (num >= 0)
				{
					inParameters = new ManagementBaseObject(wbemClassObjectFreeThreaded);
				}
			}
			if (num < 0)
			{
				if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
				{
					ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
					return;
				}
				Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
			}
		}

		/// <summary>Creates a copy of the object.</summary>
		/// <returns>The copied object.</returns>
		// Token: 0x060000EF RID: 239 RVA: 0x00006B0C File Offset: 0x00004D0C
		public override object Clone()
		{
			if (this.PutButNotGot)
			{
				this.Get();
				this.PutButNotGot = false;
			}
			IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded = null;
			int num = base.wbemObject.Clone_(out wbemClassObjectFreeThreaded);
			if (num < 0)
			{
				if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
				{
					ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
				}
				else
				{
					Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
				}
			}
			return ManagementObject.GetManagementObject(wbemClassObjectFreeThreaded, this);
		}

		/// <summary>Returns the full path of the object. This is an override of the default object implementation.</summary>
		/// <returns>The full path of the object.</returns>
		// Token: 0x060000F0 RID: 240 RVA: 0x00006B72 File Offset: 0x00004D72
		public override string ToString()
		{
			if (this.path != null)
			{
				return this.path.Path;
			}
			return "";
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006B90 File Offset: 0x00004D90
		internal override void Initialize(bool getObject)
		{
			bool flag = false;
			lock (this)
			{
				if (this.path == null)
				{
					this.path = new ManagementPath();
					this.path.IdentifierChanged += this.HandleIdentifierChange;
				}
				if (!this.IsBound && getObject)
				{
					flag = true;
				}
				if (this.scope == null)
				{
					string namespacePath = this.path.GetNamespacePath(8);
					if (0 < namespacePath.Length)
					{
						this.scope = new ManagementScope(namespacePath);
					}
					else
					{
						this.scope = new ManagementScope();
					}
					this.scope.IdentifierChanged += this.HandleIdentifierChange;
				}
				else if (this.scope.Path == null || this.scope.Path.IsEmpty)
				{
					string namespacePath2 = this.path.GetNamespacePath(8);
					if (0 < namespacePath2.Length)
					{
						this.scope.Path = new ManagementPath(namespacePath2);
					}
					else
					{
						this.scope.Path = ManagementPath.DefaultPath;
					}
				}
				ManagementScope managementScope = this.scope;
				lock (managementScope)
				{
					if (!this.scope.IsConnected)
					{
						this.scope.Initialize();
						if (getObject)
						{
							flag = true;
						}
					}
					if (flag)
					{
						if (this.options == null)
						{
							this.options = new ObjectGetOptions();
							this.options.IdentifierChanged += this.HandleIdentifierChange;
						}
						IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded = null;
						IWbemServices iwbemServices = this.scope.GetIWbemServices();
						SecurityHandler securityHandler = null;
						try
						{
							securityHandler = this.scope.GetSecurityHandler();
							string text = null;
							string relativePath = this.path.RelativePath;
							if (relativePath.Length > 0)
							{
								text = relativePath;
							}
							int num = this.scope.GetSecuredIWbemServicesHandler(iwbemServices).GetObject_(text, this.options.Flags, this.options.GetContext(), ref wbemClassObjectFreeThreaded, IntPtr.Zero);
							if (num >= 0)
							{
								base.wbemObject = wbemClassObjectFreeThreaded;
								object obj = null;
								int num2 = 0;
								int num3 = 0;
								num = base.wbemObject.Get_("__PATH", 0, ref obj, ref num2, ref num3);
								if (num >= 0)
								{
									this.path = ((DBNull.Value != obj) ? new ManagementPath((string)obj) : new ManagementPath());
									this.path.IdentifierChanged += this.HandleIdentifierChange;
								}
							}
							if (num < 0)
							{
								if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
								{
									ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
								}
								else
								{
									Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
								}
							}
						}
						finally
						{
							if (securityHandler != null)
							{
								securityHandler.Reset();
							}
						}
					}
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006E74 File Offset: 0x00005074
		private static void MapInParameters(object[] args, ManagementBaseObject inParams, IWbemClassObjectFreeThreaded inParamsClass)
		{
			int num = 0;
			if (inParamsClass != null && args != null && args.Length != 0)
			{
				int upperBound = args.GetUpperBound(0);
				int lowerBound = args.GetLowerBound(0);
				int num2 = upperBound - lowerBound;
				num = inParamsClass.BeginEnumeration_(64);
				if (num >= 0)
				{
					do
					{
						object obj = null;
						int num3 = 0;
						string text = null;
						IWbemQualifierSetFreeThreaded wbemQualifierSetFreeThreaded = null;
						num = inParamsClass.Next_(0, ref text, ref obj, ref num3, ref num3);
						if (num >= 0)
						{
							if (text == null)
							{
								break;
							}
							num = inParamsClass.GetPropertyQualifierSet_(text, out wbemQualifierSetFreeThreaded);
							if (num >= 0)
							{
								try
								{
									object obj2 = 0;
									wbemQualifierSetFreeThreaded.Get_("ID", 0, ref obj2, ref num3);
									int num4 = (int)obj2;
									if (0 <= num4 && num2 >= num4)
									{
										inParams[text] = args[lowerBound + num4];
									}
								}
								finally
								{
									wbemQualifierSetFreeThreaded.Dispose();
								}
							}
						}
					}
					while (num >= 0);
				}
				if (num < 0)
				{
					if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
					{
						ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
						return;
					}
					Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
				}
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006F70 File Offset: 0x00005170
		private static object MapOutParameters(object[] args, ManagementBaseObject outParams, IWbemClassObjectFreeThreaded outParamsClass)
		{
			object obj = null;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (outParamsClass != null)
			{
				if (args != null && args.Length != 0)
				{
					int upperBound = args.GetUpperBound(0);
					num = args.GetLowerBound(0);
					num2 = upperBound - num;
				}
				num3 = outParamsClass.BeginEnumeration_(64);
				if (num3 >= 0)
				{
					do
					{
						object obj2 = null;
						int num4 = 0;
						string text = null;
						IWbemQualifierSetFreeThreaded wbemQualifierSetFreeThreaded = null;
						num3 = outParamsClass.Next_(0, ref text, ref obj2, ref num4, ref num4);
						if (num3 >= 0)
						{
							if (text == null)
							{
								break;
							}
							if (string.Compare(text, "RETURNVALUE", StringComparison.OrdinalIgnoreCase) == 0)
							{
								obj = outParams["RETURNVALUE"];
							}
							else
							{
								num3 = outParamsClass.GetPropertyQualifierSet_(text, out wbemQualifierSetFreeThreaded);
								if (num3 >= 0)
								{
									try
									{
										object obj3 = 0;
										wbemQualifierSetFreeThreaded.Get_("ID", 0, ref obj3, ref num4);
										int num5 = (int)obj3;
										if (0 <= num5 && num2 >= num5)
										{
											args[num + num5] = outParams[text];
										}
									}
									finally
									{
										wbemQualifierSetFreeThreaded.Dispose();
									}
								}
							}
						}
					}
					while (num3 >= 0);
				}
				if (num3 < 0)
				{
					if (((long)num3 & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
					{
						ManagementException.ThrowWithExtendedInfo((ManagementStatus)num3);
					}
					else
					{
						Marshal.ThrowExceptionForHR(num3, WmiNetUtilsHelper.GetErrorInfo_f());
					}
				}
			}
			return obj;
		}

		// Token: 0x04000107 RID: 263
		internal const string ID = "ID";

		// Token: 0x04000108 RID: 264
		internal const string RETURNVALUE = "RETURNVALUE";

		// Token: 0x04000109 RID: 265
		private IWbemClassObjectFreeThreaded wmiClass;

		// Token: 0x0400010A RID: 266
		internal ManagementScope scope;

		// Token: 0x0400010B RID: 267
		internal ManagementPath path;

		// Token: 0x0400010C RID: 268
		internal ObjectGetOptions options;

		// Token: 0x0400010D RID: 269
		private bool putButNotGot;
	}
}
