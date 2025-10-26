
using System.Text;

namespace NoteAppBackend.Kernel.Helpers;
public static class NoteAppHelper
{
    public static string Encode(Guid id) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id}").AsSpan());

    public static Guid Decode(string cursor) =>
        Guid.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(cursor)).AsSpan());
}