namespace FakeItEasy.Tests
{
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using FakeItEasy.Core;
    using FakeItEasy.Creation;
    using FakeItEasy.Expressions;
    
    public static class CustomArgumentConstraints
    {
        public static T IsThisSequence<T>(this IArgumentConstraintManager<T> scope, T collection) where T : IEnumerable
        {
            return scope.Matches(
                x => x.Cast<object>().SequenceEqual(collection.Cast<object>()), 
                "This sequence: " + collection.Cast<object>().ToCollectionString(x => x.ToString(), ", "));
        }

        public static T IsThisSequence<T>(this IArgumentConstraintManager<T> scope, params object[] collection) where T : IEnumerable
        {
            return scope.Matches(x => x != null && x.Cast<object>().SequenceEqual(collection.Cast<object>()), "This sequence: " + collection.ToCollectionString(x => x.ToString(), ", "));
        }

        
        public static Expression ProducesValue(this IArgumentConstraintManager<Expression> scope, object expectedValue)
        {
            return scope.Matches(x => object.Equals(expectedValue, Helpers.GetValueProducedByExpression(x)), 
			                                string.Format(CultureInfo.InvariantCulture, "Expression that produces the value {0}", expectedValue));
        }

        public static FakeManager Fakes(this IArgumentConstraintManager<FakeManager> scope, object fakedObject)
        {
            return scope.Matches(x => x.Equals(Fake.GetFakeManager(fakedObject)), "Specified FakeObject");
        }

        internal static FakeOptions IsEmpty(this IArgumentConstraintManager<FakeOptions> scope)
        {
            return scope.Matches(
                x => 
                {
                    return x.AdditionalInterfacesToImplement == null
                        && x.ArgumentsForConstructor == null
                        && x.SelfInitializedFakeRecorder == null
                        && x.WrappedInstance == null;
                }, "Empty fake options");
        }
    }
}
