namespace KenticoCloud.Wyam.Metadata
{
    /// <summary>
    /// Provides <see cref="IElementParser" /> instances for parsing content item metadata.
    /// </summary>
    public interface IElementParserFactory
    {
        IElementParser GetParser(string type);
    }
}