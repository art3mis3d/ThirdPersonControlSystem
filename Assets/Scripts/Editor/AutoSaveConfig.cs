// using System.Collect555ions;

using UnityEngine;

namespace Art3mis3d
{
	public class AutoSaveConfig : ScriptableObject
	{
		[Tooltip("Enable auto save functionality")]
		public bool enabled;

		[Tooltip("The frequncey in minutes the auto save will activate"), Min(1)]
		public int frequency = 1;

		[Tooltip("Log a message every time the scene is autosaved")]
		public bool logging;
	}
}
