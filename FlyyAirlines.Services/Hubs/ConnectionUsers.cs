using System.Collections.Generic;

namespace FlyyAirlines.Repository
{
    public static class ConnectionUsers
    {
        public static Dictionary<string, HubUserDatas> Users = new Dictionary<string, HubUserDatas>();

        public static Dictionary<string, string> Groups = new Dictionary<string, string>(); 
    }
}
