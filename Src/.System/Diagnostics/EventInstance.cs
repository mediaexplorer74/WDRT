using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Represents language-neutral information for an event log entry.</summary>
	// Token: 0x020004C9 RID: 1225
	public class EventInstance
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventInstance" /> class using the specified resource identifiers for the localized message and category text of the event entry.</summary>
		/// <param name="instanceId">A resource identifier that corresponds to a string defined in the message resource file of the event source.</param>
		/// <param name="categoryId">A resource identifier that corresponds to a string defined in the category resource file of the event source, or zero to specify no category for the event.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="instanceId" /> parameter is a negative value or a value larger than <see cref="F:System.UInt32.MaxValue" />.  
		///  -or-  
		///  The <paramref name="categoryId" /> parameter is a negative value or a value larger than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x06002D9F RID: 11679 RVA: 0x000CD431 File Offset: 0x000CB631
		public EventInstance(long instanceId, int categoryId)
		{
			this.CategoryId = categoryId;
			this.InstanceId = instanceId;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventInstance" /> class using the specified resource identifiers for the localized message and category text of the event entry and the specified event log entry type.</summary>
		/// <param name="instanceId">A resource identifier that corresponds to a string defined in the message resource file of the event source.</param>
		/// <param name="categoryId">A resource identifier that corresponds to a string defined in the category resource file of the event source, or zero to specify no category for the event.</param>
		/// <param name="entryType">An <see cref="T:System.Diagnostics.EventLogEntryType" /> value that indicates the event type.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="entryType" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" /> value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="instanceId" /> is a negative value or a value larger than <see cref="F:System.UInt32.MaxValue" />.  
		/// -or-  
		/// <paramref name="categoryId" /> is a negative value or a value larger than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x06002DA0 RID: 11680 RVA: 0x000CD44E File Offset: 0x000CB64E
		public EventInstance(long instanceId, int categoryId, EventLogEntryType entryType)
			: this(instanceId, categoryId)
		{
			this.EntryType = entryType;
		}

		/// <summary>Gets or sets the resource identifier that specifies the application-defined category of the event entry.</summary>
		/// <returns>A numeric category value or resource identifier that corresponds to a string defined in the category resource file of the event source. The default is zero, which signifies that no category will be displayed for the event entry.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000CD45F File Offset: 0x000CB65F
		// (set) Token: 0x06002DA2 RID: 11682 RVA: 0x000CD467 File Offset: 0x000CB667
		public int CategoryId
		{
			get
			{
				return this._categoryNumber;
			}
			set
			{
				if (value > 65535 || value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._categoryNumber = value;
			}
		}

		/// <summary>Gets or sets the event type of the event log entry.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntryType" /> value that indicates the event entry type. The default value is <see cref="F:System.Diagnostics.EventLogEntryType.Information" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property is not set to a valid <see cref="T:System.Diagnostics.EventLogEntryType" /> value.</exception>
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x000CD487 File Offset: 0x000CB687
		// (set) Token: 0x06002DA4 RID: 11684 RVA: 0x000CD48F File Offset: 0x000CB68F
		public EventLogEntryType EntryType
		{
			get
			{
				return this._entryType;
			}
			set
			{
				if (!Enum.IsDefined(typeof(EventLogEntryType), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(EventLogEntryType));
				}
				this._entryType = value;
			}
		}

		/// <summary>Gets or sets the resource identifier that designates the message text of the event entry.</summary>
		/// <returns>A resource identifier that corresponds to a string defined in the message resource file of the event source.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x000CD4C5 File Offset: 0x000CB6C5
		// (set) Token: 0x06002DA6 RID: 11686 RVA: 0x000CD4CD File Offset: 0x000CB6CD
		public long InstanceId
		{
			get
			{
				return this._instanceId;
			}
			set
			{
				if (value > (long)((ulong)(-1)) || value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._instanceId = value;
			}
		}

		// Token: 0x0400271D RID: 10013
		private int _categoryNumber;

		// Token: 0x0400271E RID: 10014
		private EventLogEntryType _entryType = EventLogEntryType.Information;

		// Token: 0x0400271F RID: 10015
		private long _instanceId;
	}
}
