﻿using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Exceptions
{
  /// <summary>
  /// An exception which is thrown when mappings between <see cref="IEntity" /> and <see
  /// cref="FirestoreEntityDTO" /> are invalid.
  /// </summary>
  public sealed class InvalidEntityTypeMapException : Exception
  {
    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="InvalidEntityTypeMapException" /> class.
    /// </summary>
    /// <param name="invalidType"> The type of <see cref="IEntity" /> which is invalid. </param>
    public InvalidEntityTypeMapException(Type invalidType) : base($"No mapping exists for the type {invalidType}!")
    {
    }

    #endregion Public Constructors
  }
}