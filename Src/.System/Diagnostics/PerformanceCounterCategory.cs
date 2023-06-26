using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Represents a performance object, which defines a category of performance counters.</summary>
	// Token: 0x020004DE RID: 1246
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SharedState = true)]
	public sealed class PerformanceCounterCategory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> class, leaves the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property empty, and sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> property to the local computer.</summary>
		// Token: 0x06002F0D RID: 12045 RVA: 0x000D3298 File Offset: 0x000D1498
		public PerformanceCounterCategory()
		{
			this.machineName = ".";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> class, sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property to the specified value, and sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> property to the local computer.</summary>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which to associate this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is <see langword="null" />.</exception>
		// Token: 0x06002F0E RID: 12046 RVA: 0x000D32AB File Offset: 0x000D14AB
		public PerformanceCounterCategory(string categoryName)
			: this(categoryName, ".")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> class and sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> and <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> properties to the specified values.</summary>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which to associate this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance.</param>
		/// <param name="machineName">The computer on which the performance counter category and its associated counters exist.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string ("").  
		///  -or-  
		///  The <paramref name="machineName" /> syntax is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is <see langword="null" />.</exception>
		// Token: 0x06002F0F RID: 12047 RVA: 0x000D32BC File Offset: 0x000D14BC
		public PerformanceCounterCategory(string categoryName, string machineName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (categoryName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "categoryName", categoryName }));
			}
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", machineName }));
			}
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machineName, categoryName);
			performanceCounterPermission.Demand();
			this.categoryName = categoryName;
			this.machineName = machineName;
		}

		/// <summary>Gets or sets the name of the performance object that defines this category.</summary>
		/// <returns>The name of the performance counter category, or performance object, with which to associate this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> is <see langword="null" />.</exception>
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x000D334E File Offset: 0x000D154E
		// (set) Token: 0x06002F11 RID: 12049 RVA: 0x000D3358 File Offset: 0x000D1558
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidProperty", new object[] { "CategoryName", value }));
				}
				lock (this)
				{
					PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, this.machineName, value);
					performanceCounterPermission.Demand();
					this.categoryName = value;
				}
			}
		}

		/// <summary>Gets the category's help text.</summary>
		/// <returns>A description of the performance object that this category measures.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is <see langword="null" />. The category name must be set before getting the category help.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x000D33E0 File Offset: 0x000D15E0
		public string CategoryHelp
		{
			get
			{
				if (this.categoryName == null)
				{
					throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
				}
				if (this.categoryHelp == null)
				{
					this.categoryHelp = PerformanceCounterLib.GetCategoryHelp(this.machineName, this.categoryName);
				}
				return this.categoryHelp;
			}
		}

		/// <summary>Gets the performance counter category type.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.PerformanceCounterCategoryType" /> values.</returns>
		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x000D3420 File Offset: 0x000D1620
		public PerformanceCounterCategoryType CategoryType
		{
			get
			{
				CategorySample categorySample = PerformanceCounterLib.GetCategorySample(this.machineName, this.categoryName);
				if (categorySample.IsMultiInstance)
				{
					return PerformanceCounterCategoryType.MultiInstance;
				}
				if (PerformanceCounterLib.IsCustomCategory(".", this.categoryName))
				{
					return PerformanceCounterLib.GetCategoryType(".", this.categoryName);
				}
				return PerformanceCounterCategoryType.SingleInstance;
			}
		}

		/// <summary>Gets or sets the name of the computer on which this category exists.</summary>
		/// <returns>The name of the computer on which the performance counter category and its associated counters exist.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> syntax is invalid.</exception>
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06002F14 RID: 12052 RVA: 0x000D346D File Offset: 0x000D166D
		// (set) Token: 0x06002F15 RID: 12053 RVA: 0x000D3478 File Offset: 0x000D1678
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				if (!SyntaxCheck.CheckMachineName(value))
				{
					throw new ArgumentException(SR.GetString("InvalidProperty", new object[] { "MachineName", value }));
				}
				lock (this)
				{
					if (this.categoryName != null)
					{
						PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, value, this.categoryName);
						performanceCounterPermission.Demand();
					}
					this.machineName = value;
				}
			}
		}

		/// <summary>Determines whether the specified counter is registered to this category, which is indicated by the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> and <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> properties.</summary>
		/// <param name="counterName">The name of the performance counter to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the counter is registered to the category that is specified by the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> and <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> properties; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property has not been set.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F16 RID: 12054 RVA: 0x000D34FC File Offset: 0x000D16FC
		public bool CounterExists(string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			return PerformanceCounterLib.CounterExists(this.machineName, this.categoryName, counterName);
		}

		/// <summary>Determines whether the specified counter is registered to the specified category on the local computer.</summary>
		/// <param name="counterName">The name of the performance counter to look for.</param>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which the specified performance counter is associated.</param>
		/// <returns>
		///   <see langword="true" />, if the counter is registered to the specified category on the local computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">The category name does not exist.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F17 RID: 12055 RVA: 0x000D3536 File Offset: 0x000D1736
		public static bool CounterExists(string counterName, string categoryName)
		{
			return PerformanceCounterCategory.CounterExists(counterName, categoryName, ".");
		}

		/// <summary>Determines whether the specified counter is registered to the specified category on a remote computer.</summary>
		/// <param name="counterName">The name of the performance counter to look for.</param>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which the specified performance counter is associated.</param>
		/// <param name="machineName">The name of the computer on which the performance counter category and its associated counters exist.</param>
		/// <returns>
		///   <see langword="true" />, if the counter is registered to the specified category on the specified computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string ("").  
		///  -or-  
		///  The <paramref name="machineName" /> is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category name does not exist.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F18 RID: 12056 RVA: 0x000D3544 File Offset: 0x000D1744
		public static bool CounterExists(string counterName, string categoryName, string machineName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (categoryName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "categoryName", categoryName }));
			}
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", machineName }));
			}
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machineName, categoryName);
			performanceCounterPermission.Demand();
			return PerformanceCounterLib.CounterExists(machineName, categoryName, counterName);
		}

		/// <summary>Registers a custom performance counter category containing a single counter of type <see langword="NumberOfItems32" /> on the local computer.</summary>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system.</param>
		/// <param name="categoryHelp">A description of the custom category.</param>
		/// <param name="counterName">The name of a new counter, of type <see langword="NumberOfItems32" />, to create as part of the new category.</param>
		/// <param name="counterHelp">A description of the counter that is associated with the new custom category.</param>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new system category, or performance object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="counterName" /> is <see langword="null" /> or is an empty string ("").  
		/// -or-  
		/// The counter that is specified by <paramref name="counterName" /> already exists.  
		/// -or-  
		/// <paramref name="counterName" /> has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="counterHelp" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F19 RID: 12057 RVA: 0x000D35D8 File Offset: 0x000D17D8
		[Obsolete("This method has been deprecated.  Please use System.Diagnostics.PerformanceCounterCategory.Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, string counterName, string counterHelp) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, string counterName, string counterHelp)
		{
			CounterCreationData counterCreationData = new CounterCreationData(counterName, counterHelp, PerformanceCounterType.NumberOfItems32);
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.Unknown, new CounterCreationDataCollection(new CounterCreationData[] { counterCreationData }));
		}

		/// <summary>Registers the custom performance counter category containing a single counter of type <see cref="F:System.Diagnostics.PerformanceCounterType.NumberOfItems32" /> on the local computer.</summary>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system.</param>
		/// <param name="categoryHelp">A description of the custom category.</param>
		/// <param name="categoryType">One of the <see cref="T:System.Diagnostics.PerformanceCounterCategoryType" /> values specifying whether the category is <see cref="F:System.Diagnostics.PerformanceCounterCategoryType.MultiInstance" />, <see cref="F:System.Diagnostics.PerformanceCounterCategoryType.SingleInstance" />, or <see cref="F:System.Diagnostics.PerformanceCounterCategoryType.Unknown" />.</param>
		/// <param name="counterName">The name of a new counter to create as part of the new category.</param>
		/// <param name="counterHelp">A description of the counter that is associated with the new custom category.</param>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new system category, or performance object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="counterName" /> is <see langword="null" /> or is an empty string ("").  
		/// -or-  
		/// The counter that is specified by <paramref name="counterName" /> already exists.  
		/// -or-  
		/// <paramref name="counterName" /> has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="counterHelp" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F1A RID: 12058 RVA: 0x000D360C File Offset: 0x000D180C
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, string counterName, string counterHelp)
		{
			CounterCreationData counterCreationData = new CounterCreationData(counterName, counterHelp, PerformanceCounterType.NumberOfItems32);
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, categoryType, new CounterCreationDataCollection(new CounterCreationData[] { counterCreationData }));
		}

		/// <summary>Registers the custom performance counter category containing the specified counters on the local computer.</summary>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system.</param>
		/// <param name="categoryHelp">A description of the custom category.</param>
		/// <param name="counterData">A <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> that specifies the counters to create as part of the new category.</param>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new custom category, or performance object.</returns>
		/// <exception cref="T:System.ArgumentException">A counter name that is specified within the <paramref name="counterData" /> collection is <see langword="null" /> or an empty string ("").  
		///  -or-  
		///  A counter that is specified within the <paramref name="counterData" /> collection already exists.  
		///  -or-  
		///  The <paramref name="counterName" /> parameter has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer.  
		///  -or-  
		///  The layout of the <paramref name="counterData" /> collection is incorrect for base counters. A counter of type <see langword="AverageCount64" />, <see langword="AverageTimer32" />, <see langword="CounterMultiTimer" />, <see langword="CounterMultiTimerInverse" />, <see langword="CounterMultiTimer100Ns" />, <see langword="CounterMultiTimer100NsInverse" />, <see langword="RawFraction" />, <see langword="SampleFraction" /> or <see langword="SampleCounter" /> has to be immediately followed by one of the base counter types (<see langword="AverageBase" />, <see langword="MultiBase" />, <see langword="RawBase" />, or <see langword="SampleBase" />).</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F1B RID: 12059 RVA: 0x000D363E File Offset: 0x000D183E
		[Obsolete("This method has been deprecated.  Please use System.Diagnostics.PerformanceCounterCategory.Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, CounterCreationDataCollection counterData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, CounterCreationDataCollection counterData)
		{
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.Unknown, counterData);
		}

		/// <summary>Registers the custom performance counter category containing the specified counters on the local computer.</summary>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system.</param>
		/// <param name="categoryHelp">A description of the custom category.</param>
		/// <param name="categoryType">One of the <see cref="T:System.Diagnostics.PerformanceCounterCategoryType" /> values.</param>
		/// <param name="counterData">A <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> that specifies the counters to create as part of the new category.</param>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new custom category, or performance object.</returns>
		/// <exception cref="T:System.ArgumentException">A counter name that is specified within the <paramref name="counterData" /> collection is <see langword="null" /> or an empty string ("").  
		///  -or-  
		///  A counter that is specified within the <paramref name="counterData" /> collection already exists.  
		///  -or-  
		///  <paramref name="counterName" /> has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="counterData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="categoryType" /> value is outside of the range of the following values: <see langword="MultiInstance" />, <see langword="SingleInstance" />, or <see langword="Unknown" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer.  
		///  -or-  
		///  The layout of the <paramref name="counterData" /> collection is incorrect for base counters. A counter of type <see langword="AverageCount64" />, <see langword="AverageTimer32" />, <see langword="CounterMultiTimer" />, <see langword="CounterMultiTimerInverse" />, <see langword="CounterMultiTimer100Ns" />, <see langword="CounterMultiTimer100NsInverse" />, <see langword="RawFraction" />, <see langword="SampleFraction" />, or <see langword="SampleCounter" /> must be immediately followed by one of the base counter types (<see langword="AverageBase" />, <see langword="MultiBase" />, <see langword="RawBase" />, or <see langword="SampleBase" />).</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F1C RID: 12060 RVA: 0x000D364C File Offset: 0x000D184C
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, CounterCreationDataCollection counterData)
		{
			if (categoryType < PerformanceCounterCategoryType.Unknown || categoryType > PerformanceCounterCategoryType.MultiInstance)
			{
				throw new ArgumentOutOfRangeException("categoryType");
			}
			if (counterData == null)
			{
				throw new ArgumentNullException("counterData");
			}
			PerformanceCounterCategory.CheckValidCategory(categoryName);
			if (categoryHelp != null)
			{
				PerformanceCounterCategory.CheckValidHelp(categoryHelp);
			}
			string text = ".";
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Administer, text, categoryName);
			performanceCounterPermission.Demand();
			SharedUtils.CheckNtEnvironment();
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			PerformanceCounterCategory performanceCounterCategory;
			try
			{
				SharedUtils.EnterMutex("netfxperf.1.0", ref mutex);
				if (PerformanceCounterLib.IsCustomCategory(text, categoryName) || PerformanceCounterLib.CategoryExists(text, categoryName))
				{
					throw new InvalidOperationException(SR.GetString("PerformanceCategoryExists", new object[] { categoryName }));
				}
				PerformanceCounterCategory.CheckValidCounterLayout(counterData);
				PerformanceCounterLib.RegisterCategory(categoryName, categoryType, categoryHelp, counterData);
				performanceCounterCategory = new PerformanceCounterCategory(categoryName, text);
			}
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
					mutex.Close();
				}
			}
			return performanceCounterCategory;
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000D371C File Offset: 0x000D191C
		internal static void CheckValidCategory(string categoryName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (!PerformanceCounterCategory.CheckValidId(categoryName, 80))
			{
				throw new ArgumentException(SR.GetString("PerfInvalidCategoryName", new object[] { 1, 80 }));
			}
			if (categoryName.Length > 1024 - "netfxcustomperfcounters.1.0".Length)
			{
				throw new ArgumentException(SR.GetString("CategoryNameTooLong"));
			}
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000D3794 File Offset: 0x000D1994
		internal static void CheckValidCounter(string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			if (!PerformanceCounterCategory.CheckValidId(counterName, 32767))
			{
				throw new ArgumentException(SR.GetString("PerfInvalidCounterName", new object[] { 1, 32767 }));
			}
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000D37E8 File Offset: 0x000D19E8
		internal static bool CheckValidId(string id, int maxLength)
		{
			if (id.Length == 0 || id.Length > maxLength)
			{
				return false;
			}
			for (int i = 0; i < id.Length; i++)
			{
				char c = id[i];
				if ((i == 0 || i == id.Length - 1) && c == ' ')
				{
					return false;
				}
				if (c == '"')
				{
					return false;
				}
				if (char.IsControl(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000D3848 File Offset: 0x000D1A48
		internal static void CheckValidHelp(string help)
		{
			if (help == null)
			{
				throw new ArgumentNullException("help");
			}
			if (help.Length > 32767)
			{
				throw new ArgumentException(SR.GetString("PerfInvalidHelp", new object[] { 0, 32767 }));
			}
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000D389C File Offset: 0x000D1A9C
		internal static void CheckValidCounterLayout(CounterCreationDataCollection counterData)
		{
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < counterData.Count; i++)
			{
				if (counterData[i].CounterName == null || counterData[i].CounterName.Length == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidCounterName"));
				}
				int num = (int)counterData[i].CounterType;
				if (num == 1073874176 || num == 575735040 || num == 592512256 || num == 574686464 || num == 591463680 || num == 537003008 || num == 549585920 || num == 805438464)
				{
					if (counterData.Count <= i + 1)
					{
						throw new InvalidOperationException(SR.GetString("CounterLayout"));
					}
					num = (int)counterData[i + 1].CounterType;
					if (!PerformanceCounterLib.IsBaseCounter(num))
					{
						throw new InvalidOperationException(SR.GetString("CounterLayout"));
					}
				}
				else if (PerformanceCounterLib.IsBaseCounter(num))
				{
					if (i == 0)
					{
						throw new InvalidOperationException(SR.GetString("CounterLayout"));
					}
					num = (int)counterData[i - 1].CounterType;
					if (num != 1073874176 && num != 575735040 && num != 592512256 && num != 574686464 && num != 591463680 && num != 537003008 && num != 549585920 && num != 805438464)
					{
						throw new InvalidOperationException(SR.GetString("CounterLayout"));
					}
				}
				if (hashtable.ContainsKey(counterData[i].CounterName))
				{
					throw new ArgumentException(SR.GetString("DuplicateCounterName", new object[] { counterData[i].CounterName }));
				}
				hashtable.Add(counterData[i].CounterName, string.Empty);
				if (counterData[i].CounterHelp == null || counterData[i].CounterHelp.Length == 0)
				{
					counterData[i].CounterHelp = counterData[i].CounterName;
				}
			}
		}

		/// <summary>Removes the category and its associated counters from the local computer.</summary>
		/// <param name="categoryName">The name of the custom performance counter category to delete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category cannot be deleted because it is not a custom category.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F22 RID: 12066 RVA: 0x000D3A8C File Offset: 0x000D1C8C
		public static void Delete(string categoryName)
		{
			PerformanceCounterCategory.CheckValidCategory(categoryName);
			string text = ".";
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Administer, text, categoryName);
			performanceCounterPermission.Demand();
			SharedUtils.CheckNtEnvironment();
			categoryName = categoryName.ToLower(CultureInfo.InvariantCulture);
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SharedUtils.EnterMutex("netfxperf.1.0", ref mutex);
				if (!PerformanceCounterLib.IsCustomCategory(text, categoryName))
				{
					throw new InvalidOperationException(SR.GetString("CantDeleteCategory"));
				}
				SharedPerformanceCounter.RemoveAllInstances(categoryName);
				PerformanceCounterLib.UnregisterCategory(categoryName);
				PerformanceCounterLib.CloseAllLibraries();
			}
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
					mutex.Close();
				}
			}
		}

		/// <summary>Determines whether the category is registered on the local computer.</summary>
		/// <param name="categoryName">The name of the performance counter category to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the category is registered; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F23 RID: 12067 RVA: 0x000D3B28 File Offset: 0x000D1D28
		public static bool Exists(string categoryName)
		{
			return PerformanceCounterCategory.Exists(categoryName, ".");
		}

		/// <summary>Determines whether the category is registered on the specified computer.</summary>
		/// <param name="categoryName">The name of the performance counter category to look for.</param>
		/// <param name="machineName">The name of the computer to examine for the category.</param>
		/// <returns>
		///   <see langword="true" /> if the category is registered; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string ("").  
		///  -or-  
		///  The <paramref name="machineName" /> parameter is invalid.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.IO.IOException">The network path cannot be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.  
		///  -or-  
		///  Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F24 RID: 12068 RVA: 0x000D3B38 File Offset: 0x000D1D38
		public static bool Exists(string categoryName, string machineName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (categoryName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "categoryName", categoryName }));
			}
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", machineName }));
			}
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machineName, categoryName);
			performanceCounterPermission.Demand();
			return PerformanceCounterLib.IsCustomCategory(machineName, categoryName) || PerformanceCounterLib.CategoryExists(machineName, categoryName);
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000D3BC8 File Offset: 0x000D1DC8
		internal static string[] GetCounterInstances(string categoryName, string machineName)
		{
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machineName, categoryName);
			performanceCounterPermission.Demand();
			CategorySample categorySample = PerformanceCounterLib.GetCategorySample(machineName, categoryName);
			if (categorySample.InstanceNameTable.Count == 0)
			{
				return new string[0];
			}
			string[] array = new string[categorySample.InstanceNameTable.Count];
			categorySample.InstanceNameTable.Keys.CopyTo(array, 0);
			if (array.Length == 1 && array[0].CompareTo("systemdiagnosticsperfcounterlibsingleinstance") == 0)
			{
				return new string[0];
			}
			return array;
		}

		/// <summary>Retrieves a list of the counters in a performance counter category that contains exactly one instance.</summary>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounter" /> objects indicating the counters that are associated with this single-instance performance counter category.</returns>
		/// <exception cref="T:System.ArgumentException">The category is not a single instance.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category does not have an associated instance.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F26 RID: 12070 RVA: 0x000D3C40 File Offset: 0x000D1E40
		public PerformanceCounter[] GetCounters()
		{
			if (this.GetInstanceNames().Length != 0)
			{
				throw new ArgumentException(SR.GetString("InstanceNameRequired"));
			}
			return this.GetCounters("");
		}

		/// <summary>Retrieves a list of the counters in a performance counter category that contains one or more instances.</summary>
		/// <param name="instanceName">The performance object instance for which to retrieve the list of associated counters.</param>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounter" /> objects indicating the counters that are associated with the specified object instance of this performance counter category.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property for this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance has not been set.  
		///  -or-  
		///  The category does not contain the instance that is specified by the <paramref name="instanceName" /> parameter.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F27 RID: 12071 RVA: 0x000D3C68 File Offset: 0x000D1E68
		public PerformanceCounter[] GetCounters(string instanceName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			if (instanceName.Length != 0 && !this.InstanceExists(instanceName))
			{
				throw new InvalidOperationException(SR.GetString("MissingInstance", new object[] { instanceName, this.categoryName }));
			}
			string[] counters = PerformanceCounterLib.GetCounters(this.machineName, this.categoryName);
			PerformanceCounter[] array = new PerformanceCounter[counters.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new PerformanceCounter(this.categoryName, counters[i], instanceName, this.machineName, true);
			}
			return array;
		}

		/// <summary>Retrieves a list of the performance counter categories that are registered on the local computer.</summary>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> objects indicating the categories that are registered on the local computer.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F28 RID: 12072 RVA: 0x000D3D12 File Offset: 0x000D1F12
		public static PerformanceCounterCategory[] GetCategories()
		{
			return PerformanceCounterCategory.GetCategories(".");
		}

		/// <summary>Retrieves a list of the performance counter categories that are registered on the specified computer.</summary>
		/// <param name="machineName">The computer to look on.</param>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> objects indicating the categories that are registered on the specified computer.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is invalid.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F29 RID: 12073 RVA: 0x000D3D20 File Offset: 0x000D1F20
		public static PerformanceCounterCategory[] GetCategories(string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", machineName }));
			}
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, machineName, "*");
			performanceCounterPermission.Demand();
			string[] categories = PerformanceCounterLib.GetCategories(machineName);
			PerformanceCounterCategory[] array = new PerformanceCounterCategory[categories.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new PerformanceCounterCategory(categories[i], machineName);
			}
			return array;
		}

		/// <summary>Retrieves the list of performance object instances that are associated with this category.</summary>
		/// <returns>An array of strings representing the performance object instance names that are associated with this category or, if the category contains only one performance object instance, a single-entry array that contains an empty string ("").</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is <see langword="null" />. The property might not have been set.  
		///  -or-  
		///  The category does not have an associated instance.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F2A RID: 12074 RVA: 0x000D3D95 File Offset: 0x000D1F95
		public string[] GetInstanceNames()
		{
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			return PerformanceCounterCategory.GetCounterInstances(this.categoryName, this.machineName);
		}

		/// <summary>Determines whether the specified performance object instance exists in the category that is identified by this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> object's <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property.</summary>
		/// <param name="instanceName">The performance object instance in this performance counter category to search for.</param>
		/// <returns>
		///   <see langword="true" /> if the category contains the specified performance object instance; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is <see langword="null" />. The property might not have been set.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F2B RID: 12075 RVA: 0x000D3DC0 File Offset: 0x000D1FC0
		public bool InstanceExists(string instanceName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			CategorySample categorySample = PerformanceCounterLib.GetCategorySample(this.machineName, this.categoryName);
			return categorySample.InstanceNameTable.ContainsKey(instanceName);
		}

		/// <summary>Determines whether a specified category on the local computer contains the specified performance object instance.</summary>
		/// <param name="instanceName">The performance object instance to search for.</param>
		/// <param name="categoryName">The performance counter category to search.</param>
		/// <returns>
		///   <see langword="true" /> if the category contains the specified performance object instance; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F2C RID: 12076 RVA: 0x000D3E11 File Offset: 0x000D2011
		public static bool InstanceExists(string instanceName, string categoryName)
		{
			return PerformanceCounterCategory.InstanceExists(instanceName, categoryName, ".");
		}

		/// <summary>Determines whether a specified category on a specified computer contains the specified performance object instance.</summary>
		/// <param name="instanceName">The performance object instance to search for.</param>
		/// <param name="categoryName">The performance counter category to search.</param>
		/// <param name="machineName">The name of the computer on which to look for the category instance pair.</param>
		/// <returns>
		///   <see langword="true" /> if the category contains the specified performance object instance; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string ("").  
		///  -or-  
		///  The <paramref name="machineName" /> parameter is invalid.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F2D RID: 12077 RVA: 0x000D3E20 File Offset: 0x000D2020
		public static bool InstanceExists(string instanceName, string categoryName, string machineName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (categoryName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "categoryName", categoryName }));
			}
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", machineName }));
			}
			PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory(categoryName, machineName);
			return performanceCounterCategory.InstanceExists(instanceName);
		}

		/// <summary>Reads all the counter and performance object instance data that is associated with this performance counter category.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> that contains the counter and performance object instance data for the category.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is <see langword="null" />. The property might not have been set.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F2E RID: 12078 RVA: 0x000D3EAC File Offset: 0x000D20AC
		public InstanceDataCollectionCollection ReadCategory()
		{
			if (this.categoryName == null)
			{
				throw new InvalidOperationException(SR.GetString("CategoryNameNotSet"));
			}
			CategorySample categorySample = PerformanceCounterLib.GetCategorySample(this.machineName, this.categoryName);
			return categorySample.ReadCategory();
		}

		// Token: 0x0400279E RID: 10142
		private string categoryName;

		// Token: 0x0400279F RID: 10143
		private string categoryHelp;

		// Token: 0x040027A0 RID: 10144
		private string machineName;

		// Token: 0x040027A1 RID: 10145
		internal const int MaxCategoryNameLength = 80;

		// Token: 0x040027A2 RID: 10146
		internal const int MaxCounterNameLength = 32767;

		// Token: 0x040027A3 RID: 10147
		internal const int MaxHelpLength = 32767;

		// Token: 0x040027A4 RID: 10148
		private const string perfMutexName = "netfxperf.1.0";
	}
}
