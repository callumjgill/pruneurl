using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId;

/// <summary>
/// The immutable response to a <see cref="GetAndBumpSequenceIdRequest" />.
/// </summary>
/// <param name="SequenceId"> The <see cref="SequenceId" /> instance found. </param>
public sealed record GetAndBumpSequenceIdResponse(SequenceId SequenceId) { }
