
using System;
using System.Collections.Generic;
using System.Globalization;



public static class Events
{
    public static StringComparer EventStringComparer { get; } = StringComparer.Create(CultureInfo.InvariantCulture,
        CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols);



    private static readonly Dictionary<string, List<Action<string>>> _subscribers = new(EventStringComparer);



    public static void Subscribe(string eventName, Action<string> action)
    {
        if (_subscribers.TryGetValue(eventName, out var events))
        {
            events.Add(action);
        }
        else
        {
            _subscribers.Add(eventName, [action]);
        }
    }



    public static void Publish(string eventName, string value)
    {
        if (!_subscribers.TryGetValue(eventName, out var actions)) return;
        foreach (var action in actions)
        {
            action?.Invoke(value);
        }
    }
}
