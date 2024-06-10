using Cocona;
using Ghost.Application.Commands;

namespace Ghost.Application;

public static class DependencyInjection
{
    public static CoconaApp RegisterApplicationLayerCommands(this CoconaApp app)
    {
        app.AddCommands<ConfigurationCommand>();
        app.AddCommands<CommitCommand>();
        return app;
    }
}
