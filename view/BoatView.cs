using System;
using System.Collections.Generic;
using Model;

namespace View
{
  public class BoatView
  {
    BoatModel boatModel = new BoatModel();
    DatabaseModel databaseModel = new DatabaseModel();

    public void ManageBoats()
    {
      Console.Clear();
      Console.WriteLine("================================================");
      Console.WriteLine("               -- Manage boats --               ");
      Console.WriteLine("================================================");
      Console.WriteLine("What can I help you with today?");
      Console.WriteLine("1. Register new boat");
      Console.WriteLine("2. Edit boat");
      Console.WriteLine("3. Delete boat");
      Console.WriteLine("4. Return to main menu");

      int menuChoice = Convert.ToInt32(Console.ReadLine());

      switch (menuChoice)
      {
        case 1:
          Console.Clear();
          Console.WriteLine("================================================");
          Console.WriteLine("              -- Register boat --               ");
          Console.WriteLine("================================================");
          RegisterBoat();
          break;
        case 2:
          Console.Clear();
          Console.WriteLine("================================================");
          Console.WriteLine("                -- Edit boat --                 ");
          Console.WriteLine("================================================");
          EditBoat();
          break;
        case 3:
          Console.Clear();
          Console.WriteLine("================================================");
          Console.WriteLine("               -- Delete boat --                ");
          Console.WriteLine("================================================");
          DeleteBoat();
          break;
        default:
          break;
      }
    }

    public MemberModel CheckIfUserExistWithId()
    {
      int memberID;
      try
      {
        do
        {
          Console.WriteLine("Please enter the member identification number");
          Console.WriteLine("-------------------------------");
          Console.Write("ID: ");
          memberID = Int32.Parse(Console.ReadLine());
        }
        while (!databaseModel.CheckIfUserExistWithId(memberID));

        return databaseModel.SearchForMemberInDb(memberID);   
      }
      catch 
      {
        throw new Exception("Member does not exists");
      }
    }

    public BoatModel CheckIfBoatExist(int memberID)
    {
      MemberModel memberFromDb = databaseModel.DisplayMember(memberID);
      Console.WriteLine("=================================================");
      Console.WriteLine($"Displaying the boats registered to {memberFromDb.Name}");
      Console.WriteLine("=================================================");

      foreach (BoatModel boat in memberFromDb.Boats)
      {
        Console.WriteLine($"ID: {boat.ID}, Boat type: {boat.Type}, Boat length: {boat.Length}m");
      }

      do
      {
        Console.WriteLine("=================================================");
        Console.WriteLine("Enter the ID of the specific boat");
        try
        {
          boatModel.ID = Int32.Parse(Console.ReadLine());   
        }
        catch (System.Exception)
        {
          throw new ErrorWhileSearchingForBoat();
        }

      }
      while (!databaseModel.CheckIfBoatExistWithId(memberFromDb, boatModel));

      return boatModel;
    }

    public int CheckBoatLength()
    {
      int length = 0;
      try
      {
        do
        {
          Console.WriteLine("===============================");
          Console.WriteLine("Please enter the length of the boat in meter (max 50m)");
          Console.WriteLine("-------------------------------");
          length = Convert.ToInt32(Console.ReadLine());
          
          if (length > 50)
          {
            length = 0;
          }
        }
        while (length < 1);

        return length;
      }
      catch
      {
        Console.WriteLine("Boat length must contain a number! Press any key to enter a new number");
        Console.ReadKey(true);
        CheckBoatLength();
        return 0;
      }
    }

    private BoatType SelectBoatType()
    {
      BoatType boatType = BoatType.Sailboat;
      bool continueLoop = true;
      int menuChoice;

      do
      {
        Console.WriteLine("===============================");
        Console.WriteLine("Select the specific boat type");
        Console.WriteLine("-------------------------------");
        Console.WriteLine($"1. {BoatType.Sailboat}");
        Console.WriteLine($"2. {BoatType.Motorsailer}");
        Console.WriteLine($"3. {BoatType.Kayak}");
        Console.WriteLine($"4. {BoatType.Other}");

        menuChoice = Int32.Parse(Console.ReadLine());

        if (menuChoice == 1)
        {
          boatType = BoatType.Sailboat;
          continueLoop = false;
        }
        else if (menuChoice == 2)
        {
          boatType = BoatType.Motorsailer;
          continueLoop = false;
        }
        else if (menuChoice == 3)
        {
          boatType = BoatType.Kayak;
          continueLoop = false;
        }
        else if (menuChoice == 4)
        {
          boatType = BoatType.Other;
          continueLoop = false;
        }
        else
        {
          Console.WriteLine("Not a valid type");
        }
      }
      while (continueLoop);

      return boatType;
    }

    public void RegisterBoat()
    {
      MemberModel member = CheckIfUserExistWithId();
      int menuChoice;
      bool continueLoop = true;

      if (member.ID > 0)
      {
        do
        {
          Console.WriteLine("===============================");
          Console.WriteLine("Select the specific boat type");
          Console.WriteLine("-------------------------------");
          Console.WriteLine($"1. {BoatType.Sailboat}");
          Console.WriteLine($"2. {BoatType.Motorsailer}");
          Console.WriteLine($"3. {BoatType.Kayak}");
          Console.WriteLine($"4. {BoatType.Other}");
          Console.WriteLine("5. Return to main menu");

          menuChoice = Int32.Parse(Console.ReadLine());

          if (menuChoice == 1)
          {
            boatModel.Type = BoatType.Sailboat.ToString();
            continueLoop = false;
          }
          else if (menuChoice == 2)
          {
            boatModel.Type = BoatType.Motorsailer.ToString();
            continueLoop = false;
          }
          else if (menuChoice == 3)
          {
            boatModel.Type = BoatType.Kayak.ToString();
            continueLoop = false;
          }
          else if (menuChoice == 4)
          {
            boatModel.Type = BoatType.Other.ToString();
            continueLoop = false;
          }
          else
          {
            Console.WriteLine("Not a valid type");
          }
        }
        while (continueLoop);

        boatModel.Length = CheckBoatLength();

        try
        {
          boatModel.ID = databaseModel.GetBoatWithHighestID();
          databaseModel.CreateBoat(member, boatModel);
          Console.WriteLine("================================================");
          Console.WriteLine("The boat was successfully registered to our system");
          Console.WriteLine("Press any button to go back to the main menu");
          Console.ReadKey(true);
        }
        catch
        {
          throw new ErrorWhileCreatingBoat();
        }
      }
      else
      {
        Console.WriteLine("Member does not exist, press any key to return to main menu");
        Console.ReadKey(true);
      }
    }

    public void EditBoat()
    {
      MemberModel member = CheckIfUserExistWithId();

      if (databaseModel.SearchForMemberInDb(member.ID).Boats.Count > 0)
      {
        boatModel = CheckIfBoatExist(member.ID);
        boatModel.Type = SelectBoatType().ToString();
        boatModel.Length = CheckBoatLength();
        
        try
        {
          databaseModel.EditBoat(member, boatModel);
          Console.WriteLine("Boat information was changed");
          Console.WriteLine("Press any button to go back to main menu");
          Console.ReadKey(true);     
        }
        catch
        {
          throw new ErrorWhileEditBoat();
        }
      }
      else
      {
        Console.WriteLine("Member has no boats");
        Console.ReadKey(true);     
        return;
      }
    }

    public void DeleteBoat()
    {
      Console.Clear();
      MemberModel member = CheckIfUserExistWithId();

      if (databaseModel.SearchForMemberInDb(member.ID).Boats.Count > 0)
      {
        try
        {
          BoatModel boat = CheckIfBoatExist(member.ID);                

          Console.WriteLine("=========================");
          Console.WriteLine($"Do you want to delete boat ID {boat.ID}?");
          Console.WriteLine("1. Yes");
          Console.WriteLine("2. No");

          switch (Console.ReadLine())
          {
            case "1":
              try
              {
                databaseModel.DeleteBoat(member, boat);
                System.Console.WriteLine("Boat was removed");
                System.Console.WriteLine("Press any button to go back to main menu");
                Console.ReadKey(true);
              }
              catch (ErrorWhileDeletingBoat e)
              {
                System.Console.WriteLine(e);
                throw new ErrorWhileDeletingBoat();
              }
              break;
            default:
              break;
          }
        }
        catch
        {
          throw new ErrorWhileSearchingForBoat();
        }
      }
      else
      {
        Console.WriteLine("Member does not own any boats");
      }
    }
  }
}