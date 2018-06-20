using System.Collections.Generic;
using Wyam.Common.Configuration;
using Wyam.Common.Documents;
using Wyam.Common.Execution;
using Wyam.Common.Modules;
using Wyam.Core.Modules.IO;

namespace KenticoCloud.Wyam
{
    /// <summary>
    /// Downloads all Kentico Cloud assets found in input documents.
    /// The downloaded assets can then be processed with modules such as <see cref="WriteFiles"/>.
    /// URLs are supplied with the <see cref="WithUris"/> method.
    /// </summary>
    public class KenticoCloudAssetDownload : Download, IModule
    {
        private DocumentConfig UriDocConfig;

        public KenticoCloudAssetDownload WithUris(DocumentConfig uris)
        {
            UriDocConfig = uris;
            return this;
        }

        public new IEnumerable<IDocument> Execute(IReadOnlyList<IDocument> inputs, IExecutionContext context)
        {
            var uris = new List<string>();
            foreach (var input in inputs)
            {
                uris.AddRange((string[])UriDocConfig(input, context));
            }

            WithUris(uris.ToArray());

            return base.Execute(inputs, context);
        }
    }
}
