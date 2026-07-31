namespace CsvSerializer;

public static class Extensions
{
    public static void TrimWhitespaces(this string[] elements)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = elements[i].Trim();
        }
    }
}