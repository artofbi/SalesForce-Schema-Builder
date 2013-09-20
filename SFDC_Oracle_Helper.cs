using System;
using basicSample_cs_p.apex;
using basicSample_cs_p.apexMetadata;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace basicSample_cs_p
{
    class DBHelper
    {

        public static string TranslateSFDCDataTypeToOracle10gDataType(fieldType SFDCType
            , int length, int precision, int scale)
        {
            string tmpReturn = "";
            string tmpDType = "VARCHAR2";
            string tmpPrecision = "";
            string tmpScale = "";


            switch (SFDCType)
            {
                case fieldType.id:
                    break;

                case fieldType.base64:
                    tmpDType = "BLOB";
                    break;

                // NOTE:
                // Let's actually display FALSE/TRUE as it comes from SFDC
                case fieldType.boolean:
                    tmpDType = "VARCHAR2";
                    length = 5;
                    break;

                case fieldType.currency:
                case fieldType.@double:
                case fieldType.percent:
                    tmpDType = "DECIMAL";
                    tmpPrecision = precision.ToString();
                    tmpScale = scale.ToString();
                    break;

                case fieldType.date:
                    tmpDType = "DATE";     //should be DT_DBDATE
                    break;

                case fieldType.datetime:
                    tmpDType = "TIMESTAMP";
                    break;

                case fieldType.@int:
                    tmpDType = "INT";
                    break;

                //    case fieldType.multipicklist:
                //    case fieldType.picklist:
                //        tmpDType = DataType.DT_WSTR;
                //        break;

            };



            // Modify some data types based on pure logic and constraints
            if (tmpDType == "VARCHAR2" || tmpDType=="LONG")
            {
                if (length > 4000)
                {
                    tmpDType = "LONG";
                    length = 0;
                }
            }

            if (tmpDType == "DECIMAL" || tmpDType == "VARCHAR2")
            {
                tmpReturn = tmpDType + "(";
                if (tmpDType == "DECIMAL")
                    tmpReturn += precision + "," + scale;
                else if (tmpDType == "VARCHAR2")
                    tmpReturn += length;
                tmpReturn += ")";
            }
            else
                tmpReturn = tmpDType;

            return tmpReturn;
        }
    }




    class TextFileWriter
    {
        public static void WriteToLog(string theText)
        {
            // create a writer and open the file
            TextWriter tw = new StreamWriter("Oracle_SFDC_Scheme.sql");

            // write a line of text to the file
            tw.WriteLine(theText);

            // close the stream
            tw.Close();
        }
    }
}
