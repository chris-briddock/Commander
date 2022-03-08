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
<<<<<<< HEAD
    az sql server create -l ukwest -g commander--sql-rg -n commanderServer-02 -u chris -p "$($env:Password)"
=======
    az sql server create -l westus2 -g commander-sql-rg -n commanderServer-02 -u chris -p "$($env:Password)"
>>>>>>> afa0cff094ba9a82d27b5f4de6b8a23e703f68c1
}
catch 
{
    Write-Error "Failed to create SQL server for Server 2"
}
try 
{
<<<<<<< HEAD
    az sql server create -l uksouth -g commander--sql-rg -n commanderServer-01 -u chris -p "$($env:Password)"
=======
    az sql server create -l eastus -g commander-sql-rg -n commanderServer-01 -u chris -p "$($env:Password)"
>>>>>>> afa0cff094ba9a82d27b5f4de6b8a23e703f68c1
}
catch 
{
    Write-Error "Failed to create SQL server for Server 1"
}
try 
{
    az sql db create -g commander-sql-rg -s commanderServer-01 -n Commander --service-objective S0
}
catch
{
    Write-Error "Failed to create database for Server 1"
}
try 
{
    az sql db create -g commander-sql-rg -s commanderServer-02 -n Commander --service-objective S0
}
catch 
{
    Write-Error "Failed to create database on Server 2"
}
try 
{
    az sql failover-group create --name commanderfailover --partner-server commanderServer-02 --resource-group commander-sql-rg --server commanderServer-01
}
catch
{
    Write-Error "Failed to create failover group from Server 1 to Server 2"
}
