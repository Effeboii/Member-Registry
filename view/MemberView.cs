using System;

namespace View
{
  class MemberView
  {
    public void ManageMembers()
    {
      Console.Clear();
      Console.WriteLine("=========================");
      Console.WriteLine("Manage members");
      Console.WriteLine("=========================");
      Console.WriteLine("What can I help you with today?");
      Console.WriteLine("1. Register a new member");
      Console.WriteLine("2. Edit member");
      Console.WriteLine("3. Delete member");
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