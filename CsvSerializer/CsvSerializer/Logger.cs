using System.Text;

namespace CsvSerializer;

public static class CsvLogger
{
    private static StringBuilder _fullLog = new StringBuilder();
    private static StringBuilder _header = new StringBuilder();

    public static void Log(List<CsvPropertyInfo> list)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < list.Count; i++)
        {
            if (i < list.Count - 1)
                sb.Append(list[i].Value).Append(", ");
            else
                sb.Append(list[i].Value);
        }
        _fullLog.AppendLine(sb.ToString());
    }

    public static void CreateHeader(List<CsvPropertyInfo> list)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < list.Count; i++)
        {
            if (i < list.Count - 1)
                sb.Append(list[i].Name).Append(", ");
            else
                sb.Append(list[i].Name);
        }
        _header.AppendLine(sb.ToString());
    }

    public static string GetLog()
    {
        StringBuilder sb = new StringBuilder();
        
        sb.Append(_header.ToString());
        sb.Append(_fullLog.ToString());
        Clear();
        
        return sb.ToString();
    }
    
    public static void Clear()
    {
        _fullLog.Clear();
        _header.Clear();
    }
}