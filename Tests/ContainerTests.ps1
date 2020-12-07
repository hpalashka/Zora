$count = 0
do {
    $count++
    Write-Output "[$env:STAGE_NAME] Starting container [Attempt: $count]"
    
    $webStart = Invoke-WebRequest -Uri http://localhost:5001 -UseBasicParsing

    Write-Output "Tests results:"
    Write-Output "Web " $webStart.statuscode 
    
    if ($webStart.statuscode -eq '200')
    {
        $started = $true
    } else {
        Start-Sleep -Seconds 5
    }
    
} until ($started -or ($count -eq 3))

if (!$started) {
    exit 1
}




