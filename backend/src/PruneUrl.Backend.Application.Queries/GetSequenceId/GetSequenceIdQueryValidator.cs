﻿using FluentValidation;

namespace PruneUrl.Backend.Application.Queries.GetSequenceId
{
  /// <summary>
  /// A validator for the <see cref="GetSequenceIdQuery" />.
  /// </summary>
  public sealed class GetSequenceIdQueryValidator : AbstractValidator<GetSequenceIdQuery>
  {
    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="GetSequenceIdQueryValidator" /> class.
    /// </summary>
    public GetSequenceIdQueryValidator()
    {
      RuleFor(query => query.Id).NotEmpty();
    }

    #endregion Public Constructors
  }
}