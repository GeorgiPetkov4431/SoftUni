using System.Text;

namespace DataCenter
{
    public class Rack
    {
        public Rack(int slots)
        {
            Slots = slots;
            Servers = new List<Server>();
        }

        public int Slots { get; set; }
        public List<Server> Servers { get; set; }
        public int GetCount { get { return Servers.Count; } }

        public void AddServer(Server newServer)
        {
            if (GetCount < Slots && !Servers.Any(s => s.SerialNumber == newServer.SerialNumber))
            {
                Servers.Add(newServer);
            }
        }

        public bool RemoveServer(string serialNumber)
        {
            Server serverToRemove = Servers.FirstOrDefault(s => s.SerialNumber == serialNumber);
            
            bool removed = Servers.Remove(serverToRemove);

            return removed;
        }

        public string GetHighestPowerUsage()
        {
            Server server = Servers.MaxBy(s => s.PowerUsage);

            return server.ToString();
        }

        public int GetTotalCapacity()
        {
            return Servers.Sum(s => s.Capacity);
        }

        public string DeviceManager()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{GetCount} servers operating:");
            foreach (Server s in Servers)
            {
                sb.AppendLine(s.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
