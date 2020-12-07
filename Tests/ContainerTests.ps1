$count = 0
do {
    $count++
    Write-Output "[$env:STAGE_NAME] Starting container [Attempt: $count]"

    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12 # at some point started crashing without this???
   
    $testStart = Invoke-WebRequest -Uri http://localhost:5001 -UseBasicParsing
    
    if ($testStart.statuscode -eq '200') {
        $started = $true
    } else {
        Start-Sleep -Seconds 5
    }
    
} until ($started -or ($count -eq 3))

if (!$started) {
    exit 1
}
