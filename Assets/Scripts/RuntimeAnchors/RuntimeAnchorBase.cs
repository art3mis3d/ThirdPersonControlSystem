using System.Collections;
using System.Collections.Generic;
using BaseClasses;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RuntimeAnchorBase<T> : DescriptionBaseSO where T : UnityEngine.Object
{
	public UnityAction OnAnchorProvided;
	
	/// <summary>
	/// Any script can check if the transform is null before using it, by just checking this bool.
	/// </summary>
	[Header("Debug")]
	public bool isSet = false;

	[ReadOnly]
	[SerializeField]
	private T _value;
	public T Value => _value;

	public void Provide(T value)
	{
		if (value == null)
		{
			Debug.LogError("A null value was provided " + this.name + "runtime anchor.");
			return;
		}

		_value = value;
		isSet = true;

		OnAnchorProvided?.Invoke();
	}

	public void Unset()
	{
		_value = null;
		isSet = false;
	}
}
