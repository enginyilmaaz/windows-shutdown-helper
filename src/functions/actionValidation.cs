using System;
using System.Collections.Generic;

namespace WindowsShutdownHelper.functions
{
    internal static class actionValidation
    {
        private static readonly HashSet<string> SessionEndingActions = new HashSet<string>
        {
            config.actionTypes.shutdownComputer,
            config.actionTypes.restartComputer,
            config.actionTypes.logOffWindows
        };

        public static bool TryValidateActionForAdd(
            ActionModel newAction,
            IEnumerable<ActionModel> existingActions,
            Language language,
            out string errorMessage)
        {
            errorMessage = null;

            if (newAction == null)
            {
                errorMessage = language?.messageContent_actionChoose ?? "Invalid action.";
                return false;
            }

            if (newAction.triggerType != config.triggerTypes.systemIdle)
            {
                return true;
            }

            if (!TryGetSystemIdleSeconds(newAction, out int newActionSeconds) || newActionSeconds <= 0)
            {
                errorMessage = language?.messageContent_actionChoose ?? "Invalid action.";
                return false;
            }

            foreach (ActionModel existingAction in existingActions)
            {
                if (existingAction == null || existingAction.triggerType != config.triggerTypes.systemIdle)
                {
                    continue;
                }

                if (!TryGetSystemIdleSeconds(existingAction, out int existingActionSeconds) || existingActionSeconds <= 0)
                {
                    continue;
                }

                if (string.Equals(existingAction.actionType, newAction.actionType, StringComparison.Ordinal))
                {
                    errorMessage = language?.messageContent_idleActionConflict
                        ?? "This idle-time action conflicts with existing actions.";
                    return false;
                }

                if (IsSessionEndingAction(existingAction.actionType) && existingActionSeconds <= newActionSeconds)
                {
                    errorMessage = language?.messageContent_idleActionConflict
                        ?? "This idle-time action conflicts with existing actions.";
                    return false;
                }

                if (IsSessionEndingAction(newAction.actionType) && existingActionSeconds >= newActionSeconds)
                {
                    errorMessage = language?.messageContent_idleActionConflict
                        ?? "This idle-time action conflicts with existing actions.";
                    return false;
                }
            }

            return true;
        }

        private static bool IsSessionEndingAction(string actionType)
        {
            return !string.IsNullOrWhiteSpace(actionType) && SessionEndingActions.Contains(actionType);
        }

        private static bool TryGetSystemIdleSeconds(ActionModel action, out int seconds)
        {
            seconds = 0;

            if (action == null || string.IsNullOrWhiteSpace(action.value))
            {
                return false;
            }

            if (!int.TryParse(action.value, out int parsed))
            {
                return false;
            }

            if (string.IsNullOrEmpty(action.valueUnit))
            {
                seconds = parsed * 60;
            }
            else
            {
                seconds = parsed;
            }

            return true;
        }
    }
}
