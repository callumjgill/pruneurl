using FluentValidation;

namespace PruneUrl.Backend.Application.Commands;

/// <summary>
/// The validator for the <see cref="CreateShortUrlCommand" /> request.
/// </summary>
public sealed class CreateShortUrlCommandValidator : AbstractValidator<CreateShortUrlCommand>
{
  /// <summary>
  /// Instantiates a new instance of the <see cref="CreateShortUrlCommandValidator" /> class.
  /// </summary>
  public CreateShortUrlCommandValidator()
  {
    RuleFor(command => command.LongUrl).Must(BeUrl);
  }

  private bool BeUrl(string url)
  {
    return Uri.TryCreate(url, UriKind.Absolute, out _);
  }
}
