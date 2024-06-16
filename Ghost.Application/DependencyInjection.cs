using Cocona;
using Ghost.Infrastructure.Commands;

namespace Ghost.Infrastructure;

public static class DependencyInjection
{
    public static CoconaApp RegisterApplicationLayerCommands(this CoconaApp app)
    {
        app.AddCommands<ConfigurationCommand>();
        app.AddCommands<CommitCommand>();
        return app;
    }
}
