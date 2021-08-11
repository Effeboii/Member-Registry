using System;

namespace View
{
  class MenuView
  {
    public void Start()
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
    }
  }
}