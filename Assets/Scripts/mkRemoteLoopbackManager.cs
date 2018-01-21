using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Oculus.Avatar;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class mkRemoteLoopbackManager : MonoBehaviour
{
	[System.Serializable]
    class PacketLatencyPair
    {
        public byte[] PacketData;
		public float time;
    };

    public OvrAvatar LocalAvatar;
    public OvrAvatar LoopbackAvatar;
	public int framesSkipped;

	private bool done = false;
	private bool recording = false;
    private int PacketSequence = 0;
	private int frameskipper = 0;
	private float startTime = 0.0f;

    LinkedList<PacketLatencyPair> packetQueue = new LinkedList<PacketLatencyPair>();

    void Start()
    {
        LocalAvatar.RecordPackets = true;
        LocalAvatar.PacketRecorded += OnLocalAvatarPacketRecorded;
    }

    void OnLocalAvatarPacketRecorded(object sender, OvrAvatar.PacketEventArgs args)
    {
        using (MemoryStream outputStream = new MemoryStream())
        {
            BinaryWriter writer = new BinaryWriter(outputStream);

            if (LocalAvatar.UseSDKPackets)
            {
                var size = CAPI.ovrAvatarPacket_GetSize(args.Packet.ovrNativePacket);
                byte[] data = new byte[size];
                CAPI.ovrAvatarPacket_Write(args.Packet.ovrNativePacket, size, data);

                writer.Write(PacketSequence++);
                writer.Write(size);
                writer.Write(data);
            }
            else
            {
                writer.Write(PacketSequence++);
                args.Packet.Write(outputStream);
            }

            SendPacketData(outputStream.ToArray());
        }
    }

    void Update()
    {
		if ( Input.GetKeyDown("space") || OVRInput.Get(OVRInput.Button.One) ) {
			print ("recording started");
			recording = true;
		}
		if (!done && recording) {
	        if (packetQueue.Count > 0)
	        {
	            List<PacketLatencyPair> deadList = new List<PacketLatencyPair>();
	            foreach (var packet in packetQueue)
	            {
	            }

	            foreach (var packet in deadList)
	            {
	                packetQueue.Remove(packet);
	            }
	        }
			if (packetQueue.Count > 1) {
				print ("recording finished");
				print ("name recording");
				done = true;
				recording = false;
				frameskipper = 0;

				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Create ("Assets\\Moves\\move001.mx");
				//1 == packetQueue plp(1)
				//2 == 2
				//3 == ser(2)

				PacketLatencyPair[] ser = new PacketLatencyPair[packetQueue.Count];
				int i = 0;
				foreach(PacketLatencyPair plp in packetQueue){
					bf.Serialize (file, plp);
					ser[i] = plp;
					i++;
				}
//				bf.Serialize (file, ser);
				file.Close ();
//				bf.Serialize (file, packetQueue);
			}
		}
    }
    
    void SendPacketData(byte[] data)
    {
		if (frameskipper > framesSkipped) {
			PacketLatencyPair PacketPair = new PacketLatencyPair ();
			PacketPair.time = Time.time - startTime;
			PacketPair.PacketData = data;
			packetQueue.AddLast (PacketPair);
			frameskipper = 0;
			print ("frame recorded!");
		} else if (recording) {
			frameskipper++;
		}
    }
}