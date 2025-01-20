param(
  [string]$file
)
Write-SpectreRule -Title "2015 - 09" -LineColor Red Center
# Format-SpectrePanel -Data "" -title " Part 1 " -Expand 
if (Test-Path -Path $PSScriptRoot/$file) {
  [system.IO.File]::ReadAllLines("$PSScriptRoot/$file")
}
[System.Collections.Generic.Dictionary]
$routes = [System.Collections.Generic.Dictionary[[string,string],int]]::new()
$routes.Add(("test1","test2"),3)
$routes