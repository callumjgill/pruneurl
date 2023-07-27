using FluentValidation;

namespace PruneUrl.Backend.Application.Commands.CreateSequenceId
{
  /// <summary>
  /// A validator for the <see cref="CreateSequenceIdCommand" />.
  /// </summary>
  public sealed class CreateSequenceIdCommandValidator : AbstractValidator<CreateSequenceIdCommand>
  {
    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="CreateSequenceIdCommandValidator" /> class.
    /// </summary>
    public CreateSequenceIdCommandValidator()
    {
      RuleFor(command => command.Id).NotEmpty();
    }

    #endregion Public Constructors
  }
}