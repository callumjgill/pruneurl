using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PruneUrl.Backend.Application.Implementation.Factories.Entities;
using PruneUrl.Backend.Application.Interfaces.Providers;
using PruneUrl.Backend.TestHelpers;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace PruneUrl.Backend.Application.Implementation.Tests.UnitTests.Factories.Entities
{
  [TestFixture]
  [Parallelizable]
  public sealed class SequenceIdFactoryUnitTests
  {
    #region Private Fields

    private const int NumberOfIntsToTest = 10000;

    #endregion Private Fields

    #region Public Methods

    [Test]
    public void CreateFromExistingTest_Invalid()
    {
      const string testId = "Testing123";
      var existingSequenceId = EntityTestHelper.CreateSequenceId(testId);
      var sequenceIdFactory = new SequenceIdFactory(Substitute.For<IEntityIdProvider>());
      var negativeIntegers = GetRandomRangeOfValues(int.MinValue, 0, NumberOfIntsToTest);
      var exceptions = new ConcurrentQueue<Exception>();
      Parallel.ForEach(negativeIntegers, invalidSequenceId =>
      {
        try
        {
          Assert.That(() => sequenceIdFactory.CreateFromExisting(existingSequenceId, invalidSequenceId), Throws.TypeOf<ArgumentException>());
        }
        catch (Exception ex)
        {
          exceptions.Enqueue(ex);
        }
      });

      if (!exceptions.IsEmpty)
      {
        throw new AggregateException(exceptions);
      }
    }

    [Test]
    public void CreateFromExistingTest_Valid()
    {
      const string testId = "Testing123";
      var existingSequenceId = EntityTestHelper.CreateSequenceId(testId);
      var positiveIntegers = GetRandomRangeOfValues(0, int.MaxValue, NumberOfIntsToTest);
      var exceptions = new ConcurrentQueue<Exception>();
      Parallel.ForEach(positiveIntegers, sequenceId =>
      {
        try
        {
          var entityIdProvider = Substitute.For<IEntityIdProvider>();

          entityIdProvider.NewId().Returns(testId);

          var sequenceIdFactory = new SequenceIdFactory(entityIdProvider);
          var actualSequenceId = sequenceIdFactory.CreateFromExisting(existingSequenceId, sequenceId);
          Assert.Multiple(() =>
          {
            Assert.That(actualSequenceId.Id, Is.EqualTo(testId));
            Assert.That(actualSequenceId.Value, Is.EqualTo(sequenceId));
          });
          entityIdProvider.DidNotReceive().NewId();
        }
        catch (Exception ex)
        {
          exceptions.Enqueue(ex);
        }
      });

      if (!exceptions.IsEmpty)
      {
        throw new AggregateException(exceptions);
      }
    }

    [Test]
    public void CreateTest_Invalid()
    {
      var sequenceIdFactory = new SequenceIdFactory(Substitute.For<IEntityIdProvider>());
      var negativeIntegers = GetRandomRangeOfValues(int.MinValue, 0, NumberOfIntsToTest);
      var exceptions = new ConcurrentQueue<Exception>();
      Parallel.ForEach(negativeIntegers, invalidSequenceId =>
      {
        try
        {
          Assert.That(() => sequenceIdFactory.Create(invalidSequenceId), Throws.TypeOf<ArgumentException>());
        }
        catch (Exception ex)
        {
          exceptions.Enqueue(ex);
        }
      });

      if (!exceptions.IsEmpty)
      {
        throw new AggregateException(exceptions);
      }
    }

    [Test]
    public void CreateTest_Valid()
    {
      const string testId = "Testing123";
      var positiveIntegers = GetRandomRangeOfValues(0, int.MaxValue, NumberOfIntsToTest);
      var exceptions = new ConcurrentQueue<Exception>();
      Parallel.ForEach(positiveIntegers, sequenceId =>
      {
        try
        {
          var entityIdProvider = Substitute.For<IEntityIdProvider>();

          entityIdProvider.NewId().Returns(testId);

          var sequenceIdFactory = new SequenceIdFactory(entityIdProvider);
          var actualSequenceId = sequenceIdFactory.Create(sequenceId);
          Assert.Multiple(() =>
          {
            Assert.That(actualSequenceId.Id, Is.EqualTo(testId));
            Assert.That(actualSequenceId.Value, Is.EqualTo(sequenceId));
          });
          entityIdProvider.Received().NewId();
        }
        catch (Exception ex)
        {
          exceptions.Enqueue(ex);
        }
      });

      if (!exceptions.IsEmpty)
      {
        throw new AggregateException(exceptions);
      }
    }

    #endregion Public Methods

    #region Private Methods

    private IEnumerable<int> GetRandomRangeOfValues(int inclusiveMinValue, int exclusiveMaxValue, int count)
    {
      var rangeOfInts = new List<int>();
      while (count > 0)
      {
        rangeOfInts.Add(RandomNumberGenerator.GetInt32(inclusiveMinValue, exclusiveMaxValue));
        count--;
      }

      return rangeOfInts;
    }

    #endregion Private Methods
  }
}