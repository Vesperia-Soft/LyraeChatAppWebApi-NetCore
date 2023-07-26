using Microsoft.AspNetCore.SignalR;

namespace LyraeChatApp.Presentation.signalR;

public class ChatHub : Hub
{
    private readonly string _botUser;
    private readonly IDictionary<string, UserConnection> _connection;
    private readonly IList<string> _activeUsers;
   
    public ChatHub(IDictionary<string, UserConnection> connections)
    {
        _botUser = "MyChat Bot";
        _connection = connections;
        _activeUsers = new List<string>();
    }

    public override Task OnConnectedAsync()
    {
        string userName = Context.GetHttpContext().Request.Query["username"];
        if (string.IsNullOrEmpty(userName))
        {
            Context.Abort();
            return Task.CompletedTask;
        }

        _activeUsers.Add(userName); // Bağlantı kimliğini _activeUsers koleksiyonuna ekleyin

        var activeUsers = GetActiveUsers();
        Clients.Caller.SendAsync("UsersInRoom", activeUsers); // Bağlantıyı yapan client'a aktif kullanıcıları gönder

        return base.OnConnectedAsync();
    }

    public IList<string> GetActiveUsers()
    {
        return _activeUsers.ToList();
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (_connection.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
        {
            _connection.Remove(Context.ConnectionId);
            //Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser, $"{userConnection.User} has left");

            SendConnectedUsers(userConnection.Room);
        }

        return base.OnDisconnectedAsync(exception);
    }
    public async Task SendMessage(string message)
    {
        if (_connection.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
        {
            await Clients.Group(userConnection.Room)
                .SendAsync("ReceiveMessage", userConnection.User, message);
        }
    }
    public async Task JoinRoom(UserConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

        _connection[Context.ConnectionId] = userConnection;

        //await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser);

        await SendConnectedUsers(userConnection.Room);
    }


    public Task SendConnectedUsers(string room)
    {
        var users = _connection.Values.Where(c => c.Room == room).Select(c => c.User);

        return Clients.Group(room).SendAsync("UsersInRoom", users);
    }
}
