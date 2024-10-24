using Network;
using System;

namespace Network
{

    public enum NICType
    {
        Ethernet,
        TokenRing
    }

    
    public class NIC
    {
       
        private static NIC instance = null;

       
        private static readonly object lockObj = new object();

        
        public string Manufacture { get; private set; }
        public string MACAddress { get; private set; }
        public NICType Type { get; private set; }

        
        private NIC(string manufacture, string macAddress, NICType type)
        {
            Manufacture = manufacture;
            MACAddress = macAddress;
            Type = type;
        }

      
        public static NIC GetInstance(string manufacture, string macAddress, NICType type)
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new NIC(manufacture, macAddress, type);
                    }
                }
            }
            return instance;
        }

        
        public void DisplayNICInfo()
        {
            Console.WriteLine($"NIC Information: \nManufacture: {Manufacture}\nMAC Address: {MACAddress}\nType: {Type}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        
        NIC nic = NIC.GetInstance("Intel", "00-14-22-01-23-45", NICType.Ethernet);
        nic.DisplayNICInfo();

        
        NIC anotherNic = NIC.GetInstance("Another Manufacture", "00-14-22-01-23-46", NICType.TokenRing);
        anotherNic.DisplayNICInfo();

        
    }
}
