using System;
using CodeInUnity.Commands;

namespace CodeInUnity.Command
{
    public class CommandsFactory
    {
        public static BaseCommand Instantiate(CommandData data)
        {
            switch (data.type)
            {
                case "DelayCommand":
                case "delay":
                    return UnityEngine.JsonUtility.FromJson<DelayCommand>(data.data);
                case "RotateToCommand":
                case "rotateTo":
                    return UnityEngine.JsonUtility.FromJson<RotateToCommand>(data.data);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
