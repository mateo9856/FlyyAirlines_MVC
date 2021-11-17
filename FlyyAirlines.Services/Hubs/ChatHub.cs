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
            var UserName = GetUserValue.Select(d => d.Value.UserName).First();
            await Clients.Group($"user_{UserName}").SendAsync("ReceiveMessage", user, message);
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

        public IEnumerable<HubUserDatas> GetActiveSupports()
        {
            var Users = ConnectionUsers.Users.Where(d => d.Value.IsSupport == true).Select(d => d.Value);
            return Users;
        }
        
        public async Task JoinSupportToGroup(string id)
        {
            try
            {
                var GetByConnectionId = ConnectionUsers.Users.Where(d => d.Key == id).Select(d => d.Value.UserName);
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{GetByConnectionId.First()}");
            } catch(Exception)
            {
                return;
            }
            
        }

        public async Task LeaveSupportFromGroup(string id)
        {
            try
            {
                var GetByConnectionId = ConnectionUsers.Users.Where(d => d.Key == id).Select(d => d.Value.UserName);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{GetByConnectionId.First()}");
            }
            catch(Exception)
            {
                return;
            }
        }

        public override async Task OnConnectedAsync()
        {
            var IsSupport = Context.User.Claims.Where(d => d.Type.Contains("IsSupport")).Any(d => d.Value.Equals("True"));
            var email = Context.User.Claims.FirstOrDefault(d => d.Type.Contains("Email")).Value;
            var userName = Context.User.Claims.FirstOrDefault(d => d.Type.Contains("User")).Value;

            if(!IsSupport)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userName}");
            }

            if (!ConnectionUsers.Users.Any(d => d.Value.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)))
            {
                ConnectionUsers.Users.Add(Context.ConnectionId, new HubUserDatas(userName, email, IsSupport , Context.ConnectionId));
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var GetUserName = Context.User.Claims.FirstOrDefault(d => d.Type.Contains("User")).Value;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{GetUserName}");
            ConnectionUsers.Users.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
