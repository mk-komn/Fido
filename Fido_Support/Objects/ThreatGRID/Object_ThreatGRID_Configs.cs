﻿/*
 *
 *  Copyright 2015 Netflix, Inc.
 *
 *     Licensed under the Apache License, Version 2.0 (the "License");
 *     you may not use this file except in compliance with the License.
 *     You may obtain a copy of the License at
 *
 *         http://www.apache.org/licenses/LICENSE-2.0
 *
 *     Unless required by applicable law or agreed to in writing, software
 *     distributed under the License is distributed on an "AS IS" BASIS,
 *     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *     See the License for the specific language governing permissions and
 *     limitations under the License.
 *
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Fido_Main.Fido_Support.ErrorHandling;
using Fido_Main.Fido_Support.FidoDB;
using Fido_Main.Fido_Support.Objects.Config;

namespace Fido_Main.Fido_Support.Objects.ThreatGRID
{
  class Object_ThreatGRID_Configs : Dictionary_Config
  {
   
    internal static Object_ThreatGRID_IP_ConfigClass.ParseConfigs GetThreatGridConfigs(string detect)
    {
      //todo: move this to the database, assign a variable to 'detect' and replace being using in GEtFidoConfigs
      var query = @"SELECT * from configs_threatfeed_threatgrid WHERE api_call = '" + detect + @"'";
      var fidoTemp = GetThreatGridTable(query);
      var fidoReturn = new Object_ThreatGRID_IP_ConfigClass.ParseConfigs();
      try
      {
        if (fidoTemp != null)
        {
          fidoReturn = Object_ThreatGRID_IP_ConfigClass.FormatParse(fidoTemp);
        }
        if (fidoReturn != null) return fidoReturn;
      }
      catch (Exception e)
      {
        Fido_EventHandler.SendEmail("Fido Error", "Fido Failed: {0} Unable to gather parse configs." + e);
      }
      return null;
    }

    private static DataTable GetThreatGridTable(string query)
    {
      var fidoSQlite = new SqLiteDB();
      var fidoData = new DataTable();
      try
      {
        fidoData = fidoSQlite.GetDataTable(query);
      }
      catch (Exception e)
      {
        Fido_EventHandler.SendEmail("Fido Error", "Fido Failed: {0} Unable to format datatable return." + e);
      }

      return fidoData;
    }
  }
}
