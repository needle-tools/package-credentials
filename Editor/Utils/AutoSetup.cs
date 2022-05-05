using System;
using System.Threading.Tasks;
using Editor.Utils;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Needle.PackageCredentials.Core
{
	internal static class AutoSetup
	{
		private static readonly ParseHelper npmHelper = new ParseHelper() { CacheClipboard = true };
		
		internal static async void TryAddCredentialFromClipboardAutomatically(RegistryManager manager)
		{
			if (npmHelper.TryParseNpmCredentialsFromClipboard(out var reg, out var tok))
			{
				foreach (var cred in manager.credentialManager.CredentialSet)
				{
					if (cred.url == reg) return;
				}
				manager.credentialManager.CredentialSet.Add(new NPMCredential()
				{
					url = reg, token = tok
				});
				manager.credentialManager.Write();

				await Task.Delay(1000); 
				if (EditorUtility.DisplayDialog("Credentials added",
					    "Credentials for " + reg + " have been successfully added from your clipboard. Restart the Editor now finish setup", "Restart now",
					    "Don't restart"))
				{
					if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
						EditorApplication.OpenProject(Environment.CurrentDirectory);
				}
			}
		}
	}
}