using System.Reflection;
using AssetLoader.Resources;
using UnityEngine;

namespace UnityAssetLoader
{
    /// <summary>
    /// Class for loading specifically asset bundles and the assets inside them.
    /// Inhertits from <see cref="IAssetLoader"/>
    /// </summary>
    public class AssetBundleLoader : IAssetLoader
    {
        /// <summary>
        /// Contructor for creating this with a name and an file extention (including the period).
        /// </summary>
        /// <param name="bundleName">Bundle name without extention</param>
        /// <param name="bundleExt">Bundle's file extention</param>
        /// <param name="assembly">Optional assembly of this asset loader</param>
        public AssetBundleLoader(string bundleName, string bundleExt, Assembly assembly = null)
        {
            AssetName = bundleName + bundleExt;
            if (assembly != null)
            {
                _asm = assembly;
            }
        }

        /// <summary>
        /// Contructor for creating this with a name that includes the file extention.
        /// </summary>
        /// <param name="bundleName">Bundle name including extention</param>
        /// <param name="assembly">Optional assembly of this asset loader</param>
        public AssetBundleLoader(string bundleName, Assembly assembly = null)
        {
            AssetName = bundleName;
            if (assembly != null)
            {
                _asm = assembly;
            }
        }

        /// <summary>
        /// Bundle Name with extention
        /// </summary>
        public string AssetName { get; } = "";

        /// <summary>
        /// Gets all of the assets in this asset bundle loader's asset bundle
        /// </summary>
        public List<UnityEngine.Object> GetAssets() => GetBundle().LoadAllAssets().ToList();

        Assembly _asm { get; } = Assembly.GetExecutingAssembly();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Assembly Assembly { get { return _asm; } }

        /// <summary>
        /// Gets all of the assets in this asset bundle loader's asset bundle with the provided type
        /// </summary>
        /// <typeparam name="T">Unity UnityEngine.Object type of the UnityEngine.Objects you're tying to get</typeparam>
        /// <returns></returns>
        public List<T> GetAssets<T>() where T : UnityEngine.Object => GetBundle().LoadAllAssets<T>().ToList();

        /// <summary>
        /// Gets the <see cref="AssetBundle"/> with the bundle name and file extention of this <see cref="AssetBundleLoader"/> inside of your assembly's metadata
        /// </summary>
        /// <returns></returns>
        public AssetBundle GetBundle()
        {
            return Assembly.LoadEmbeddedBundle(AssetName);
        }

        /// <summary>
        /// Changed to <see cref="GetBundle()"/>
        /// </summary>
        public AssetBundle Bundle => GetBundle();

        /// <summary>
        /// Gets the <see cref="AssetBundle"/> from memory with the provided bytes.
        /// </summary>
        /// <param name="bundleBytes">Bytes of the bundle</param>
        /// <returns></returns>
        public static AssetBundle GetBundle(byte[] bundleBytes) => AssetBundle.LoadFromMemory(bundleBytes);

        /// <summary>
        /// Returns every bundle inside of an assembly
        /// </summary>
        /// <param name="asm">Assembly to dump the asset bundles from</param>
        /// <returns></returns>
        public static List<AssetBundle> GetEmbeddedBundles(Assembly asm)
        {
            List<AssetBundle> bundles = new();

            foreach (string name in asm.GetManifestResourceNames())
            {
                AssetBundle bundle = asm.LoadEmbeddedBundle(name);

                if (bundle != null)
                {
                    bundles.Add(bundle);
                }
            }

            return bundles;
        }

        /// <summary>
        /// Gets an Unity UnityEngine.Object with the provided name and type
        /// </summary>
        /// <param name="UnityEngine.ObjectType">Type of the UnityEngine.Object to get</param>
        /// <param name="prefabName">Name of the asset to load from the bundle (including file extenstion)</param>
        /// <returns></returns>
        public UnityEngine.Object GetAsset(Type objectType, string prefabName) => GetBundle().LoadAsset(prefabName, objectType);

        /// <summary>
        /// Gets an Unity UnityEngine.Object with the provide name
        /// </summary>
        /// <param name="prefabName">Name of the asset to load from the bundle (including file extenstion)</param>
        /// <returns></returns>
        public UnityEngine.Object GetAsset(string prefabName) => GetBundle().LoadAsset(prefabName);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of the UnityEngine.Object to get</typeparam>
        /// <param name="assetName">Name of the asset to load from the bundle (including file extenstion)</param>
        /// <returns></returns>
        public T GetAsset<T>(string assetName) where T : UnityEngine.Object => GetBundle().LoadAsset<T>(assetName);

    }

    /// <summary>
    /// Extentions for <see cref="IAssetLoader"/>
    /// </summary>
    public static class IAssetLoaderExt
    {
        /// <summary>
        /// Loads the bytes of the file that this asset loader points to inside of your project's metadata
        /// </summary>
        /// <param name="assetLoader"></param>
        /// <returns></returns>
        public static byte[] LoadAssetBytes(this IAssetLoader assetLoader)
        {
            return assetLoader.Assembly.GetManifestResourceStream(assetLoader.AssetName).Bytes();
        }

        public static Stream LoadAssetStream(this IAssetLoader assetLoader)
        {
            return assetLoader.Assembly.GetManifestResourceStream(assetLoader.AssetName);
        }
    }
}
