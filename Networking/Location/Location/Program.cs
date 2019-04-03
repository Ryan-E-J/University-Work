using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace Location
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    //If no arguments are entered when running the client (running straight from the .exe file) this will run to open the UI
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new LocationInterface());
                    //This is taken from the start of a Windows Form C# document, it allows the UI to open and become the main focus of the program, meaning it is waiting for user input to the UI
                }
                else
                {
                    //This is if the client is run from the CMD with arguments, this will print all information to the console.
                    TcpClient client = new TcpClient();
                    int timeout = 1000;
                    client.SendTimeout = timeout;
                    client.ReceiveTimeout = timeout; //These timeouts test to see if the server responds within 1 second, if it does not it will drop the connection instantly
                    int port = 43;
                    string address = "localhost";
                    bool UI = false;//This bool exists to show that the client is not using the UI, allowing it to later post it's messages to the correct locations
                    sorter(args, address, port, client,UI);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Origin {0} \r\n Debug Message is : {1}", e.Source, e.Message);
            }
        }
        public void test(string[] args, string address, int port, TcpClient client, bool UI)
        {
            //This method is here so the UI can get access to the rest of the Program's methods as they need to be accessed by both the UI and Console view, this was my solution for the UI not being able to access static methods and the console not being able to access non-static methods
            sorter(args,address,port,client,UI);
        }

        private static void sorter(string[] args, string address, int port, TcpClient client, bool UI)
        {
            //This method is the main sorting method, this will take the input from the user and split it apart into the sections it needs to be to send off the correct message to the server
            bool h = false;
            bool p = false;
            string http = "none";
            if (args.Contains("-h")) //This statement will check for inputs that contain -h
            {
                if (args.Contains("-p"))//This statement will check for inputs that contain -h & -p
                {
                    string[] userInput = new string[args.Length - 4];
                    int arrayCounter = 0;
                    for (int f = 0; f < args.Length; f++) //This loop take the split up args and puts the data required for host and format into a new variable that will be used later on to define how it needs to send the data
                    {
                        if (args[f].Contains("-h"))
                        {
                            if (args[f].Contains("-h9"))//this will check to see if it finds a request for HTTP 0.9 usage, if so it stores the type in the HTTP variable
                            {
                                http = "h9";
                            }
                            else if (args[f].Contains("-h0"))//this will check to see if it finds a request for HTTP 1.0 usage, if so it stores the type in the HTTP variable
                            {
                                http = "h0";
                            }
                            else if (args[f].Contains("-h1"))//this will check to see if it finds a request for HTTP 1.1 usage, if so it stores the type in the HTTP variable
                            {
                                http = "h1";
                            }
                            else
                            {
                                h = true;
                                address = args[f + 1];
                            }
                        }
                        else if (args[f].Contains("-p"))
                        {
                            p = true;
                            port = int.Parse(args[f + 1]);
                        }
                        else if (p == true)
                        {
                            p = false;
                        }
                        else if (h == true)
                        {
                            h = false;
                        }
                        else //If the rest of these if statments aren't triggered then the current args will be added to a new array that will be passed on containing only name and location
                        {
                            userInput[arrayCounter] = args[f];
                            arrayCounter++;
                        }
                    }
                    connecter(userInput, address, port, client, http, UI); //The userInput contains the name and the location, address contains the address to send the data to, port is what port of that address this should be sent to, HTTP defines what protocol will be used, UI says if the UI is in use or not.
                }
                else
                { //if this only contains -h it will change the new array to a different length to show the absence of port decleration
                    string[] userInput = new string[args.Length - 2];
                    int arrayCounter = 0;
                    for (int f = 0; f < args.Length; f++) //this loop does the same as above without the extra input of -p
                    {
                        if (args[f].Contains("-h"))
                        {
                            if (args[f].Contains("-h9"))
                            {
                                http = "h9";
                            }
                            else if (args[f].Contains("-h0"))
                            {
                                http = "h0";
                            }
                            else if (args[f].Contains("-h1"))
                            {
                                http = "h1";
                            }
                            else
                            {
                                h = true;
                                address = args[f + 1];
                            }
                        }
                        else if (h == true)
                        {
                            h = false;
                        }
                        else //This adds to the array under the same conditions of the loop above
                        {
                            userInput[arrayCounter] = args[f];
                            arrayCounter++;
                        }
                    }
                    connecter(userInput, address, port, client, http, UI); //These variables contain the same data as the same line above
                }
            }
            else if (args.Contains("-p")) //This one is if the input has no -h input and only port inputs
            {
                string[] userInput = new string[args.Length - 2];
                int arrayCounter = 0;
                for (int f = 0; f < args.Length; f++)
                {
                    if (args[f].Contains("-p")) //this loop does the same as the solo -h loop however does it for -p instead
                    {
                        p = true;
                        port = int.Parse(args[f + 1]);
                    }
                    else if (p == true)
                    {
                        p = false;
                    }
                    else if (args[f].Contains("-h9"))
                    {
                        http = "h9";
                    }
                    else if (args[f].Contains("-h0"))
                    {
                        http = "h0";
                    }
                    else if (args[f].Contains("-h1"))
                    {
                        http = "h1";
                    }
                    else
                    { //Once again this adds to a new array under the same conditions as the loop above this
                        userInput[arrayCounter] = args[f];
                        arrayCounter++;
                    }
                }
                connecter(userInput, address, port, client, http, UI);//These variables contain the same data as the same line above
            }
            else
            {
                string[] userInput = new string[args.Length];
                int arrayCounter = 0;
                for (int f = 0; f < args.Length; f++) //this loop does what the above loops do however in the absence of -h and -p there is no need to check for them
                {
                    if (args[f].Contains("-h9"))
                    {
                        http = "h9";
                    }
                    else if (args[f].Contains("-h0"))
                    {
                        http = "h0";
                    }
                    else if (args[f].Contains("-h1"))
                    {
                        http = "h1";
                    }
                    else
                    {
                        userInput[arrayCounter] = args[f];
                        arrayCounter++;
                    }
                }
                connecter(userInput, address, port, client, http, UI);//These variables contain the same data as the same line above
            }
        }

        private static void connecter(string[] userIn, string address, int port, TcpClient client, string http, bool UI)
        {
            List<string> userInput = new List<string>(); //This list is to have the results of the userInput array as sometimes it would have null stored due to no data being in there, this removes the null data from the array
            for (int i = 0; i < userIn.Length; i++)
            {
                if(userIn[i] != null)
                {
                    userInput.Add(userIn[i]);
                }
            }
            if (http == "none") //This takes the http variable and checks if there was any http requests, if there were none it uses the who-is protocol format
            {
                if (userInput.Count == 1) //This checks if the client requested a location check
                {
                    client.Connect(address, port);//this line connects the client to the server requested by the client, or by default one hosted by this PC
                    StreamWriter sw = new StreamWriter(client.GetStream());//This line allows the client to send messages to the server
                    StreamReader sr = new StreamReader(client.GetStream());//this line allows the client to recieve messages from the server
                    sw.WriteLine(userInput[0]);//This writes into the streamwriter the name that the user wants to check
                    sw.Flush();//This is required to send an unfilled packet, windows does not send the packets by default unless they are filled.
                    string response = sr.ReadToEnd();//this will take the response the server gives and puts it into a string
                    if (response == "ERROR: no entries found\r\n")//this will check if the server found nothing or if the server has information stored on the name's location
                    {
                        if (UI == true)//if the UI is active this will send out what the server responded with in a message box rather than the console which is for if it is run from CMD
                        {
                            MessageBox.Show(response);
                        }
                        else
                        {
                            Console.WriteLine(response);
                        }
                    }
                    else
                    {
                        //if the server has information stored on the name the client will print it out as a sentence in the format: <name><space>is<space><location>
                        if (UI == true)
                        {
                            MessageBox.Show(userInput[0] + " is " + response);
                        }
                        else
                        {
                            Console.WriteLine(userInput[0] + " is " + response);
                        }
                    }
                }
                else if (userInput.Count == 2) //This checks if the client requested a location change
                {
                    client.Connect(address, port);
                    StreamWriter sw = new StreamWriter(client.GetStream());
                    StreamReader sr = new StreamReader(client.GetStream());
                    sw.WriteLine(userInput[0] + " " + userInput[1]);
                    sw.Flush();
                    string response = sr.ReadToEnd();
                    if (response == "OK\r\n")//if th server replies with this the location has been changed in the server's database and the client will print out to the user that it has been changed in this format: <name><space>location<space>changed<space>to<space>be<space><location>
                    {
                        if (UI == true)
                        {
                            MessageBox.Show(userInput[0] + " location changed to be " + userInput[1]);
                        }
                        else
                        {
                            Console.WriteLine(userInput[0] + " location changed to be " + userInput[1]);
                        }
                    }
                    else
                    {
                        if (UI == true)//if the server replies with anything else or not at all then the client will say to the client it is unreachable and that they should retry again in a few minutes to see if the issue is fixed
                        {
                            MessageBox.Show("Server unreachable, please try again in several minutes.");
                        }
                        else
                        {
                            Console.WriteLine("Server unreachable, please try again in several minutes.");
                        }
                    }
                }
                else//if the user has some how managed to get too many arguments into the client then the server will state that there is too many arguments inputted and inform the client to refer to the readme file
                {
                    Console.WriteLine("Too many arguments detected, please refer to the readme file and try again.");
                }
            }
            else if (http == "h9")//This is for HTTP0.9 formatting
            {
                if (userInput.Count == 1)
                {
                    bool locationChange = false;//this bool is here to simplify later on whether or not the user has requested a location change or not, this is not a location change so it is false
                    client.Connect(address, port);
                    StreamWriter sw = new StreamWriter(client.GetStream());
                    StreamReader sr = new StreamReader(client.GetStream());
                    sw.WriteLine($"GET /{userInput[0]}");//this is the how the HTTP0.9 protocol needs to be formatted for a location check
                    sw.Flush();
                    string response = sr.ReadLine();
                    httpResponseProcessing(response, sr, userInput, locationChange, UI);//this method is here to process all of the HTTP protocol client work, it is sent the response from the server, the stream reader, the location change bool, and if the UI is running or not
                }
                else if (userInput.Count == 2)
                {
                    bool locationChange = true;//this is a location change so the bool is set to true
                    client.Connect(address, port);
                    StreamWriter sw = new StreamWriter(client.GetStream());
                    StreamReader sr = new StreamReader(client.GetStream());
                    sw.WriteLine($"PUT /{userInput[0]}");//this is the how the HTTP0.9 protocol needs to be formatted for a location change
                    sw.WriteLine();
                    sw.WriteLine(userInput[1]);
                    sw.Flush();
                    string response = sr.ReadLine();
                    httpResponseProcessing(response, sr, userInput, locationChange, UI);//once again the same information will be sent to the HTTP response process
                }
                else
                {
                    Console.WriteLine("Too many arguments detected, please refer to the readme file and try again.");
                }
            }
            else if (http == "h0")
            {
                if (userInput.Count == 1)
                {
                    bool locationChange = false;//this is not a location change so the bool is set to false
                    client.Connect(address, port);
                    StreamWriter sw = new StreamWriter(client.GetStream());
                    StreamReader sr = new StreamReader(client.GetStream());
                    sw.WriteLine($"GET /?{userInput[0]} HTTP/1.0");//this is the format for a HTTP 1.0 protocol location check
                    sw.WriteLine("");
                    sw.Flush();
                    string response = sr.ReadLine();
                    httpResponseProcessing(response, sr, userInput, locationChange, UI);//this will send the same information as the other HTTP protocol's to the processing method
                }
                else if (userInput.Count == 2)
                {
                    bool locationChange = true;//this is a location change so the bool is set to true
                    client.Connect(address, port);
                    StreamWriter sw = new StreamWriter(client.GetStream());
                    StreamReader sr = new StreamReader(client.GetStream());
                    sw.WriteLine($"POST /{userInput[0]} HTTP/1.0");//this is the format for a HTTP 1.0 protocol location change
                    sw.WriteLine($"Content-Length: {userInput[1].Length}");
                    sw.WriteLine();
                    sw.Write(userInput[1]);
                    sw.Flush();
                    string response = sr.ReadLine();
                    httpResponseProcessing(response, sr, userInput, locationChange, UI);//this will send the same information as the other HTTP protocol's to the processing method
                }
                else
                {
                    Console.WriteLine("Too many arguments detected, please refer to the readme file and try again.");
                }
            }
            else if (http == "h1")
            {
                if (userInput.Count == 1)
                {
                    bool locationChange = false;//this is not a location change so the bool is set to false
                    client.Connect(address, port);
                    StreamWriter sw = new StreamWriter(client.GetStream());
                    StreamReader sr = new StreamReader(client.GetStream());
                    sw.WriteLine($"GET /?name={userInput[0]} HTTP/1.1");
                    sw.WriteLine($"Host: {address}");//this is the address of the host, this is so the reciever can confirm it was for them
                    sw.WriteLine();
                    sw.Flush();
                    string response = sr.ReadLine();
                    httpResponseProcessing(response, sr, userInput, locationChange, UI);//this will send the same information as the other HTTP protocol's to the processing method
                }
                else if (userInput.Count == 2)
                {
                    bool locationChange = true;//this is a location change so the bool is set to true
                    client.Connect(address, port);
                    StreamWriter sw = new StreamWriter(client.GetStream());
                    StreamReader sr = new StreamReader(client.GetStream());
                    
                    int contentLength = 15 + userInput[0].Length + userInput[1].Length;//This maths is required due to the fact 1.1 requiring a count of how many characters you are sending in your message, it adds together the length of the location, name and the required text there for formatting
                    sw.Write($"POST / HTTP/1.1\r\n");//this is the format for a HTTP 1.1 protocol location change
                    sw.Write($"Host: {address}\r\n");//this is the address of the host, this is so the reciever can confirm it was for them
                    sw.Write($"Content-Length: {contentLength.ToString()}\r\n");//this is where the length of the content is stated
                    sw.Write("\r\n");
                    sw.Write($"name={userInput[0]}&location={userInput[1]}");//this is the line of content it requests the length of.
                    sw.Flush();
                    string response = sr.ReadLine();
                    httpResponseProcessing(response, sr, userInput, locationChange, UI);//this will send the same information as the other HTTP protocol's to the processing method
                }
                else
                {
                    Console.WriteLine("Too many arguments detected, please refer to the readme file and try again.");
                }
            }
        }
        private static void httpResponseProcessing(string response, StreamReader sr, List<string> userInput, bool locationChange, bool UI)
        {//all if statements use the first line sent to client rather than trying to guess how many lines it has been sent
            if (response.Contains("404") && response.Contains("0.9"))//this checks to see if the reply from the server shows that there is no data from the requested name in the format of HTTP 0.9
            {
                if (UI == true) //this is converted to the same style of output used for the who-is protocol to keep it universal throughout all formatting
                {
                    MessageBox.Show("ERROR: no entries found");
                }
                else
                {
                    Console.WriteLine("ERROR: no entries found");
                }
            }
            else if (response.Contains("200") && response.Contains("0.9"))//if the response is 200 this could mean it has either found the name in it's database or successfully changed the location of the name
            {
                if (locationChange == true)//the locationChange bool now comes into use as there is no way to define whether or not it is a location change or not in the return message, this bool allows the client to recieve all of the data it needs from the server.
                {//if it is a location change the client already knows all the data required and uses it for the output to the user
                    if (UI == true)//this is once again put into the format of the who-is output as well for consistency
                    {
                        MessageBox.Show(userInput[0] + " location changed to be  " + userInput[1]);
                    }
                    else
                    {
                        Console.WriteLine(userInput[0] + " location changed to be  " + userInput[1]);
                    }
                }
                else
                {//if it isn't a location it needs to get the final line sent by the server to get the name's location in the server, this is always the case due to the formatting of the protocol
                    for (int i = 0; i < 3; i++)
                    {
                        response = sr.ReadLine();
                    }
                    if (UI == true)//this is once again put into the format of the who-is output as well for consistency
                    {
                        MessageBox.Show($"{userInput[0]} is {response}");
                    }
                    else
                    {
                        Console.WriteLine($"{userInput[0]} is {response}");
                    }
                }
            }
            else if (response.Contains("404") && response.Contains("1.0"))//this checks to see if the reply from the server shows that there is no data from the requested name in the format of HTTP 1.0
            {
                if (UI == true)//this is once again put into the format of the who-is output as well for consistency
                {
                    MessageBox.Show("ERROR: no entries found");
                }
                else
                {
                    Console.WriteLine("ERROR: no entries found");
                }
            }
            else if (response.Contains("200") && response.Contains("1.0"))//if the response is 200 this could mean it has either found the name in it's database or successfully changed the location of the name
            {
                if (locationChange == true)//if it is a location change the client already knows all the data required and uses it for the output to the user
                {
                    if (UI == true)//this is once again put into the format of the who-is output as well for consistency
                    {
                        MessageBox.Show(userInput[0] + " location changed to be  " + userInput[1]);
                    }
                    else
                    {
                        Console.WriteLine(userInput[0] + " location changed to be  " + userInput[1]);
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)//This needs to be looped through like with the 0.9 message from the server due to the fact that it does not have the data the client needs until the final line to provide the output to the user
                    {
                        response = sr.ReadLine();
                    }
                    if (UI == true)//this is once again put into the format of the who-is output as well for consistency
                    {
                        MessageBox.Show($"{userInput[0]} is {response}");
                    }
                    else
                    {
                        Console.WriteLine($"{userInput[0]} is {response}");
                    }
                }
            }
            else if (response.Contains("404") && response.Contains("1.1"))//this checks to see if the reply from the server shows that there is no data from the requested name in the format of HTTP 1.1
            {
                if (UI == true)//this is once again put into the format of the who-is output as well for consistency
                {
                    MessageBox.Show("ERROR: no entries found");
                }
                else
                {
                    Console.WriteLine("ERROR: no entries found");
                }
            }
            else if (response.Contains("200") && response.Contains("1.1"))
            {
                if (locationChange == true)//if it is a location change the client already knows all the data required and uses it for the output to the user
                {
                    if (UI == true)//this is once again put into the format of the who-is output as well for consistency
                    {
                        MessageBox.Show(userInput[0] + " location changed to be  " + userInput[1]);
                    }
                    else
                    {
                        Console.WriteLine(userInput[0] + " location changed to be  " + userInput[1]);
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)//This needs to be looped through like with the 0.9 message from the server due to the fact that it does not have the data the client needs until the final line to provide the output to the user
                    {
                        response = sr.ReadLine();
                    }
                    if (UI == true)//this is once again put into the format of the who-is output as well for consistency
                    {
                        MessageBox.Show($"{userInput[0]} is {response}");
                    }
                    else
                    {
                        Console.WriteLine($"{userInput[0]} is {response}");
                    }
                }
            }
        }
    }
}
