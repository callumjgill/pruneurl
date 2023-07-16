using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace PruneUrl.Backend.TestHelpers
{
  /// <summary>
  /// A <see cref="TestCaseAttribute" /> which can specify generic arguments.
  /// </summary>
  /// <typeparam name="T"> The generic argument to use. </typeparam>
  /// <remarks>
  /// This has been adapted from the following stack overflow post (answer from György Kőszeg): https://stackoverflow.com/questions/2364929/nunit-testcase-with-generics
  /// </remarks>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
  public sealed class TestCaseAttribute<T> : TestCaseAttribute, ITestBuilder
  {
    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="TestCaseAttribute{T}" /> class.
    /// </summary>
    /// <param name="arguments"> The arguments for the test case. </param>
    public TestCaseAttribute(params object[] arguments) : base(arguments)
    {
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// The type arguments
    /// </summary>
    public Type TypeArgument => typeof(T);

    #endregion Public Properties

    #region Public Methods

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

    #endregion Public Methods
  }
}