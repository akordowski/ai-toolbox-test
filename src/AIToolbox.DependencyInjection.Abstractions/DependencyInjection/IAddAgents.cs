using AIToolbox.Options.Agents;

namespace AIToolbox.DependencyInjection;

public interface IAddAgents
{
    IAgentServiceBuilder AddAgents(AgentOptions? options = null);
    IAgentServiceBuilder AddAgents(Action<AgentOptions> optionsAction);
}
