using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace locationserver
{
    class locationServer
    {
        static void Main(string[] args)
        {
            LocationsData Locations = new LocationsData();//this sets up the dictionary for the storage of names and locations that have been sent to the server
            runServer(Locations);//this new dictionary is sent to the runServer method to start the server running
        }
        
        static void runServer(LocationsData Locations)
        {
            TcpListener listener;//this is here to set up a way to listen on a certain port of the IP that the server will use
            Socket connection;
            Handler RequestHandler;//this is the handler class that was set up to handle any connects the server recieves
            try
            {
                Console.WriteLine("Server launching...");
                listener = new TcpListener(IPAddress.Any,43);
                listener.Start();
                Console.WriteLine("Server launched.");
                Console.WriteLine("Awaiting client connection...");
                while (true)
                {
                    connection = listener.AcceptSocket();//this collects the data from the new connection and puts it into the socket to be able to communicate with the connection
                    Console.WriteLine("Awaiting client action...");
                    RequestHandler = new Handler();
                    Thread t = new Thread(() => RequestHandler.doRequest(Locations, connection));//when a new connection is made it is taken by a new thread and when this thread is started it goes to the doRequest method within Handler
                    t.Start();//this launches the new thread
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }

    }
    class Handler
    {
        public void doRequest(LocationsData Locations, Socket connection)
        {//this section here sets up a new stream to be able to send it to the client, if it takes longer than a second for either the client or this server it will then cut the connection and timeout
            NetworkStream socketStream;
            socketStream = new NetworkStream(connection);
            Console.WriteLine("Connection to client established.");
            connection.ReceiveTimeout = 1000;
            connection.SendTimeout = 1000;
            socketStream.ReadTimeout = 1000;
            socketStream.WriteTimeout = 1000;
            try
            {
                bool httpActive = false;//this bool is here to see if the server has recieved a HTTP request from the client
                StreamWriter sw = new StreamWriter(socketStream);
                StreamReader sr = new StreamReader(socketStream);
                string input = sr.ReadLine();//this will get the first line of input from the client
                string[] sections = input.Split(new char[] { ' ' }, 2);//this splits that input into two section after a space
                if (input.StartsWith("GET /") || input.StartsWith("PUT /") || input.StartsWith("POST /"))
                {//this if statement checks if it is a HTTP request or not, this is so it skips the future if statements
                    HTTPRequest(sw, sr, sections, Locations);//this then sends the stream reader, stream writer, sections array, and the Locations dictionary so it can insert data
                    httpActive = true;
                }
                else if (sections.Length == 2 & sections[0] == "")//this will check to see what the client has sent to the server and proceed to write it to the console
                {
                    Console.WriteLine("Client sent: Null " + sections[1]);
                }
                else if (sections[0] == "")
                {
                    Console.WriteLine("Client sent: Null");
                }
                else if (sections.Length == 2)
                {
                    if (sections[1] == "")
                    {
                        Console.WriteLine("Client sent: " + input + " Null");
                    }
                    else
                    {
                        Console.WriteLine("Client sent: " + input);
                    }
                }
                else
                {
                    Console.WriteLine("Client sent: " + input);
                }
                if (sections.Length == 1 && httpActive == false)//this will check if the client requested a location check
                {
                    Console.WriteLine("\n Client requested a location check. \n");//this will then write it to the console and ask the LocationsData class to check if it exists
                    string Location = Locations.checkLocation(sections[0]);
                    if (Location == "ERROR: no entries found")//if it finds nothing it will then write back to the client that nothing was found
                    {
                        sw.WriteLine("ERROR: no entries found");
                        Console.WriteLine("Server response: ERROR: no entries found \n");
                    }
                    else//if the server has found something it will then reply to the client with the data that it has found
                    {
                        sw.WriteLine(Location);
                        if (Location == "")
                        {
                            Console.WriteLine("Server response: Null \n");
                        }
                        else
                        {
                            Console.WriteLine("Server response: " + Location + "\n");
                        }
                    }
                    sw.Flush();//this is here to send the packet before it is full as it will not be sent otherwise
                }
                else if (httpActive == false)//if the client has requested a Location Change it will then proceed to change the data stored for the location of that name and send back OK if it has succeeded
                {
                    try
                    {
                        Console.WriteLine("\n Client requested a location change. \n");
                        Locations.addItem(sections[0], sections[1]);
                        sw.WriteLine("OK");
                        Console.WriteLine("Server response: OK \n");
                        sw.Flush();
                    }
                    catch
                    {
                        Console.WriteLine("Error with adding items to dictionary");//if an error occurs it will then send a message to the client containing that there has been an error
                        sw.WriteLine("An error occured, please try again.");
                        sw.Flush();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
            finally
            {
                socketStream.Close();//once it has completed this it will close the socket and the connection so the thread can also close
                connection.Close();
            }
        }

        public void HTTPRequest(StreamWriter sw, StreamReader sr, string[] sections, LocationsData Locations)//this method handles all of the HTTP requests
        {
            if (sections[1].EndsWith("HTTP/1.0"))//these are divided up through using the end containing HTTP/<Ver.Num.>
            {
                if (sections[0].StartsWith("POST"))//if it starts with POST this means the client wants to do a location change
                {
                    try
                    {
                        string name = sections[1].TrimStart('/');//this will then trim the start of the line to get the name from it
                        string result = "";
                        for (int i = 0; i < 3; i++)//this will then loop until it gets to the line required to get the location data
                        {
                            result = sr.ReadLine();
                        }
                        Console.WriteLine("\n Client requested a http 1.0 location change. \n");
                        Locations.addItem(name, result);//this will then add the data to the dictionary and send back to the user that it has successfully added/changed the data
                        sw.WriteLine("HTTP/1.0 200 OK");
                        sw.WriteLine("Content-Type: text/plain");
                        sw.WriteLine();
                        sw.Flush();
                    }
                    catch
                    {
                        Console.WriteLine("Error with adding items to dictionary");
                        sw.WriteLine("An error occured, please try again.");
                        sw.Flush();
                    }
                }
                else
                {
                    string name = sections[1].Replace("/?", "");//this is to be able to get the name as it has /? infront of it
                    Console.WriteLine("\n Client requested a HTTP 1.0 location check. \n");
                    string Location = Locations.checkLocation(name);//this sends off the name to the dictionary to get the location of the name
                    if (Location == "ERROR: no entries found")//if there is nothing found it sends a 404
                    {
                        sw.WriteLine("HTTP/1.0 404 Not Found");
                        sw.WriteLine("Content-Type: text/plain");
                    }
                    else
                    {
                        sw.WriteLine("HTTP/1.0 200 OK");//if there is something found the server will then send location in the correct format
                        sw.WriteLine("Content-Type: text/plain");
                        sw.WriteLine();
                        sw.WriteLine(Location);
                        if (Location == "")
                        {
                            Console.WriteLine("Server response: Null \n");
                        }
                        else
                        {
                            Console.WriteLine("Server response: " + Location + "\n");
                        }
                    }
                    sw.Flush();
                }
            }
            else if (sections[1].EndsWith("HTTP/1.1"))//these are divided up through using the end containing HTTP/<Ver.Num.>
            {
                if (sections[0].StartsWith("POST"))//if it starts with POST this means the client wants to do a location change
                {
                    try
                    {
                        string result = "";
                        for (int i = 0; i < 4; i++)//this will loop through 4 times to be able to get the location and name information
                        {
                            result = sr.ReadLine();
                        }
                        string[] resultSections = result.Split(new char[] { '&' }, 2);
                        string name = resultSections[0].Replace("name=", "");
                        string location = resultSections[1].Replace("location=", "");
                        Console.WriteLine("\n Client requested a http 1.1 location change. \n");//this uses .Replace as it is the most efficient way of being able to remove the name= and location= and extra the information needed
                        Locations.addItem(name, location);
                        sw.WriteLine("HTTP/1.1 200 OK");//it will then send that it has successfully entered it into the dictionary
                        sw.WriteLine("Content-Type: text/plain");
                        sw.WriteLine();
                        sw.Flush();
                    }
                    catch
                    {
                        Console.WriteLine("Error with adding items to dictionary");
                        sw.WriteLine("An error occured, please try again.");
                        sw.Flush();
                    }
                }
                else
                {
                    string name = sections[1].Replace("/?name=", "");//this will extract the name from the message
                    name = name.Replace(" HTTP/1.1", "");
                    Console.WriteLine("\n Client requested a HTTP 1.1 location check. \n");
                    string Location = Locations.checkLocation(name);//this will then check the dictionary for the name and anything associated with it
                    if (Location == "ERROR: no entries found")//if nothing is found it will return a 404
                    {
                        sw.WriteLine("HTTP/1.1 404 Not Found");
                        sw.WriteLine("Content-Type: text/plain");
                        sw.WriteLine();
                    }
                    else
                    {
                        sw.WriteLine("HTTP/1.1 200 OK");//if something is found it will then send a message back to the connection with the location attatched to it
                        sw.WriteLine("Content-Type: text/plain");
                        sw.WriteLine();
                        sw.WriteLine(Location);
                        if (Location == "")
                        {
                            Console.WriteLine("Server response: Null \n");
                        }
                        else
                        {
                            Console.WriteLine("Server response: " + Location + "\n");
                        }
                    }
                    sw.Flush();
                }
            }
            else
            {
                string name = sections[1].TrimStart('/');
                if (sections[0].StartsWith("PUT"))//if it starts with PUT this means the client wants to do a location change
                {
                    {
                        try
                        {
                            string result = "";
                            for (int i = 0; i < 2; i++)
                            {
                                result = sr.ReadLine();
                            }
                            Console.WriteLine("\n Client requested a http 0.9 location change. \n");//this will get name from the first line by getting rid of the / and then the location from the final line and send them to the dictionary
                            Locations.addItem(name, result);
                            sw.WriteLine("HTTP/0.9 200 OK");//it will then reply with a 200 message to signify that it has succesfully added it to the dictionary
                            sw.WriteLine("Content-Type: text/plain");
                            sw.WriteLine();
                            sw.Flush();
                        }
                        catch
                        {
                            Console.WriteLine("Error with adding items to dictionary");
                            sw.WriteLine("An error occured, please try again.");
                            sw.Flush();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n Client requested a HTTP 0.9 location check. \n");
                    string Location = Locations.checkLocation(name);//the name is on the first line at the start trimmed from / once again
                    if (Location == "ERROR: no entries found")//If nothing is found it send a 404 message
                    {
                        sw.WriteLine("HTTP/0.9 404 Not Found");
                        sw.WriteLine("Content-Type: text/plain");
                        sw.WriteLine();
                    }
                    else
                    {
                        sw.WriteLine("HTTP/0.9 200 OK");//if it finds the location in the dictionary it will send a 200 OK message with the location on the end of it
                        sw.WriteLine("Content-Type: text/plain");
                        sw.WriteLine();
                        sw.WriteLine(Location);
                        if (Location == "")
                        {
                            Console.WriteLine("Server response: Null \n");
                        }
                        else
                        {
                            Console.WriteLine("Server response: " + Location + "\n");
                        }
                    }
                    sw.Flush();
                }
            }
        }
    }
}
