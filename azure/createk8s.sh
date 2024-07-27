clusterName="ks-aksmani"
resourceGroup="rg-aksmani"
location="eastus"

az aks create --name $clusterName --resource-group $resourceGroup --location $location --network-plugin azure --network-plugin-mode overlay --pod-cidr 192.168.0.0/16 --generate-ssh-keys

