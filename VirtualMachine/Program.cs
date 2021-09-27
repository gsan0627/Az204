using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;

namespace VirtualMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentials = SdkContext.AzureCredentialsFactory.FromFile("./azureauth.properties");

            var azure = Azure.Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithDefaultSubscription();

            var location = Region.USEast2;

            Console.WriteLine($"Creating resource group {Constant.GROUP_NAME}...");
            var resourceGroup = azure.ResourceGroups.Define(Constant.GROUP_NAME)
                .WithRegion(location)
                .Create();

            //Every virtual machine needs to be connected to a virtual network.
            Console.WriteLine($"Creating virtual network {Constant.VNET_NAME}...");
            var network = azure.Networks.Define(Constant.VNET_NAME)
                .WithRegion(location)
                .WithExistingResourceGroup(Constant.GROUP_NAME)
                .WithAddressSpace(Constant.VNET_ADDRESS)
                .Create();

            //Any virtual machine need a network interface for connecting to the virtual network
            Console.WriteLine($"Creating network interface {Constant.NICNAME}...");
            var nic = azure.NetworkInterfaces.Define(Constant.NICNAME)
                .WithRegion(location)
                .WithExistingResourceGroup(Constant.GROUP_NAME)
                .WithExistingPrimaryNetwork(network)
                .WithSubnet(Constant.SUBNET_NAME)
                .WithPrimaryPrivateIPAddressDynamic()
                .Create();

            //create the virtual machine
            Console.WriteLine($"Creating virtual machinee {Constant.VIRTUAL_MACHINE}...");
            azure.VirtualMachines.Define(Constant.VIRTUAL_MACHINE)
                .WithRegion(location)
                .WithExistingResourceGroup(Constant.GROUP_NAME)
                .WithExistingPrimaryNetworkInterface(nic)
                .WithLatestWindowsImage(Constant.PUBLISHER, Constant.OFFER, Constant.SKU)
                .WithAdminUsername(Constant.ADMIN_USER)
                .WithAdminPassword(Constant.ADMIN_PASSWORD)
                .WithComputerName(Constant.VIRTUAL_MACHINE)
                .WithSize(VirtualMachineSizeTypes.StandardDS2V2)
                .Create();
        }
    }
}
