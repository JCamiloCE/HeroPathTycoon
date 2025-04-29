using JCC.Utils.DebugManager;
using System.Collections.Generic;

public class Fake_IDebug : IDebug
{
    public List<string> generalMessages = new List<string>();
    public List<string> verboseMessages = new List<string>();
    public List<string> errorMessages = new List<string>();
    public List<string> warningMessages = new List<string>();

    void IDebug.LogError(string message)
    {
        generalMessages.Add(message);
        errorMessages.Add(message);
    }

    void IDebug.LogVerbose(string message)
    {
        generalMessages.Add(message);
        verboseMessages.Add(message);
    }

    void IDebug.LogWarning(string message)
    {
        generalMessages.Add(message);
        warningMessages.Add(message);
    }
}
