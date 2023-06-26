using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000154 RID: 340
	internal abstract class ODataParameterReaderCore : ODataParameterReader, IODataReaderWriterListener
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x0001CD85 File Offset: 0x0001AF85
		protected ODataParameterReaderCore(ODataInputContext inputContext, IEdmFunctionImport functionImport)
		{
			this.inputContext = inputContext;
			this.functionImport = functionImport;
			this.EnterScope(ODataParameterReaderState.Start, null, null);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0001CDBF File Offset: 0x0001AFBF
		public sealed override ODataParameterReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001CDDC File Offset: 0x0001AFDC
		public override string Name
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Name;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001CDF9 File Offset: 0x0001AFF9
		public override object Value
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001CE16 File Offset: 0x0001B016
		protected IEdmFunctionImport FunctionImport
		{
			get
			{
				return this.functionImport;
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001CE20 File Offset: 0x0001B020
		public override ODataCollectionReader CreateCollectionReader()
		{
			this.VerifyCanCreateSubReader(ODataParameterReaderState.Collection);
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Active;
			IEdmTypeReference elementType = ((IEdmCollectionType)this.GetParameterTypeReference(this.Name).Definition).ElementType;
			return this.CreateCollectionReader(elementType);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001CE5E File Offset: 0x0001B05E
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001CE84 File Offset: 0x0001B084
		public sealed override Task<bool> ReadAsync()
		{
			this.VerifyCanRead(false);
			return this.ReadAsynchronously().FollowOnFaultWith(delegate(Task<bool> t)
			{
				this.EnterScope(ODataParameterReaderState.Exception, null, null);
			});
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001CEA4 File Offset: 0x0001B0A4
		void IODataReaderWriterListener.OnException()
		{
			this.EnterScope(ODataParameterReaderState.Exception, null, null);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001CEAF File Offset: 0x0001B0AF
		void IODataReaderWriterListener.OnCompleted()
		{
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Completed;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001CEB8 File Offset: 0x0001B0B8
		protected internal IEdmTypeReference GetParameterTypeReference(string parameterName)
		{
			IEdmFunctionParameter edmFunctionParameter = this.FunctionImport.FindParameter(parameterName);
			if (edmFunctionParameter == null)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_ParameterNameNotInMetadata(parameterName, this.FunctionImport.Name));
			}
			return this.inputContext.EdmTypeResolver.GetParameterType(edmFunctionParameter);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001CF00 File Offset: 0x0001B100
		protected internal void EnterScope(ODataParameterReaderState state, string name, object value)
		{
			if (state == ODataParameterReaderState.Value && value != null && !(value is ODataComplexValue) && !EdmLibraryExtensions.IsPrimitiveType(value.GetType()))
			{
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCore_ValueMustBePrimitiveOrComplexOrNull));
			}
			if (this.scopes.Count == 0 || this.State != ODataParameterReaderState.Exception)
			{
				if (state == ODataParameterReaderState.Completed)
				{
					List<string> list = new List<string>();
					foreach (IEdmFunctionParameter edmFunctionParameter in this.FunctionImport.Parameters.Skip(this.FunctionImport.IsBindable ? 1 : 0))
					{
						if (!this.parametersRead.Contains(edmFunctionParameter.Name) && !this.inputContext.EdmTypeResolver.GetParameterType(edmFunctionParameter).IsNullable)
						{
							list.Add(edmFunctionParameter.Name);
						}
					}
					if (list.Count > 0)
					{
						this.scopes.Push(new ODataParameterReaderCore.Scope(ODataParameterReaderState.Exception, null, null));
						throw new ODataException(Strings.ODataParameterReaderCore_ParametersMissingInPayload(this.FunctionImport.Name, string.Join(",", list.ToArray())));
					}
				}
				else if (name != null)
				{
					if (this.parametersRead.Contains(name))
					{
						throw new ODataException(Strings.ODataParameterReaderCore_DuplicateParametersInPayload(name));
					}
					this.parametersRead.Add(name);
				}
				this.scopes.Push(new ODataParameterReaderCore.Scope(state, name, value));
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001D06C File Offset: 0x0001B26C
		protected internal void PopScope(ODataParameterReaderState state)
		{
			this.scopes.Pop();
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001D07A File Offset: 0x0001B27A
		protected void OnParameterCompleted()
		{
			this.subReaderState = ODataParameterReaderCore.SubReaderState.None;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001D084 File Offset: 0x0001B284
		protected bool ReadImplementation()
		{
			bool flag;
			switch (this.State)
			{
			case ODataParameterReaderState.Start:
				flag = this.ReadAtStartImplementation();
				break;
			case ODataParameterReaderState.Value:
			case ODataParameterReaderState.Collection:
				this.OnParameterCompleted();
				flag = this.ReadNextParameterImplementation();
				break;
			case ODataParameterReaderState.Exception:
			case ODataParameterReaderState.Completed:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCore_ReadImplementation));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCore_ReadImplementation));
			}
			return flag;
		}

		// Token: 0x06000934 RID: 2356
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x06000935 RID: 2357
		protected abstract bool ReadNextParameterImplementation();

		// Token: 0x06000936 RID: 2358
		protected abstract ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference);

		// Token: 0x06000937 RID: 2359 RVA: 0x0001D0F3 File Offset: 0x0001B2F3
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001D0FB File Offset: 0x0001B2FB
		protected virtual Task<bool> ReadAsynchronously()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadImplementation));
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001D10E File Offset: 0x0001B30E
		private static string GetCreateReaderMethodName(ODataParameterReaderState state)
		{
			return "Create" + state.ToString() + "Reader";
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001D12C File Offset: 0x0001B32C
		private void VerifyCanCreateSubReader(ODataParameterReaderState expectedState)
		{
			this.inputContext.VerifyNotDisposed();
			if (this.State != expectedState)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState(ODataParameterReaderCore.GetCreateReaderMethodName(expectedState), this.State));
			}
			if (this.subReaderState != ODataParameterReaderCore.SubReaderState.None)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_CreateReaderAlreadyCalled(ODataParameterReaderCore.GetCreateReaderMethodName(expectedState), this.Name));
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001D188 File Offset: 0x0001B388
		private T InterceptException<T>(Func<T> action)
		{
			T t;
			try
			{
				t = action();
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					this.EnterScope(ODataParameterReaderState.Exception, null, null);
				}
				throw;
			}
			return t;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001D1C4 File Offset: 0x0001B3C4
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataParameterReaderState.Exception || this.State == ODataParameterReaderState.Completed)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
			if (this.State == ODataParameterReaderState.Collection)
			{
				if (this.subReaderState == ODataParameterReaderCore.SubReaderState.None)
				{
					throw new ODataException(Strings.ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall(this.State, ODataParameterReaderCore.GetCreateReaderMethodName(this.State)));
				}
				if (this.subReaderState == ODataParameterReaderCore.SubReaderState.Active)
				{
					throw new ODataException(Strings.ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall(this.State, ODataParameterReaderCore.GetCreateReaderMethodName(this.State)));
				}
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001D267 File Offset: 0x0001B467
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				this.VerifySynchronousCallAllowed();
				return;
			}
			this.VerifyAsynchronousCallAllowed();
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001D279 File Offset: 0x0001B479
		private void VerifySynchronousCallAllowed()
		{
			if (!this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001D293 File Offset: 0x0001B493
		private void VerifyAsynchronousCallAllowed()
		{
			if (this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_AsyncCallOnSyncReader);
			}
		}

		// Token: 0x04000367 RID: 871
		private readonly ODataInputContext inputContext;

		// Token: 0x04000368 RID: 872
		private readonly IEdmFunctionImport functionImport;

		// Token: 0x04000369 RID: 873
		private readonly Stack<ODataParameterReaderCore.Scope> scopes = new Stack<ODataParameterReaderCore.Scope>();

		// Token: 0x0400036A RID: 874
		private readonly HashSet<string> parametersRead = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x0400036B RID: 875
		private ODataParameterReaderCore.SubReaderState subReaderState;

		// Token: 0x02000155 RID: 341
		private enum SubReaderState
		{
			// Token: 0x0400036D RID: 877
			None,
			// Token: 0x0400036E RID: 878
			Active,
			// Token: 0x0400036F RID: 879
			Completed
		}

		// Token: 0x02000156 RID: 342
		protected sealed class Scope
		{
			// Token: 0x06000941 RID: 2369 RVA: 0x0001D2AD File Offset: 0x0001B4AD
			public Scope(ODataParameterReaderState state, string name, object value)
			{
				this.state = state;
				this.name = name;
				this.value = value;
			}

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x06000942 RID: 2370 RVA: 0x0001D2CA File Offset: 0x0001B4CA
			public ODataParameterReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x06000943 RID: 2371 RVA: 0x0001D2D2 File Offset: 0x0001B4D2
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001D2DA File Offset: 0x0001B4DA
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04000370 RID: 880
			private readonly ODataParameterReaderState state;

			// Token: 0x04000371 RID: 881
			private readonly string name;

			// Token: 0x04000372 RID: 882
			private readonly object value;
		}
	}
}
