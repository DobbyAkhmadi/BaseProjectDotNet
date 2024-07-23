using BaseProjectDotnet.Models;

namespace BaseProjectDotnet.Helpers.External;

public interface ICurlExternal
{
    DbResponseResult RequestApiExt(string method, string url, string token, object? param = null);
}