using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Art3mis3d
{
	/// <summary>
	/// Unity has probably discussed an auto-save feature countless times over the years
	/// and decided not to implement... so take that information as you'd like.
	/// 
	/// your friendly neighbourhood Art3mis3d
	/// </summary>
	[CustomEditor(typeof(AutoSaveConfig))]
	internal class Art3mis3dAutoSave : Editor
	{
		private static AutoSaveConfig config;
		private static CancellationTokenSource tokenSource;
		private static Task task;

		[InitializeOnLoadMethod]
		private static void OnInitialize()
		{
			FetchConfig();
			CancelTask();

			tokenSource = new CancellationTokenSource();
			task = SaveInterval(tokenSource.Token);
		}

		private static void FetchConfig()
		{
			while (true)
			{
				if (config != null)
					return;

				var path = GetConfigPath();

				if (path == null)
				{
					AssetDatabase.CreateAsset(CreateInstance<AutoSaveConfig>(), $"Assets/{nameof(AutoSaveConfig)}.asset");
					Debug.Log("A config file has been created at the root of your project.<b> You can move this anywhere you'd like.</b>");
					continue;
				}
				config = AssetDatabase.LoadAssetAtPath<AutoSaveConfig>(path);

				break;
			}
		}
		
		private static string GetConfigPath()
		{
			List<string> paths = AssetDatabase.FindAssets(nameof(AutoSaveConfig)).Select(AssetDatabase.GUIDToAssetPath).Where(c => c.EndsWith(".asset")).ToList();
			if (paths.Count > 1)
				Debug.LogWarning("Multiple auto save config assets found. Delete one.");
			return paths.FirstOrDefault();
		}

		private static void CancelTask()
		{
			if (task == null)
				return;
			tokenSource.Cancel();
			task.Wait();
		}
		
		private static async Task SaveInterval(CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				await Task.Delay(config.frequency * 1000 * 60, token);
				if (config == null)
					FetchConfig();

				if (!config.enabled || Application.isPlaying || BuildPipeline.isBuildingPlayer || EditorApplication.isCompiling)
					continue;
				if (!UnityEditorInternal.InternalEditorUtility.isApplicationActive)
					continue;

				EditorSceneManager.SaveOpenScenes();
				if (config.logging)
					Debug.Log($"Auto-saved at {DateTime.Now:u}");
			}



		}
		
		[MenuItem("Window/Auto save/Find config")]
		public static void ShowConfig()
		{
			FetchConfig();

			var path = GetConfigPath();
			EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<AutoSaveConfig>(path).GetInstanceID());
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			EditorGUILayout.Space();
			EditorGUILayout.HelpBox("You can move this asset where ever you'd like.\nWith ❤, Art3mis3d.", MessageType.Info);
		}
	}
}
