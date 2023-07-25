using FluentValidation;

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
    public GetShortUrlQueryValidator()
    {
      RuleFor(query => query.ShortUrl).NotEmpty();
    }

    #endregion Public Constructors
  }
}