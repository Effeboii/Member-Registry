using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Model
{
  class MemberModel
  {
    DatabaseModel database = new DatabaseModel();
    private int _memberID;
    private string _fullName;
    private string _socialSecurityNumber;

    public int MemberID()
    {
      get { return _memberID; }
      set { _memberID = value; }
    }

      public string FullName
    {
      get { return _fullName; }
      set { _fullName = value; }
    }

    public string SocialSecurityNumber
    {
      get { return _socialSecurityNumber; }
      set { _socialSecurityNumber = value; }
    }
  }
}