using System.Collections.Generic;
using System.IO;
using System.Linq;
using KenticoCloud.Wyam.Models;
using Wyam.Common.Documents;

namespace KenticoCloud.Wyam.Html
{
    /// <summary>
    /// Helper methods to aid with Razor templates
    /// </summary>
    public static class HtmlHelpers
    {
        public static string GetFirstAssetUrl(this IDocument document, string codename)
        {
            if (document == null)
            {
                return string.Empty;
            }

            var assets = document.Get<List<Asset>>(codename);
            return assets != null && assets.Any() ? assets[0].Url : string.Empty;
        }

        public static string GetFirstAssetLocalUrl(this IDocument document, string codename, string folderPath = "")
        {
            var assetUrl = GetFirstAssetUrl(document, codename);
            if (string.IsNullOrEmpty(assetUrl))
            {
                return string.Empty;
            }

            return Path.Combine("/", folderPath, KenticoCloudAssetHelper.GetAssetFileName(assetUrl)).Replace(@"\", "/");
        }
    }
}
