using System;
using LiteNetLib;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Base.Classes.Pattern.Registers;

namespace ParallelOrigin.Core.Network {
    /// <summary>
    ///     A class which manages the network flow.
    ///     For client and server
    /// </summary>
    public partial class Network
    {
        public Network()
        {
            Listener = new EventBasedNetListener();
            Writer = new NetDataWriter();
            Processor = new NetPacketProcessor();

            Manager = new NetManager(Listener)
            {
                AutoRecycle = true
            };

            ServiceLocator.Register(this);
        }

        /// <summary>
        ///     The IP Adress of this network connection
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        ///     The port of this network connection
        /// </summary>
        public ushort Port { get; set; }

        /// <summary>
        ///     The event listener
        /// </summary>
        public EventBasedNetListener Listener { get; set; }

        /// <summary>
        ///     The writer used to serialize packets and send them
        /// </summary>
        public NetDataWriter Writer { get; set; }

        /// <summary>
        ///     The reader used to deserialize packets and receive them
        /// </summary>
        public NetDataReader Reader { get; set; }

        /// <summary>
        ///     The packet processor, does use reader and writer for serializitation of custom structs and commands
        /// </summary>
        public NetPacketProcessor Processor { get; set; }

        /// <summary>
        ///     The networking manager instance
        /// </summary>
        public NetManager Manager { get; set; }

        /// <summary>
        ///     Sets up the network
        /// </summary>
        protected virtual void Setup()
        {
            Listener.NetworkReceiveEvent += OnReceive;
        }

        /// <summary>
        ///     Starts the network connection and server/client.
        /// </summary>
        public virtual void Start()
        {
            Setup();

#if SERVER
        Manager.Start(Port);
#elif CLIENT
            Manager.Start();
            Manager.Connect(IP, Port, "SomeConnectionKey");
#endif
        }

        /// <summary>
        ///     Updates the network connection
        /// </summary>
        public void Update()
        {
            Manager.PollEvents();
        }

        /// <summary>
        ///     Stops the network
        /// </summary>
        public virtual void Stop()
        {
            Manager.Stop();
        }

        /// <summary>
        ///     Sends an packet to all other connected clients.
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        public void Send<T>(ref T command) where T : struct, INetSerializable
        {
#if SERVER
        Processor.SendNetSerializable(Manager, command, DeliveryMethod.ReliableOrdered);
#elif CLIENT
            var server = Manager.FirstPeer;
            if(server == null || server.ConnectionState == ConnectionState.Disconnected) return;
            Processor.SendNetSerializable(server, command, DeliveryMethod.ReliableOrdered);
#endif
        }

        /// <summary>
        ///     Sends an packet to a specific connected client.
        ///     If the lib has automatic packet merging and a way to store packets to release them later, this method will only write the bytes to the certain packet to be filled.
        ///     This way each call does not create a new packet.
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        public void Send<T>(NetPeer peer, ref T command) where T : struct, INetSerializable
        {
            Processor.SendNetSerializable(peer, command, DeliveryMethod.ReliableOrdered);
        }

        /// <summary>
        ///     Sends an packet to a specific connected client.
        ///     If the lib has automatic packet merging and a way to store packets to release them later, this method will only write the bytes to the certain packet to be filled.
        ///     This way each call does not create a new packet.
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        public void Send<T>(NetPeer peer, ref T command, DeliveryMethod method) where T : struct, INetSerializable
        {
            Processor.SendNetSerializable(peer, command, method);
        }

        /// <summary>
        ///     Gets invoked once we receive a certain command.
        /// </summary>
        /// <param name="onReceive"></param>
        /// <param name="packetConstructor"></param>
        /// <typeparam name="T"></typeparam>
        public void OnReceive<T>(Action<T, NetPeer> onReceive, Func<T> packetConstructor) where T : struct, INetSerializable
        {
            Processor.SubscribeNetSerializable(onReceive, packetConstructor);
        }
    }

    /// <summary>
    ///     An extension for the build in server management
    /// </summary>
    public partial class Network
    {
        /// <summary>
        ///     Gets invoked once an packet came in to trigger the <see cref="Processor" />
        /// </summary>
        /// <param name="server"></param>
        /// <param name="request"></param>
        private void OnReceive(NetPeer peer, NetDataReader reader, DeliveryMethod method)
        {
            Processor.ReadAllPackets(reader, peer);
        }
    }
}