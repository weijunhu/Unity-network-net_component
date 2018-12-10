using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Network : MonoBehaviour {
    private Socket m_socket;
    IPAddress address;
    IPEndPoint endPoint;
    SocketError error;
    // Use this for initialization
    void Start () {
        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        address = IPAddress.Parse("192.168.2.217");
        endPoint = new IPEndPoint(address, 7000);
    }
	
    private void OnGUI()
    {
        if(GUILayout.Button("test",new GUILayoutOption[] {GUILayout.Width(100),GUILayout.Height(100) }))
        {
            m_socket.BeginConnect(endPoint, asyncResult => {
                try{
                    m_socket.EndConnect(asyncResult);
                    Debug.Log(">>>>>>>");
                }catch (SocketException ex)
                { //无法连接目标主机10060  主动拒绝10061  读写时主机断开10053 
                    Debug.LogError("==connectException=>" + ex.NativeErrorCode);
                    return;
                }
            },m_socket);
        }


        if (GUILayout.Button("send",new GUILayoutOption[] {GUILayout.Width(100),GUILayout.Height(100)}))
        {
            string str = "aaa";
            byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
            m_socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, asyncBack=>{
                int length = m_socket.EndSend(asyncBack);
                Debug.Log(length);

            },m_socket);

        }
    }
}
