param(
  [string]$MaximumConcurrencyLevel

)

get-service "Orders*" | Stop-Service

$services=Get-ChildItem  "Orders*.config" -recurse  | Select-Object FullName

foreach ($service in $services)
{
    Write-Host ($service.FullName)

    (Get-Content $service.FullName) | 
    Foreach-Object {$_ -replace ("MaximumConcurrencyLevel=`"`\d+`"","MaximumConcurrencyLevel=`"{0}`""  -f $MaximumConcurrencyLevel)}  | 
    Out-File $service.FullName
    
}


get-service "t5*" | Start-Service


