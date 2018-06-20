using System.Collections.Generic;
using System.Linq;
using KenticoCloud.Wyam.Models;
using Wyam.Common.Documents;
using Wyam.Common.Execution;
using Wyam.Common.Modules;

namespace KenticoCloud.Wyam
{
    /// <summary>
    /// Parses document content by replacing <c>!!local-assets/</c> paths with URLs to downloaded assets.
    /// URLs are matched by the file name of the asset.
    /// </summary>
    public class KenticoCloudLocalAssetParser : IModule
    {
        private string _folderPath = string.Empty;

        public KenticoCloudLocalAssetParser WithFolderPath(string folderPath)
        {
            _folderPath = folderPath + "/";
            return this;
        }

        public IEnumerable<IDocument> Execute(IReadOnlyList<IDocument> inputs, IExecutionContext context)
        {
            foreach (var doc in inputs)
            {
                var newDoc = doc;
                var content = doc.Content;

                var assets = doc.Metadata.Where(x => x.Value is List<Asset>).ToList();
                if (assets.Any())
                {
                    foreach (var metaAsset in assets)
                    {
                        var asset = (List<Asset>)metaAsset.Value;

                        foreach (var image in asset)
                        {
                            content = content.Replace($"!!local-assets/{image.Name}", $"/{_folderPath}{KenticoCloudAssetHelper.GetAssetFileName(image.Url)}");
                        }
                    }

                    newDoc = context.GetDocument(doc, context.GetContentStream(content));
                }

                yield return newDoc;
            }
        }
    }
}
