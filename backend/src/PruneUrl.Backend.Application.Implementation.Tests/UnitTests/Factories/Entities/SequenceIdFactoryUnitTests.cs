using System.Collections.Concurrent;
using System.Security.Cryptography;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PruneUrl.Backend.Application.Implementation.Factories.Entities;

namespace PruneUrl.Backend.Application.Implementation.Tests.UnitTests.Factories.Entities;

[TestFixture]
[Parallelizable]
public sealed class SequenceIdFactoryUnitTests
{
  private const int NumberOfIntsToTest = 10000;

  [Test]
  public void CreateTest_Invalid()
  {
    const string testId = "Testing123";
    var sequenceIdFactory = new SequenceIdFactory();
    var negativeIntegers = GetRandomRangeOfValues(int.MinValue, 0, NumberOfIntsToTest);
    var exceptions = new ConcurrentQueue<Exception>();
    Parallel.ForEach(
      negativeIntegers,
      invalidSequenceId =>
      {
        try
        {
          Assert.That(
            () => sequenceIdFactory.Create(testId, invalidSequenceId),
            Throws.TypeOf<ArgumentException>()
          );
        }
        catch (Exception ex)
        {
          exceptions.Enqueue(ex);
        }
      }
    );

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
    Parallel.ForEach(
      positiveIntegers,
      sequenceId =>
      {
        try
        {
          var sequenceIdFactory = new SequenceIdFactory();
          var actualSequenceId = sequenceIdFactory.Create(testId, sequenceId);
          Assert.Multiple(() =>
          {
            Assert.That(actualSequenceId.Id, Is.EqualTo(testId));
            Assert.That(actualSequenceId.Value, Is.EqualTo(sequenceId));
          });
        }
        catch (Exception ex)
        {
          exceptions.Enqueue(ex);
        }
      }
    );

    if (!exceptions.IsEmpty)
    {
      throw new AggregateException(exceptions);
    }
  }

  private IEnumerable<int> GetRandomRangeOfValues(
    int inclusiveMinValue,
    int exclusiveMaxValue,
    int count
  )
  {
    var rangeOfInts = new List<int>();
    while (count > 0)
    {
      rangeOfInts.Add(RandomNumberGenerator.GetInt32(inclusiveMinValue, exclusiveMaxValue));
      count--;
    }

    return rangeOfInts;
  }
}
