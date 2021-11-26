using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;
using System.Threading;
using System.Windows.Threading;

namespace MS.Internal.Text.TextInterface
{
	public sealed class Factory : CriticalHandle
	{
		/// <summary>
		/// A pointer to the wrapped DWrite factory object.
		/// </summary>
		IDWriteFactory _pFactory;
		IDWriteTextAnalyzer _pTextAnalyzer;
				  
		/// <summary>
		/// The custom loader used by WPF to load font collections.
		/// </summary>
		IFontSourceCollectionFactory  _fontSourceCollectionFactory;

		/// <summary>
		/// The custom loader used by WPF to load font files.
		/// </summary>
		IFontSourceFactory            _fontSourceFactory;

		GCHandle _managedFactoryHandle; // A GCHandle for this object

		[ThreadStatic]
		static Dictionary<Uri,FILETIME> _timeStampCache;
		
		[ThreadStatic]
		static DispatcherOperation _timeStampCacheCleanupOp;

		// Interface for our native helper
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate int CreateEnumeratorFromKeyFn(
			IntPtr managedFactoryHandle,
			IntPtr collectionKey,
			uint collectionKeySize,
			[MarshalAs(UnmanagedType.Interface)] out IDWriteFontFileEnumeratorMirror fontFileEnumerator);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate int CreateStreamFromKeyFn(
			IntPtr managedFactoryHandle,
			IntPtr fontFileReferenceKey,
			uint fontFileReferenceKeySize,
			[MarshalAs(UnmanagedType.Interface)] out IDWriteFontFileStreamMirror fontFileStream);

		[DllImport("wmwpfdwhelper.dll", CallingConvention=CallingConvention.StdCall)]
		private static extern IntPtr RegisterLoaders(
			[MarshalAs(UnmanagedType.Interface)] IDWriteFactory dwritefactory,
			CreateEnumeratorFromKeyFn enum_fn,
			CreateStreamFromKeyFn stream_fn,
			IntPtr managedFactoryHandle);

		[DllImport("wmwpfdwhelper.dll", CallingConvention=CallingConvention.StdCall)]
		private static extern void ReleaseRegisteredLoaders(IntPtr handle);

		[DllImport("wmwpfdwhelper.dll", CallingConvention=CallingConvention.StdCall)]
		private static extern IntPtr GetDWriteFileLoader(IntPtr handle);

		[DllImport("wmwpfdwhelper.dll", CallingConvention=CallingConvention.StdCall)]
		private static extern IntPtr GetDWriteCollectionLoader(IntPtr handle);

		internal IDWriteFactory DWriteFactory
		{
			get {
				return _pFactory;
			}
		}

		internal IntPtr WpfFontFileLoader
		{
			get
			{
				if (IsInvalid || IsClosed)
					throw new InvalidOperationException("Object is closed");
				return GetDWriteFileLoader(handle);
			}
		}

		internal IntPtr WpfFontCollectionLoader
		{
			get
			{
				if (IsInvalid || IsClosed)
					throw new InvalidOperationException("Object is closed");
				return GetDWriteCollectionLoader(handle);
			}
		}

		public static bool IsLocalUri(Uri uri)
		{
			return (uri.IsFile && uri.IsLoopback && !uri.IsUnc);
		}

		public static DWriteMatrix GetIdentityTransform()
		{
			DWriteMatrix transform = new DWriteMatrix();
			transform.M11=1;
			transform.M12=0;
			transform.M22=1;
			transform.M21=0;
			transform.Dx =0;
			transform.Dy =0;

			return transform;
		}

		public static Factory Create(FactoryType factoryType,
							  IFontSourceCollectionFactory fontSourceCollectionFactory,
							  IFontSourceFactory fontSourceFactory)
		{
			return new Factory(factoryType, fontSourceCollectionFactory, fontSourceFactory);
		}

		private Factory(
						FactoryType                   factoryType,
						IFontSourceCollectionFactory  fontSourceCollectionFactory,
						IFontSourceFactory            fontSourceFactory
						) : base(IntPtr.Zero)
		{
			Initialize(factoryType);

			_fontSourceFactory = fontSourceFactory;
			_fontSourceCollectionFactory = fontSourceCollectionFactory;

			_managedFactoryHandle = GCHandle.Alloc(this, GCHandleType.Weak);

			handle = RegisterLoaders(_pFactory, CreateEnumeratorFromKey, CreateStreamFromKey, GCHandle.ToIntPtr(_managedFactoryHandle));
		}

		[DllImport("dwrite", CallingConvention=CallingConvention.StdCall)]
		extern static int DWriteCreateFactory(
			FactoryType factoryType,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid iid,
			[Out, MarshalAs(UnmanagedType.Interface)] out IDWriteFactory factory);

		void Initialize(FactoryType factoryType)
		{
			Guid IID_IDWriteFactory = typeof(IDWriteFactory).GUID;
			IDWriteFactory factoryTemp;

			int hr = DWriteCreateFactory(
				factoryType,
				IID_IDWriteFactory,
				out factoryTemp);

			Marshal.ThrowExceptionForHR(hr);

			_pFactory = factoryTemp;
			_pTextAnalyzer = _pFactory.CreateTextAnalyzer();
		}

		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			ReleaseRegisteredLoaders(handle);

			handle = IntPtr.Zero;

			_managedFactoryHandle.Free();

			return true;
		}

		public FontFile CreateFontFile(Uri filePathUri)
		{        
			IDWriteFontFile dwriteFontFile = null;
			try {
				dwriteFontFile = CreateFontFile(_pFactory, WpfFontFileLoader, filePathUri);
			}
			catch {
				// If DWrite's CreateFontFileReference fails then try opening the file using WPF's logic.
				// The failures that WPF returns are more granular than the HRESULTs that DWrite returns
				// thus we use WPF's logic to open the file to throw the same exceptions that
				// WPF would have thrown before.
				IFontSource fontSource = _fontSourceFactory.Create(filePathUri.AbsoluteUri);
				fontSource.TestFileOpenable();
				throw;
			}

			return new FontFile(dwriteFontFile);

		}

		public FontFace CreateFontFace(Uri filePathUri, uint faceIndex)
		{        
			return CreateFontFace(
								 filePathUri,
								 faceIndex,
								 FontSimulations.None
								 );
		}

		unsafe public FontFace CreateFontFace(Uri filePathUri, uint faceIndex, FontSimulations fontSimulationFlags)
		{
			FontFile fontFile = CreateFontFile(filePathUri);
			FontFileType dwriteFontFileType;
			FontFaceType dwriteFontFaceType;
			uint numberOfFaces = 0;

			int hr;
			if (fontFile.Analyze(
								 out dwriteFontFileType,
								 out dwriteFontFaceType,
								 out numberOfFaces,
								 out hr
								 ))
			{
				if (faceIndex >= numberOfFaces)
				{
					throw new ArgumentOutOfRangeException("faceIndex");
				}

				IntPtr dwriteFontFace = IntPtr.Zero; // IDWriteFontFace
				IntPtr dwriteFontFile = fontFile.DWriteFontFileIface;
				
				try {
					dwriteFontFace = _pFactory.CreateFontFace(
														 dwriteFontFaceType,
														 1,
														 &dwriteFontFile,
														 faceIndex,
														 fontSimulationFlags
														 );
				}
				finally {
					Marshal.Release(dwriteFontFile);
				}

				return new FontFace(dwriteFontFace);
			}
			
			// This path is here because there is a behavior mismatch between DWrite and WPF.
			// If a directory was given instead of a font uri WPF previously throws 
			// System.UnauthorizedAccessException. We handle most of the exception behavior mismatch 
			// in FontFile^ Factory::CreateFontFile by opening the file using WPF's previous (prior to DWrite integration) logic if 
			// CreateFontFileReference fails (please see comments in FontFile^ Factory::CreateFontFile).
			// However in this special case DWrite's CreateFontFileReference will succeed if given
			// a directory instead of a font file and it is the Analyze() call will fail returning DWRITE_E_FILEFORMAT.
			// Thus, incase the hr returned from Analyze() was DWRITE_E_FILEFORMAT we do as we did in FontFile^ Factory::CreateFontFile
			// to try and open the file using WPF old logic and throw System.UnauthorizedAccessException as WPF used to do.
			// If a file format exception is expected then opening the file should succeed and ConvertHresultToException()
			// Should throw the correct exception.
			// A final note would be that this overhead is only incurred in error conditions and so the normal execution path should
			// not be affected.
			else
			{
				if (hr == unchecked((int)0x88985000)) // DWRITE_E_FILEFORMAT
				{
					IFontSource fontSource = _fontSourceFactory.Create(filePathUri.AbsoluteUri);
					fontSource.TestFileOpenable();
				}
				Marshal.ThrowExceptionForHR(hr);
			}

			return null;
		}

		public FontCollection GetSystemFontCollection()
		{
			return GetSystemFontCollection(false);
		}

		public FontCollection GetSystemFontCollection(bool checkForUpdates)
		{
			IDWriteFontCollection dwriteFontCollection = null;
			_pFactory.GetSystemFontCollection(out dwriteFontCollection, checkForUpdates);

			return new FontCollection(dwriteFontCollection);
		}

		unsafe public FontCollection GetFontCollection(Uri uri)
		{
			string uriString = uri.AbsoluteUri;
			IDWriteFontCollection dwriteFontCollection = null;

			fixed (char* pUriString = uriString)
			{
				dwriteFontCollection = _pFactory.CreateCustomFontCollection(
								   WpfFontCollectionLoader,
								   new IntPtr(pUriString),
								   (uint)(uriString.Length+1) * 2
								   );
			}

			return new FontCollection(dwriteFontCollection);
		}  

		unsafe internal static IDWriteFontFile CreateFontFile(
							   IDWriteFactory factory,
							   IntPtr fontFileLoader, // IDWriteFontFileLoader*
							   Uri filePathUri
							   )
		{
			IDWriteFontFile dwriteFontFile;
			bool isLocal = Factory.IsLocalUri(filePathUri);

			if (isLocal)
			{
				// DWrite currently has a slow lookup for the last write time, which
				// introduced a noticable perf regression when we switched over.
				// To mitigate this scenario, we will fetch the timestamp ourselves
				// and cache it for future calls.
				//
				// Note: we only do this if a Dispatcher exists for the current
				// thread.  There is a seperate cache for each thread.
				FILETIME cachedTimeStamp;
				IntPtr pTimeStamp = IntPtr.Zero; // If something fails, do nothing and let DWrite sort it out.
				Dispatcher currentDispatcher = Dispatcher.FromThread(Thread.CurrentThread);
				if(currentDispatcher != null)
				{
					try {
						// One-time initialization per thread.
						if(_timeStampCache == null)
						{
							_timeStampCache = new Dictionary<Uri,FILETIME>();                
						}
						
						if(!_timeStampCache.TryGetValue(filePathUri, out cachedTimeStamp))
						{
							long longFileTime = File.GetLastWriteTime(filePathUri.LocalPath).ToFileTime();
							cachedTimeStamp.dwLowDateTime = (int)(longFileTime);
							cachedTimeStamp.dwHighDateTime = (int)(longFileTime >> 32);
							_timeStampCache.Add(filePathUri, cachedTimeStamp);
									
							// We don't want to hold this cached value for a long time since
							// all font references will be tied to this timestamp, and any font
							// update during the lifetime of the application will cause is to
							// encounter errors.  So we use a dispatcher operation to clear
							// the cache as soon as we get back to pumping messages.
							if(_timeStampCacheCleanupOp == null)
							{
								_timeStampCacheCleanupOp = currentDispatcher.BeginInvoke(new Action(CleanupTimeStampCache));
							}
						}

						pTimeStamp = Marshal.AllocCoTaskMem(Marshal.SizeOf<FILETIME>());
						Marshal.StructureToPtr<FILETIME>(cachedTimeStamp, pTimeStamp, false);
					}
					catch { }
				}

				try {
					dwriteFontFile = factory.CreateFontFileReference(
														  filePathUri.LocalPath,
														  pTimeStamp
														  );
				}
				finally {
					Marshal.FreeCoTaskMem(pTimeStamp);
				}
			}
			else
			{
				string filePath = filePathUri.AbsoluteUri;
				
				fixed (char* pFilePath = filePath)
				{
					dwriteFontFile = factory.CreateCustomFontFileReference(
																new IntPtr(pFilePath),
																(uint)(filePath.Length + 1) * 2,
																fontFileLoader
																);
				}
			}
			return dwriteFontFile;
		}

		static void CleanupTimeStampCache()
		{
			_timeStampCacheCleanupOp = null;
			_timeStampCache.Clear();
		}

		public TextAnalyzer CreateTextAnalyzer()
		{
			return new TextAnalyzer(_pFactory, _pTextAnalyzer);
		}

		public override bool IsInvalid
		{
			get {
				return (handle == IntPtr.Zero);
			}
		}

		internal static int CreateEnumeratorFromKey(
			IntPtr managedFactoryHandle,
			IntPtr collectionKey,
			uint collectionKeySize,
			out IDWriteFontFileEnumeratorMirror fontFileEnumerator)
		{
			Factory factory = (Factory)GCHandle.FromIntPtr(managedFactoryHandle).Target;

			uint numberOfCharacters = collectionKeySize / 2;
			fontFileEnumerator = null;
			if (   (collectionKeySize % 2 != 0)                        // The collectionKeySize must be divisible by sizeof(WCHAR)
				|| (numberOfCharacters <= 1)                                       // The collectionKey cannot be less than or equal 1 character as it has to contain the NULL character.
				|| (Marshal.ReadInt16(collectionKey, ((int)numberOfCharacters - 1) * 2) != '\0'))  // The collectionKey must end with the NULL character
			{
				return unchecked((int)0x80070057); // E_INVALIDARG
			}

			fontFileEnumerator = null;

			string uriString = Marshal.PtrToStringUni(collectionKey);
			int hr = 0;

			try
			{
				IFontSourceCollection fontSourceCollection = factory._fontSourceCollectionFactory.Create(uriString);
				FontFileEnumerator fontFileEnum = new FontFileEnumerator(
													  fontSourceCollection,
													  factory.WpfFontFileLoader,
													  factory._pFactory
													  );
				GC.KeepAlive(factory);
				fontFileEnumerator = (IDWriteFontFileEnumeratorMirror)fontFileEnum;
			}
			catch(Exception exception)
			{
				hr = Marshal.GetHRForException(exception);
			}

			return hr;
		}

		internal static int CreateStreamFromKey(
			IntPtr managedFactoryHandle,
			IntPtr fontFileReferenceKey,
			uint fontFileReferenceKeySize,
			out IDWriteFontFileStreamMirror fontFileStream)
		{
			Factory factory = (Factory)GCHandle.FromIntPtr(managedFactoryHandle).Target;

			uint numberOfCharacters = fontFileReferenceKeySize / 2;

			fontFileStream = null;
		   
			if ((fontFileReferenceKeySize % 2 != 0)                      // The fontFileReferenceKeySize must be divisible by sizeof(WCHAR)
				|| (numberOfCharacters <= 1)                                            // The fontFileReferenceKey cannot be less than or equal 1 character as it has to contain the NULL character.
				|| (Marshal.ReadInt16(fontFileReferenceKey, ((int)numberOfCharacters - 1) * 2) != '\0'))    // The fontFileReferenceKey must end with the NULL character
			{
				return unchecked((int)0x80070057); // E_INVALIDARG
			}

			string uriString = Marshal.PtrToStringUni(fontFileReferenceKey);

			int hr = 0;

			try
			{
				IFontSource fontSource = factory._fontSourceFactory.Create(uriString);        
				FontFileStream customFontFileStream = new FontFileStream(fontSource);

				fontFileStream = (IDWriteFontFileStreamMirror)customFontFileStream;
			}
			catch(System.Exception exception)
			{
				hr = Marshal.GetHRForException(exception);
			}

			return hr;
		}
	}
}
