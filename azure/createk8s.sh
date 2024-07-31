clusterName="k8s-aksmani"
resourceGroup="rg-aksmani"
location="eastus"


# Create a resource group
az group create --name $resourceGroup --location $location

# Create a new AKS cluster with network policy enabled by default  plugin overlay and pod-cidr 

az aks create --name $clusterName --resource-group $resourceGroup --location $location --network-plugin azure --network-plugin-mode overlay --pod-cidr 192.168.0.0/16 --generate-ssh-keys
