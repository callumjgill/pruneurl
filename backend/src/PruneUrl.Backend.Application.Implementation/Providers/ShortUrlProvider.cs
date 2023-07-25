using PruneUrl.Backend.Application.Interfaces.Providers;

namespace PruneUrl.Backend.Application.Implementation.Providers
{
  /// <summary>
  /// A provider/converter for "short" url's. This takes the long url and converts it's string
  /// representation into a base 62 representation. This representation uses all numbers, lowercase
  /// letters and uppercase letters.
  /// </summary>
  public sealed class ShortUrlProvider : IShortUrlProvider, ISequenceIdProvider
  {
    #region Private Fields

    private readonly int baseNumber = 62;
    private readonly char[] characterMap = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    #endregion Private Fields

    #region Public Methods

    /// <inheritdoc cref="ISequenceIdProvider.GetSequenceId(string)" />
    public int GetSequenceId(string shortUrl)
    {
      int sequenceId = 0;
      char[] shortUrlChars = shortUrl.ToCharArray();
      double exponent = shortUrlChars.Length - 1;
      foreach (char shortUrlCharacter in shortUrlChars)
      {
        double indexInCharMap = Array.FindIndex(characterMap, character => character == shortUrlCharacter);
        sequenceId += (int)(indexInCharMap * Math.Pow(baseNumber, exponent));
        exponent--;
      }

      return sequenceId;
    }

    /// <inheritdoc cref="IShortUrlProvider.GetShortUrl(int)" />
    public string GetShortUrl(int sequenceId)
    {
      var shortUrlCharacters = new Stack<char>();
      do
      {
        char nextCharacter = characterMap[sequenceId % baseNumber];
        shortUrlCharacters.Push(nextCharacter);
        sequenceId = sequenceId / baseNumber;
      } while (sequenceId > 0);

      return string.Concat(shortUrlCharacters);
    }

    #endregion Public Methods
  }
}