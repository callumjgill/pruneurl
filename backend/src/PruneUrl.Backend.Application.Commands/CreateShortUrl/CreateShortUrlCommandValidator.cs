using FluentValidation;

namespace PruneUrl.Backend.Application.Commands.CreateShortUrl
{
  /// <summary>
  /// The validator for the <see cref="CreateShortUrlCommand" /> request.
  /// </summary>
  public sealed class CreateShortUrlCommandValidator : AbstractValidator<CreateShortUrlCommand>
  {
    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="CreateShortUrlCommandValidator" /> class.
    /// </summary>
    public CreateShortUrlCommandValidator()
    {
      RuleFor(command => command.SequenceId).GreaterThanOrEqualTo(0);
      RuleFor(command => command.LongUrl).Must(BeUrl);
    }

    #endregion Public Constructors

    #region Private Methods

    private bool BeUrl(string url)
    {
      return Uri.TryCreate(url, UriKind.Absolute, out _);
    }

    #endregion Private Methods
  }
}