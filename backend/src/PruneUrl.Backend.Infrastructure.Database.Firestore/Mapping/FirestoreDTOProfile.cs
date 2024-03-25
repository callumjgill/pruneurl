using AutoMapper;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Mapping
{
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
}
