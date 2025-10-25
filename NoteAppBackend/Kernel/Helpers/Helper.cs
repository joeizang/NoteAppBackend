
using System.Text;

namespace NoteAppBackend.Kernel.Helpers;
public static class NoteAppHelper
{
    public static string Encode(DateTime date) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes($"{date}").AsSpan());

    public static DateTime Decode(string cursor) =>
        DateTime.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(cursor)).AsSpan());
}