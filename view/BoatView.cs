using System;

namespace View
{
  class BoatView
  {
    public void ManageBoats()
    {
      Console.Clear();
      Console.WriteLine("=========================");
      Console.WriteLine("Manage boats");
      Console.WriteLine("=========================");
      Console.WriteLine("What can I help you with today?");
      Console.WriteLine("1. Register new boat");
      Console.WriteLine("2. Edit boat");
      Console.WriteLine("3. Delete boat");
      Console.WriteLine("4. Return to main menu");

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
        default:
          break;
      }
    }
  }
}