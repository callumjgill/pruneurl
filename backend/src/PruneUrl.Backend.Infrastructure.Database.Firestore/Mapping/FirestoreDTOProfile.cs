using AutoMapper;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore;

/// <summary>
/// A <see cref="Profile" /> for mapping <see cref="FirestoreEntityDTO" />'s to their equivalent
/// <see cref="IEntity" />.
/// </summary>
public sealed class FirestoreDTOProfile : Profile
{
  /// <summary>
  /// Instantiates a new instance of the <see cref="FirestoreDTOProfile" /> class.
  /// </summary>
  public FirestoreDTOProfile()
  {
    CreateMap<SequenceIdDTO, SequenceId>().ReverseMap();
    CreateMap<ShortUrlDTO, ShortUrl>().ReverseMap();
  }
}
