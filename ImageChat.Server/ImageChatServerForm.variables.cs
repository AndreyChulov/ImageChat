using ImageChat.Server.Server;

namespace ImageChat.Server
{
    public partial class ImageChatServerForm
    {
        private readonly ServerLocatorService _serverLocatorService 
            = new ServerLocatorService();
    }
}