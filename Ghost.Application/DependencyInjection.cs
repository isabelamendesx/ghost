using Cocona;

namespace Ghost.Application;

public static class DependencyInjection
{
    public static CoconaApp RegisterApplicationLayerCommands(this CoconaApp services)
    {
        return services;
    }
}
