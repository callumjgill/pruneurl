using NUnit.Framework;

namespace PruneUrl.Backend.Application.Configuration.Tests;

[TestFixture]
[Parallelizable]
public sealed class SequenceIdOptionsUnitTests
{
  [Test]
  public void IdTest_DefaultValue()
  {
    var sequenceIdOptions = new SequenceIdOptions();
    Assert.That(sequenceIdOptions.Id, Is.EqualTo(string.Empty));
  }

  [Test]
  public void IdTest_Set()
  {
    var testId = Guid.NewGuid().ToString();
    var sequenceIdOptions = new SequenceIdOptions() { Id = testId };
    Assert.That(sequenceIdOptions.Id, Is.EqualTo(testId));
  }
}
