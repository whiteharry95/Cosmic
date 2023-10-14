namespace Cosmic.Server {
    using Cosmic.Entities.Characters;
    using System.Net;
    using System.Net.Sockets;

    public class ServerPlayer {
        public TcpClient tcpClient;

        public IPAddress iPAddress;
        public int port;

        public Player player;

        public ServerPlayer(TcpClient tcpClient) {
            this.tcpClient = tcpClient;

            IPEndPoint iPEndPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;

            iPAddress = iPEndPoint.Address;
            port = iPEndPoint.Port;
        }
    }
}