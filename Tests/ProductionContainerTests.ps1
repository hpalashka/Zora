$count = 0
do {
    $count++
    Write-Output "[$env:STAGE_NAME] Starting container [Attempt: $count]"

    $testStart = Invoke-WebRequest -Uri http://146.148.85.152 -UseBasicParsing
    
    if ($testStart.statuscode -eq '200') {
        $started = $true
    } else {
        Start-Sleep -Seconds 5
    }
    
} until ($started -or ($count -eq 3))

if (!$started) {
    exit 1
}
