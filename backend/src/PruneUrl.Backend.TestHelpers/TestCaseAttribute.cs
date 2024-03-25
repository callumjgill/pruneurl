using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace PruneUrl.Backend.TestHelpers;

/// <summary>
/// A <see cref="TestCaseAttribute" /> which can specify generic arguments.
/// </summary>
/// <typeparam name="T"> The generic argument to use. </typeparam>
/// <remarks>
/// This has been adapted from the following stack overflow post (answer from György Kőszeg): https://stackoverflow.com/questions/2364929/nunit-testcase-with-generics
/// </remarks>
/// <param name="arguments"> The arguments for the test case. </param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class TestCaseAttribute<T>(params object[] arguments)
  : TestCaseAttribute(arguments),
    ITestBuilder
{
  /// <summary>
  /// The type arguments
  /// </summary>
  public Type TypeArgument => typeof(T);

  /// <inheritdoc cref="ITestBuilder.BuildFrom(IMethodInfo, Test?)"
  public new IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test? suite)
  {
    if (!method.IsGenericMethodDefinition)
    {
      return base.BuildFrom(method, suite);
    }

    var genMethod = method.MakeGenericMethod(TypeArgument);
    return base.BuildFrom(genMethod, suite);
  }
}
