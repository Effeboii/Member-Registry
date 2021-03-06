using System;
using System.Collections.Generic;
using Model;

namespace View
{
  public class MemberView
  {
    DatabaseModel databaseModel = new DatabaseModel();

    public void ManageMembers()
    {
      Console.Clear();
      Console.WriteLine("================================================");
      Console.WriteLine("              -- Manage members --              ");
      Console.WriteLine("================================================");
      Console.WriteLine("Please select an option");
      Console.WriteLine("1. Register a new member");
      Console.WriteLine("2. Edit member");
      Console.WriteLine("3. Delete member");
      Console.WriteLine("4. Return to main menu");

      int menuChoice = Convert.ToInt32(Console.ReadLine());

      switch (menuChoice)
      {
        case 1:
          RegisterMember();
          break;
        case 2:
          EditMember();
          break;
        case 3:
          DeleteMember();
          break;
        default:
          break;
      }
    }

    public void ListMemberInfo() 
    {
      Console.Clear();
      Console.WriteLine("================================================");
      Console.WriteLine("          -- List member information --         ");
      Console.WriteLine("================================================");
      Console.WriteLine("Please select an option");
      Console.WriteLine($"1. List a specific member");
      Console.WriteLine($"2. View {ListType.Compact} list");
      Console.WriteLine($"3. View {ListType.Verbose} list");
      Console.WriteLine("4. Return to main menu");

      int menuChoice = Convert.ToInt32(Console.ReadLine());

      switch (menuChoice)
      {
        case 1:
          Console.Clear();
          DisplayMember();
          Console.WriteLine("================================================");
          Console.WriteLine("Press any button to go back to the main menu..");
          Console.ReadKey(true);
          break;
        case 2:
          Console.Clear();
          Console.WriteLine("================================================");
          Console.WriteLine("   -- Displaying a compact list of members --   ");
          Console.WriteLine("================================================");
          Console.WriteLine(" ");
          DisplayAllMembers(ListType.Compact);
          Console.WriteLine("=================================================");
          Console.WriteLine("Press any button to go back to the main menu..");
          Console.ReadKey(true);
          break;
        case 3:
          Console.Clear();
          Console.WriteLine("================================================");
          Console.WriteLine("   -- Displaying a verbose list of members --   ");
          Console.WriteLine("================================================");
          Console.WriteLine(" ");
          DisplayAllMembers(ListType.Verbose);
          Console.WriteLine("=================================================");
          Console.WriteLine("Press any button to go back to the main menu..");
          Console.ReadKey(true);
          break;
        default:
          break;
      }
    }

    public void DisplayMember()
    {
      int id = EnterMemberID();
      MemberModel member = databaseModel.DisplayMember(id);
      DisplayVerboseListOfMembersWithBoats(member);
    }

    private void DisplayAllMembers(ListType listType)
    {
      List<MemberModel> listOfMembers = databaseModel.DisplayAllMembers();

      foreach (var member in listOfMembers)
      {
        Console.WriteLine("==================--Member--====================\n");

        if (listType == ListType.Compact)
        {
          Console.WriteLine( $"ID: {member.ID}, Name: {member.Name}, SSN: {member.SSN}, Number of boats: {member.Boats.Count}\n");
        } 
        else if (listType == ListType.Verbose)
        {
          DisplayVerboseListOfMembersWithBoats(member);
        }                
      }
    }

    private void DisplayVerboseListOfMembersWithBoats (MemberModel member)
    {
      Console.WriteLine($"ID: {member.ID}\nName: {member.Name}\nSSN: {member.SSN}\n");
      DisplayAllBoatsFromOneMember(member);
    }

    private void DisplayAllBoatsFromOneMember(MemberModel member)
    {
      Console.WriteLine("==================--Boats--======================\n");
    
      foreach (var boat in member.Boats)
      {
        Console.WriteLine($"Boat ID: {boat.ID}, Boat type: {boat.Type}, Boat length: {boat.Length}\n");
      }
    }

    public int EnterMemberID()
    {
      int id = 0;
      try
      {
        do
        {
          Console.WriteLine("Please enter your member identification number");
          Console.WriteLine("------------------------------------------------");
          Console.Write("ID: ");
          id = Convert.ToInt32(Console.ReadLine());
          Console.WriteLine(" ");
          Console.WriteLine("==================--Member--====================\n");
        }
        while (id.ToString().Length < 0);

        return id; 
      }
      catch 
      {
        throw new ErrorWhileEnteringMemberId();
      }
    }

    public void RegisterMember()
    {
      Console.Clear();
      string name;
      string ssn;
      bool continueLoop  = true;
      
      do
      {
        Console.WriteLine("================================================");
        Console.WriteLine("            -- Register new member --           ");
        Console.WriteLine("================================================");
        Console.WriteLine("Please enter your full name");
        Console.WriteLine("Press 'q' to quit");
        Console.WriteLine("------------------------------------------------");
        Console.Write("Name: ");
        name = Console.ReadLine();

        if (name == 'q'.ToString())
        {
          return;
        }
      }
      while (name == "");

      do
      {
        Console.WriteLine("================================================");
        Console.WriteLine("Please enter social security number (format: YYMMDDNNNN)");
        Console.WriteLine("------------------------------------------------");
        Console.Write("SSN: ");
        ssn = Console.ReadLine();

        if (databaseModel.CheckSSN(ssn))
        {
          continueLoop = false;
        }
      } 
      while (continueLoop == true);

      try
      {
        databaseModel.CreateMember(name, ssn);
        Console.WriteLine("================================================");
        Console.WriteLine($"The member \"{name}\" was added successfully");
        Console.WriteLine("Press any button to go back to main menu..");
        Console.ReadKey(true);
      } 
      catch (Exception e) 
      {
        Console.WriteLine(e);
        throw new ErrorWhileCreatingMember();
      }
    }

    public void EditMember()
    {
      Console.Clear();
      Console.WriteLine("================================================");
      Console.WriteLine("                -- Edit member --               ");
      Console.WriteLine("================================================");
      int id = EnterMemberID();

      try
      {
        List<MemberModel> dataFromDatabase = databaseModel.GetDataFromDatabase();

        foreach (var member in dataFromDatabase)
        {
          if (member.ID == id)
          {
            Console.WriteLine($"Current name: {member.Name}");
            string newName = "";

            while (newName.Length <= 0)
            {
              Console.WriteLine("Please enter new name: ");
              Console.Write("Name: ");
              newName = Console.ReadLine();
              Console.WriteLine("------------------------------------------------");

            }

            Console.WriteLine($"Current SSN: {member.SSN}");
            member.Name = newName;
            Console.WriteLine("Please enter new SSN: ");
            Console.Write("SSN: ");
            string newSSN = Console.ReadLine();
            Console.WriteLine("------------------------------------------------");

            while (!databaseModel.CheckSSN(newSSN))
            {
              Console.WriteLine("Enter new SSN: ");
              newSSN = Console.ReadLine();
            }
            member.SSN = newSSN;
          }
        }

        databaseModel.WriteMemberToDatabase(dataFromDatabase);
        Console.WriteLine("================================================");
        Console.WriteLine("Member information was changed");
        Console.WriteLine("Press any button to go back to main menu..");
        Console.ReadKey(true);
      }
      catch
      {
        throw new ErrorWhileEditingMember();
      }
    }

    public void DeleteMember()
    {
      Console.Clear();
      Console.WriteLine("================================================");
      Console.WriteLine("              -- Delete member --               ");
      Console.WriteLine("================================================");
      int id = EnterMemberID();
      MemberModel member = null;

      try
      {
        member = databaseModel.SearchForMemberInDb(id);
      }
      catch
      {
        throw new Exception("Error while deleting member");
      }
      
      Console.WriteLine("------------------------------------------------");
      Console.WriteLine("Member found");
      Console.WriteLine($"ID: {member.ID}");
      Console.WriteLine($"Name: {member.Name}");
      Console.WriteLine($"SSN: {member.SSN}");
      Console.WriteLine(" ");
      Console.WriteLine("Do you want to delete this member?");
      Console.WriteLine("1. Yes");
      Console.WriteLine("2. No");

      switch (Console.ReadLine())
      {
        case "1":
          databaseModel.DeleteMember(member);
          Console.WriteLine("================================================");
          Console.WriteLine("Member was deleted from the system");
          Console.WriteLine("Press any button to go back to main menu..");
          Console.ReadKey(true);
            break;
        default:
          break;
      }
    }
  }
}