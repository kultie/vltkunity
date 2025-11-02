
using System;
using System.Collections.Generic;

namespace game.resource.settings.npcres
{
    public class Callback
    {
        private class OnAction
        {
            public Func<object> endFunction;
            public int endLoopTimes;
            public int endCallTimes;
        }

        // action.name => <...>
        private readonly Dictionary<string, Callback.OnAction> onActions;

        public Callback()
        {
            this.onActions = new Dictionary<string, OnAction>();
        }

        public void SetActionEnd(string _actionName, Func<object> _callback, int _loopTimes)
        {
            if(this.onActions.ContainsKey(_actionName) == false)
            {
                this.onActions[_actionName] = new OnAction();
            }

            this.onActions[_actionName].endFunction = _callback;
            this.onActions[_actionName].endLoopTimes = _loopTimes;
            this.onActions[_actionName].endCallTimes = 0;
        }

        public void OnActionEnd(string _actionName)
        {
            if(this.onActions.ContainsKey(_actionName) == false)
            {
                return;
            }

            Callback.OnAction action = this.onActions[_actionName];

            if(action.endCallTimes >= action.endLoopTimes)
            {
                return;
            }

            action.endCallTimes++;
            action.endFunction();
        }

        public void OnActionEnd(string _actionName, ushort _frameCurrent, ushort _frameEnd)
        {
            if(_frameCurrent != _frameEnd)
            {
                return;
            }

            this.OnActionEnd(_actionName);
        }
    }
}
