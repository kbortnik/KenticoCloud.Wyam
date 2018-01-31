using System.Collections.Generic;

namespace KenticoCloud.Wyam.Metadata
{
    public interface IElementParser
    {
        void ParseMetadata(List<KeyValuePair<string, object>> metadata, dynamic element);
    }
}