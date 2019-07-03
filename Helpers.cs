using System.Collections.Generic;
using System;
using NexusReportingApi.Models;

namespace NexusReportingApi.API {

    public class Helpers{

        private static Random _rand = new Random();

        private static string GetRandom(IList<string> items){
           
            return items[_rand.Next(items.Count)];
        }
    internal static string MakeCustomerEmail(string customerName){
           
            return $"contact@{customerName.ToLower()}.com";
        }
         internal static string MakeCustomerState(){
            return GetRandom(usStates);
         }

        internal static string MakeCustomerName(){
            var prefix = GetRandom(bizPrefix);
            var suffix = GetRandom(bixSuffix);
            return prefix+suffix;
        }

        internal static decimal GetRandomOrderTotal(){
            return _rand.Next(100,5000);
        }

        internal static DateTime GetRandomOrderPlaced(){
            var end = DateTime.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes),0);
            return start + newSpan;

        }

        

        internal static DateTime? GetRandomOrderComplete(DateTime placed){

            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - placed;

            if(timePassed < minLeadTime){
                return null;
            }

            return placed.AddDays(_rand.Next(7,14));
        }

      

        private static readonly List<string> usStates = new List<string>()
        {
            "AL",
            "AK",
            "AZ",
            "AR",
            "CA",
            "CO",
            "CT",
            "DE",
            "DC",
            "FL",
            "GA",
            "HI",
            "ID",
            "IL",
            "IN",
            "IA",
            "KS",
            "KY",
            "LA",
            "ME",
            "MD",
            "MA",
            "MI",
            "MN",
            "MS",
            "MO",
            "MT",
            "NE",
            "NV",
            "NH",
            "NJ",
            "NM",
            "NY",
            "NC",
            "ND",
            "OH",
            "OK",
            "OR",
            "PA",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VT",
            "VA",
            "WA",
            "WV",
            "WI",
            "WY"
        };

    private static readonly List<string> bizPrefix = new List<string>(){
            "ABC,",
            "XYZ",
            "MaintSt",
            "Enterprise",
            "Ready",
            "Quick",
            "Budget",
            "Peak",
            "Magic",
            "Family",
            "Confort",
            };

        private static readonly List<string> bixSuffix = new List<string>(){
            "Corporations,",
            "Co",
            "Logistics",
            "Transit",
            "Bakery",
            "Goods",
            "Foods",
            "Cleaners",
            "Hotels",
            "Planners",
            "Automotive",
            "Books",
            };
        
    }

}