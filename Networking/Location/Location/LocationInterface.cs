using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace Location
{
    public partial class LocationInterface : Form
    {
        public LocationInterface()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//Once this button is pressed the client will need to set up the defaults for all of the things that may be left empty
            int port = 43;
            string address = "localhost";
            string name = "532163";
            int argsCount = 1;//this is so the program will know how large to make the args array, it is defaulted to 1 as it will always have a name
            TcpClient client = new TcpClient();
            int timeout = 1000;//this is the default timeout time, it is 1 second
            string location = "";
            string http = "";
            try//this try is here to catch any issues with input into any of the text boxes
            {
                if (timetxt.Text != "")
                {
                    timeout = int.Parse(timetxt.Text);//this does not get added to the array as it can change the variable directly
                }
                if (porttxt.Text != "")
                {
                    port = int.Parse(porttxt.Text);//this does not get added to the array as it can change the variable directly
                }
                if (addtxt.Text != "")
                {
                    address = addtxt.Text;//this does not get added to the array as it can change the variable directly
                }
                if (usrtxt.Text != "")
                {
                    name = usrtxt.Text;//this does not get added to the array as it can change the variable directly
                }
                if (loctxt.Text != "")
                {
                    location = loctxt.Text;
                    argsCount++;//this adds an extra argscount as it will be added to the array along with the name
                }
                if (http9rad.Checked == true)
                {
                    argsCount++;//this adds an extra argscount as it will be added to the array along with the name
                    http = "-h9";
                }
                else if (HTTP0rad.Checked == true)
                {
                    argsCount++;//this adds an extra argscount as it will be added to the array along with the name
                    http = "-h0";
                }
                else if (HTTP1rad.Checked == true)
                {
                    argsCount++;//this adds an extra argscount as it will be added to the array along with the name
                    http = "-h1";
                }
                string[] args = new string[argsCount];//an args is created so it can be inserted in place of what would usually be put into the client through the CMD, this builds it within the program using what the user inputted to the UI
                if (whorad.Checked == true)//if the who-is protocol radio button is checked then the program will know that it only needs to insert the name and possibly the location
                {
                    if (argsCount == 1)
                    {
                        args[0] = name;
                    }
                    else
                    {
                        args[0] = name;
                        args[1] = location;
                    }//this will only add the name and location as the who-is protocol does not need to be defined to be used.
                }
                else
                {
                    if (argsCount == 2)
                    {
                        args[0] = name;
                        args[1] = http;
                    }
                    else
                    {
                        args[0] = name;
                        args[1] = location;
                        args[2] = http;
                    }//The most information it will contain is the name, the location and the HTTP style this is because it can directly change the host due to it being able to know where the address and port will come from unlike with the CMD method
                }
                client.SendTimeout = timeout;
                client.ReceiveTimeout = timeout;
                Program test = new Program();
                bool UI = true;//this will allow the program later on find out if the user is using the UI or CMD
                test.test(args, address, port, client, UI);//this will then send it back into the program and once it has finished it will allow the user to send out another from the UI
            }
            catch (Exception es)
            {
                MessageBox.Show($"Exception Origin {es.Source} \r\n Debug Message is : {es.Message}");
            }
        }
    }
}
