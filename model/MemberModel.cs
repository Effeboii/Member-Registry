using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Model
{
  public class MemberModel
  {
    private List<BoatModel> _boatList = new List<BoatModel>();
    private int _id;
    private string _name;
    private string _ssn;

    public int ID
    {
      get { return _id; }
      private set { _id = value; }
    }

    public string Name
    {
      get { return _name; }
      set
      {
        if (value.Length < 0)
        {
          throw new ArgumentException("Member name must contain at least one letter");
        }
          _name = value;
      }
    }
        
    public string SSN
    {
      get { return _ssn; }
      set
      {
        if (value.Length != 10)
        {
          throw new ArgumentException("Member SSN must contain ten digits");
        } 
        _ssn = value;
      }
    }

    public List<BoatModel> Boats
    {
      get { return _boatList; }
      set { _boatList = value; }
    }

    public MemberModel (int id, string name, string ssn) {
      ID = id;
      Name = name;
      SSN = ssn;
    }
  }
}