az login --use-device-code
az group create -l westus3 -n commander-sql-rg
az sql server create -l westus3 -g commander-sql-rg -n commanderServer-03 -u chris -p $($env:Password)
az sql server create -l eastus2 -g commander-sql-rg -n commanderServer-04 -u chris -p $($env:Password)
az sql db create -g commander-sql-rg -s commanderServer-03 -n Commander --service-objective S0
az sql db create -g commander-sql-rg -s commanderServer-04 -n Commander --service-objective S0
az sql failover-group create --name commanderfailover --partner-server commanderServer-04 --resource-group commander-sql-rg --server commanderServer-03