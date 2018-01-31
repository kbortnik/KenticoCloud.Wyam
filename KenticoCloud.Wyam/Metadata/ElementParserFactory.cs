namespace KenticoCloud.Wyam.Metadata
{
    /// <inheritdoc />
    public class ElementParserFactory : IElementParserFactory
    {
        public IElementParser GetParser(string type)
        {
            switch (type.ToLower())
            {
                case "asset":
                    return new AssetElementParser();

                default:
                    return new DefaultElementParser();
            }
        }
    }
}