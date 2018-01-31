using System.Collections.Generic;
using System.Linq;
using KenticoCloud.Wyam.Models;
using Wyam.Common.Documents;
using Wyam.Common.Execution;
using Wyam.Common.Modules;

namespace KenticoCloud.Wyam
{
    /// <summary>
    /// Parses document content by replacing <c>!!assets/</c> paths with Kentico Cloud asset URLs.
    /// URLs are matched by the file name of the asset.
    /// </summary>
    public class KenticoCloudAssetParser : IModule
    {
        /// <inheritdoc />
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
                            content = content.Replace($"!!assets/{image.Name}", image.Url);
                        }
                    }

                    newDoc = context.GetDocument(doc, context.GetContentStream(content));
                }

                yield return newDoc;
            }
        }
    }
}
