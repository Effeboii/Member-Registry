using System;
using System.Collections.Generic;

namespace Model
{
  public class BoatModel
  {
    private int _id;
    private string _type;
    private int _length;

    public int ID
    {
      get { return _id; }
      set
      {
        if (value <= 0)
        {
          throw new ArgumentOutOfRangeException("ID cannot be less than zero");
        }
        _id = value;
      }
    }

    public string Type
    {
      get { return _type; }
      set
      {
        if (value.Length < 0)
        {
          throw new ArgumentOutOfRangeException("Type must contain at least one letter");
        }
        _type = value; 
      }
    }

    public int Length
    {
      get { return _length; }
      set
      {
        if (value < 0)
        {
          throw new ArgumentOutOfRangeException("Length cannot be less than zero");
        }
        _length = value;
      }
    }
  }
}