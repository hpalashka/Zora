$count = 0
do {
    $count++
    Write-Output "[$env:STAGE_NAME] Starting container [Attempt: $count]"
    
    $studentsStart = Invoke-WebRequest -Uri http://localhost:5003/Students -UseBasicParsing

    Write-Output "Tests results:"
    Write-Output "Students " $studentsStart.statuscode 
    
    if ($studentsStart.statuscode -eq '200')
    {
        $started = $true
    } else {
        Start-Sleep -Seconds 5
    }
    
} until ($started -or ($count -eq 3))

if (!$started) {
    exit 1
}




