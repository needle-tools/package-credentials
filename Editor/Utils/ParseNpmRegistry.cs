using System;
using System.Text.RegularExpressions;
using UnityEditor;

namespace Editor.Utils
{
	public class ParseHelper
	{
		public bool CacheClipboard = true;
		
		private string lastClipboardStringFound;
		// https://regex101.com/r/zqq3io/1
		private static readonly Regex npmCredentials = new Regex("npm config set (?<registry_url>.+?)((\\/-\\/)|:).*?_authToken \"(?<token>.+)\"");

		public bool TryParseNpmCredentialsFromClipboard(out string reg, out string token)
		{
			var cb = EditorGUIUtility.systemCopyBuffer;
			reg = null;
			token = null;
			if (CacheClipboard && cb == lastClipboardStringFound) return false;
			// cb = "npm config set //packages.needle.tools/:_authToken \"...\"";
			lastClipboardStringFound = cb;

			var match = npmCredentials.Match(cb);
			if (match.Success)
			{
				reg = match.Groups["registry_url"].Value;
				token = match.Groups["token"].Value;
				if (reg.Length > 0 && (!reg.StartsWith("http:") && !reg.StartsWith("https:")))
					reg = "https:" + reg;
				token = token.Trim();
				reg = reg.Trim().TrimEnd('/');
				return true;
			}
			
			// const string tokenStartMarker = ":_authToken \"";
			// var tokenIndexStart = cb.IndexOf(tokenStartMarker, StringComparison.Ordinal);
			// if (tokenIndexStart > 0)
			// {
			// 	token = cb.Substring(tokenIndexStart + tokenStartMarker.Length).TrimEnd('\"');
			// 	if (!string.IsNullOrWhiteSpace(token))
			// 	{
			// 		const string setConfigMarker = "npm config set ";
			// 		var regStartIndex = setConfigMarker.Length;
			// 		if (regStartIndex > tokenIndexStart) return false;
			// 		reg = cb.Substring(regStartIndex, tokenIndexStart - regStartIndex);
			// 		if (reg.Length > 0 && (!reg.StartsWith("http:") && !reg.StartsWith("https:")))
			// 			reg = "https:" + reg;
			//
			// 		token = token.Trim();
			// 		reg = reg.Trim().TrimEnd('/');
			// 		return true;
			// 	}
			// }
			return false;
		}
	}
}