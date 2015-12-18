using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

namespace ExamAutoSel
{
    public class CExamPFLib
    {
        public int Delay { get; set; }

        private string mHostUrl = string.Empty;
        private string mAccount = string.Empty;
        private string mPassword = string.Empty;
        private Random myRand = new Random();

        Encoding mPageEncode = Encoding.UTF8;
        HttpRequest.HttpRequest pfRequest = new HttpRequest.HttpRequest();
        public CExamPFLib(string hostUrl, string pAccount, string pPass)
        {
            this.mHostUrl = hostUrl;
            this.mAccount = pAccount;
            this.mPassword = pPass;
            Delay = 20;
        }
        public string CommitData(bool IsAsc,string pExamID)
        {
            string urlStr = string.Empty;
            string refStr = string.Empty;
            string postData = string.Empty;
            string htmlStr = string.Empty;
            string hrStr = string.Empty;
            string pattern = string.Empty;
            Match pfMath = null;
            try
            {
                urlStr = string.Format(@"http://{0}", mHostUrl);
                refStr = urlStr;
                pfRequest.OpenRequest(urlStr, refStr);

                urlStr = string.Format(@"http://{0}/system/manager/terminalLogin.do", mHostUrl);
                postData = string.Format(@"manager.name={0}&manager.password={1}&systemType=1&actionFormNeedEditFlag=true&x=47&y=19", HttpUtility.UrlEncode(mAccount, mPageEncode)
                    , HttpUtility.UrlEncode(mPassword, mPageEncode));
                pfRequest.OpenRequest(urlStr, refStr, postData);
                htmlStr = pfRequest.HtmlDocument;

                if (!pfRequest.responseUrl.Contains("queryJoinExam"))
                {
                    throw new Exception("login error or not redirect to query Join Exam");
                }
                pattern = @"beginexam\s*\(\s*(?<examid>\d+)\s*,(?<userid>\d+?)\s*,";
                MatchCollection mc = Regex.Matches(htmlStr, pattern, RegexOptions.IgnoreCase);

                string examID = string.Empty;
                string userID = string.Empty;
                int maxExamID = 0;
                if (IsAsc)
                {
                    maxExamID = 0;
                    foreach (Match m in mc)
                    {
                        int ival = int.Parse(m.Groups["examid"].Value);
                        if (ival > maxExamID)
                        {
                            maxExamID = ival;
                            userID = m.Groups["userid"].Value;
                        }
                    }
                }
                else
                {
                    maxExamID = 100000;
                    foreach (Match m in mc)
                    {
                        int ival = int.Parse(m.Groups["examid"].Value);
                        if (ival < maxExamID)
                        {
                            maxExamID = ival;
                            userID = m.Groups["userid"].Value;
                        }
                    }
                }


                examID = maxExamID.ToString();
                if (!string.IsNullOrEmpty(pExamID))
                {
                    examID = pExamID;
                }


                urlStr = string.Format(@"http://{0}/exam/examUser/confimExam.do?examId={1}", mHostUrl, examID);
                pfRequest.OpenRequest(urlStr, refStr);
                htmlStr = pfRequest.HtmlDocument;

                string ExamUserID = string.Empty;
                pattern = @"<input[^>]+?examuserid[^>]+?value\s*=\s*""(?<val>\d*)""";
                pfMath = Regex.Match(htmlStr, pattern, RegexOptions.IgnoreCase);
                if (pfMath.Success)
                {
                    ExamUserID = pfMath.Groups["val"].Value;
                }
                urlStr = string.Format(@"http://{0}/exam/examUser/beginExam.do?exam.id={1}&userId={2}{3}", mHostUrl, examID, userID, string.IsNullOrEmpty(ExamUserID) ? string.Empty : string.Format("&examUserId={0}", ExamUserID));
                pfRequest.OpenRequest(urlStr, refStr);
                htmlStr = pfRequest.HtmlDocument;

                if (htmlStr.Contains("不可在规定的开考时间段之外开始考试"))
                {
                    throw new Exception("请勾选或者取消考试项目顺序选择");
                }


                pattern = @"(?<=')\S+?exam\.do\S+?(?=')";
                pfMath = Regex.Match(htmlStr, pattern, RegexOptions.IgnoreCase);
                if (!pfMath.Success)
                {
                    throw new Exception("jump to exam.do error");
                }
                urlStr = string.Format(@"http://{0}/{1}", mHostUrl, pfMath.Value);
                pfRequest.OpenRequest(urlStr, refStr);
                htmlStr = pfRequest.HtmlDocument;

                Dictionary<string, string> DictParams = new Dictionary<string, string>();
                pattern = @"div\s+id\s*=\s*""(?<id>[\s\S]+?)""\s*>(?<val>[\s\S]+?)<\s*/\s*div";
                mc = Regex.Matches(htmlStr, pattern, RegexOptions.IgnoreCase);
                foreach (Match m in mc)
                {
                    if (!DictParams.ContainsKey(m.Groups["id"].Value))
                    {
                        DictParams.Add(m.Groups["id"].Value, m.Groups["val"].Value);
                    }
                }

                urlStr = string.Format(@"http://{7}/exam/userTestPaper/createTestPaperXml.do?randomNUMSW={0}&testPaperId={1}&userTestPaperId={2}&testPaperMode={3}&testPaperUserType={4}&series={5}&testPaperOwnerId={6}",
                    myRand.Next(100),
                    DictParams["testPaperId"],
                    DictParams["userTestPaperId"],
                    DictParams["testPaperMode"],
                    DictParams["testPaperUserType"],
                    DictParams["series"],
                    DictParams["testPaperOwnerId"],
                    mHostUrl
                    );
                pfRequest.addtionHeader = "x-requested-with: XMLHttpRequest";
                refStr = string.Format(@"http://{0}/jsp/admin/exam/testPaper/showTestPaperIFrame.jsp", mHostUrl);
                pfRequest.OpenRequest(urlStr, refStr);
                htmlStr = pfRequest.HtmlDocument;

                CMakeUserPaperData makeObj = new CMakeUserPaperData(htmlStr);
                postData = makeObj.GetUserPostData();
                if (string.IsNullOrEmpty(postData))
                {
                    throw new Exception("make commit data error");
                }

                urlStr = string.Format(@"http://{0}/exam/userTestPaper/commitUserTestPaper.do", mHostUrl);
                refStr = string.Format(@"http://{0}/jsp/admin/exam/testPaper/testPaperAction.jsp", mHostUrl);
                postData = string.Format(@"userId={0}&userTestPaperData={1}&userTestPaperId={2}&leaveTime={3}&canViewScoreAfterExam=1&testPaperUserScore=100&moveOutNum=0", userID, HttpUtility.UrlEncode(postData, mPageEncode), DictParams["userTestPaperId"], 3000 + myRand.Next(1000));
                pfRequest.OpenRequest(urlStr, refStr, postData);
                htmlStr = pfRequest.HtmlDocument;

                pattern = @"(?<=showmessage\s*\(\s*')[\s\S]+?(?=')";
                pfMath = Regex.Match(htmlStr, pattern, RegexOptions.IgnoreCase);
                if (!pfMath.Success)
                {
                    throw new Exception("get commit message error");
                }
                hrStr = pfMath.Value;
            }
            catch (System.Exception ex)
            {
                hrStr = ex.Message;
            }
            return hrStr;
        }



    }
}
