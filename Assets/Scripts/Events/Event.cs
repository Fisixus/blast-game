using System;
using System.Collections.Generic;

namespace Events
{
    public class Event
    {
        private readonly List<Action<object>> _actions = new List<Action<object>>();

        public void AddListener(Action<object> action)
        {
            _actions.Add(action);
        }

        public void RemoveListener(Action<object> action)
        {
            _actions.Remove(action);
        }

        public void Trigger(object item)
        {
            foreach (var action in _actions)
            {
                action?.Invoke(item);
            }
        }
    }
}
