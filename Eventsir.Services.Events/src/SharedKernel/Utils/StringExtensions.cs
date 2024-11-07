namespace SharedKernel.Utils
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string input)
        {
            return char.IsUpper(input[0]) ? input[0].ToString().ToLower() + input.Substring(1, input.Length - 1) : input;
        }
    }
}
