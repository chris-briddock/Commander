az login --use-device-code
az group create -l westus3 -n commander-sql-rg
az sql server create -l westus3 -g commander-sql-rg -n commanderServer-01 -u chris -p $($env:Password)
az sql server create -l eastus -g commander--sql-rg -n commanderServer-02 -u chris -p $($env:Password)
az sql db create -g commander-sql-rg -s commanderServer-01 -n Commander --service-objective S0
az sql db create -g commander-sql-rg -s commanderServer-02 -n Commander --service-objective S0
az sql failover-group create --name commanderFailover --partner-server commanderServer-02 --resource-group commander-sql-rg --server commanderServer-01