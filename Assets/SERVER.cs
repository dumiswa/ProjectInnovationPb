using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SERVER : MonoBehaviour
{
    private TcpListener server;
    private UdpClient udpServer;
    private int port = 49154;
    private int broadCastPort = 49153;

    private bool acceptingClients = true;

    public List<TcpClient> connectedClients = new List<TcpClient>();
    public List<int> shipIndices = new List<int>();
    private const int maxClients = 4; // Maximum allowed clients

    private List<string> names = new List<string>(4) {"","","",""};

    public bool connected = false;

    [SerializeField] private TextMeshProUGUI portText;

    public static SERVER instance;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("2 SERVERS IN SCENE, DELETE AT LEAST ONE OF THEM");
        }

        //connectionsText.text = $"Connected controllers: {connectedClients.Count}";
        for (int i = port; i <= 65535; i++)
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, i);
                listener.Start();
                Debug.Log($"Port {i} is available.");
                listener.Stop();
                portText.text = $"Port: {i}";
                port = i;
                break;
            }
            catch (Exception ex)
            {
                Debug.Log($"Port {i} is not available: {ex.Message}");
            }
        }

        udpServer = new UdpClient(port);
        udpServer.EnableBroadcast = true;
        InvokeRepeating(nameof(BroadcastServer), 0f, 2f); // Broadcast every 2 seconds
        Debug.Log($"Port {port} chosen");
        StartServer();
    }

    public void OnSceneChange()
    {
        for (int i = 0; i < connectedClients.Count; i++)
        {
            GameManager.instance.AddNewPlayer(i + 1, shipIndices[i]);
            CheckPoint.Instance.RegisterPlayers(names[i], i + 1);
        }
    }

    public void StopAcceptingClients()
    {
        acceptingClients = false;
        CancelInvoke(nameof(BroadcastServer));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DisconnectAll();
        }
    }

    #region Networking

    async void BroadcastServer()
    {
        string serverMessage = "ServerDiscovery";
        byte[] data = Encoding.ASCII.GetBytes(serverMessage);
        await udpServer.SendAsync(data, data.Length, new IPEndPoint(IPAddress.Broadcast, broadCastPort));
    }

    private void StartServer()
    {
        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Debug.Log($"Server listening on port {port}... with ip {server.LocalEndpoint}");

            // Start accepting client connections in a separate thread
            AcceptClients();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error starting server: {e.Message}");
        }
    }

    private async void AcceptClients()
    {
        while (acceptingClients)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            if (connectedClients.Count < maxClients)
            {
                connected = true;
                connectedClients.Add(client);
                SelectorManager.instance.AddPlayer(connectedClients.Count);
                //GameManager.instance.AddNewPlayer(connectedClients.Count);
                //connectionsText.text = $"Connected controllers: {connectedClients.Count}";

                HandleClient(client);
            }
            else
            {
                // Handle rejection (e.g., send a message to the client)
                client.GetStream().Write(Encoding.ASCII.GetBytes("Connection failed: Maximum client limit reached"));
                Debug.LogWarning("Maximum client limit reached. Connection rejected.");
                client.Close();
            }
        }
    }

    private string message;

    public void ToControllerScreen()
    {
        foreach (var client in connectedClients)
        {
            var stream = client.GetStream();
            stream.Write(Encoding.ASCII.GetBytes("Play"));
        }
    }

    private async void HandleClient(TcpClient client)
    {
        try
        {
            int controllerIndex = connectedClients.IndexOf(client) + 1;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            stream.Write(Encoding.ASCII.GetBytes($"ConnectClient"));

            while (client.Connected && (bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                if (message.Contains("DisconnectClient"))
                {
                    client.Close();
                    connectedClients.Remove(client);
                    //connectionsText.text = $"Connected controllers: {connectedClients.Count}";
                }

                if (message.Contains("Name:"))
                {
                    var shipName = message.Split(':')[1];
                    SelectorManager.instance.selectors[controllerIndex - 1].SetName(shipName);
                    names[controllerIndex - 1] = shipName;
                }
                if (message.Contains("SelectShip:"))
                {
                    message = message.Split(':')[1];
                    int data = int.Parse(message);
                    SelectorManager.instance.ChangeSelection(controllerIndex, data);
                }

                if (message.Contains("ControlData:"))
                {
                    message = message.Split(':')[1];
                    string[] data = message.Split(',');
                    if(InputManager.instance != null)
                        InputManager.instance.SetInput(controllerIndex, float.Parse(data[1]), float.Parse(data[2]),
                            int.Parse(data[0]) == 1);
                }



                // Process the message (e.g., game input) here
                // You can broadcast this message to other clients if needed
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error handling client: {e.Message}, message: {message}");
        }
        finally
        {
        }
    }

    private void DisconnectAll()
    {
        foreach (var client in connectedClients)
        {
            NetworkStream stream = client.GetStream();
            stream.Write(Encoding.ASCII.GetBytes("DisconnectClient"));
            stream.Close();
            client.Close();
        }

        connectedClients.Clear();
        //connectionsText.text = $"Connected controllers: {connectedClients.Count}";
    }

    #endregion

    private void OnDestroy()
    {
        DisconnectAll();
    }

    private void OnApplicationQuit()
    {
        DisconnectAll();
    }
}