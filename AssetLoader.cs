using System.Reflection;

namespace UnityAssetLoader
{
    /// <summary>
    /// Class to load embedded resources. Inherits from <see cref="IAssetLoader"/>
    /// </summary>
    public class AssetLoader : IAssetLoader
    {
        /// <summary>
        /// Contructor for creating this with a name and an file extention (including the period).
        /// </summary>
        /// <param name="assetName">Name of the embedded resource</param>
        /// <param name="assetExt">File extention of the embedded resource</param>
        /// <param name="assembly">Optional assembly of this asset loader</param>
        public AssetLoader(string assetName, string assetExt, Assembly assembly = null)
        {
            AssetName = assetName + assetExt;
            if (assembly != null)
            {
                _asm = assembly;
            }
        }

        /// <summary>
        /// Contructor for creating this with a name that includes the file extention.
        /// </summary>
        /// <param name="assetName">Name of the embedded resource including extention</param>
        /// <param name="assembly">Optional assembly of this asset loader</param>
        public AssetLoader(string assetName, Assembly assembly = null)
        {
            AssetName = assetName;
            if (assembly != null)
            {
                _asm = assembly;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string AssetName { get; } = "";
        Assembly _asm { get; } = Assembly.GetExecutingAssembly();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Assembly Assembly { get { return _asm; } }
    }
}
