Param(
	[Switch]
	$Build,

	[Switch]
	$Run
)

$scriptPath = Split-Path $script:MyInvocation.MyCommand.Path
$rootPath = [System.IO.Path]::GetFullPath("$scriptPath\..")

If($rootPath -ne [System.IO.Path]::GetFullPath($PWD)) {
	Write-Host "ERR: This script can be run only from repo root location. You can fix it by exexuting next command:" -ForegroundColor Red
	Write-Host "cd ${rootPath}" -ForegroundColor Red
	exit
}

Clear-Host

If ($Run -eq $False) {
    $Build = $True
}

If($Build) {
	Write-Host "BUILDING CUSTOMERS SERVICE" -ForegroundColor Red
	docker-compose `
		-f $rootPath\docker-compose.yml -f $rootPath\docker-compose.override.yml `
		build
}

If($Run) {
	Write-Host "RUNNING CUSTOMERS SERVICE" -ForegroundColor Red
	docker-compose -f $rootPath\docker-compose.yml -f $rootPath\docker-compose.override.yml up -d
}