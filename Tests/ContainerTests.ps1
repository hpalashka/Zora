$count = 0
do {
    $count++
    Write-Output "[$env:STAGE_NAME] Starting container [Attempt: $count]"
    
    $webStart = Invoke-WebRequest -Uri http://localhost:5001 -UseBasicParsing
    $identityStart = Invoke-WebRequest -Uri http://localhost:5005 -UseBasicParsing
    $studentsStart = Invoke-WebRequest -Uri http://localhost:5003 -UseBasicParsing
    $paymentsStart = Invoke-WebRequest -Uri http://localhost:5011 -UseBasicParsing
    $statisticsStart = Invoke-WebRequest -Uri http://localhost:5013 -UseBasicParsing
    $notificationsStart = Invoke-WebRequest -Uri http://localhost:5017 -UseBasicParsing
    $outstandingpaymentsStart = Invoke-WebRequest -Uri http://localhost:5009 -UseBasicParsing

    Write-Output "Tests results:"
    Write-Output "Identity"  $identityStart.statusCode
    
    if ($webStart.statuscode -eq '200' -and $identityStart.statuscode -eq '200' -and
        $studentsStart.statuscode -eq '200' -and $paymentsStart.statuscode -eq '200' -and
        $statisticsStart.statuscode -eq '200' -and $notificationsStart.statuscode -eq '200' -and
        $outstandingpaymentsStart.statuscode -eq '200')
         {
        $started = $true
    } else {
        Start-Sleep -Seconds 5
    }
    
} until ($started -or ($count -eq 3))

if (!$started) {
    exit 1
}




