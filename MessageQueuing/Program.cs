using System;
using System.Messaging;

namespace MessageQueuing
{

    class Program
    {
        static void Main(string[] args)
        {
            string sComputerName = Environment.MachineName; // Current computer name
            string sPrivatePath = @".\Private$\listener"; // Private Queue Path
            string sFullPath = sPrivatePath.Replace(".", sComputerName); // Private path after change

            MessageQueue oMessageQueue = null; // Null queue message
            try
            {
                Console.WriteLine($"--- About to check the existance of message queue {sFullPath} ---");
                Console.WriteLine();
                if (MessageQueue.Exists(sFullPath)) // If exist delete and create a new one
                {
                    Console.WriteLine(" - Old queue has been found! Trying deleting it...");
                    MessageQueue.Delete(sFullPath); // Delete old queue
                    Console.WriteLine(" - Old queue has been deleted successfully!");
                    Console.WriteLine(" - Trying creating a new one after deleting old one...");

                    oMessageQueue = MessageQueue.Create(sFullPath, true); // Create new queue

                    oMessageQueue.Label = " - Test Queue Created Successfully! ( After deleting the old one )";
                    Console.WriteLine(" - Queue has been created successfully please check!");
             

                }
                else if (!MessageQueue.Exists(sFullPath)) // If Doesn't exist
                {

                    Console.WriteLine(" - No old queue found.");
                    Console.WriteLine(" - Trying creating a new queue...");
                    oMessageQueue = MessageQueue.Create(sFullPath); // Create new queue
                    
                    oMessageQueue.Label = " - Test Queue Created Successfully! ( New )";
                    Console.WriteLine(" - Queue has been created successfully please check!");
                    
                }

                Console.WriteLine(" - About to set usage rights");
                oMessageQueue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl, AccessControlEntryType.Allow);
                Console.WriteLine(" - Usage rights set");

                Console.WriteLine(" - Deleting message queue");
                MessageQueue.Delete(sFullPath);
                Console.WriteLine(" - Queue deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
         

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}