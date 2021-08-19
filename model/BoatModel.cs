using System;
using System.Collections.Generic;

namespace Model
{
  public class BoatModel
  {
    DatabaseModel database = new DatabaseModel();
    private int _boatID;
    private BoatType _boatType;
    private int _boatLength;

    public int ID
    {
      get { return _boatID; }
      set
      {
        if (value <= 0)
        {
          throw new Exception("Length cannot be less than zero");
        }
        _boatID = value;
      }
    }

    public BoatType Type
    {
      get { return _boatType; }
      set { _boatType = value; }
    }

    public int Length
    {
      get { return _boatLength; }
      set
      {
        if (value < 0)
        {
          throw new Exception("Length cannot be less than zero");
        }
        _boatLength = value;
      }
    }

    public int CheckBoatHighestIdNumber() 
    {
      int highestNumber = 0;
      foreach (var member in database.ReadFromDatabase())
      {
        foreach (var boat in member.boatList)
        {
          if (highestNumber < boat.ID)
          {
            highestNumber = boat.ID;
          } 
        }
      }
      return highestNumber + 1;
    }

    public bool CheckIfBoatExistWithID(int memberID, int boatID)
    {            
      List<MemberModel> dataFromDatabase = database.ReadFromDatabase();
      bool boatExist = false;

      foreach (var member in dataFromDatabase)
      {
        if (member.MemberID == memberID)
        {
          foreach (var boat in member.boatList)
          {
            if (boat.ID == boatID)
            {
              boatExist = true;
            }
          }
        }
      }
      return boatExist;
    }

    public int SearchUniqueBoat(int memberID, int boatID)
    {            
      List<MemberModel> dataFromDatabase = database.ReadFromDatabase();
      int boatName = 0;

      foreach (var member in dataFromDatabase)
      {
        if (member.MemberID == memberID)
        {
          foreach (var boat in member.boatList)
          {
            if (boat.ID == boatID)
            {
              boatName = boat.ID;
            }
          }
        }
      }
      return boatName;
    }

    public void RegisterBoat(int memberId, BoatType boatType, int length)
    {
      List<MemberModel> dataFromDatabase = database.ReadFromDatabase();
      foreach (var member in dataFromDatabase)
      {
        if (member.MemberID == memberId) 
        {
          member.boatList.Add(new BoatModel 
          {
            ID = CheckBoatHighestIdNumber(), Type = boatType, Length = length
          });
        }
      }
      database.WriteToDatabase(dataFromDatabase);
    }

    public void EditBoat(int memberID, int boatID, BoatType boatType, int boatLength)
    {
      List<MemberModel> dataFromDatabase = database.ReadFromDatabase();

      foreach (var member in dataFromDatabase)
      {
        if (member.MemberID == memberID)
        {
          foreach (var boat in member.boatList)
          {
            if(boat.ID == boatID)
            {
              boat.Type = boatType;
              boat.Length = boatLength; 
            }
          }
        }
      }
      database.WriteToDatabase(dataFromDatabase);
    }

    public void deleteBoat(int memberID, int boatID)
    {
      List<MemberModel> dataFromDatabase = database.ReadFromDatabase();
      BoatModel boatToRemove = new BoatModel();

      foreach (var member in dataFromDatabase)
      {
        if (member.MemberID == memberID)
        {
          foreach (var boat in member.boatList)
          {
            if (boat.ID == boatID)
            {
              boatToRemove = boat;
            }
          }

          member.boatList.Remove(boatToRemove);
        }
      }
      database.WriteToDatabase(dataFromDatabase);
    }
  }
}
