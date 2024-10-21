using AIToolbox.Options.Agents;

namespace AIToolbox.DependencyInjection;

public interface IAddAgents
{
    IAgentsBuilder AddAgents(AgentOptions? options = null);
    IAgentsBuilder AddAgents(Action<AgentOptions> optionsAction);
}
