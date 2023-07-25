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
    /// <param name="requestName"> The name of the request that is invalid. </param>
    /// <param name="validationFailures">
    /// The collection of <see cref="ValidationFailure" />'s to create the exception message from.
    /// </param>
    public InvalidRequestException(string requestName, IEnumerable<ValidationFailure> validationFailures) : base(GetExceptionMessage(requestName, validationFailures))
    {
    }

    #endregion Public Constructors

    #region Private Methods

    private static string GetExceptionMessage(string requestName, IEnumerable<ValidationFailure> validationFailures)
    {
      var builder = new StringBuilder();
      builder.Append("The '")
             .Append(requestName)
             .AppendLine("' request is invalid! Validation errors:");
      foreach (var validationFailure in validationFailures)
      {
        builder.Append("\t")
               .Append("Property '")
               .Append(validationFailure.PropertyName)
               .Append("' is invalid: ")
               .AppendLine(validationFailure.ErrorMessage);
      }

      return builder.ToString();
    }

    #endregion Private Methods
  }
}