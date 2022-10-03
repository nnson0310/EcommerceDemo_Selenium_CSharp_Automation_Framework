[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [string]
    $GeckoDriverOutputPath,    
    [Parameter(Mandatory = $false)]
    [Switch]
    $ForceDownload,
    [Parameter(Mandatory = $true)]
    [string]
    $GeckoDriverVersion,
    [string]
    $GeckoDriverUrl = "https://github.com/mozilla/geckodriver/releases/download/v" + $GeckoDriverVersion + "/geckodriver-v" + $GeckoDriverVersion,
    [string]
    $PathToWindowVersion = $GeckoDriverUrl + "-win64.zip",
    [string]
    $PathToLinuxVersion = $GeckoDriverUrl + "-linux64.tar.gz",
    [string]
    $PathToMacVersion = $GeckoDriverUrl + "-macos.tar.gz" 
)

$TempFilePath = [System.IO.Path]::GetTempFileName();
$TempZipFilePath = $TempFilePath.Replace(".tmp", ".zip");
Rename-Item -Path $TempFilePath -NewName $TempZipFilePath;
$TempFileUnzipPath = $TempFilePath.Replace(".tmp", "");
# Download latest gecko driver in address: https://github.com/mozilla/geckodriver/releases/

If ($IsWindows -or $Env:OS) {
    Invoke-WebRequest $PathToWindowVersion -OutFile $TempZipFilePath -UseBasicParsing;
    Expand-Archive $TempZipFilePath -DestinationPath $TempFileUnzipPath;
    Move-Item "$TempFileUnzipPath/geckodriver.exe" -Destination $GeckoDriverOutputPath -Force;
}
ElseIf ($IsLinux) {
    Invoke-WebRequest $PathToLinuxVersion -OutFile $TempZipFilePath -UseBasicParsing;
    Expand-Archive $TempZipFilePath -DestinationPath $TempFileUnzipPath;
    Move-Item "$TempFileUnzipPath/geckodriver" -Destination $GeckoDriverOutputPath -Force;
}
ElseIf ($IsMacOS) {
    Invoke-WebRequest $PathToMacVersion -OutFile $TempZipFilePath -UseBasicParsing;
    Expand-Archive $TempZipFilePath -DestinationPath $TempFileUnzipPath;
    Move-Item "$TempFileUnzipPath/geckodriver" -Destination $GeckoDriverOutputPath -Force;
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