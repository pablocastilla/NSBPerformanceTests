NSBPerformanceTests
===================

Proyect for quick evaluating the performance of a NServiceBus installation



##How a worker can be disconnected?
If the worker is configured using the NServiceBus.Distributor.MSMQ NuGet there is a PowerShell functionallity that can be use to remove a Worker from a Distributor. The steps are the following:

1. Load the [NServiceBus PowerShell CmdLet](managing-nservicebus-using-powershell.md) and execute Remove-NServiceBusMSMQWorker WorkerAddress DistributorAddress
--* The WorkerAddress = the worker queue name, eg Worker@localhost
--* The DistributorAddress = the distributor queue name eg MyDistributor@localhost, Note: you just pass the distributor queue name, the PS script automatically appends ".distributor.control" to the end of the distributor queue.

2. Wait for worker to drain all queued messages in its input queue.
3. Shutdown the endpoint.

##What's happening inside the distributor after the PowerShell is executed?
1. An unregister message is sent by the PowerShell to the distributor control queue.
2. The worker with the address specified in the message is set with SessionID  "disconnected" when that message is processed.
3. Ready messages sent back by the worker never match the session, so they are skipped and the worker doesn't exist for the distributor any more.
