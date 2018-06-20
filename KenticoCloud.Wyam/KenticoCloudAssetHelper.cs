using System.Collections.Generic;
using System.Linq;
using KenticoCloud.Wyam.Models;
using Wyam.Common.Documents;

namespace KenticoCloud.Wyam
{
    public static class KenticoCloudAssetHelper
    {
        public static string[] GetAssetUris(IDocument doc)
        {
            var assetUrls = new List<string>();

            var assets = doc.Metadata.Where(x => x.Value is List<Asset>).ToList();
            if (assets.Any())
            {
                foreach (var metaAsset in assets)
                {
                    var asset = (List<Asset>)metaAsset.Value;

                    foreach (var image in asset)
                    {
                        assetUrls.Add(image.Url);
                    }
                }
            }

            return assetUrls.ToArray();
        }

        public static string GetAssetFileName(IDocument doc)
        {
            var fileUrl = doc.Get<string>("SourceUri");
            return GetAssetFileName(fileUrl);
        }

        public static string GetAssetFileName(string fileUrl)
        {
            var splitPath = fileUrl.Split('/');
            return $"{splitPath[splitPath.Length - 2]}/{splitPath[splitPath.Length - 1]}";
        }
    }
}