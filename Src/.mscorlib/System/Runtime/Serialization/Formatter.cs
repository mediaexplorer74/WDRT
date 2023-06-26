using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Provides base functionality for the common language runtime serialization formatters.</summary>
	// Token: 0x02000746 RID: 1862
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class Formatter : IFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatter" /> class.</summary>
		// Token: 0x06005254 RID: 21076 RVA: 0x0012243D File Offset: 0x0012063D
		protected Formatter()
		{
			this.m_objectQueue = new Queue();
			this.m_idGenerator = new ObjectIDGenerator();
		}

		/// <summary>When overridden in a derived class, deserializes the stream attached to the formatter when it was created, creating a graph of objects identical to the graph originally serialized into that stream.</summary>
		/// <param name="serializationStream">The stream to deserialize.</param>
		/// <returns>The top object of the deserialized graph of objects.</returns>
		// Token: 0x06005255 RID: 21077
		public abstract object Deserialize(Stream serializationStream);

		/// <summary>Returns the next object to serialize, from the formatter's internal work queue.</summary>
		/// <param name="objID">The ID assigned to the current object during serialization.</param>
		/// <returns>The next object to serialize.</returns>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The next object retrieved from the work queue did not have an assigned ID.</exception>
		// Token: 0x06005256 RID: 21078 RVA: 0x0012245C File Offset: 0x0012065C
		protected virtual object GetNext(out long objID)
		{
			if (this.m_objectQueue.Count == 0)
			{
				objID = 0L;
				return null;
			}
			object obj = this.m_objectQueue.Dequeue();
			bool flag;
			objID = this.m_idGenerator.HasId(obj, out flag);
			if (flag)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_NoID"));
			}
			return obj;
		}

		/// <summary>Schedules an object for later serialization.</summary>
		/// <param name="obj">The object to schedule for serialization.</param>
		/// <returns>The object ID assigned to the object.</returns>
		// Token: 0x06005257 RID: 21079 RVA: 0x001224AC File Offset: 0x001206AC
		protected virtual long Schedule(object obj)
		{
			if (obj == null)
			{
				return 0L;
			}
			bool flag;
			long id = this.m_idGenerator.GetId(obj, out flag);
			if (flag)
			{
				this.m_objectQueue.Enqueue(obj);
			}
			return id;
		}

		/// <summary>When overridden in a derived class, serializes the graph of objects with the specified root to the stream already attached to the formatter.</summary>
		/// <param name="serializationStream">The stream to which the objects are serialized.</param>
		/// <param name="graph">The object at the root of the graph to serialize.</param>
		// Token: 0x06005258 RID: 21080
		public abstract void Serialize(Stream serializationStream, object graph);

		/// <summary>When overridden in a derived class, writes an array to the stream already attached to the formatter.</summary>
		/// <param name="obj">The array to write.</param>
		/// <param name="name">The name of the array.</param>
		/// <param name="memberType">The type of elements that the array holds.</param>
		// Token: 0x06005259 RID: 21081
		protected abstract void WriteArray(object obj, string name, Type memberType);

		/// <summary>When overridden in a derived class, writes a Boolean value to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x0600525A RID: 21082
		protected abstract void WriteBoolean(bool val, string name);

		/// <summary>When overridden in a derived class, writes an 8-bit unsigned integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x0600525B RID: 21083
		protected abstract void WriteByte(byte val, string name);

		/// <summary>When overridden in a derived class, writes a Unicode character to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x0600525C RID: 21084
		protected abstract void WriteChar(char val, string name);

		/// <summary>When overridden in a derived class, writes a <see cref="T:System.DateTime" /> value to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x0600525D RID: 21085
		protected abstract void WriteDateTime(DateTime val, string name);

		/// <summary>When overridden in a derived class, writes a <see cref="T:System.Decimal" /> value to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x0600525E RID: 21086
		protected abstract void WriteDecimal(decimal val, string name);

		/// <summary>When overridden in a derived class, writes a double-precision floating-point number to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x0600525F RID: 21087
		protected abstract void WriteDouble(double val, string name);

		/// <summary>When overridden in a derived class, writes a 16-bit signed integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x06005260 RID: 21088
		protected abstract void WriteInt16(short val, string name);

		/// <summary>When overridden in a derived class, writes a 32-bit signed integer to the stream.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x06005261 RID: 21089
		protected abstract void WriteInt32(int val, string name);

		/// <summary>When overridden in a derived class, writes a 64-bit signed integer to the stream.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x06005262 RID: 21090
		protected abstract void WriteInt64(long val, string name);

		/// <summary>When overridden in a derived class, writes an object reference to the stream already attached to the formatter.</summary>
		/// <param name="obj">The object reference to write.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="memberType">The type of object the reference points to.</param>
		// Token: 0x06005263 RID: 21091
		protected abstract void WriteObjectRef(object obj, string name, Type memberType);

		/// <summary>Inspects the type of data received, and calls the appropriate <see langword="Write" /> method to perform the write to the stream already attached to the formatter.</summary>
		/// <param name="memberName">The name of the member to serialize.</param>
		/// <param name="data">The object to write to the stream attached to the formatter.</param>
		// Token: 0x06005264 RID: 21092 RVA: 0x001224E0 File Offset: 0x001206E0
		protected virtual void WriteMember(string memberName, object data)
		{
			if (data == null)
			{
				this.WriteObjectRef(data, memberName, typeof(object));
				return;
			}
			Type type = data.GetType();
			if (type == typeof(bool))
			{
				this.WriteBoolean(Convert.ToBoolean(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(char))
			{
				this.WriteChar(Convert.ToChar(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(sbyte))
			{
				this.WriteSByte(Convert.ToSByte(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(byte))
			{
				this.WriteByte(Convert.ToByte(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(short))
			{
				this.WriteInt16(Convert.ToInt16(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(int))
			{
				this.WriteInt32(Convert.ToInt32(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(long))
			{
				this.WriteInt64(Convert.ToInt64(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(float))
			{
				this.WriteSingle(Convert.ToSingle(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(double))
			{
				this.WriteDouble(Convert.ToDouble(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(DateTime))
			{
				this.WriteDateTime(Convert.ToDateTime(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(decimal))
			{
				this.WriteDecimal(Convert.ToDecimal(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(ushort))
			{
				this.WriteUInt16(Convert.ToUInt16(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(uint))
			{
				this.WriteUInt32(Convert.ToUInt32(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(ulong))
			{
				this.WriteUInt64(Convert.ToUInt64(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type.IsArray)
			{
				this.WriteArray(data, memberName, type);
				return;
			}
			if (type.IsValueType)
			{
				this.WriteValueType(data, memberName, type);
				return;
			}
			this.WriteObjectRef(data, memberName, type);
		}

		/// <summary>When overridden in a derived class, writes an 8-bit signed integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x06005265 RID: 21093
		[CLSCompliant(false)]
		protected abstract void WriteSByte(sbyte val, string name);

		/// <summary>When overridden in a derived class, writes a single-precision floating-point number to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x06005266 RID: 21094
		protected abstract void WriteSingle(float val, string name);

		/// <summary>When overridden in a derived class, writes a <see cref="T:System.TimeSpan" /> value to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x06005267 RID: 21095
		protected abstract void WriteTimeSpan(TimeSpan val, string name);

		/// <summary>When overridden in a derived class, writes a 16-bit unsigned integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x06005268 RID: 21096
		[CLSCompliant(false)]
		protected abstract void WriteUInt16(ushort val, string name);

		/// <summary>When overridden in a derived class, writes a 32-bit unsigned integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x06005269 RID: 21097
		[CLSCompliant(false)]
		protected abstract void WriteUInt32(uint val, string name);

		/// <summary>When overridden in a derived class, writes a 64-bit unsigned integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write.</param>
		/// <param name="name">The name of the member.</param>
		// Token: 0x0600526A RID: 21098
		[CLSCompliant(false)]
		protected abstract void WriteUInt64(ulong val, string name);

		/// <summary>When overridden in a derived class, writes a value of the given type to the stream already attached to the formatter.</summary>
		/// <param name="obj">The object representing the value type.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="memberType">The <see cref="T:System.Type" /> of the value type.</param>
		// Token: 0x0600526B RID: 21099
		protected abstract void WriteValueType(object obj, string name, Type memberType);

		/// <summary>When overridden in a derived class, gets or sets the <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> used with the current formatter.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> used with the current formatter.</returns>
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600526C RID: 21100
		// (set) Token: 0x0600526D RID: 21101
		public abstract ISurrogateSelector SurrogateSelector { get; set; }

		/// <summary>When overridden in a derived class, gets or sets the <see cref="T:System.Runtime.Serialization.SerializationBinder" /> used with the current formatter.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SerializationBinder" /> used with the current formatter.</returns>
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x0600526E RID: 21102
		// (set) Token: 0x0600526F RID: 21103
		public abstract SerializationBinder Binder { get; set; }

		/// <summary>When overridden in a derived class, gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for the current serialization.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for the current serialization.</returns>
		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06005270 RID: 21104
		// (set) Token: 0x06005271 RID: 21105
		public abstract StreamingContext Context { get; set; }

		/// <summary>Contains the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> used with the current formatter.</summary>
		// Token: 0x04002475 RID: 9333
		protected ObjectIDGenerator m_idGenerator;

		/// <summary>Contains a <see cref="T:System.Collections.Queue" /> of the objects left to serialize.</summary>
		// Token: 0x04002476 RID: 9334
		protected Queue m_objectQueue;
	}
}
