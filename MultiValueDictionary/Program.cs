using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionary
{
    public class Program
    {
        static Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        static string userInput = string.Empty;
        static void Main(string[] args)
        {
            for (int i = 0; ; i++)
            {
                userInput = PromptUser();
                if (userInput.Length == 0)
                {
                    break;
                }
                else
                {
                    if (userInput.ToUpper().Contains("ADD"))
                        ADD();
                    if (userInput.ToUpper().Contains("KEYS"))
                        KEYS();
                    if (userInput.ToUpper().Contains("ALLMEMBERS"))
                        ALLMEMBERS();
                    if (userInput.ToUpper().Contains("MEMBERS") && !userInput.ToUpper().Contains("ALLMEMBERS"))
                        MEMBERS();
                    if (userInput.ToUpper().Contains("KEYEXISTS"))
                        KEYEXISTS();
                    if (userInput.ToUpper().Contains("VALUEEXISTS"))
                        VALUEEXISTS();
                    if (userInput.ToUpper().Contains("REMOVE") && !userInput.ToUpper().Contains("REMOVEALL"))
                        REMOVE();
                    if (userInput.ToUpper().Contains("REMOVEALL"))
                        REMOVEALL();
                    if (userInput.ToUpper().Contains("CLEAR"))
                        CLEAR();
                    if (userInput.ToUpper().Contains("ITEMS"))
                        ITEMS();
                }
            }
        }
        private static string PromptUser()
        {
            Console.WriteLine("Enter the Command name and input values");
            string input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// Returns all the values in the dictionary.  Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        private static void ALLMEMBERS()
        {
            int i = 1;
            foreach (var dicVal in dictionary)
            {
                foreach (var values in dictionary[dicVal.Key])
                {
                    Console.WriteLine(i + ")" + values);
                    i++;
                }
            }
            if (i < 2)
            {
                Console.WriteLine("No members available");
            }
        }

        /// <summary>
        /// Returns all keys in the dictionary and all of their values.  Returns nothing if there are none.  Order is not guaranteed.
        /// </summary>
        private static void ITEMS()
        {
            int i = 1;
            foreach (var dicVal in dictionary)
            {
                foreach (var values in dictionary[dicVal.Key])
                {
                    Console.WriteLine(i + ")" + dicVal.Key + ":" + values);
                    i++;
                }
            }
        }

        /// <summary>
        /// Add a Key value to the new Dictionary
        /// </summary>
        private static void ADD()
        {
            string[] inputs = userInput.Split(' ');
            string key = string.Empty;
            string command = string.Empty;
            List<string> valuesList = inputs.ToList();
            if (valuesList.Count > 2)
            {
                key = valuesList[1].ToString();
                if (string.IsNullOrEmpty(key))
                {
                    Console.WriteLine("Please provide a string for KEY");
                }
                else
                {
                    //Check if the key already exists and if we are adding same value to that key
                    if (dictionary.ContainsKey(key))
                    {
                        var existingList = dictionary[key];
                        command = valuesList[0].ToString();
                        valuesList.RemoveRange(0, 2);
                        if (valuesList.Count > 0)
                        {
                            if ((existingList.Contains(valuesList[0])))
                            {
                                Console.WriteLine("Value already exists, you cannot add it again.");
                            }
                            else
                            {
                                existingList.AddRange(valuesList);
                                dictionary[key] = existingList;
                                Console.WriteLine("Added");
                            }
                        }
                        else Console.WriteLine("Please provide a value for KEY");
                    }
                    else
                    {
                        //If it is a new key value add to the dictionary
                        if (valuesList.Count > 1)
                        {
                            command = valuesList[0].ToString();
                            valuesList.RemoveRange(0, 2);
                        }
                        if (valuesList.Count > 0)
                        {
                            dictionary.Add(key, valuesList);
                            Console.WriteLine("Added");
                        }
                        else
                            Console.WriteLine("Please provide a value for KEY");
                    }
                }

            }
            else
            {
                Console.WriteLine("Please provide a key and value to add to the dictionary");
            }
        }

        /// <summary>
        /// Show all the available keys in the Dictionary
        /// </summary>
        private static void KEYS()
        {
            List<string> keys = dictionary.Keys.ToList();
            if (keys.Count > 0)
            {
                foreach (string key in keys)
                {
                    Console.WriteLine(key);
                }
            }
            else
                Console.WriteLine("No keys to Display");
        }

        /// <summary>
        /// Check if the Input Key Exists
        /// </summary>
        private static void KEYEXISTS()
        {
            string[] inputs = userInput.Split(' ');
            if (dictionary.Keys.Contains(inputs[1]))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
        }

        /// <summary>
        /// Remove all the Values associated with key and return error if the key does not exists.
        /// </summary>
        private static void REMOVEALL()
        {
            List<string> inputs = userInput.Split(' ').ToList();
            if (!string.IsNullOrEmpty(inputs[1]) && dictionary.ContainsKey(inputs[1]))
            {
                dictionary.Remove(inputs[1]);
                Console.WriteLine("Removed");
            }
            else
                Console.WriteLine("Error! Key does not exists.");

        }

        /// <summary>
        /// Removes a value from a key.  If the last value is removed from the key, they key is removed from the dictionary. If the key or value does not exist, displays an error.
        /// </summary>
        private static void REMOVE()
        {
            List<string> inputs = userInput.Split(' ').ToList();
            if (dictionary.ContainsKey(inputs[1]))
            {
                List<string> values = dictionary[inputs[1]].ToList();
                int indexOfItemToRemove = values.IndexOf(inputs[2]);
                string ValueToRemove = values[indexOfItemToRemove];

                if (values.Count > 1)
                {
                    dictionary[inputs[1]].Remove(ValueToRemove);

                }
                //dictionary[inputs[1]].RemoveAt(values.Count - 1);                
                else
                    dictionary.Remove(inputs[1]);
                Console.WriteLine("REMOVED");
            }
            else
            {
                Console.WriteLine("Error: Key does not exists");
            }

        }

        /// <summary>
        /// Removes all keys and all values from the dictionary.
        /// </summary>
        private static void CLEAR()
        {
            if (dictionary.Count > 0)
            {
                dictionary = new Dictionary<string, List<string>>();
                Console.WriteLine("Cleared");
            }
            else
                Console.WriteLine("Sorry! No key/value pairs exits to clear!");
        }

        /// <summary>
        /// Returns whether a value exists within a key.  Returns false if the key does not exist.
        /// </summary>
        private static void VALUEEXISTS()
        {
            List<string> inputs = userInput.Split(' ').ToList();
            if (inputs.Count > 2)
            {
                if (dictionary.ContainsKey(inputs[1]))
                {
                    if (dictionary[inputs[1]].Contains(inputs[2]))
                        Console.WriteLine("true");
                    else
                        Console.WriteLine("false");
                }
                else
                {
                    Console.WriteLine("Key not found");
                }
            }
            else
                Console.WriteLine("Pleasse provide a command-name  followed space and key followed by space and value. ");
        }

        /// <summary>
        /// Returns the collection of strings for the given key.  Return order is not guaranteed.  Returns an error if the key does not exists.
        /// </summary>
        private static void MEMBERS()
        {
            string[] inputs = userInput.Split(' ');
            if (dictionary.ContainsKey(inputs[1]))
            {
                List<string> values = dictionary[inputs[1]];
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i].Length > 0)
                    {
                        Console.WriteLine(i + 1 + ")" + values[i]);
                    }
                }
            }
            else
                Console.WriteLine("key not found");
        }
    }
}
