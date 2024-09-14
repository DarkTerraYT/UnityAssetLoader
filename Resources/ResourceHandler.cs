using System.Reflection;
using UnityEngine;

namespace UnityAssetLoader.Resources
{
    internal static class ResourceHandler
    {
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
