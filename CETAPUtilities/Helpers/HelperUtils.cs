using System;
using System.Linq;

namespace CETAPUtilities.Helper
{
    public static class HelperUtils
    {
        public static string GetFacultyName(string faculty)
        {
            if (!string.IsNullOrWhiteSpace(faculty)) faculty = faculty.Trim();
            string mf = "";
            switch (faculty)
            {
                case "A":
                    mf = "Allied Healthcare/Nursing";
                    break;

                case "C":
                    mf = "Business/Commerce/Management";
                    break;

                case "B":
                    mf = "Art/Design";
                    break;

                case "D":
                    mf = "Education";
                    break;

                case "E":
                    mf = "Engineering/Built Environment";
                    break;

                case "G":
                    mf = "Hospitality/Tourism";
                    break;

                case "H":
                    mf = "Humanities";
                    break;

                case "I":
                    mf = "Information & Communication Technology";
                    break;

                case "J":
                    mf = "Law";
                    break;

                case "K":
                    mf = "Science/Mathematics";
                    break;

                case "L":
                    mf = "Other";
                    break;

                case "Y":
                    mf = "Health Science";
                    break;

                default:
                    break;

            }
            return mf;
        }
        public static bool IsNumeric(string s)
        {
            return s.All(char.IsDigit);
        }
        public static string ToSAID(long? SAID)
        {
            string myID = "";
            long myvalue;
            if (SAID.HasValue == true)
            {
                myvalue = (Int64)SAID;
                myID = myvalue.ToString("D13");
            }

            return myID;
        }
        public static string DOBfromSAID(string SAID)
        {
            if (SAID.Length < 10) return "";
            Int64 mySAID = Convert.ToInt64(SAID); // way to get all 13 digits of ID
            SAID = mySAID.ToString("D13");
            string mydob = SAID.Substring(0, 6);
            if (IsNumeric(mydob))
            {

                string day = SAID.Substring(4, 2);
                string month = SAID.Substring(2, 2);
                string yr = SAID.Substring(0, 2);
                string year = DateTime.Now.Year.ToString();
                int PresentYear2digits = Convert.ToInt32(year.Substring(2, 2));
                if (PresentYear2digits > Convert.ToInt32(yr))
                {
                    yr = "20" + yr;
                }
                else
                {
                    yr = "19" + yr;
                }
                return (day + "/" + month + "/" + yr);
            }
            else
            {
                return null;
            }
        }
    }
}
