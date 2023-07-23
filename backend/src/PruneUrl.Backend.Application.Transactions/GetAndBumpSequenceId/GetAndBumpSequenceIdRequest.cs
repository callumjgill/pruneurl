using MediatR;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId
{
  /// <summary>
  /// An immutable transaction request used to retrieve a <see cref="SequenceId" /> entity and bump
  /// it's internal value by one.
  /// </summary>
  public sealed record GetAndBumpSequenceIdRequest : IRequest<GetAndBumpSequenceIdResponse>
  {
  }
}