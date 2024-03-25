using MediatR;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Queries.GetSequenceId;

/// <summary>
/// An immutable query for retrieving a <see cref="SequenceId" /> entity.
/// </summary>
/// <param name="Id"> The unique identifier for the <see cref="SequenceId" /> entity. </param>
public sealed record GetSequenceIdQuery(string Id) : IRequest<GetSequenceIdQueryResponse> { }
