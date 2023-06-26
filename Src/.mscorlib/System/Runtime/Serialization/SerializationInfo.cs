using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Serialization
{
	/// <summary>Stores all the data needed to serialize or deserialize an object. This class cannot be inherited.</summary>
	// Token: 0x0200073F RID: 1855
	[ComVisible(true)]
	public sealed class SerializationInfo
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize.</param>
		/// <param name="converter">The <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> used during deserialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="converter" /> is <see langword="null" />.</exception>
		// Token: 0x06005202 RID: 20994 RVA: 0x001217B2 File Offset: 0x0011F9B2
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter)
			: this(type, converter, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize.</param>
		/// <param name="converter">The <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> used during deserialization.</param>
		/// <param name="requireSameTokenInPartialTrust">Indicates whether the object requires same token in partial trust.</param>
		// Token: 0x06005203 RID: 20995 RVA: 0x001217C0 File Offset: 0x0011F9C0
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter, bool requireSameTokenInPartialTrust)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			this.objectType = type;
			this.m_fullTypeName = type.FullName;
			this.m_assemName = type.Module.Assembly.FullName;
			this.m_members = new string[4];
			this.m_data = new object[4];
			this.m_types = new Type[4];
			this.m_nameToIndex = new Dictionary<string, int>();
			this.m_converter = converter;
			this.requireSameTokenInPartialTrust = requireSameTokenInPartialTrust;
		}

		/// <summary>Gets or sets the full name of the <see cref="T:System.Type" /> to serialize.</summary>
		/// <returns>The full name of the type to serialize.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value this property is set to is <see langword="null" />.</exception>
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06005204 RID: 20996 RVA: 0x00121855 File Offset: 0x0011FA55
		// (set) Token: 0x06005205 RID: 20997 RVA: 0x0012185D File Offset: 0x0011FA5D
		public string FullTypeName
		{
			get
			{
				return this.m_fullTypeName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_fullTypeName = value;
				this.isFullTypeNameSetExplicit = true;
			}
		}

		/// <summary>Gets or sets the assembly name of the type to serialize during serialization only.</summary>
		/// <returns>The full name of the assembly of the type to serialize.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value the property is set to is <see langword="null" />.</exception>
		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06005206 RID: 20998 RVA: 0x0012187B File Offset: 0x0011FA7B
		// (set) Token: 0x06005207 RID: 20999 RVA: 0x00121883 File Offset: 0x0011FA83
		public string AssemblyName
		{
			get
			{
				return this.m_assemName;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.requireSameTokenInPartialTrust)
				{
					SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.m_assemName, value);
				}
				this.m_assemName = value;
				this.isAssemblyNameSetExplicit = true;
			}
		}

		/// <summary>Sets the <see cref="T:System.Type" /> of the object to serialize.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005208 RID: 21000 RVA: 0x001218B8 File Offset: 0x0011FAB8
		[SecuritySafeCritical]
		public void SetType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this.requireSameTokenInPartialTrust)
			{
				SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.ObjectType.Assembly.FullName, type.Assembly.FullName);
			}
			if (this.objectType != type)
			{
				this.objectType = type;
				this.m_fullTypeName = type.FullName;
				this.m_assemName = type.Module.Assembly.FullName;
				this.isFullTypeNameSetExplicit = false;
				this.isAssemblyNameSetExplicit = false;
			}
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x0012193C File Offset: 0x0011FB3C
		private static bool Compare(byte[] a, byte[] b)
		{
			if (a == null || b == null || a.Length == 0 || b.Length == 0 || a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x0012197A File Offset: 0x0011FB7A
		[SecuritySafeCritical]
		internal static void DemandForUnsafeAssemblyNameAssignments(string originalAssemblyName, string newAssemblyName)
		{
			if (!SerializationInfo.IsAssemblyNameAssignmentSafe(originalAssemblyName, newAssemblyName))
			{
				CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
			}
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x0012198C File Offset: 0x0011FB8C
		internal static bool IsAssemblyNameAssignmentSafe(string originalAssemblyName, string newAssemblyName)
		{
			if (originalAssemblyName == newAssemblyName)
			{
				return true;
			}
			AssemblyName assemblyName = new AssemblyName(originalAssemblyName);
			AssemblyName assemblyName2 = new AssemblyName(newAssemblyName);
			return !string.Equals(assemblyName2.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) && !string.Equals(assemblyName2.Name, "mscorlib.dll", StringComparison.OrdinalIgnoreCase) && SerializationInfo.Compare(assemblyName.GetPublicKeyToken(), assemblyName2.GetPublicKeyToken());
		}

		/// <summary>Gets the number of members that have been added to the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The number of members that have been added to the current <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</returns>
		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x0600520C RID: 21004 RVA: 0x001219EB File Offset: 0x0011FBEB
		public int MemberCount
		{
			get
			{
				return this.m_currMember;
			}
		}

		/// <summary>Returns the type of the object to be serialized.</summary>
		/// <returns>The type of the object being serialized.</returns>
		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x0600520D RID: 21005 RVA: 0x001219F3 File Offset: 0x0011FBF3
		public Type ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		/// <summary>Gets whether the full type name has been explicitly set.</summary>
		/// <returns>
		///   <see langword="true" /> if the full type name has been explicitly set; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x0600520E RID: 21006 RVA: 0x001219FB File Offset: 0x0011FBFB
		public bool IsFullTypeNameSetExplicit
		{
			get
			{
				return this.isFullTypeNameSetExplicit;
			}
		}

		/// <summary>Gets whether the assembly name has been explicitly set.</summary>
		/// <returns>
		///   <see langword="true" /> if the assembly name has been explicitly set; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x0600520F RID: 21007 RVA: 0x00121A03 File Offset: 0x0011FC03
		public bool IsAssemblyNameSetExplicit
		{
			get
			{
				return this.isAssemblyNameSetExplicit;
			}
		}

		/// <summary>Returns a <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" /> used to iterate through the name-value pairs in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" /> for parsing the name-value pairs contained in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</returns>
		// Token: 0x06005210 RID: 21008 RVA: 0x00121A0B File Offset: 0x0011FC0B
		public SerializationInfoEnumerator GetEnumerator()
		{
			return new SerializationInfoEnumerator(this.m_members, this.m_data, this.m_types, this.m_currMember);
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x00121A2C File Offset: 0x0011FC2C
		private void ExpandArrays()
		{
			int num = this.m_currMember * 2;
			if (num < this.m_currMember && 2147483647 > this.m_currMember)
			{
				num = int.MaxValue;
			}
			string[] array = new string[num];
			object[] array2 = new object[num];
			Type[] array3 = new Type[num];
			Array.Copy(this.m_members, array, this.m_currMember);
			Array.Copy(this.m_data, array2, this.m_currMember);
			Array.Copy(this.m_types, array3, this.m_currMember);
			this.m_members = array;
			this.m_data = array2;
			this.m_types = array3;
		}

		/// <summary>Adds a value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store, where <paramref name="value" /> is associated with <paramref name="name" /> and is serialized as being of <see cref="T:System.Type" /><paramref name="type" />.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The value to be serialized. Any children of this object will automatically be serialized.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to associate with the current object. This parameter must always be the type of the object itself or of one of its base classes.</param>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="name" /> or <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x06005212 RID: 21010 RVA: 0x00121ABE File Offset: 0x0011FCBE
		public void AddValue(string name, object value, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.AddValueInternal(name, value, type);
		}

		/// <summary>Adds the specified object into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store, where it is associated with a specified name.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The value to be serialized. Any children of this object will automatically be serialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x06005213 RID: 21011 RVA: 0x00121AE5 File Offset: 0x0011FCE5
		public void AddValue(string name, object value)
		{
			if (value == null)
			{
				this.AddValue(name, value, typeof(object));
				return;
			}
			this.AddValue(name, value, value.GetType());
		}

		/// <summary>Adds a Boolean value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The Boolean value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x06005214 RID: 21012 RVA: 0x00121B0B File Offset: 0x0011FD0B
		public void AddValue(string name, bool value)
		{
			this.AddValue(name, value, typeof(bool));
		}

		/// <summary>Adds a Unicode character value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The character value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x06005215 RID: 21013 RVA: 0x00121B24 File Offset: 0x0011FD24
		public void AddValue(string name, char value)
		{
			this.AddValue(name, value, typeof(char));
		}

		/// <summary>Adds an 8-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The <see langword="Sbyte" /> value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x06005216 RID: 21014 RVA: 0x00121B3D File Offset: 0x0011FD3D
		[CLSCompliant(false)]
		public void AddValue(string name, sbyte value)
		{
			this.AddValue(name, value, typeof(sbyte));
		}

		/// <summary>Adds an 8-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The byte value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x06005217 RID: 21015 RVA: 0x00121B56 File Offset: 0x0011FD56
		public void AddValue(string name, byte value)
		{
			this.AddValue(name, value, typeof(byte));
		}

		/// <summary>Adds a 16-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The <see langword="Int16" /> value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x06005218 RID: 21016 RVA: 0x00121B6F File Offset: 0x0011FD6F
		public void AddValue(string name, short value)
		{
			this.AddValue(name, value, typeof(short));
		}

		/// <summary>Adds a 16-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The <see langword="UInt16" /> value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x06005219 RID: 21017 RVA: 0x00121B88 File Offset: 0x0011FD88
		[CLSCompliant(false)]
		public void AddValue(string name, ushort value)
		{
			this.AddValue(name, value, typeof(ushort));
		}

		/// <summary>Adds a 32-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The <see langword="Int32" /> value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x0600521A RID: 21018 RVA: 0x00121BA1 File Offset: 0x0011FDA1
		public void AddValue(string name, int value)
		{
			this.AddValue(name, value, typeof(int));
		}

		/// <summary>Adds a 32-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The <see langword="UInt32" /> value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x0600521B RID: 21019 RVA: 0x00121BBA File Offset: 0x0011FDBA
		[CLSCompliant(false)]
		public void AddValue(string name, uint value)
		{
			this.AddValue(name, value, typeof(uint));
		}

		/// <summary>Adds a 64-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The Int64 value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x0600521C RID: 21020 RVA: 0x00121BD3 File Offset: 0x0011FDD3
		public void AddValue(string name, long value)
		{
			this.AddValue(name, value, typeof(long));
		}

		/// <summary>Adds a 64-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The <see langword="UInt64" /> value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x0600521D RID: 21021 RVA: 0x00121BEC File Offset: 0x0011FDEC
		[CLSCompliant(false)]
		public void AddValue(string name, ulong value)
		{
			this.AddValue(name, value, typeof(ulong));
		}

		/// <summary>Adds a single-precision floating-point value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The single value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x0600521E RID: 21022 RVA: 0x00121C05 File Offset: 0x0011FE05
		public void AddValue(string name, float value)
		{
			this.AddValue(name, value, typeof(float));
		}

		/// <summary>Adds a double-precision floating-point value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The double value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x0600521F RID: 21023 RVA: 0x00121C1E File Offset: 0x0011FE1E
		public void AddValue(string name, double value)
		{
			this.AddValue(name, value, typeof(double));
		}

		/// <summary>Adds a decimal value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The decimal value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">If The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">If a value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x06005220 RID: 21024 RVA: 0x00121C37 File Offset: 0x0011FE37
		public void AddValue(string name, decimal value)
		{
			this.AddValue(name, value, typeof(decimal));
		}

		/// <summary>Adds a <see cref="T:System.DateTime" /> value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later.</param>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />.</exception>
		// Token: 0x06005221 RID: 21025 RVA: 0x00121C50 File Offset: 0x0011FE50
		public void AddValue(string name, DateTime value)
		{
			this.AddValue(name, value, typeof(DateTime));
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x00121C6C File Offset: 0x0011FE6C
		internal void AddValueInternal(string name, object value, Type type)
		{
			if (this.m_nameToIndex.ContainsKey(name))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_SameNameTwice"));
			}
			this.m_nameToIndex.Add(name, this.m_currMember);
			if (this.m_currMember >= this.m_members.Length)
			{
				this.ExpandArrays();
			}
			this.m_members[this.m_currMember] = name;
			this.m_data[this.m_currMember] = value;
			this.m_types[this.m_currMember] = type;
			this.m_currMember++;
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x00121CF8 File Offset: 0x0011FEF8
		internal void UpdateValue(string name, object value, Type type)
		{
			int num = this.FindElement(name);
			if (num < 0)
			{
				this.AddValueInternal(name, value, type);
				return;
			}
			this.m_data[num] = value;
			this.m_types[num] = type;
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x00121D30 File Offset: 0x0011FF30
		private int FindElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int num;
			if (this.m_nameToIndex.TryGetValue(name, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x00121D60 File Offset: 0x0011FF60
		private object GetElement(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_NotFound", new object[] { name }));
			}
			foundType = this.m_types[num];
			return this.m_data[num];
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x00121DA8 File Offset: 0x0011FFA8
		[ComVisible(true)]
		private object GetElementNoThrow(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				foundType = null;
				return null;
			}
			foundType = this.m_types[num];
			return this.m_data[num];
		}

		/// <summary>Retrieves a value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the value to retrieve. If the stored value cannot be converted to this type, the system will throw a <see cref="T:System.InvalidCastException" />.</param>
		/// <returns>The object of the specified <see cref="T:System.Type" /> associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to <paramref name="type" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x06005227 RID: 21031 RVA: 0x00121DD8 File Offset: 0x0011FFD8
		[SecuritySafeCritical]
		public object GetValue(string name, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			Type type2;
			object element = this.GetElement(name, out type2);
			if (RemotingServices.IsTransparentProxy(element))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(element);
				if (RemotingServices.ProxyCheckCast(realProxy, runtimeType))
				{
					return element;
				}
			}
			else if (type2 == type || type.IsAssignableFrom(type2) || element == null)
			{
				return element;
			}
			return this.m_converter.Convert(element, type);
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x00121E58 File Offset: 0x00120058
		[SecuritySafeCritical]
		[ComVisible(true)]
		internal object GetValueNoThrow(string name, Type type)
		{
			Type type2;
			object elementNoThrow = this.GetElementNoThrow(name, out type2);
			if (elementNoThrow == null)
			{
				return null;
			}
			if (RemotingServices.IsTransparentProxy(elementNoThrow))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(elementNoThrow);
				if (RemotingServices.ProxyCheckCast(realProxy, (RuntimeType)type))
				{
					return elementNoThrow;
				}
			}
			else if (type2 == type || type.IsAssignableFrom(type2) || elementNoThrow == null)
			{
				return elementNoThrow;
			}
			return this.m_converter.Convert(elementNoThrow, type);
		}

		/// <summary>Retrieves a Boolean value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The Boolean value associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a Boolean value.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x06005229 RID: 21033 RVA: 0x00121EB4 File Offset: 0x001200B4
		public bool GetBoolean(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(bool))
			{
				return (bool)element;
			}
			return this.m_converter.ToBoolean(element);
		}

		/// <summary>Retrieves a Unicode character value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The Unicode character associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a Unicode character.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x0600522A RID: 21034 RVA: 0x00121EEC File Offset: 0x001200EC
		public char GetChar(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(char))
			{
				return (char)element;
			}
			return this.m_converter.ToChar(element);
		}

		/// <summary>Retrieves an 8-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The 8-bit signed integer associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to an 8-bit signed integer.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x0600522B RID: 21035 RVA: 0x00121F24 File Offset: 0x00120124
		[CLSCompliant(false)]
		public sbyte GetSByte(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(sbyte))
			{
				return (sbyte)element;
			}
			return this.m_converter.ToSByte(element);
		}

		/// <summary>Retrieves an 8-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The 8-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to an 8-bit unsigned integer.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x0600522C RID: 21036 RVA: 0x00121F5C File Offset: 0x0012015C
		public byte GetByte(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(byte))
			{
				return (byte)element;
			}
			return this.m_converter.ToByte(element);
		}

		/// <summary>Retrieves a 16-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The 16-bit signed integer associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 16-bit signed integer.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x0600522D RID: 21037 RVA: 0x00121F94 File Offset: 0x00120194
		public short GetInt16(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(short))
			{
				return (short)element;
			}
			return this.m_converter.ToInt16(element);
		}

		/// <summary>Retrieves a 16-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The 16-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 16-bit unsigned integer.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x0600522E RID: 21038 RVA: 0x00121FCC File Offset: 0x001201CC
		[CLSCompliant(false)]
		public ushort GetUInt16(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(ushort))
			{
				return (ushort)element;
			}
			return this.m_converter.ToUInt16(element);
		}

		/// <summary>Retrieves a 32-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name of the value to retrieve.</param>
		/// <returns>The 32-bit signed integer associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 32-bit signed integer.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x0600522F RID: 21039 RVA: 0x00122004 File Offset: 0x00120204
		public int GetInt32(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(int))
			{
				return (int)element;
			}
			return this.m_converter.ToInt32(element);
		}

		/// <summary>Retrieves a 32-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The 32-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 32-bit unsigned integer.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x06005230 RID: 21040 RVA: 0x0012203C File Offset: 0x0012023C
		[CLSCompliant(false)]
		public uint GetUInt32(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(uint))
			{
				return (uint)element;
			}
			return this.m_converter.ToUInt32(element);
		}

		/// <summary>Retrieves a 64-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The 64-bit signed integer associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 64-bit signed integer.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x06005231 RID: 21041 RVA: 0x00122074 File Offset: 0x00120274
		public long GetInt64(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(long))
			{
				return (long)element;
			}
			return this.m_converter.ToInt64(element);
		}

		/// <summary>Retrieves a 64-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The 64-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 64-bit unsigned integer.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x06005232 RID: 21042 RVA: 0x001220AC File Offset: 0x001202AC
		[CLSCompliant(false)]
		public ulong GetUInt64(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(ulong))
			{
				return (ulong)element;
			}
			return this.m_converter.ToUInt64(element);
		}

		/// <summary>Retrieves a single-precision floating-point value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name of the value to retrieve.</param>
		/// <returns>The single-precision floating-point value associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a single-precision floating-point value.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x06005233 RID: 21043 RVA: 0x001220E4 File Offset: 0x001202E4
		public float GetSingle(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(float))
			{
				return (float)element;
			}
			return this.m_converter.ToSingle(element);
		}

		/// <summary>Retrieves a double-precision floating-point value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The double-precision floating-point value associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a double-precision floating-point value.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x06005234 RID: 21044 RVA: 0x0012211C File Offset: 0x0012031C
		public double GetDouble(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(double))
			{
				return (double)element;
			}
			return this.m_converter.ToDouble(element);
		}

		/// <summary>Retrieves a decimal value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>A decimal value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a decimal.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x06005235 RID: 21045 RVA: 0x00122154 File Offset: 0x00120354
		public decimal GetDecimal(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(decimal))
			{
				return (decimal)element;
			}
			return this.m_converter.ToDecimal(element);
		}

		/// <summary>Retrieves a <see cref="T:System.DateTime" /> value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The <see cref="T:System.DateTime" /> value associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a <see cref="T:System.DateTime" /> value.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x06005236 RID: 21046 RVA: 0x0012218C File Offset: 0x0012038C
		public DateTime GetDateTime(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(DateTime))
			{
				return (DateTime)element;
			}
			return this.m_converter.ToDateTime(element);
		}

		/// <summary>Retrieves a <see cref="T:System.String" /> value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <returns>The <see cref="T:System.String" /> associated with <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a <see cref="T:System.String" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance.</exception>
		// Token: 0x06005237 RID: 21047 RVA: 0x001221C4 File Offset: 0x001203C4
		public string GetString(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(string) || element == null)
			{
				return (string)element;
			}
			return this.m_converter.ToString(element);
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06005238 RID: 21048 RVA: 0x001221FE File Offset: 0x001203FE
		internal string[] MemberNames
		{
			get
			{
				return this.m_members;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06005239 RID: 21049 RVA: 0x00122206 File Offset: 0x00120406
		internal object[] MemberValues
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x04002451 RID: 9297
		private const int defaultSize = 4;

		// Token: 0x04002452 RID: 9298
		private const string s_mscorlibAssemblySimpleName = "mscorlib";

		// Token: 0x04002453 RID: 9299
		private const string s_mscorlibFileName = "mscorlib.dll";

		// Token: 0x04002454 RID: 9300
		internal string[] m_members;

		// Token: 0x04002455 RID: 9301
		internal object[] m_data;

		// Token: 0x04002456 RID: 9302
		internal Type[] m_types;

		// Token: 0x04002457 RID: 9303
		private Dictionary<string, int> m_nameToIndex;

		// Token: 0x04002458 RID: 9304
		internal int m_currMember;

		// Token: 0x04002459 RID: 9305
		internal IFormatterConverter m_converter;

		// Token: 0x0400245A RID: 9306
		private string m_fullTypeName;

		// Token: 0x0400245B RID: 9307
		private string m_assemName;

		// Token: 0x0400245C RID: 9308
		private Type objectType;

		// Token: 0x0400245D RID: 9309
		private bool isFullTypeNameSetExplicit;

		// Token: 0x0400245E RID: 9310
		private bool isAssemblyNameSetExplicit;

		// Token: 0x0400245F RID: 9311
		private bool requireSameTokenInPartialTrust;
	}
}
