using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido_Main.Fido_Support.ErrorHandling;
using Fido_Main.Fido_Support.FidoDB;

namespace Fido_Main.Fido_Support.Objects.Config
{

    public static class Dictionary_Config
    {

    private static Dictionary<string, string> _dict = new Dictionary<string, string>();    
    
    internal static void LoadConfigFromDb(string table)
    {
      var fidoSQLite = new SqLiteDB();
      _dict = fidoSQLite.GetDataTable("select key, value from " + table).AsEnumerable().ToDictionary<DataRow, string, string>(row => row.Field<string>(0), row => row.Field<string>(1));
    }

    public static string GetAsString(string name, string dft)
    {
      return _dict.ContainsKey(name) ? _dict[name] : dft;
    }

    public static int GetAsInt(string name, int dft)
    {
      return _dict.ContainsKey(name) ? int.Parse(_dict[name]) : dft;
    }

    public static double GetAsDouble(string name, double dft)
    {
      return _dict.ContainsKey(name) ? double.Parse(_dict[name]) : dft;
    }

    public static bool GetAsBool(string name, bool dft)
    {
      return _dict.ContainsKey(name) ? bool.Parse(_dict[name]) : dft;
    }
    }
}
