try 
{
    az login --use-device-code
}
catch 
{
    Write-Error "Failed to login to Azure."
}
try 
{
    az group create -l uksouth -n commander-sql-rg
}
catch
{
    Write-Error "Faild to create resource group."
}
try 
{
    az sql server create -l ukwest -g commander-sql-rg -n commanderServer-03 -u chris -p $($env:Password)
}
catch 
{
    Write-Error "Failed to create SQL server for Server 2"
}
try 
{
    az sql server create -l uksouth -g commander-sql-rg -n commanderServer-04 -u chris -p $($env:Password)
}
catch 
{
    Write-Error "Failed to create SQL server for Server 1"
}
try 
{
    az sql db create -g commander-sql-rg -s commanderServer-03 -n Commander --service-objective S0
}
catch
{
    Write-Error "Failed to create database for Server 1"
}
try 
{
    az sql db create -g commander-sql-rg -s commanderServer-04 -n Commander --service-objective S0
}
catch 
{
    Write-Error "Failed to create database on Server 2"
}
try 
{
    az sql failover-group create --name commanderfailover --partner-server commanderServer-04 --resource-group commander-sql-rg --server commanderServer-03
}
catch
{
    Write-Error "Failed to create failover group from Server 1 to Server 2"
}
