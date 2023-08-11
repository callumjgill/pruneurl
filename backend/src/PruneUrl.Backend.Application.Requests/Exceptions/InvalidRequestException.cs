using FluentValidation.Results;
using MediatR;
using System.Text;

namespace PruneUrl.Backend.Application.Requests.Exceptions
{
  /// <summary>
  /// An exception which is thrown when a <see cref="IRequest" /> or <see cref="IRequest{T}" /> is invalid.
  /// </summary>
  public sealed class InvalidRequestException : Exception
  {
    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="InvalidRequestException" />
    /// </summary>
    /// <param name="validationFailures">
    /// The collection of <see cref="ValidationFailure" />'s to create the exception message from.
    /// </param>
    public InvalidRequestException(IEnumerable<ValidationFailure> validationFailures) : base(GetExceptionMessage(validationFailures))
    {
    }

    #endregion Public Constructors

    #region Private Methods

    private static string GetExceptionMessage(IEnumerable<ValidationFailure> validationFailures)
    {
      var builder = new StringBuilder();
      builder.AppendLine("The request is invalid!");
      foreach (string propertyName in validationFailures.Select(validationFailure => validationFailure.PropertyName).Distinct())
      {
        builder.Append("\t")
               .Append("Property '")
               .Append(propertyName)
               .AppendLine("' is invalid.");
      }

      return builder.ToString();
    }

    #endregion Private Methods
  }
}