using NUnit.Framework;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Domain.Entities.Tests.UnitTests;

[TestFixture]
[Parallelizable]
public sealed class SequenceIdUnitTests
{
  [TestCase("", default(int))]
  [TestCase("ab0ed3aa-b540-4732-a11f-1d43333a659d", 1076)]
  [TestCase("This is an id", 432)]
  public void ConstructorTest(string id, int value)
  {
    SequenceId testSequenceId = EntityTestHelper.CreateSequenceId(id, value);
    Assert.Multiple(() =>
    {
      Assert.That(testSequenceId.Id, Is.EqualTo(id));
      Assert.That(testSequenceId.Value, Is.EqualTo(value));
    });
  }
}
