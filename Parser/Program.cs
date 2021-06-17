/*
* Team "Whatever"
*
* Team Members:
*
* Marcus Blaisdell
* Kimi Phan
* Hannah Bogen
* Joseph Crissey
*
* Milestone 1: Parser program 
*
* Code Author: Marcus Blaisdell
*
* This program was written in C#, using Visual Studio Code on Mac
* Modified from previous version written in Visual Studio 2015 on PC
* 
* It makes use of the Newtonsoft library for JSON handling 
*
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace JSONTest
{
    class Program
    {
            // global variable 'whereTo', 
            // if 0, write to screen,
            // if 1, write to file:

        public static int whereTo = 1;
        public static string insertStatement = "";
        public static string insertValues = "";

        static void printLineCheckIns (string testLine, System.IO.StreamWriter outFile)
        {
                // test for an empty row:
                // if row is empty, return to caller 

            if (testLine.Length == 0)
            {
                return;
            } // end return to caller if row is blank

            var jObj = JObject.Parse(testLine);
            string theBusinessId = "";

                // loop through each key-value-pair:

            //insertStatement = "INSERT INTO CheckIn (business_id_num, day, time_checkin, check_ins_num) VALUES (  ";
            //insertValues = "";

            // Find business_id:

            foreach (var kvp in jObj.Cast<KeyValuePair<string, JToken>>().ToList())
            {
                if (kvp.Key == "business_id")
                {
                    theBusinessId += kvp.Value;
                } // end capture user id
            } // end iterate through entire JSON object to get user_id 

                // now, iterate through again to be able to use business_id as primary key for ...

            foreach (var kvp in jObj.Cast<KeyValuePair<string, JToken>>().ToList())
            {
                if (kvp.Key == "time")
                {
                    printDays (theBusinessId, kvp.Value, outFile);
                }

                
                
            } // end foreach loop

                // Add a newline:

            // remove trailing commas and spaces, append values to statement:
            /*
            insertValues = insertValues.Remove (insertValues.Length - 2);
            insertStatement = insertStatement.Remove (insertStatement.Length - 2);
            //insertStatement += ") VALUES (";
            insertStatement += insertValues;
            insertStatement += ");\n";
            
            if (whereTo == 0)
            {
                //System.Console.Write(");\n");
                System.Console.Write (insertStatement);

            } // end if screen
            else
            {
                //outFile.Write(");\r\n");
                outFile.Write (insertStatement);

            } // end else, file
            */
            
            
        } // end printLine function 


        static void printDays (string theBusinessId, JToken theObject, System.IO.StreamWriter outFile)
        {
            // declare new strings to build the insert Statesments:

            string insertStatement = "";
            string insertValues = "";
            string theDay = "";

            foreach (var kvp in theObject.Cast<KeyValuePair<string, JToken>>().ToList())
            {
                theDay = "";
                if ((kvp.Value.GetType()) == typeof(Newtonsoft.Json.Linq.JObject))
                {
                    theDay += kvp.Key;
                    printCheckIns (theBusinessId, theDay, kvp.Value, outFile);
                } // end get the day 
                

            } // end for each token 
            
            
        } // end printDays function

        static void printCheckIns (string theBusinessId, string theDay, JToken theObject, System.IO.StreamWriter outFile)
        {
            foreach (var kvp in theObject.Cast<KeyValuePair<string, JToken>>().ToList())
            {
                outFile.Write ("INSERT INTO CheckIn VALUES ('" + theBusinessId + "', '" + theDay + "', '" + kvp.Key + "', '" + kvp.Value + "');\n");
            } // end iterate through all checkins:

        } // end function printCheckins

        static void printElite (string theUserId, JToken theObject, System.IO.StreamWriter outFileElite)
        {
            // declare new strings to build the insert Statesments:

            string insertStatement = "";
            string insertValues = "";


            //foreach (var kvp in theObject.Cast<KeyValuePair<string, JToken>>().ToList())
            foreach (var kvp in theObject)
            {
                // If the value is another JObject, it's key is the parent for all of it's 
                // sub-attributes:
                
                outFileElite.Write ("INSERT INTO Elite VALUES ('" + theUserId +  "', '" + kvp + "');\n");

            } // end foreach loop
            
            
        } // end printElite function

        /////
        static void printHours (string theBusinessId, string theKey, JToken theObject, System.IO.StreamWriter outFileHours)
        {
            // declare new strings to build the insert Statesments:

            string insertStatement = "";
            string insertValues = "";


            foreach (var kvp in theObject.Cast<KeyValuePair<string, JToken>>().ToList())
            {
                // If the value is another JObject, it's key is the parent for all of it's 
                // sub-attributes:

                
                /*
                if ((kvp.Value.GetType()) == typeof(Newtonsoft.Json.Linq.JObject))
                {
                    outFileHours.Write ("INSERT INTO Hours ('" + theBusinessId + "', '" + kvp.Key + "');\n");
                    printHours (theBusinessId, kvp.Key, kvp.Value, outFileHours);

                } // end recurse on objects
                else
                {
                    outFileHours.Write ("INSERT INTO Hours ('" + theBusinessId + "', '" + kvp.Key + "', '" + kvp.Value + "');\n");
                    
                    
                } // end, else, print the key and value as a tuple
                */
                outFileHours.Write ("INSERT INTO Hours VALUES ('" + theBusinessId + "', '" + kvp.Key + "', '" + kvp.Value + "');\n");
                
                

            } // end foreach loop
            
            
        } // end printHours function

        /////

        static int countArray (JToken theArray)
        {
            int count = 0;

            foreach (var kvp in theArray)
            {
                count++;
            }

            return count;
        }

        static void printArray(JToken theObject, System.IO.StreamWriter outFile)
        {
            foreach (var kvp in theObject)
            {
                if (whereTo == 0)
                {
                    System.Console.Write("(" + kvp + ") ");

                } // end if screen
                else
                {
                    outFile.Write("(" + kvp + ") ");

                } // end else, file
                
            } // end foreach loop

            if (whereTo == 0)
            {
                System.Console.Write("]\n");

            } // end if screen
            else
            {
                outFile.Write("]\r\n");


            } // end else, file
            
        } // end printArray function



        static void Main(string[] args)
        {
            int i = 0;

                // Store all the files we need to parse in an array:
                // Use small files for testing, 
                // Use full size files for go-time

            string[] theFile = { "yelp_checkin.JSON" };
            //string[] theFile = { "yelp_business_small.JSON", "yelp_checkin_small.JSON", "yelp_review_small.JSON", "yelp_user_small.JSON" };
            //string[] theFile = { "yelp_business.JSON", "yelp_checkin.JSON", "yelp_review.JSON", "yelp_user.JSON" };

                // set the path:

            //string thePath = "\\Users\\Marcus\\Documents\\Classes\\2019\\Spring\\Databases\\Project\\Mileston_1\\Yelp-CptS451-2019\\";

            for (i = 0; i < theFile.Length; i++)
            {
                    // Open the file for reading:

                System.IO.StreamReader
                    file = new
                    //System.IO.StreamReader(@thePath + theFile[i]);
                    System.IO.StreamReader(@theFile[i]);

                    // Open file for writing:
                    // replace "JSON" by "txt"

                //string newFile = Regex.Replace(theFile[i], @"JSON", "txt");
                string newFile = "insertCheckins.sql";

                System.IO.StreamWriter
                    outFile = new
                    //System.IO.StreamWriter(@thePath + newFile);
                    System.IO.StreamWriter(@newFile);

                System.IO.StreamWriter
                    outFileElite = new
                    System.IO.StreamWriter(@"Elite.sql");

                System.IO.StreamWriter
                    outFileFriends = new
                    System.IO.StreamWriter(@"Friends.sql");

                System.IO.StreamWriter
                    outFileHours = new
                    System.IO.StreamWriter(@"Hours.sql");

                
                    // read the first line:

                var testLine = @file.ReadLine();

                    // print the field names:

                //printKeys(testLine, outFile);

                    // Parse as a JObject:

                var jObj = JObject.Parse(testLine);
                
                printLineCheckIns (testLine, outFile);


                    // Now loop to get the remaining lines:
                
                
                while ((testLine = @file.ReadLine()) != null)
                {
                    printLineCheckIns (testLine, outFile);

                } // end while loop 
                 

                // Add a newline:

                if (whereTo == 0)
                {
                    System.Console.Write("\n");

                } // end if screen
                else
                {
                    outFile.Write("\r\n");

                } // end else, file
                
                file.Close ();
                outFile.Close ();
                outFileElite.Close ();
                outFileFriends.Close ();
                    
            } // end iterate through all files

            // Wait for input from user before closing window:

            System.Console.WriteLine ("*** Complete ***");
            System.Console.ReadLine ();

        } // end Main 

    } // end Program 

} // end namespace JSONTest
