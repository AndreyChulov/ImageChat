using ImageChatServer.Server;

namespace ImageChatServer
{
    public partial class ImageChatServerForm
    {
        private readonly ServerLocatorService _serverLocatorService 
            = new ServerLocatorService();
    }
}