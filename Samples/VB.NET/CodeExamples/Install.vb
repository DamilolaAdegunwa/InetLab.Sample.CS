Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace CodeSamples
   Public Class Install
		Public Sub InstallLicenseFileFromEmbeddedResources()
			'HOW TO INSTALL LICENSE FILE
			'====================
			'After purchase of developer license you will receive Inetlab.SMPP.license file per E-Mail. 
			'Add this file into the root of project where you have a reference on Inetlab.SMPP.dll. Change "Build Action" of the file to "Embedded Resource". 

			'Set license before using Inetlab.SMPP classes in your code:

			'<InstallLicenseFile>

			Inetlab.SMPP.LicenseManager.SetLicense(Me.GetType().Assembly.GetManifestResourceStream(Me.GetType(), "Inetlab.SMPP.license"))

			'</InstallLicenseFile>
		End Sub

		Public Sub InstallLicenseFileFromString()
			'<InstallLicenseFileFromString>

			Dim licenseContent As String = "
-----BEGIN INETLAB LICENSE------
EBAXG23FO4BR23LJMNAGCZLMFZQXG23F
GY4DEMJTG43DGBMAQFD4DPHQ2UEANACB
BY5I4D6XBCAACRUJXKZKI7K2N76CTXSC
NDJP2CIM4KHV5V7VCXT75R4XRDSLZZQS
2NKD6JHCIG4PNPUN5A7G4KRZQSZSNL44
NB2LTYRP5FATRVKCHD26FC64E2TSQFX5
Q6GWNF3HVVQIE2YKOO74C4FVR6HDUGD6
FYO4DHCPCPQ2GY3WQRMOFOXOZQ======
-----END INETLAB LICENSE--------"

			Inetlab.SMPP.LicenseManager.SetLicense(licenseContent)

			'</InstallLicenseFileFromString>
		End Sub
   End Class
End Namespace
