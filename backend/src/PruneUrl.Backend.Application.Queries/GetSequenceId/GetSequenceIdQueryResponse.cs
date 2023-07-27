using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Queries.GetSequenceId
{
  /// <summary>
  /// The immutable response to a <see cref="GetSequenceIdQuery" /> request.
  /// </summary>
  /// <param name="SequenceId">
  /// The <see cref="Domain.Entities.SequenceId" /> entity found, or <c> null </c> if not found.
  /// </param>
  public sealed record GetSequenceIdQueryResponse(SequenceId? SequenceId)
  {
  }
}