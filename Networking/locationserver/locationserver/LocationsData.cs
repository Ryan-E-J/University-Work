using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace locationserver
{
    class LocationsData
    {
        private Dictionary<string, string> Locations = new Dictionary<string, string>();
        public void addItem(string key, string info)//this method is to add an item
        {
            if (Locations.ContainsKey(key))//if it already exists it just changes the information stored with the key
            {
                Locations[key] = info;
            }
            else//if it doesn't exist then it will add it to the dictionary under the key of the name
            {
                Locations.Add(key, info);
            }
        }
        public string checkLocation(string key)//this method is to check for a location
        {
            if (Locations.ContainsKey(key))//this will check to see if the name is a key in the dictionary, if it is it will send back the information stored with it
            {
                string Location = Locations[key];
                return Location;
            }
            else//this will send back no enteries found if there is nothing found in the dictionary
            {
                string Location = "ERROR: no entries found";
                return Location;
            }
        }
    }
}
