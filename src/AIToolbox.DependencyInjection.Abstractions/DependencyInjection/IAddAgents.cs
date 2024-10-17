using AIToolbox.Options;

namespace AIToolbox.DependencyInjection;

public interface IAddAgents
{
    IAgentsBuilder AddAgents(AgentOptions? options = null);
    IAgentsBuilder AddAgents(Action<AgentOptions> optionsAction);
}
