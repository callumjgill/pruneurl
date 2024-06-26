﻿using System.Text;
using FluentValidation.Results;
using MediatR;

namespace PruneUrl.Backend.Application.Requests;

/// <summary>
/// An exception which is thrown when a <see cref="IRequest" /> or <see cref="IRequest{T}" /> is invalid.
/// </summary>
/// <param name="validationFailures">
/// The collection of <see cref="ValidationFailure" />'s to create the exception message from.
/// </param>
public sealed class InvalidRequestException(IEnumerable<ValidationFailure> validationFailures)
  : Exception(GetExceptionMessage(validationFailures))
{
  private static string GetExceptionMessage(IEnumerable<ValidationFailure> validationFailures)
  {
    var builder = new StringBuilder();
    builder.AppendLine("The request is invalid!");
    foreach (
      string propertyName in validationFailures
        .Select(validationFailure => validationFailure.PropertyName)
        .Distinct()
    )
    {
      builder.Append("\t").Append("Property '").Append(propertyName).AppendLine("' is invalid.");
    }

    return builder.ToString();
  }
}
