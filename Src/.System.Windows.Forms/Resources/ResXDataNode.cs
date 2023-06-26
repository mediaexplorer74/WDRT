using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace System.Resources
{
	/// <summary>Represents an element in an XML resource (.resx) file.</summary>
	// Token: 0x020000E9 RID: 233
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[Serializable]
	public sealed class ResXDataNode : ISerializable
	{
		// Token: 0x0600032E RID: 814 RVA: 0x00002843 File Offset: 0x00000A43
		private ResXDataNode()
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008CF0 File Offset: 0x00006EF0
		internal ResXDataNode DeepClone()
		{
			return new ResXDataNode
			{
				nodeInfo = ((this.nodeInfo != null) ? this.nodeInfo.Clone() : null),
				name = this.name,
				comment = this.comment,
				typeName = this.typeName,
				fileRefFullPath = this.fileRefFullPath,
				fileRefType = this.fileRefType,
				fileRefTextEncoding = this.fileRefTextEncoding,
				value = this.value,
				fileRef = ((this.fileRef != null) ? this.fileRef.Clone() : null),
				typeNameConverter = this.typeNameConverter
			};
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXDataNode" /> class.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="value">The resource to store.</param>
		/// <exception cref="T:System.InvalidOperationException">The resource named in <paramref name="value" /> does not support serialization.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a string of zero length.</exception>
		// Token: 0x06000330 RID: 816 RVA: 0x00008D9C File Offset: 0x00006F9C
		public ResXDataNode(string name, object value)
			: this(name, value, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXDataNode" /> class.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="value">The resource to store.</param>
		/// <param name="typeNameConverter">A reference to a method that takes a <see cref="T:System.Type" /> and returns a string containing the <see cref="T:System.Type" /> name.</param>
		/// <exception cref="T:System.InvalidOperationException">The resource named in <paramref name="value" /> does not support serialization.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a string of zero length.</exception>
		// Token: 0x06000331 RID: 817 RVA: 0x00008DA8 File Offset: 0x00006FA8
		public ResXDataNode(string name, object value, Func<Type, string> typeNameConverter)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this.typeNameConverter = typeNameConverter;
			Type type = ((value == null) ? typeof(object) : value.GetType());
			if (value != null && !type.IsSerializable)
			{
				throw new InvalidOperationException(SR.GetString("NotSerializableType", new object[] { name, type.FullName }));
			}
			if (value != null)
			{
				this.typeName = MultitargetUtil.GetAssemblyQualifiedName(type, this.typeNameConverter);
			}
			this.name = name;
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXDataNode" /> class with a reference to a resource file.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="fileRef">The file reference to use as the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" /> or <paramref name="fileRef" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a string of zero length.</exception>
		// Token: 0x06000332 RID: 818 RVA: 0x00008E4A File Offset: 0x0000704A
		public ResXDataNode(string name, ResXFileRef fileRef)
			: this(name, fileRef, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXDataNode" /> class with a reference to a resource file.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="fileRef">The file reference to use as the resource.</param>
		/// <param name="typeNameConverter">A reference to a method that takes a <see cref="T:System.Type" /> and returns a string containing the <see cref="T:System.Type" /> name.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" /> or <paramref name="fileRef" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a string of zero length.</exception>
		// Token: 0x06000333 RID: 819 RVA: 0x00008E58 File Offset: 0x00007058
		public ResXDataNode(string name, ResXFileRef fileRef, Func<Type, string> typeNameConverter)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (fileRef == null)
			{
				throw new ArgumentNullException("fileRef");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this.name = name;
			this.fileRef = fileRef;
			this.typeNameConverter = typeNameConverter;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00008EAF File Offset: 0x000070AF
		internal ResXDataNode(DataNodeInfo nodeInfo, string basePath)
		{
			this.nodeInfo = nodeInfo;
			this.InitializeDataNode(basePath);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00008EC8 File Offset: 0x000070C8
		private void InitializeDataNode(string basePath)
		{
			Type type = null;
			if (!string.IsNullOrEmpty(this.nodeInfo.TypeName))
			{
				type = ResXDataNode.internalTypeResolver.GetType(this.nodeInfo.TypeName, false, true);
			}
			if (type != null && type.Equals(typeof(ResXFileRef)))
			{
				string[] array = ResXFileRef.Converter.ParseResxFileRefString(this.nodeInfo.ValueData);
				if (array != null && array.Length > 1)
				{
					if (!Path.IsPathRooted(array[0]) && basePath != null)
					{
						this.fileRefFullPath = Path.Combine(basePath, array[0]);
					}
					else
					{
						this.fileRefFullPath = array[0];
					}
					this.fileRefType = array[1];
					if (array.Length > 2)
					{
						this.fileRefTextEncoding = array[2];
					}
				}
			}
		}

		/// <summary>Gets or sets an arbitrary comment regarding this resource.</summary>
		/// <returns>A string that represents the comment.</returns>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00008F78 File Offset: 0x00007178
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00008FAD File Offset: 0x000071AD
		public string Comment
		{
			get
			{
				string text = this.comment;
				if (text == null && this.nodeInfo != null)
				{
					text = this.nodeInfo.Comment;
				}
				if (text != null)
				{
					return text;
				}
				return "";
			}
			set
			{
				this.comment = value;
			}
		}

		/// <summary>Gets or sets the name of this resource.</summary>
		/// <returns>A string that corresponds to the resource name. If no name is assigned, this property returns a zero-length string.</returns>
		/// <exception cref="T:System.ArgumentNullException">The name property is set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The name property is set to a string of zero length.</exception>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00008FB8 File Offset: 0x000071B8
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00008FE4 File Offset: 0x000071E4
		public string Name
		{
			get
			{
				string text = this.name;
				if (text == null && this.nodeInfo != null)
				{
					text = this.nodeInfo.Name;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Name");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("Name");
				}
				this.name = value;
			}
		}

		/// <summary>Gets the file reference for this resource.</summary>
		/// <returns>The file reference, if this resource uses one. If this resource stores its value as an <see cref="T:System.Object" />, this property will return <see langword="null" />.</returns>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00009010 File Offset: 0x00007210
		public ResXFileRef FileRef
		{
			get
			{
				if (this.FileRefFullPath == null)
				{
					return null;
				}
				if (this.fileRef == null)
				{
					if (string.IsNullOrEmpty(this.fileRefTextEncoding))
					{
						this.fileRef = new ResXFileRef(this.FileRefFullPath, this.FileRefType);
					}
					else
					{
						this.fileRef = new ResXFileRef(this.FileRefFullPath, this.FileRefType, Encoding.GetEncoding(this.FileRefTextEncoding));
					}
				}
				return this.fileRef;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00009080 File Offset: 0x00007280
		private string FileRefFullPath
		{
			get
			{
				string text = ((this.fileRef == null) ? null : this.fileRef.FileName);
				if (text == null)
				{
					text = this.fileRefFullPath;
				}
				return text;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000090B0 File Offset: 0x000072B0
		private string FileRefType
		{
			get
			{
				string text = ((this.fileRef == null) ? null : this.fileRef.TypeName);
				if (text == null)
				{
					text = this.fileRefType;
				}
				return text;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600033D RID: 829 RVA: 0x000090E0 File Offset: 0x000072E0
		private string FileRefTextEncoding
		{
			get
			{
				string text = ((this.fileRef == null) ? null : ((this.fileRef.TextFileEncoding == null) ? null : this.fileRef.TextFileEncoding.BodyName));
				if (text == null)
				{
					text = this.fileRefTextEncoding;
				}
				return text;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00009124 File Offset: 0x00007324
		[MethodImpl(MethodImplOptions.NoInlining)]
		private IFormatter CreateSoapFormatter()
		{
			return new SoapFormatter();
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000912C File Offset: 0x0000732C
		private static string ToBase64WrappedString(byte[] data)
		{
			string text = Convert.ToBase64String(data);
			if (text.Length > 80)
			{
				StringBuilder stringBuilder = new StringBuilder(text.Length + text.Length / 80 * 3);
				int i;
				for (i = 0; i < text.Length - 80; i += 80)
				{
					stringBuilder.Append("\r\n");
					stringBuilder.Append("        ");
					stringBuilder.Append(text, i, 80);
				}
				stringBuilder.Append("\r\n");
				stringBuilder.Append("        ");
				stringBuilder.Append(text, i, text.Length - i);
				stringBuilder.Append("\r\n");
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000091DC File Offset: 0x000073DC
		private void FillDataNodeInfoFromObject(DataNodeInfo nodeInfo, object value)
		{
			CultureInfo cultureInfo = value as CultureInfo;
			if (cultureInfo != null)
			{
				nodeInfo.ValueData = cultureInfo.Name;
				nodeInfo.TypeName = MultitargetUtil.GetAssemblyQualifiedName(typeof(CultureInfo), this.typeNameConverter);
				return;
			}
			if (value is string)
			{
				nodeInfo.ValueData = (string)value;
				if (value == null)
				{
					nodeInfo.TypeName = MultitargetUtil.GetAssemblyQualifiedName(typeof(ResXNullRef), this.typeNameConverter);
					return;
				}
			}
			else
			{
				if (value is byte[])
				{
					nodeInfo.ValueData = ResXDataNode.ToBase64WrappedString((byte[])value);
					nodeInfo.TypeName = MultitargetUtil.GetAssemblyQualifiedName(typeof(byte[]), this.typeNameConverter);
					return;
				}
				Type type = ((value == null) ? typeof(object) : value.GetType());
				if (value != null && !type.IsSerializable)
				{
					throw new InvalidOperationException(SR.GetString("NotSerializableType", new object[] { this.name, type.FullName }));
				}
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				bool flag = converter.CanConvertTo(typeof(string));
				bool flag2 = converter.CanConvertFrom(typeof(string));
				try
				{
					if (flag && flag2)
					{
						nodeInfo.ValueData = converter.ConvertToInvariantString(value);
						nodeInfo.TypeName = MultitargetUtil.GetAssemblyQualifiedName(type, this.typeNameConverter);
						return;
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				bool flag3 = converter.CanConvertTo(typeof(byte[]));
				bool flag4 = converter.CanConvertFrom(typeof(byte[]));
				if (flag3 && flag4)
				{
					byte[] array = (byte[])converter.ConvertTo(value, typeof(byte[]));
					string text = ResXDataNode.ToBase64WrappedString(array);
					nodeInfo.ValueData = text;
					nodeInfo.MimeType = ResXResourceWriter.ByteArraySerializedObjectMimeType;
					nodeInfo.TypeName = MultitargetUtil.GetAssemblyQualifiedName(type, this.typeNameConverter);
					return;
				}
				if (value == null)
				{
					nodeInfo.ValueData = string.Empty;
					nodeInfo.TypeName = MultitargetUtil.GetAssemblyQualifiedName(typeof(ResXNullRef), this.typeNameConverter);
					return;
				}
				if (this.binaryFormatter == null)
				{
					this.binaryFormatter = new BinaryFormatter();
					this.binaryFormatter.Binder = new ResXSerializationBinder(this.typeNameConverter);
				}
				MemoryStream memoryStream = new MemoryStream();
				this.binaryFormatter.Serialize(memoryStream, value);
				string text2 = ResXDataNode.ToBase64WrappedString(memoryStream.ToArray());
				nodeInfo.ValueData = text2;
				nodeInfo.MimeType = ResXResourceWriter.DefaultSerializedObjectMimeType;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009440 File Offset: 0x00007640
		private object GenerateObjectFromDataNodeInfo(DataNodeInfo dataNodeInfo, ITypeResolutionService typeResolver)
		{
			object obj = null;
			string mimeType = dataNodeInfo.MimeType;
			string text = ((dataNodeInfo.TypeName == null || dataNodeInfo.TypeName.Length == 0) ? MultitargetUtil.GetAssemblyQualifiedName(typeof(string), this.typeNameConverter) : dataNodeInfo.TypeName);
			if (mimeType != null && mimeType.Length > 0)
			{
				if (string.Equals(mimeType, ResXResourceWriter.BinSerializedObjectMimeType) || string.Equals(mimeType, ResXResourceWriter.Beta2CompatSerializedObjectMimeType) || string.Equals(mimeType, ResXResourceWriter.CompatBinSerializedObjectMimeType))
				{
					string valueData = dataNodeInfo.ValueData;
					byte[] array = ResXDataNode.FromBase64WrappedString(valueData);
					if (this.binaryFormatter == null)
					{
						this.binaryFormatter = new BinaryFormatter();
						this.binaryFormatter.Binder = new ResXSerializationBinder(typeResolver);
					}
					IFormatter formatter = this.binaryFormatter;
					if (array != null && array.Length != 0)
					{
						obj = formatter.Deserialize(new MemoryStream(array));
						if (obj is ResXNullRef)
						{
							obj = null;
						}
					}
				}
				else if (string.Equals(mimeType, ResXResourceWriter.SoapSerializedObjectMimeType) || string.Equals(mimeType, ResXResourceWriter.CompatSoapSerializedObjectMimeType))
				{
					string valueData2 = dataNodeInfo.ValueData;
					byte[] array2 = ResXDataNode.FromBase64WrappedString(valueData2);
					if (array2 != null && array2.Length != 0)
					{
						IFormatter formatter2 = this.CreateSoapFormatter();
						obj = formatter2.Deserialize(new MemoryStream(array2));
						if (obj is ResXNullRef)
						{
							obj = null;
						}
					}
				}
				else if (string.Equals(mimeType, ResXResourceWriter.ByteArraySerializedObjectMimeType) && text != null && text.Length > 0)
				{
					Type type = this.ResolveType(text, typeResolver);
					if (!(type != null))
					{
						string @string = SR.GetString("TypeLoadException", new object[]
						{
							text,
							dataNodeInfo.ReaderPosition.Y,
							dataNodeInfo.ReaderPosition.X
						});
						XmlException ex = new XmlException(@string, null, dataNodeInfo.ReaderPosition.Y, dataNodeInfo.ReaderPosition.X);
						TypeLoadException ex2 = new TypeLoadException(@string, ex);
						throw ex2;
					}
					TypeConverter converter = TypeDescriptor.GetConverter(type);
					if (converter.CanConvertFrom(typeof(byte[])))
					{
						string valueData3 = dataNodeInfo.ValueData;
						byte[] array3 = ResXDataNode.FromBase64WrappedString(valueData3);
						if (array3 != null)
						{
							obj = converter.ConvertFrom(array3);
						}
					}
				}
			}
			else if (text != null && text.Length > 0)
			{
				Type type2 = this.ResolveType(text, typeResolver);
				if (type2 != null)
				{
					if (type2 == typeof(ResXNullRef))
					{
						return null;
					}
					if (text.IndexOf("System.Byte[]") != -1 && text.IndexOf("mscorlib") != -1)
					{
						return ResXDataNode.FromBase64WrappedString(dataNodeInfo.ValueData);
					}
					TypeConverter converter2 = TypeDescriptor.GetConverter(type2);
					if (!converter2.CanConvertFrom(typeof(string)))
					{
						return obj;
					}
					string valueData4 = dataNodeInfo.ValueData;
					try
					{
						return converter2.ConvertFromInvariantString(valueData4);
					}
					catch (NotSupportedException ex3)
					{
						string string2 = SR.GetString("NotSupported", new object[]
						{
							text,
							dataNodeInfo.ReaderPosition.Y,
							dataNodeInfo.ReaderPosition.X,
							ex3.Message
						});
						XmlException ex4 = new XmlException(string2, ex3, dataNodeInfo.ReaderPosition.Y, dataNodeInfo.ReaderPosition.X);
						NotSupportedException ex5 = new NotSupportedException(string2, ex4);
						throw ex5;
					}
				}
				string string3 = SR.GetString("TypeLoadException", new object[]
				{
					text,
					dataNodeInfo.ReaderPosition.Y,
					dataNodeInfo.ReaderPosition.X
				});
				XmlException ex6 = new XmlException(string3, null, dataNodeInfo.ReaderPosition.Y, dataNodeInfo.ReaderPosition.X);
				TypeLoadException ex7 = new TypeLoadException(string3, ex6);
				throw ex7;
			}
			return obj;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00009810 File Offset: 0x00007A10
		internal DataNodeInfo GetDataNodeInfo()
		{
			bool flag = true;
			if (this.nodeInfo != null)
			{
				flag = false;
			}
			else
			{
				this.nodeInfo = new DataNodeInfo();
			}
			this.nodeInfo.Name = this.Name;
			this.nodeInfo.Comment = this.Comment;
			if (flag || this.FileRefFullPath != null)
			{
				if (this.FileRefFullPath != null)
				{
					this.nodeInfo.ValueData = this.FileRef.ToString();
					this.nodeInfo.MimeType = null;
					this.nodeInfo.TypeName = MultitargetUtil.GetAssemblyQualifiedName(typeof(ResXFileRef), this.typeNameConverter);
				}
				else
				{
					this.FillDataNodeInfoFromObject(this.nodeInfo, this.value);
				}
			}
			return this.nodeInfo;
		}

		/// <summary>Retrieves the position of the resource in the resource file.</summary>
		/// <returns>A structure that specifies the location of this resource in the resource file as a line position (<see cref="P:System.Drawing.Point.X" />) and a column position (<see cref="P:System.Drawing.Point.Y" />). If this resource is not part of a resource file, this method returns a structure that has an <see cref="P:System.Drawing.Point.X" /> of 0 and a <see cref="P:System.Drawing.Point.Y" /> of 0.</returns>
		// Token: 0x06000343 RID: 835 RVA: 0x000098C8 File Offset: 0x00007AC8
		public Point GetNodePosition()
		{
			if (this.nodeInfo == null)
			{
				return default(Point);
			}
			return this.nodeInfo.ReaderPosition;
		}

		/// <summary>Retrieves the type name for the value by using the specified type resolution service.</summary>
		/// <param name="typeResolver">The type resolution service to use to locate a converter for this type.</param>
		/// <returns>A string that represents the fully qualified name of the type.</returns>
		/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found.</exception>
		// Token: 0x06000344 RID: 836 RVA: 0x000098F4 File Offset: 0x00007AF4
		public string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			if (this.typeName == null || this.typeName.Length <= 0)
			{
				string text = this.FileRefType;
				Type type = null;
				if (text != null)
				{
					type = this.ResolveType(this.FileRefType, typeResolver);
				}
				else if (this.nodeInfo != null)
				{
					text = this.nodeInfo.TypeName;
					if (text == null || text.Length == 0)
					{
						if (this.nodeInfo.MimeType != null && this.nodeInfo.MimeType.Length > 0)
						{
							object obj = null;
							try
							{
								obj = this.GenerateObjectFromDataNodeInfo(this.nodeInfo, typeResolver);
							}
							catch (Exception ex)
							{
								if (ClientUtils.IsCriticalException(ex))
								{
									throw;
								}
								text = MultitargetUtil.GetAssemblyQualifiedName(typeof(object), this.typeNameConverter);
							}
							if (obj != null)
							{
								text = MultitargetUtil.GetAssemblyQualifiedName(obj.GetType(), this.typeNameConverter);
							}
						}
						else
						{
							text = MultitargetUtil.GetAssemblyQualifiedName(typeof(string), this.typeNameConverter);
						}
					}
					else
					{
						type = this.ResolveType(this.nodeInfo.TypeName, typeResolver);
					}
				}
				if (type != null)
				{
					if (type == typeof(ResXNullRef))
					{
						text = MultitargetUtil.GetAssemblyQualifiedName(typeof(object), this.typeNameConverter);
					}
					else
					{
						text = MultitargetUtil.GetAssemblyQualifiedName(type, this.typeNameConverter);
					}
				}
				return text;
			}
			if (this.typeName.Equals(MultitargetUtil.GetAssemblyQualifiedName(typeof(ResXNullRef), this.typeNameConverter)))
			{
				return MultitargetUtil.GetAssemblyQualifiedName(typeof(object), this.typeNameConverter);
			}
			return this.typeName;
		}

		/// <summary>Retrieves the type name for the value by examining the specified assemblies.</summary>
		/// <param name="names">The assemblies to examine for the type.</param>
		/// <returns>A string that represents the fully qualified name of the type.</returns>
		/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found.</exception>
		// Token: 0x06000345 RID: 837 RVA: 0x00009A84 File Offset: 0x00007C84
		public string GetValueTypeName(AssemblyName[] names)
		{
			return this.GetValueTypeName(new AssemblyNamesTypeResolutionService(names));
		}

		/// <summary>Retrieves the object that is stored by this node by using the specified type resolution service.</summary>
		/// <param name="typeResolver">The type resolution service to use when looking for a type converter.</param>
		/// <returns>The object that corresponds to the stored value.</returns>
		/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found, or an appropriate type converter is not available.</exception>
		// Token: 0x06000346 RID: 838 RVA: 0x00009A94 File Offset: 0x00007C94
		public object GetValue(ITypeResolutionService typeResolver)
		{
			if (this.value != null)
			{
				return this.value;
			}
			object obj = null;
			if (this.FileRefFullPath != null)
			{
				Type type = this.ResolveType(this.FileRefType, typeResolver);
				if (!(type != null))
				{
					string @string = SR.GetString("TypeLoadExceptionShort", new object[] { this.FileRefType });
					TypeLoadException ex = new TypeLoadException(@string);
					throw ex;
				}
				if (this.FileRefTextEncoding != null)
				{
					this.fileRef = new ResXFileRef(this.FileRefFullPath, this.FileRefType, Encoding.GetEncoding(this.FileRefTextEncoding));
				}
				else
				{
					this.fileRef = new ResXFileRef(this.FileRefFullPath, this.FileRefType);
				}
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(ResXFileRef));
				obj = converter.ConvertFrom(this.fileRef.ToString());
			}
			else
			{
				if (obj != null || this.nodeInfo.ValueData == null)
				{
					return null;
				}
				obj = this.GenerateObjectFromDataNodeInfo(this.nodeInfo, typeResolver);
			}
			return obj;
		}

		/// <summary>Retrieves the object that is stored by this node by searching the specified assemblies.</summary>
		/// <param name="names">The list of assemblies to search for the type of the object.</param>
		/// <returns>The object that corresponds to the stored value.</returns>
		/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found, or an appropriate type converter is not available.</exception>
		// Token: 0x06000347 RID: 839 RVA: 0x00009B83 File Offset: 0x00007D83
		public object GetValue(AssemblyName[] names)
		{
			return this.GetValue(new AssemblyNamesTypeResolutionService(names));
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00009B94 File Offset: 0x00007D94
		private static byte[] FromBase64WrappedString(string text)
		{
			if (text.IndexOfAny(ResXDataNode.SpecialChars) != -1)
			{
				StringBuilder stringBuilder = new StringBuilder(text.Length);
				for (int i = 0; i < text.Length; i++)
				{
					char c = text[i];
					if (c != '\n' && c != '\r' && c != ' ')
					{
						stringBuilder.Append(text[i]);
					}
				}
				return Convert.FromBase64String(stringBuilder.ToString());
			}
			return Convert.FromBase64String(text);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00009C04 File Offset: 0x00007E04
		private Type ResolveType(string typeName, ITypeResolutionService typeResolver)
		{
			Type type = null;
			if (typeResolver != null)
			{
				type = typeResolver.GetType(typeName, false);
				if (type == null)
				{
					string[] array = typeName.Split(new char[] { ',' });
					if (array != null && array.Length >= 2)
					{
						string text = array[0].Trim();
						string text2 = array[1].Trim();
						text = text + ", " + text2;
						type = typeResolver.GetType(text, false);
					}
				}
			}
			if (type == null)
			{
				type = Type.GetType(typeName, false);
			}
			return type;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the target object.</summary>
		/// <param name="si">An  object to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x0600034A RID: 842 RVA: 0x00009C80 File Offset: 0x00007E80
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			DataNodeInfo dataNodeInfo = this.GetDataNodeInfo();
			si.AddValue("Name", dataNodeInfo.Name, typeof(string));
			si.AddValue("Comment", dataNodeInfo.Comment, typeof(string));
			si.AddValue("TypeName", dataNodeInfo.TypeName, typeof(string));
			si.AddValue("MimeType", dataNodeInfo.MimeType, typeof(string));
			si.AddValue("ValueData", dataNodeInfo.ValueData, typeof(string));
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00009D1C File Offset: 0x00007F1C
		private ResXDataNode(SerializationInfo info, StreamingContext context)
		{
			this.nodeInfo = new DataNodeInfo
			{
				Name = (string)info.GetValue("Name", typeof(string)),
				Comment = (string)info.GetValue("Comment", typeof(string)),
				TypeName = (string)info.GetValue("TypeName", typeof(string)),
				MimeType = (string)info.GetValue("MimeType", typeof(string)),
				ValueData = (string)info.GetValue("ValueData", typeof(string))
			};
			this.InitializeDataNode(null);
		}

		// Token: 0x040003AB RID: 939
		private static readonly char[] SpecialChars = new char[] { ' ', '\r', '\n' };

		// Token: 0x040003AC RID: 940
		private DataNodeInfo nodeInfo;

		// Token: 0x040003AD RID: 941
		private string name;

		// Token: 0x040003AE RID: 942
		private string comment;

		// Token: 0x040003AF RID: 943
		private string typeName;

		// Token: 0x040003B0 RID: 944
		private string fileRefFullPath;

		// Token: 0x040003B1 RID: 945
		private string fileRefType;

		// Token: 0x040003B2 RID: 946
		private string fileRefTextEncoding;

		// Token: 0x040003B3 RID: 947
		private object value;

		// Token: 0x040003B4 RID: 948
		private ResXFileRef fileRef;

		// Token: 0x040003B5 RID: 949
		private IFormatter binaryFormatter;

		// Token: 0x040003B6 RID: 950
		private static ITypeResolutionService internalTypeResolver = new AssemblyNamesTypeResolutionService(new AssemblyName[]
		{
			new AssemblyName("System.Windows.Forms")
		});

		// Token: 0x040003B7 RID: 951
		private Func<Type, string> typeNameConverter;
	}
}
