using System;

namespace View
{
  class MenuView
  {
    public void Start()
    {
      try
      {
        while (true)
        {
          Console.Clear();
          Console.WriteLine("================================================");
          Console.WriteLine("Welcome to \"The Jolly Pirate Management System\".");
          Console.WriteLine("================================================");
          Console.WriteLine("What can I help you with today?");
          Console.WriteLine("1: Manage members");
          Console.WriteLine("2: Manage boats");
          Console.WriteLine("3: List member information");
          Console.WriteLine("4: Exit");

          int menuChoice = Convert.ToInt32(Console.ReadLine());

          switch (menuChoice)
          {
            case 1:
              Console.WriteLine("Option 1");
              break;
            case 2:
              Console.WriteLine("Option 2");
              break;
            case 3:
              Console.WriteLine("Option 3");
              break;
            case 4:
              Console.WriteLine("Thank you for choosing \"The Jolly Pirate Boat Club\".");
              Console.WriteLine("Console terminates...");
              Environment.Exit(0);
              break;
            default:
              break;
          }
        }
      }
      catch (Exception e)
      {               
        Console.WriteLine("ERROR: " + e);
        Start();
      }
    }
  }
}