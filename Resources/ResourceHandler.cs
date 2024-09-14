using System.Reflection;
using UnityEngine;

namespace UnityAssetLoader.Resources
{
    /// <summary>
    /// Extention class for getting resources from assemblies
    /// </summary>
    public static class ResourceHandler
    {
        /// <summary>
        /// Returns all manifest resource names in this <see cref="Assembly"/> as a list
        /// </summary>
        /// <param name="asm"></param>
        /// <returns></returns>
        public static List<string> ResourceNames(this Assembly asm) => asm.GetManifestResourceNames().ToList();
        /// <summary>
        /// Returns all manifest resource names from this <see cref="IAssetLoader"/>'s <see cref="Assembly"/> as a list
        /// </summary>
        /// <param name="assetLoader"></param>
        /// <returns></returns>
        public static List<string> ResourceNames(this IAssetLoader assetLoader) => assetLoader.Assembly.ResourceNames();

        /// <summary>
        /// Gets the bytes of this <see cref="Stream"/>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] Bytes(this Stream stream)
        {
            try
            {
                using (stream)
                {
                    if (stream is MemoryStream memoryStream)
                    {
                        return memoryStream.ToArray();
                    }

                    using (memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns an embedded <see cref="AssetBundle"/> with the provided name
        /// </summary>
        /// <param name="asm"></param>
        /// <param name="name">Name of the bundle</param>
        /// <param name="bundleExtention">Extention of the bundle (uneeded if file extention is included in the name)</param>
        /// <returns></returns>
        public static AssetBundle LoadEmbeddedBundle(this Assembly asm, string name, string bundleExtention = "")
        {
            var bytes = asm.GetManifestResourceStream(name + bundleExtention).Bytes();
            if (bytes == null)
            {
                return null;
            }

            return AssetBundle.LoadFromMemory(bytes);
        }
    }
}
