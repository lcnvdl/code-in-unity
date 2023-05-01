using System;
using CodeInUnity.Commands;
using CodeInUnity.Core.Utils;
using UnityEngine;

namespace CodeInUnity.Command
{
    public class CommandsFactory
    {
        public virtual BaseCommand Instantiate(CommandData data)
        {
            BaseCommand cmd;

            switch (data.type)
            {
                case "DelayCommand":
                case "delay":
                    cmd = JsonUtility.FromJson<DelayCommand>(data.data);
                    break;
                case "MoveToCommand":
                case "moveTo":
                    cmd = JsonUtility.FromJson<MoveToCommand>(data.data);
                    break;
                case "RotateToCommand":
                case "rotateTo":
                    cmd = JsonUtility.FromJson<RotateToCommand>(data.data);
                    break;
                default:
                    throw new InvalidOperationException("Unknown command: " + data.type);
            }

            if (cmd != null)
            {
                this.AssignMetadataToNewCommand(cmd, data);
            }

            return cmd;
        }

        protected virtual void AssignMetadataToNewCommand(BaseCommand cmd, CommandData data)
        {
            cmd.internalId = data.internalId;
            cmd.priority = data.priority;

            if (data.targetInstanceId > 0)
            {
                var objectFromId = GameObjectUtils.FindObjectFromInstanceID(data.targetInstanceId);

                cmd.manualTarget = objectFromId as GameObject;

                if (cmd.manualTarget == null)
                {
                    if (objectFromId is Component c)
                    {
                        cmd.manualTarget = c.gameObject;
                    }
                }
            }
        }
    }
}
