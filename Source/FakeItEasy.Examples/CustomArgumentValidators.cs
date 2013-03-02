namespace FakeItEasy.Examples
{
    using System.Globalization;

    public static class CustomArgumentValidators
    {
        public static string IsLongerThan(this IArgumentConstraintManager<string> validations, int length)
        {
            return validations.Matches(x => x.Length > length, string.Format(CultureInfo.InvariantCulture, "Longer than {0}", length));
        }
    }
}
