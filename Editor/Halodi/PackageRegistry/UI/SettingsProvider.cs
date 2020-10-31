using System.Collections.Generic;
using Halodi.PackageRegistry.Core;
using UnityEditor;
using UnityEditorInternal;

namespace Halodi.PackageRegistry.UI
{
    static class CredentialManagerSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            RegistryManager registryManager;
            ReorderableList credentialDrawer = null;
            ReorderableList registryDrawer = null;
            
            var provider = new SettingsProvider("Project/Package Manager/Credentials", SettingsScope.Project)
            {
                label = "Credentials",
                activateHandler = (str, v) =>
                {
                    registryManager = new RegistryManager();
                    registryDrawer = RegistryManagerView.GetRegistryListView(registryManager);
                    credentialDrawer = CredentialManagerView.GetCredentialList(registryManager.credentialManager);
                },
                guiHandler = (searchContext) =>
                {
                    EditorGUILayout.Space();
                    registryDrawer.DoLayoutList();
                    
                    EditorGUILayout.Space();
                    credentialDrawer.DoLayoutList();
                },
                // Populate the search keywords to enable smart search filtering and label highlighting
                keywords = new HashSet<string>(new[] { "UPM", "NPM", "Credentials", "Packages", "Authentication", "Scoped Registry" })
            };

            return provider;
        }
    }
}