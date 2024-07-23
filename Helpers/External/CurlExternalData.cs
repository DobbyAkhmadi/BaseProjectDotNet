using BaseProjectDotnet.Models;

namespace BaseProjectDotnet.Helpers.External;

public class CurlExternalData : ICurlExternal
{
    public DbResponseResult RequestApiExt(string method, string url, string token, object? param = null)
    {
        return new DbResponseResult();
    }
}