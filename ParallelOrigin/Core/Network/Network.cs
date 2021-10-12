using System;
using LiteNetLib;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Base.Classes.Pattern.Registers;

namespace ParallelOrigin.Core.Network {
    
    /// <summary>
    /// A class which manages the network flow. 
    /// </summary>
    public partial class Network {

        public const string IP = "localhost";
        public const ushort PORT = 9050;
        private const ushort MAX_CONNECTIONS = 10;
        
        public Network() {
            
            Listener = new EventBasedNetListener();
            Writer = new NetDataWriter();
            Processor = new NetPacketProcessor();
            
            Manager = new NetManager(Listener) {
                AutoRecycle = true
            };
            
            ServiceLocator.Register(this);
        }

        /// <summary>
        /// Starts the network connection and server/client. 
        /// </summary>
        public void Start() {

#if SERVER
            Listener.ConnectionRequestEvent += ApproveConnection;
#endif
            Manager.Start(PORT);
#if CLIENT
            Manager.Connect(IP, PORT, "SomeConnectionKey");  
#endif
        }

        /// <summary>
        /// Updates the network connection
        /// </summary>
        public void Update() {
            Manager.PollEvents();
        }

        /// <summary>
        /// Sends an packet to all other connected clients. 
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        public void Send<T>(ref T command) where T : struct, INetSerializable {
            Processor.SendNetSerializable<T>(Manager, command, DeliveryMethod.ReliableOrdered);
        }
        
        /// <summary>
        /// Sends an packet to a specific connected client.
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        public void Send<T>(NetPeer peer, ref T command) where T : struct, INetSerializable {
            Processor.SendNetSerializable(peer, command, DeliveryMethod.ReliableOrdered);
        }

        /// <summary>
        /// Gets invoked once we receive a certain command. 
        /// </summary>
        /// <param name="onReceive"></param>
        /// <param name="packetConstructor"></param>
        /// <typeparam name="T"></typeparam>
        public void OnReceive<T>(Action<T, NetPeer> onReceive, Func<T> packetConstructor) where T : struct, INetSerializable{
            Processor.SubscribeNetSerializable<T,NetPeer>(onReceive, packetConstructor);
        }

        /// <summary>
        /// The event listener
        /// </summary>
        public EventBasedNetListener Listener { get; set; }
        
        /// <summary>
        /// The writer used to serialize packets and send them
        /// </summary>
        public NetDataWriter Writer { get; set; }
        
        /// <summary>
        /// The reader used to deserialize packets and receive them
        /// </summary>
        public NetDataReader Reader { get; set; }
        
        /// <summary>
        /// The packet processor, does use reader and writer for serializitation of custom structs and commands
        /// </summary>
        public NetPacketProcessor Processor { get; set; }
        
        /// <summary>
        /// The networking manager instance
        /// </summary>
        public NetManager Manager { get; set; }
    }

    /// <summary>
    /// An extension for the build in server management
    /// </summary>
    public partial class Network {
        
        /// <summary>
        /// Approves an incoming connection if the <see cref="MAX_CONNECTIONS"/> wasnt reached.
        /// Otherwhise it will reject them. 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="request"></param>
        private void ApproveConnection(ConnectionRequest request) {

            if (Manager.ConnectedPeersCount < MAX_CONNECTIONS)
                request.Accept();
            else
                request.Reject();
        }
    }
}