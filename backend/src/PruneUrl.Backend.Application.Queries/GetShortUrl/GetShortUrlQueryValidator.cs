using FluentValidation;
using PruneUrl.Backend.Application.Interfaces.Providers;

namespace PruneUrl.Backend.Application.Queries.GetShortUrl
{
  /// <summary>
  /// The validator for the <see cref="GetShortUrlQuery" />.
  /// </summary>
  public sealed class GetShortUrlQueryValidator : AbstractValidator<GetShortUrlQuery>
  {
    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="GetShortUrlQueryValidator" /> class.
    /// </summary>
    /// <param name="sequenceIdProvider">
    /// The <see cref="ISequenceIdProvider" /> to use to validate the short url format.
    /// </param>
    public GetShortUrlQueryValidator(ISequenceIdProvider sequenceIdProvider)
    {
      RuleFor(query => query.ShortUrl).NotEmpty().Must(shortUrl => BeValidShortUrl(sequenceIdProvider, shortUrl));
    }

    #endregion Public Constructors

    #region Private Methods

    private static bool BeValidShortUrl(ISequenceIdProvider sequenceIdProvider, string shortUrl)
    {
      int sequenceId = sequenceIdProvider.GetSequenceId(shortUrl);
      return sequenceId >= 0;
    }

    #endregion Private Methods
  }
}