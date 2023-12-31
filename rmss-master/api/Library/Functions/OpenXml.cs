﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using DocumentFormat.OpenXml.Packaging;
using System.IO;

namespace Library.Functions
{
    public class OpenXml
    {
        /// <summary>
        /// Ver 1.0 By Jeffrey Lee, 2009-07-29
        /// </summary>
        public class DocxHelper
        {
            /// <summary>
            /// Replace the parser tags in docx document
            /// </summary>
            /// <param name="oxp">OpenXmlPart object</param>
            /// <param name="dct">Dictionary contains parser tags to replace</param>
            //private static void parse(OpenXmlPart oxp, Dictionary<string, string> dct)
            //{
            //    string xmlString = null;
            //    using (StreamReader sr = new StreamReader(oxp.GetStream()))
            //    { xmlString = sr.ReadToEnd(); }
            //    foreach (string key in dct.Keys)
            //        xmlString = xmlString.Replace("[$" + key + "$]", dct[key]);
            //    using (StreamWriter sw = new StreamWriter(oxp.GetStream(FileMode.Create)))
            //    { sw.Write(xmlString); }
            //}

            /// <summary>
            /// Parse template file and replace all parser tags, return the binary content of
            /// new docx file.
            /// </summary>
            /// <param name="templateFile">template file path</param>
            /// <param name="dct">a Dictionary containing parser tags and values</param>
            /// <returns></returns>
            //public static string MakeDocx(string templateFile, string tempFile, Dictionary<string, string> dct)
            //{
            //    //string tempFile = Path.GetTempPath() + ".docx";
            //    File.Copy(templateFile, tempFile);

            //    using (WordprocessingDocument wd = WordprocessingDocument.Open(
            //        tempFile, true))
            //    {
            //        //Replace document body
            //        parse(wd.MainDocumentPart, dct);
            //        foreach (HeaderPart hp in wd.MainDocumentPart.HeaderParts)
            //            parse(hp, dct);
            //        foreach (FooterPart fp in wd.MainDocumentPart.FooterParts)
            //            parse(fp, dct);
            //    }
            //    //byte[] buff = File.ReadAllBytes(tempFile);
            //    //File.Delete(tempFile);
            //    return tempFile;
            //}

        }
    }
}
