using PackageCredentials.Core;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace PackageCredentials.UI
{
    public class CredentialManagerView
    {
        internal static ReorderableList GetCredentialList(CredentialManager credentialManager)
        {
            ReorderableList credentialList = null;
            credentialList = new ReorderableList(credentialManager.CredentialSet, typeof(NPMCredential), false, true, true, true)
            {
                drawHeaderCallback = rect =>
                {
                    GUI.Label(rect, "User Credentials on this computer");
                },
                drawElementCallback = (rect, index, active, focused) =>
                {
                    var credential = credentialList.list[index] as NPMCredential;
                    if (credential == null) return;
                    
                    rect.width -= 60;
                    EditorGUI.LabelField(rect, credential.url);
                    
                    rect.x += rect.width;
                    rect.width = 60;
                    if (GUI.Button(rect, "Edit"))
                    {
                        CredentialEditorView credentialEditor = EditorWindow.GetWindow<CredentialEditorView>(true, "Edit credential", true);
                        credentialEditor.Edit(credential, credentialManager);
                    }
                },
                onAddCallback = reorderableList =>
                {
                    CredentialEditorView credentialEditor = EditorWindow.GetWindow<CredentialEditorView>(true, "Add credential", true);
                    credentialEditor.CreateNew(credentialManager);
                },
                onRemoveCallback = reorderableList =>
                {
                    var credential = credentialList.list[credentialList.index] as NPMCredential;
                    
                    credentialManager.RemoveCredential(credential.url);
                    credentialManager.Write();
                }
            };
            return credentialList;
        }
    }
}
