namespace HHApiLib.Configurations;

public static class Setup
{
    private static Config _config;

    // TODO: internal
    public static Config Conf => _config;

    public static void Init(Config config)
    {
        if (_config == null)
        {
            _config = config;
        }
    }
}
