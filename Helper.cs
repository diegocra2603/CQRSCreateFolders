namespace CQRSCreateFolders;

public static class Helper
{
    public static string FirstCharLower(this string input)
    {
        return char.ToLower(input[0]) + input.Substring(1);
    }

    public static string ArrayToString(this string[] values)
    {
        var value = "";

        foreach (var item in values)
        {
            value += item;
        }

        return value;
    }
}
