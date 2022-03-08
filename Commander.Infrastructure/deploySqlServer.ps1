az login --use-device-code
az sql server create -l westus3 -g commander-rg -n commanderServer-01 -u chris -p "*gj8~jUZD@!/u]:^j)~!(9@!2nYwwQeaGu2G/mDA8^G2>*)7V:tk$%@m[Bsm*jVBh)T!.kU.FxYnq<;PB~J2T83+Ps6==o:WT#"
az sql server create -l eastus -g commander-rg -n commanderServer-02 -u chris -p "*gj8~jUZD@!/u]:^j)~!(9@!2nYwwQeaGu2G/mDA8^G2>*)7V:tk$%@m[Bsm*jVBh)T!.kU.FxYnq<;PB~J2T83+Ps6==o:WT#"
az sql db create -g commander-rg -s commanderServer-01 -n Commander --service-objective S0
az sql db create -g commander-rg -s commanderServer-02 -n Commander --service-objective S0
az sql failover-group create --name commanderFailover --partner-server commanderServer-02 --resource-group commander-rg --server commanderServer-01