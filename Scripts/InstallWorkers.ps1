$services=Get-ChildItem "Orders*" | where {$_.Attributes -eq 'Directory'} | Select-Object BaseName
$machineName = hostname


foreach ($service in $services)
{

    Invoke-Expression (".\{0}\NServiceBus.Host.exe /install /serviceName:{0} /displayName:{0} /description:{0} /userName:{1}\Administrator /password:xxxxxx NServiceBus.Production NServiceBus.Worker" -f $service.BaseName, $machineName )
}
    