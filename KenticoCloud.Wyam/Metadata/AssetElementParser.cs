using System.Collections.Generic;
using System.Linq;
using KenticoCloud.Wyam.Models;
using Newtonsoft.Json.Linq;

namespace KenticoCloud.Wyam.Metadata
{
    /// <summary>
    /// Parses content item assets as a <see cref="List{Asset}">List&lt;Asset&gt;</see>.
    /// </summary>
    public class AssetElementParser : IElementParser
    {
        public void ParseMetadata(List<KeyValuePair<string, object>> metadata, dynamic element)
        {
            if (element.Value != null && ((IEnumerable<object>)element.Value.value).Any())
            {
                metadata.Add(new KeyValuePair<string, object>(element.Name, (from arrayItem in (JArray)element.Value.value
                                                                             select new Asset
                                                                             {
                                                                                 Name = arrayItem["name"].Value<string>(),
                                                                                 Url = arrayItem["url"].Value<string>()
                                                                             }).ToList()));
            }
        }
    }
}