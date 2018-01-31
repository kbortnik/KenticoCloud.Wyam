using System.Collections.Generic;

namespace KenticoCloud.Wyam.Metadata
{
    public class DefaultElementParser : IElementParser
    {
        public void ParseMetadata(List<KeyValuePair<string, object>> metadata, dynamic element)
        {
            metadata.Add(new KeyValuePair<string, object>(element.Name, element.Value.value));
        }
    }
}