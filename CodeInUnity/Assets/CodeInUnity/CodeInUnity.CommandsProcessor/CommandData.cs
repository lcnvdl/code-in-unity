using System;

namespace CodeInUnity.CommandsProcessor
{
    [Serializable]
    public class CommandData
    {
        public string type;

        public string data;

        public int priority;

        public string internalId;

        public int targetInstanceId;
    }

    [Serializable]
    public class CommandDataGroup
    {
        public CommandData[] commands;
    }
}
