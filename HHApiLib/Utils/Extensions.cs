using Flurl.Http;

namespace HHApiLib.Utils;

internal static class Extensions
{
    public static IFlurlRequest WithHeaderAgent(this IFlurlRequest request)
    {
        request.WithHeader("HH-User-Agent", $"kvd_hh_control/1.0 (kvd_z1cat@gmail.com)");
        return request;
    }
}
