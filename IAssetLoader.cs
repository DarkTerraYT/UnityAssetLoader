using System.Reflection;
namespace UnityAssetLoader
{
    /// <summary>
    /// Base interface for asset loader classes, used for loading embedded resources
    /// </summary>
    public interface IAssetLoader
    {
        /// <summary>
        /// Asset Name of the asset to load
        /// </summary>
        public string AssetName { get; }

        /// <summary>
        /// Assembly of this Asset Loader
        /// </summary> 
        public Assembly Assembly { get; }
    }
}
