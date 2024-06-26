﻿using NUnit.Framework;

namespace PruneUrl.Backend.Application.Exceptions.Tests;

[TestFixture]
[Parallelizable]
public sealed class InvalidConfigurationExceptionUnitTests
{
  [Test]
  public void ConstructorTest()
  {
    const string sectionName = "TestSection";
    const string error = "TestError";
    var invalidConfigException = new InvalidConfigurationException(sectionName, error);
    Assert.That(
      invalidConfigException.Message,
      Is.EqualTo($"The section '{sectionName}' is invalid! {error}")
    );
  }
}
