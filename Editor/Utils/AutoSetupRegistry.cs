using System;
using System.Threading.Tasks;
using Editor.Utils;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Needle.PackageCredentials.Core
{
	public static class AutoSetupRegistry
	{
		[InitializeOnLoadMethod]
		private static async void InternalInterval()
		{
			while (true)
			{
				AutoSetupRegistry.TryAddCredentialFromClipboardAutomatically(true);
				await Task.Delay(1000);
			}
		}
		
		private static readonly ParseHelper npmHelper = new ParseHelper() { CacheClipboard = true };

		public static bool TryAddCredentialFromClipboardAutomatically(bool allowShowingDialogue = true)
		{
			if (npmHelper.TryParseNpmCredentialsFromClipboard(out var reg, out var tok))
			{
				var manager = RegistryManager.Instance;
				if (manager == null) return false;
				foreach (var cred in manager.credentialManager.CredentialSet)
				{
					if (cred.url == reg) return false;
				}
				manager.credentialManager.CredentialSet.Add(new NPMCredential()
				{
					url = reg, token = tok
				});
				manager.credentialManager.Write();
				if (allowShowingDialogue)
					DisplayRestartDialogue(reg, 1000);
				return true;
			}
			return true;
		}

		private static async void DisplayRestartDialogue(string registryAdded, int delay)
		{
			await Task.Delay(delay);
			if (EditorUtility.DisplayDialog("Credentials added",
				    "Credentials for " + registryAdded + " have been successfully added from your clipboard. Restart the Editor now to finish setup",
				    "Restart now",
				    "Don't restart"))
			{
				if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
					EditorApplication.OpenProject(Environment.CurrentDirectory);
			}
		}
	}
}