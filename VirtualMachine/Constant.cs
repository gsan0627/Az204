using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualMachine
{
    public static class Constant
    {
        public const string GROUP_NAME = "az204-resourceGroup";
        public const string VIRTUAL_MACHINE = "az204-VMTesting";
        public const string VNET_NAME = "az204VNET";
        public const string VNET_ADDRESS = "172.16.0.0/16";
        public const string SUBNET_NAME = "az204Subnet";
        public const string SUBNET_ADDRESS = "127.16.0.0/24";
        public const string NICNAME = "az204NIC";
        public const string ADMIN_USER = "azureadmin";
        public const string ADMIN_PASSWORD = "P@$$w0rd2021";
                
        public const string PUBLISHER = "MicrosoftWindowsServe";
        public const string OFFER = "windowServer";
        public const string SKU = "2012-R2-Datacenter";
    }
}
