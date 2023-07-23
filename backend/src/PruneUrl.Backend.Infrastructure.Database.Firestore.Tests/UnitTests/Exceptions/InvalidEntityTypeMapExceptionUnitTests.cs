﻿using NUnit.Framework;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Exceptions;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Exceptions
{
  [TestFixture]
  [Parallelizable]
  public sealed class InvalidEntityTypeMapExceptionUnitTests
  {
    #region Public Methods

    [TestCase<SequenceId>]
    [TestCase<ShortUrl>]
    public void ConstructorTest<T>()
    {
      var exception = new InvalidEntityTypeMapException(typeof(T));
      Assert.That(exception.Message, Is.EqualTo($"No mapping exists for the type {typeof(T)}!"));
    }

    #endregion Public Methods
  }
}