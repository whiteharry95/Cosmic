namespace Cosmic.Server {
    using Cosmic.Entities;
    using Cosmic.Entities.Characters;
    using Cosmic.Universes;
    using Microsoft.Xna.Framework;
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    public class Server {
        public ServerPlayer[] netPlayers = new ServerPlayer[256];

        public IPAddress iPAddress;
        public int port;

        public TcpListener tcpListener;

        public Thread listenForPlayersThread;

        public Server(IPAddress iPAddress, int port) {
            this.iPAddress = iPAddress;
            this.port = port;
        }

        public void Start() {
            tcpListener = new TcpListener(iPAddress, port);
            tcpListener.Start();

            listenForPlayersThread = new Thread(ListenForPlayers);
            listenForPlayersThread.Start();
        }

        public void Update() {
            // put onto different thread
            for (int i = 0; i < netPlayers.Length; i++) {
                if (netPlayers[i] != null) {
                    if (netPlayers[i].player == null) {
                        netPlayers[i].player = EntityManager.AddEntity<Player>(new Vector2(UniverseManager.universeCurrent.worldCurrent.Width, UniverseManager.universeCurrent.worldCurrent.Height) / 2f);
                    }

                    NetworkStream networkStream = netPlayers[i].tcpClient.GetStream();

                    byte[] buffer = new byte[256];
                    int bufferReadTotal;

                    if ((bufferReadTotal = networkStream.Read(buffer, 0, buffer.Length)) != 0) {
                        Debug.WriteLine(Encoding.UTF8.GetString(buffer, 0, bufferReadTotal));
                    }
                }
            }
        }

        public void ListenForPlayers() {
            while (true) {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();

                for (int i = 0; i < netPlayers.Length; i++) {
                    if (netPlayers[i] == null) {
                        netPlayers[i] = new ServerPlayer(tcpClient);
                        Debug.WriteLine($"Player {i} with ip address {netPlayers[i].iPAddress} and port {netPlayers[i].port} has joined the server.");
                        break;
                    }
                }
            }
        }
    }
}