using Microsoft.AspNetCore.SignalR;

namespace LyraeChatApp.Presentation.signalR;

public class ChatHub : Hub
{
    private readonly string _botUser;
    private readonly IDictionary<string, UserConnection> _connection;

    public ChatHub(IDictionary<string, UserConnection> connections)
    {
        _botUser = "MyChat Bot";
        _connection = connections;
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
