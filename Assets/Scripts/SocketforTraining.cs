using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

public class SocketforTraining : MonoBehaviour
{
    public String host = "localhost";
    public Int32 port = 4567;
    public GameObject path;
    private List<Transform> listt;
    public int current = 0;
    public bool near = false;

    int a;

    public CarRemoteControl CarRemoteControl;
    public Camera FrontFacingCamera;
    private CarController _carController;
    public bool crash;
    public bool finished;
    public bool reverse;
    public float time;
    public bool reversepenalty;

    internal Boolean socket_ready = false;
    internal String input_buffer = "";
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;

    void UpdateMe()
    {
        String message = readSocket();
        if(message == "Send")
        {
            String s = Convert.ToBase64String(CameraHelper.CaptureFrame(FrontFacingCamera));
            writeSocket(s);
        }
        else if (message == "crash")
        {
            writeSocket(crash.ToString());
        }
        else if(message.Contains("Action"))
        {
            a = Int32.Parse(message.Substring(6));
            writeSocket("Done");
        }
        else if(message == "restart")
        {
            restartCar();
            writeSocket("Done");
        }
        else if(message == "finish")
        {
            writeSocket(finished.ToString());
        }
        else if(message == "speed")
        {
            writeSocket(_carController.CurrentSpeed.ToString());
        }
        else if (message.Contains("state"))
        {
            String[] state = GameObject.Find("Car").GetComponent<CarRecord>().state;
            String t = "";
            for(int i = 0; i < state.Length; i++)
            {
                if (i == state.Length - 1)
                {
                    t += state[i];
                }
                else
                {
                    t += state[i] + ",";
                }
            }
            writeSocket(t);
        }
    }

    void Update()
    {
        UpdateMe();
        Action(a);
        crashFinder();
        checkReverse();
    }

    void checkReverse()
    {
        if (reverse)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                reversepenalty = true;
            }
            else
            {
                reversepenalty = false;
            }
        }
        else
        {
            time = 8f;
        }
    }

    void restartCar()
    {
        a = 3;
        GameObject.Find("Car").transform.position = new Vector3(0f, 0f, -20f);
        GameObject.Find("Car").transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        GameObject.Find("Car").GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }

    void crashFinder()
    {
        crash = CarRemoteControl.crash;
        finished = CarRemoteControl.finished;
        reverse = GameObject.Find("Car").GetComponent<CarController>().reverse;
    }

    void Awake()
    {
        a = 3;
        time = 8f;
        _carController = CarRemoteControl.GetComponent<CarController>();
        setupSocket();
    }



    void OnApplicationQuit()
    {
        closeSocket();
    }

    public void setupSocket()
    {
        try
        {
            tcp_socket = new TcpClient(host, port);

            net_stream = tcp_socket.GetStream();
            socket_writer =  new StreamWriter(net_stream, Encoding.ASCII, 200000);
            socket_reader = new StreamReader(net_stream);

            socket_ready = true;
        }
        catch (Exception e)
        {
            // Something went wrong
            Debug.Log("Socket error: " + e);
        }
    }

    public void writeSocket(string line)
    {
        if (!socket_ready)
            return;

        line = line + "\r\n";
        socket_writer.Write(line);
        socket_writer.Flush();
    }

    public String readSocket()
    {
        if (!socket_ready)
        {
            return "";
        }

        if (net_stream.DataAvailable)
            return socket_reader.ReadLine();

        return "";
    }

    public void closeSocket()
    {
        if (!socket_ready)
            return;

        socket_writer.Close();
        socket_reader.Close();
        tcp_socket.Close();
        socket_ready = false;
    }

     public void Action(int action_number)
    {
        switch (action_number)
        {
            case 0:
                CarRemoteControl.SteeringAngle = 0.0f;// acc no dir
                CarRemoteControl.Acceleration = 1.0f;
                break;
            case 1:
                CarRemoteControl.SteeringAngle = 0.5f;//forwardright
                CarRemoteControl.Acceleration = 1.0f;
                break;
            case 2:
                CarRemoteControl.SteeringAngle = -0.5f;//forwardleft
                CarRemoteControl.Acceleration = 1.0f;
                break;
            case 3:
                CarRemoteControl.SteeringAngle = 0.0f;
                CarRemoteControl.Acceleration = 0.0f;
                break;
        }
    }

}
