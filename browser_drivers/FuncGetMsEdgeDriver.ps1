[CmdletBinding()]
param (
    [Parameter(Mandatory = $True)]
    [string]
    $MsEdgeDriverOutputPath,    
    [Parameter(Mandatory = $false)]
    [string]
    $MsEdgeVersion, 
    [Parameter(Mandatory = $false)]
    [Switch]
    $ForceDownload
)

# store original preference to revert back later
$OriginalProgressPreference = $ProgressPreference;
# setting progress preference to silently continue will massively increase the performance of downloading the MsEdgeDriver
$ProgressPreference = 'SilentlyContinue';

Function Get-MsEdgeVersion {
    # $IsWindows will PowerShell Core but not on PowerShell 5 and below, but $Env:OS does
    # this way you can safely check whether the current machine is running Windows pre and post PowerShell Core
    If ($IsWindows -or $Env:OS) {
        Try {
            (Get-Item (Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\msedge.exe' -ErrorAction Stop).'(Default)').VersionInfo.FileVersion;
        }
        Catch {
            Throw "Microsoft Edge not found in registry";
        }
    }
    Else {
        Throw "Your operating system is not supported by this script.";
    }
}

# Instructions from https://msedgedriver.chromium.org/downloads/version-selection
#   First, find out which version of MsEdge you are using. Let's say you have MsEdge 72.0.3626.81.
If ([string]::IsNullOrEmpty($MsEdgeVersion)) {
    $MsEdgeVersion = Get-MsEdgeVersion -ErrorAction Stop;
    Write-Output "Microsoft Edge version $MsEdgeVersion found on machine";
}


$TempFilePath = [System.IO.Path]::GetTempFileName();
$TempZipFilePath = $TempFilePath.Replace(".tmp", ".zip");
Rename-Item -Path $TempFilePath -NewName $TempZipFilePath;
$TempFileUnzipPath = $TempFilePath.Replace(".tmp", "");
#   Use the URL created in the last step to retrieve a small file containing the version of MsEdgeDriver to use. For example, the above URL will get your a file containing "72.0.3626.69". (The actual number may change in the future, of course.)
#   Use the version number retrieved from the previous step to construct the URL to download MsEdgeDriver. With version 72.0.3626.69, the URL would be "https://msedgedriver.storage.googleapis.com/index.html?path=72.0.3626.69/".

If ($IsWindows -or $Env:OS) {
    Invoke-WebRequest "https://msedgedriver.azureedge.net/$MsEdgeVersion/edgedriver_win64.zip" -OutFile $TempZipFilePath -UseBasicParsing;
    Expand-Archive $TempZipFilePath -DestinationPath $TempFileUnzipPath;
    Move-Item "$TempFileUnzipPath/msedgedriver.exe" -Destination $MsEdgeDriverOutputPath -Force;
}
Else {
    Throw "Your operating system is not supported by this script.";
}

#   After the initial download, it is recommended that you occasionally go through the above process again to see if there are any bug fix releases.

# Clean up temp files
Remove-Item $TempZipFilePath;
Remove-Item $TempFileUnzipPath -Recurse;

# reset back to original Progress Preference
$ProgressPreference = $OriginalProgressPreference;