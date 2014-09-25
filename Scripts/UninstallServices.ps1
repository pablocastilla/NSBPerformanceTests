$services = get-service "Orders*" | Select-Object $ServiceName

foreach ($service in $services)
{
   Invoke-Expression (".\{0}\NServiceBus.Host.exe /uninstall /serviceName:{0}" -f $service)
}
    