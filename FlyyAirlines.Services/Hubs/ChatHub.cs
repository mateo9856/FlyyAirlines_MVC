using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines.Repository
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            var GetUserValue = ConnectionUsers.Users.Where(d => d.Key == user);
            var UserName = GetUserValue.Select(d => d.Value.UserName).ToArray()[0];
            await Clients.Client(user).SendAsync("ReceiveMessage", UserName, message);
        }

        public IEnumerable<HubUserDatas> GetConnectedUsers()
        {
            var GetUsers = ConnectionUsers.Users.Where(d => d.Key != Context.ConnectionId)
                .Select(d => d.Value);
            return GetUsers;
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public override Task OnConnectedAsync()
        {
            var email = Context.User.Claims.FirstOrDefault(d => d.Type.Contains("Email")).Value;
            var userName = Context.User.Claims.FirstOrDefault(d => d.Type.Contains("User")).Value;
            if(!ConnectionUsers.Users.Any(d => d.Value.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)))
            {
                ConnectionUsers.Users.Add(Context.ConnectionId, new HubUserDatas(userName, email, Context.ConnectionId));
            }
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectionUsers.Users.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
