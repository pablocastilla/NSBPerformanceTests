Import-Module .\NServiceBus\Binaries\NServiceBus.Powershell.dll
Install-NServiceBusDtc
Install-NServiceBusMSMQ
Install-NServiceBusRavenDB
Install-NServiceBusPerformanceCounters
Install-NServiceBusLicense  .\License.xml
Set-NServiceBusLocalMachineSettings -ErrorQueue error
Set-NServiceBusLocalMachineSettings -AuditQueue audit