using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ExamAutoSel
{
    public class CMakeUserPaperData
    {
        private string Model = "ABCDEFGHIJKLMN";
        private Random myRand = new Random();
        private string mXmlData = string.Empty;
        public CMakeUserPaperData(string xmlData)
        {
            mXmlData = xmlData;
        }
        public string GetUserPostData()
        {
            try
            {
                XElement root = XElement.Parse(mXmlData);
                XElement userTestPaperData = new XElement("userTestPaperData");
                foreach (XElement testPaperOptions in root.Elements("testPaperOptions"))
                {
                    XElement[] ArrayTestPaperQuestion = testPaperOptions.Elements("testPaperQuestion").ToArray();
                    for (int i = 0; i < ArrayTestPaperQuestion.Length;i++ )
                    {
                        int p1 = myRand.Next(ArrayTestPaperQuestion.Length);
                        int p2 = myRand.Next(ArrayTestPaperQuestion.Length);
                        XElement tempElement = ArrayTestPaperQuestion[p1];
                        ArrayTestPaperQuestion[p1] = ArrayTestPaperQuestion[p2];
                        ArrayTestPaperQuestion[p2] = tempElement;
                    }
                    for (int i = 0; i < ArrayTestPaperQuestion.Length;i++ )
                    {
                        userTestPaperData.Add(QuestionToAnswer(ArrayTestPaperQuestion[i], i.ToString()));
                    }
                    

                }
                //string s = userTestPaperData.ToString(SaveOptions.DisableFormatting);
                //s = Regex.Replace(s, @"\s", string.Empty);

                //return s;
                return userTestPaperData.ToString(SaveOptions.DisableFormatting);
                //return Regex.Replace(userTestPaperData.ToString(SaveOptions.DisableFormatting),@"\s",string.Empty);
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        private XElement QuestionToAnswer(XElement quesionElement, string sort)
        {
            try
            {
                string optionFlag=string.Empty;
                if (!string.IsNullOrEmpty((string)quesionElement.Attribute("options")))
                {
                    int optionCount = ((string)quesionElement.Attribute("options")).Split(new string[] { ";" }, StringSplitOptions.None).Length;
                    optionFlag = Model.Substring(0, optionCount);
                    char[] ca = optionFlag.ToCharArray();
                    for (int i = 0; i < optionCount; i++)
                    {
                        int pos = myRand.Next(optionCount);
                        char tempChar = ca[pos];
                        ca[pos] = ca[0];
                        ca[0] = tempChar;
                    }
                    optionFlag = new string(ca);
                    
                }

                return new XElement("result",
                    new XAttribute("userQuestionSortFlag", sort), new XAttribute("userQuestionOptionsSortFlag", optionFlag), new XAttribute("testPaperQuestionId", (string)quesionElement.Attribute("id")), new XAttribute("userAnswer", (string)quesionElement.Attribute("answer")), new XAttribute("userScore", "0"), new XAttribute("userStatus", "0")
                    );

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
