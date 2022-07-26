using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ScriptableObjectHelper
{
	public static void GenerateButtonsForEvents<T>(Object target) where T : ScriptableObject
	{
		var targetIr = target as T;
		if (targetIr != null)
		{
			var typeIr = targetIr.GetType();
			var events = typeIr.GetEvents();

			foreach (var ev in events)
			{
				if (GUILayout.Button(ev.Name))
				{
					// Delegates doesn't support direct access to RaiseMethod, must use backing field.
					// https://stackoverflow.com/questions/14885325/eventinfo-getraisemethod-always-null
					var eventDelegate = typeIr.GetField(ev.Name, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(targetIr) as MulticastDelegate;

					try
					{
						eventDelegate?.DynamicInvoke();
					}
					catch
					{
						Debug.LogWarning($"Event'{ev.Name}' requires some arguments which weren't provided. Delegate can't be invoked directly from UI.");
					}
				}
			}
		}
	}
}