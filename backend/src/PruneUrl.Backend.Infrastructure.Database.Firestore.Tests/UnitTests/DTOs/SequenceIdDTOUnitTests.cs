using NUnit.Framework;
using NUnit.Framework.Internal;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests;

[TestFixture]
[Parallelizable]
internal sealed class SequenceIdDTOUnitTests : FirestoreEntityDTOUnitTests
{
  [TestCase(0)]
  [TestCase(1)]
  [TestCase(2)]
  [TestCase(4)]
  [TestCase(8)]
  [TestCase(16)]
  [TestCase(32)]
  [TestCase(64)]
  [TestCase(128)]
  [TestCase(256)]
  public void ValueTest(int testValue)
  {
    PropertyTest(
      null,
      testValue,
      (entityDTO) => ((SequenceIdDTO)entityDTO).Value,
      (entityDTO, newValue) => ((SequenceIdDTO)entityDTO).Value = newValue
    );
  }

  protected override FirestoreEntityDTO CreateDTO()
  {
    return new SequenceIdDTO();
  }
}
