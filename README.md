# M-Files-Vault-Application-Template-for-Visual-Studio
## Description
- Uses the M-Files Vault Application Framework (VAF) to create vault applications in C# in Visual Studio. Includes vault application installer and automatically references the VAF.
- This project should be considered in development and should not be used in production without thorough testing.
- Development credit goes to Mikko at M-Files. Posted to GitHub with permission. I claim no copyright and assign no license.

## How to install
- Create a zip file of the MFVaultApplicationTemplate folder
- Save the zip file in <user>/Documents/Visual Studio <version>/Templates/Project Templates/Visual C#. Don't unzip.
- Restart Visual Studio
- Click New>Project
- Find the M-Files Vault Application option

## How to install your app to your vault
- Open the Post Build Command Line window
- Change where it says "Sample Vault" to the name of the vault. It must be a vault running on your local machine. Your windows user account must have admin access to the vault
- Build solution (F7)
- A window will pop up. It will install the application, restart the vault, and let you know when it is done.

## To-Do
- Upload Mikko's VAF powerpoint that has the documentation needed to start using VAF
