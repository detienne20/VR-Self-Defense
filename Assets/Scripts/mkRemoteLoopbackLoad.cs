using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Oculus.Avatar;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Linq;

public class mkRemoteLoopbackLoad : MonoBehaviour
{
	[System.Serializable]
    class PacketLatencyPair
    {
        public byte[] PacketData;
		public float time;
    };

    public OvrAvatar LoopbackAvatar;

    private int PacketSequence = 0;

	private PacketLatencyPair[] test;
    LinkedList<PacketLatencyPair> packetQueue = new LinkedList<PacketLatencyPair>();

	public void LoadMove() {
		if (File.Exists ("Assets\\Moves\\move001.mx")) {
			print ("MOOAOFASOFOO?");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open ("Assets\\Moves\\move001.mx", FileMode.Open);

//			object datasObj = bf.Deserialize(file);
//			Debug.Log(datasObj.GetType().FullName);
//			packetQueue 
			try 
			{
				BinaryFormatter formatter = new BinaryFormatter();

				// Deserialize the hashtable from the file and 
				// assign the reference to the local variable.
				PacketLatencyPair p = formatter.Deserialize(file) as PacketLatencyPair;
				print("TEST");
			}
			catch (SerializationException e) 
			{
				Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
				throw;
			}
			finally 
			{
				file.Close();
			}
//
//			packetQueue.AddLast( bf.Deserialize (file) as PacketLatencyPair );
//			print (packetQueue.First);
////			packetQueue.AddLast( (PacketLatencyPair)bf.Deserialize (file) );
////			var keek = (PacketLatencyPair[])bf.Deserialize (file);
//			file.Close ();
//			print ("SUCCESSS HOEPFPEUPFFGLLLY?");
		}
	}

    void Start()
    {
		print ("STARTLOAD");
		LoadMove ();
    }

//    void Update()
//    {
//		if (packetQueue.Count > 0) {
//			List<PacketLatencyPair> deadList = new List<PacketLatencyPair> ();
//			foreach (var packet in packetQueue) {
//				packet.time -= Time.deltaTime;
////				print ("TEST");
//
//				if (packet.time < 0f) {
//					ReceivePacketData (packet.PacketData);
//					deadList.Add (packet);
//				}
//			}
//
//			foreach (var packet in deadList) {
//				packetQueue.Remove (packet);
//			}
//		} else {
////			LoadMove ();
//		}
//    }
//
//    void ReceivePacketData(byte[] data)
//    {
//        using (MemoryStream inputStream = new MemoryStream(data))
//        {
//            BinaryReader reader = new BinaryReader(inputStream);
//            int sequence = reader.ReadInt32();
//
//            OvrAvatarPacket avatarPacket;
//            if (LoopbackAvatar.UseSDKPackets)
//            {
//                int size = reader.ReadInt32();
//                byte[] sdkData = reader.ReadBytes(size);
//
//                IntPtr packet = CAPI.ovrAvatarPacket_Read((UInt32)data.Length, sdkData);
//                avatarPacket = new OvrAvatarPacket { ovrNativePacket = packet };
//            }
//            else
//            {
//                avatarPacket = OvrAvatarPacket.Read(inputStream);
//            }
//
//            LoopbackAvatar.GetComponent<OvrAvatarRemoteDriver>().QueuePacket(sequence, avatarPacket);
//        }
//    }
}