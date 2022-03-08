az login --use-device-code
az sql server create -l westus3 -g commander-rg -n commanderServer-01 -u chris -p $(Password)
az sql server create -l eastus -g commander-rg -n commanderServer-02 -u chris -p $(Password)
az sql db create -g commander-rg -s commanderServer-01 -n Commander --service-objective S0
az sql db create -g commander-rg -s commanderServer-02 -n Commander --service-objective S0
az sql failover-group create --name commanderFailover --partner-server commanderServer-02 --resource-group commander-rg --server commanderServer-01