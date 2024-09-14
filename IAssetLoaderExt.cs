using UnityAssetLoader.Resources;

namespace UnityAssetLoader
{
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

        /// <summary>
        /// Gets the stream of this asset loader
        /// </summary>
        /// <param name="assetLoader"></param>
        /// <returns></returns>
        public static Stream LoadAssetStream(this IAssetLoader assetLoader)
        {
            return assetLoader.Assembly.GetManifestResourceStream(assetLoader.AssetName);
        }
    }
}
