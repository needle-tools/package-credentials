# Package Credentials

This package provides a simple UI to set package registry credentials from within the Unity Editor.  
It's based on `Halodi Unity Package Registry Manager`, with mostly functionality removed to only keep the essentials (setting passwords for scoped registries that require authentication).

## Installation

In Unity, go to `Window > Package Manager`.

Add package from git url (press the + in the top left of the Window).  
Use the repository URL for this package:  

```
https://github.com/needle-tools/package-credentials.git
```

## Usage 

After installation, use `Preferences > Package Manager > Credentials` to enter authentication information.  

Credentials are per-user-per-machine, that is, if you have set them up in any project they're available for all other projects.  

## Manage credentials

Under "Manage credential" you can add, edit and remove credentials in ~/.upmconfig.toml. 

Each registry logs in using a token. If your NPM provider provides a token directly, enter it here. If your provider requires a login, select the method and press "Get Token". Enter required information and press "Login". A token will be requested from the registry. The login information will not get saved.

To always authenticate, set "Always auth" to true;
After setting the registry credentials, it is advised to restart Unity to reload the package manager.  

### Notes for specific providers

**Github**: Create a [Personal access token](https://help.github.com/en/github/authenticating-to-github/creating-a-personal-access-token-for-the-command-line), make sure to select "read:packages" for adding packages to a project and "write:packages" if you want to publish packages. Copy the personal access token directly in the "Token" field (ignore Generate Token).

**Bintray**: In Generate Token, select "bintray" as method and press "Get token". Enter your credentials. Note: Your credentials are not checked here, the token is calculated from your credentials.

**Verdaccio**: In Generate Token, select "npm login" as method and press "Get token". Enter your credentials.

## Credits

UI adjustments and minimal version: [hybridherbst](felix@needle.tools)  
Original implementation: [Jesper](jesper@halodi.com)  
