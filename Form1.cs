#define REMOTE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

using System.IO;


namespace ExamAutoSel
{

    public partial class Form1 : Form
    {

        string dataFileName = @"Radio.dat";
        public Form1()
        {
            InitializeComponent();

            try
            {
                string readLines = string.Empty;
                using (StreamReader sr = new StreamReader(dataFileName))
                {
                    readLines = sr.ReadToEnd();
                    sr.Close();
                }
                MatchCollection mc = Regex.Matches(readLines, @"""(?<val>[\s\S]+?)""",RegexOptions.IgnoreCase);
                foreach (Match _m in mc)
                {
                    hsAnswer.Add(_m.Groups["val"].Value);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        HashSet<string> hsAnswer = new HashSet<string>();

           /// <summary>
           /// 参数设置区域
           /// </summary>
           /// <param name="mark"></param>
           /// <param name="sel"></param>

        private delegate void BeginInvokeDelegate(bool mark, bool sel);


        bool paramsUpdata_mark = true;
        bool paramsUpdata_sel = true;
        bool paramsAutoNext = true;

        bool study_Quick = true;
        bool study_AutoNext = true;

        const int MAXTRYUPDATACOUNT = 3;

      //string HostUrl = @"file:///C:/Users/wangyong/Downloads/考试页面文件保存/考试页面文件保存/信息技术科/6.htm";
#if REMOTE   
       string HostUrl = @"http://guangxi.chinahrt.com/portal/guangxizhuanji/index.jsp";
#else

       //string HostUrl = @"file:///C:/Users/wangyong/Downloads/考试页面文件保存/考试页面文件保存/信息技术科/9_files/ucenterExam_list.htm";
        string HostUrl = @"file:///C:/Users/wangyong/Desktop/题库采集/题库/5/1_files/ucenterExam_list.htm";
#endif



       /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>





        private void Form1_Load(object sender, EventArgs e)
        {
            //this.webBrowser1.Navigated += webBrowser1_Navigated;
            this.webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            this.webBrowser1.Navigate(HostUrl);

            //this.webBrowser1.Navigate("file:///C:/Users/wangyong/Downloads/152_.htm");

            this.webBrowser1.Navigating += webBrowser1_Navigating;
        }

        void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //throw new NotImplementedException();
            
        }

        private void AutoFill(bool mark,bool sel)
        {
            try
                {
#if REMOTE
                   HtmlDocument frameDoc= this.webBrowser1.Document.Window.Frames["examIframe"].Document;
#else
                    HtmlDocument frameDoc = this.webBrowser1.Document;
#endif
                    Console.WriteLine("");
                    HtmlAgilityPack.HtmlDocument hapDoc = new HtmlAgilityPack.HtmlDocument();
                    //hapDoc.LoadHtml(this.webBrowser1.DocumentText);
                    hapDoc.LoadHtml(frameDoc.Body.InnerHtml);
                    
                    HtmlAgilityPack.HtmlNodeCollection inputNodes = hapDoc.DocumentNode.SelectNodes(@"//div[@id=""mainQuestionContent""]//input[@id]");
                    foreach (HtmlAgilityPack.HtmlNode _node in inputNodes)
                    {
                        try
                        {
                            string inputID = _node.GetAttributeValue("id", "Error");
                            string orgInputID = inputID;
                            //////////////////////////////////////////////////////////////////////////
                            //开始处理判断题
                            //////////////////////////////////////////////////////////////////////////
                            if (inputID.StartsWith(@"judge"))
                            {
                                //处理判断题
                                //1、获取题目
                                string questionBody = _node.SelectSingleNode(@"preceding::div[@class=""questionTitleDiv"" or  @class=""nameQuestionsDiv""][1]").LastChild.InnerText;
                                questionBody =System.Text.RegularExpressions.Regex.Replace(questionBody, @"\s", string.Empty);
                                questionBody += inputID.Substring(0, 7);
                                inputID = WebQQ.Util.CommonClass.GetMD5str(questionBody);
                            }



                            ///////////////////////////////////////////////////////////////////////////
                            //处理判断题结束
                            //////////////////////////////////////////////////////////////////////////



                            if (hsAnswer.Contains(inputID))
                            {
                                inputID = orgInputID;
                                HtmlElement elementInput =frameDoc.GetElementById(inputID);
                                if (mark)
                                {
                                    if (elementInput != null)
                                    {
                                        foreach (HtmlElement _elementChildren in elementInput.Parent.Children)
                                        {
                                            if (_elementChildren.GetAttribute("htmlfor") == inputID)
                                            {
                                                // _elementChildren.SetAttribute("style", _elementChildren.Style+ ";color:red");
                                                _elementChildren.Style += ";color:red";
                                            }


                                        }
                                    }
                                }            


                                if (sel)
                                {

                                    System.Threading.Thread.Sleep(100);
                                    elementInput.ScrollIntoView(true);
                                    elementInput.Focus();
                                    elementInput.InvokeMember("click");
                                    //elementInput.Focus();
                                    //elementInput.InvokeMember("click");
                                    //elementInput.Focus();
                                    //elementInput.InvokeMember("click");
                                }
                                
                                  
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                }
                catch (System.Exception)
                { }
        }

        
        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //WebDocNavCompleted_Gxpx365(e);

            switch (e.Url.Host.ToLower())
            {
                    case "passport.chinahrt.com":
                    case "guangxi.chinahrt.com":
                        WebDocNavCompleted_Chinalrt(e);
            	break;
                case "www.gxpx365.com":
                WebDocNavCompleted_Gxpx365(e);
                break;
            }
        }

        private void WebDocNavCompleted_Gxpx365(WebBrowserDocumentCompletedEventArgs e)
        {
            string trigerUrl = @"cmd/ExamControl?flag=answer&paper_id";
            
            try
            {
                if (e.Url.ToString().Contains(trigerUrl))
                {
                    HtmlDocument frameDoc = this.webBrowser1.Document;
                    HtmlAgilityPack.HtmlDocument hapDoc = new HtmlAgilityPack.HtmlDocument();
                    hapDoc.LoadHtml(frameDoc.Body.InnerHtml);
                    HtmlAgilityPack.HtmlNodeCollection NodeInputs = hapDoc.DocumentNode.SelectNodes(@"//input[@name][contains(@onclick,'public'
)]");
                    foreach (HtmlAgilityPack.HtmlNode _nodeInput in NodeInputs)
                    {
                        string _inputName=_nodeInput.GetAttributeValue("name", "error");
                        string _inputValue=_nodeInput.GetAttributeValue("value", "error");

                        string inputItem = string.Format(@"gxpx365{0}{1}",_inputName , _inputValue);
                        inputItem = WebQQ.Util.CommonClass.GetMD5str(inputItem);
                        if (hsAnswer.Contains(inputItem))
                        {
                            HtmlElementCollection ecc = frameDoc.All.GetElementsByName(_inputName);
                            foreach (HtmlElement _hele in ecc)
                            {
                                if (_hele.GetAttribute("value") == _inputValue)
                                {
                                    if (paramsUpdata_sel)
                                    {
                                        _hele.SetAttribute("checked", "true");
                                        //_hele.InvokeMember("onClick");
                                    }
                                }
                            }

                            
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            
        }
        private void WebDocNavCompleted_Chinalrt(WebBrowserDocumentCompletedEventArgs e)
        {


#if REMOTE
            string trigerUrl = @"userAnswer_showUpload_dialog.do";
#else
            string trigerUrl = @"userAnswer_showUpload_dialog.htm";
#endif
            // string trigerUrl = @"ucenterExam_list.do";

            Console.WriteLine(e.Url.ToString());
            if (e.Url.ToString().Contains(trigerUrl))
            {
                //AutoFill(true,true);
                //this.webBrowser1.BeginInvoke(new BeginInvokeDelegate(AutoFill), new object[] { true, true });
                //AutoFill(true,false);
                UpdataPaper();

                //string nextPageUrl=GetNextPageUrl();
                if (paramsAutoNext)
                {
                    GetNextPageUrl();
                }


            }

            //////////////////////////////////////////////////////////////////////////
            //获取教学视频时长；
            //http://res.chinahrt.com/course/3a605ee0-4294-4611-a5dc-84756394a9fd/video/smoothly/09.mp4?start=0
            string trigUrl_getVideoLength = @"http://res.chinahrt.com/course/";
            if (study_Quick && e.Url.ToString().StartsWith(trigUrl_getVideoLength) && e.Url.ToString().Contains("/content/media.htm"))
            {
                try
                {
                    HtmlDocument frameDoc = this.webBrowser1.Document.Window.Frames["text"].Document;

                    string _student_id = frameDoc.InvokeScript("eval", new string[] { "top.API.cmi.core.student_id.cmivalue;" }) as string;
                    string _student_name = frameDoc.InvokeScript("eval", new string[] { "top.API.cmi.core.student_name.cmivalue;" }) as string;
                    int _currentScoID = (int)frameDoc.InvokeScript("eval", new string[] { "top.CPFrame.currentScoID;" });
                    string _entityID = frameDoc.InvokeScript("eval", new string[] { "top.document.getElementById('entityId').value;" }) as string;

                    frameDoc.InvokeScript("eval", new string[] { "top.unsave=false;" });

                    CParamUpadeServer updateParamObj = new CParamUpadeServer()
                    {
                        student_id = _student_id,
                        student_name = _student_name,
                        currentScoID = _currentScoID,
                        cookies = this.webBrowser1.Document.Cookie,
                        refString = this.webBrowser1.Document.Url.ToString(),
                        entityID = _entityID
                    };
                    System.Threading.Thread threadUpdateServer = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(FunUpdateServer));

                    //快速学习
                    threadUpdateServer.Start(updateParamObj);


                    Console.WriteLine(e.Url.ToString());
                }
                catch (System.Exception) { }
            }
            //快速学习结束
            //////////////////////////////////////////////////////////////////////////


            //////////////////////////////////////////////////////////////////////////
            //开始处理自动学习
            //http://lms.chinahrt.com/pages/launch_gxzj.jsp?entityId=3a605ee0-4294-4611-a5dc-84756394a9fd&exit=http://student.chinahrt.com/index/index.do?SESIONID=c7f805be-2635-4b3f-bb77-58eb5f1896a8&courseId=7b0021ac-c5c2-4b65-9100-784b00467e02
            //http://lms.chinahrt.com/pages/playerfiles/LMSFrame.jsp?t=2c2201a5-9049-4552-bc9f-75e91aa5bc68

            //http://lms.chinahrt.com/pages/Cover?i=267502014923224138179&entityId=3a605ee0-4294-4611-a5dc-84756394a9fd&exit=http://student.chinahrt.com/index/index.do?SESIONID=c7f805be-2635-4b3f-bb77-58eb5f1896a8


            string url_study_info_main = @"http://lms.chinahrt.com/pages/Cover?i";
            if (study_AutoNext && e.Url.ToString().StartsWith(url_study_info_main))
            {
                try
                {
                    HtmlDocument frameDoc = this.webBrowser1.Document;
                    //    for (int i = 0; i < _mc.Count;i++ )
                    //    {
                    //        if (_mc[i].Groups["status"].Value!="completed")
                    //        {

                    //            break;
                    //        }


                    HtmlDocument frameDocText = this.webBrowser1.Document.Window.Frames["text"].Document;
                    HtmlElementCollection _studyTrs = frameDocText.GetElementsByTagName("tr");
                    int tri = 0;
                    foreach (HtmlElement _studyTr in _studyTrs)
                    {
                        try
                        {
                            if (!_studyTr.InnerHtml.Contains("已学完"))
                            {
                                //HtmlElement _link = _studyTr.GetElementsByTagName("a")[0];
                                //_link.InvokeMember("click");
                                //break;
                                frameDoc.InvokeScript("eval", new string[] { string.Format(@"top.window.frames.code.NavigateItems('{0}')", tri) });
                                break;
                            }
                        }
                        catch (System.Exception) { }
                        tri++;
                    }

                }
                catch (System.Exception ex_study_autoNext)
                {
                    Console.WriteLine(ex_study_autoNext);
                }
            }

        }


        string postDataUpdateServer = @"cmi.core.student_id~r@l@ad~{0}^r@l@ad^cmi.core.student_name~r@l@ad~{1}^r@l@ad^cmi.core.lesson_location~r@l@ad~{2}^r@l@ad^cmi.core.credit~r@l@ad~credit^r@l@ad^cmi.core.lesson_status~r@l@ad~completed^r@l@ad^cmi.core.entry~r@l@ad~ab-initio^r@l@ad^cmi.core.score.raw~r@l@ad~^r@l@ad^cmi.core.score.max~r@l@ad~^r@l@ad^cmi.core.score.min~r@l@ad~^r@l@ad^cmi.core.total_time~r@l@ad~0000:00:00.00^r@l@ad^cmi.core.lesson_mode~r@l@ad~normal^r@l@ad^cmi.core.exit~r@l@ad~^r@l@ad^cmi.core.session_time~r@l@ad~{3}^r@l@ad^cmi.suspend_data~r@l@ad~{2}^r@l@ad^cmi.launch_data~r@l@ad~^r@l@ad^cmi.comments~r@l@ad~^r@l@ad^cmi.comments_from_lms~r@l@ad~^r@l@ad^cmi.objectives._count~r@l@ad~0^r@l@ad^cmi.student_data.mastery_score~r@l@ad~^r@l@ad^cmi.student_data.max_time_allowed~r@l@ad~^r@l@ad^cmi.student_data.time_limit_action~r@l@ad~^r@l@ad^cmi.student_preference.audio~r@l@ad~0^r@l@ad^cmi.student_preference.language~r@l@ad~^r@l@ad^cmi.student_preference.speed~r@l@ad~0^r@l@ad^cmi.student_preference.text~r@l@ad~0^r@l@ad^cmi.interactions._count~r@l@ad~0^r@l@ad^";
        void FunUpdateServer(object arg)
        {
            try
            {
                CParamUpadeServer paramObj = arg as CParamUpadeServer;
                HttpRequest.HttpRequest _webRequest = new HttpRequest.HttpRequest();

                //////////////////////////////////////////////////////////////////////////
                //处理Cookie
                foreach (string _s in paramObj.cookies.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    try
                    {
                        string _cn = _s.Split(new string[] { "=" }, StringSplitOptions.None)[0].Trim();
                        string _cv = _s.Split(new string[] { "=" }, StringSplitOptions.None)[1].Trim();
                        _webRequest.craboCookie.Add(new Cookie(_cn, _cv, "/", "lms.chinahrt.com"));

                    }
                    catch (System.Exception)
                    { }
                }
                
                string urlString = @"http://lms.chinahrt.com/pages/playerfiles/lms";
                string refString = paramObj.refString;

                CLessonInfoSecItem[] lesson = null;
                switch (paramObj.entityID)
                {
                    case "c6482080-a39b-4711-9815-478e79e40da1":
                        {
                            lesson = CHelper.TimeLessonDTJJ;
                        }
                        break;
                    case "3a605ee0-4294-4611-a5dc-84756394a9fd":
                        {
                            lesson = CHelper.TimeLessonInfoSec;
                        }
                        break;

                    //低碳经济与绿色生活
                    case "2727f40e-6f87-46da-840b-e2ed6ef2d871":
                        {
                            lesson = CHelper.TimeLessonDTJJ_LSSH;
                        }
                        break;
                    //广西生态文明与可持续发展
                    case "b08fdf58-ccf5-4550-936b-5fe61ee2d3b9":
                        {
                            lesson = CHelper.TimeLessonGXSTWM_KCXFZ;
                        }
                        break;
                }


                string dataStr = string.Format(postDataUpdateServer, paramObj.student_id, paramObj.student_name,
                    lesson[paramObj.currentScoID].lengthS,
                    lesson[paramObj.currentScoID].lengthHMS
                    );
                string postData = string.Format(@"nextAction=none&itemID={0}&data={1}&lmsAction=update",paramObj.currentScoID,
                    System.Web.HttpUtility.UrlEncode(dataStr,Encoding.Default));

                int tryTimeUpdateServer = 4;
                int _tryCount=0;
                bool _updateServerSuccess = false;
                while(_tryCount<tryTimeUpdateServer)
                {
                    _webRequest.OpenRequest(urlString, refString, postData);
                    string htmlString = _webRequest.HtmlDocument;

                    if (!_webRequest.responseUrl.Contains("error"))
                    {
                        _updateServerSuccess = true;
                        break;
                    }
                }

                FunBrowserNav(paramObj.refString,_webRequest.HtmlDocument);

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FunBrowserNav(string navUrl,string htmlResutl)
        {
           if (this.webBrowser1.InvokeRequired)
            {
                this.webBrowser1.Invoke(new Action<string,string>(FunBrowserNav), new object[] { navUrl,htmlResutl });
                return;
            }
            try
            {

                //this.webBrowser1.Refresh();
                this.webBrowser1.Navigate(navUrl);


                //if (study_AutoNext)
                //{
                //    string pattern = @"prereqmodelstatus\s*\+=\s*""(?<id>\S+?)、[\s\S]+?status\s+is\s+(?<status>[\s\S]+?)\\n";
                //    MatchCollection _mc = Regex.Matches(htmlResutl, pattern, RegexOptions.IgnoreCase);

                //    //HtmlDocument frameDoc = this.webBrowser1.Document.Window.Frames["text"].Document;
                //    HtmlDocument frameDoc = this.webBrowser1.Document;
                //    for (int i = 0; i < _mc.Count;i++ )
                //    {
                //        if (_mc[i].Groups["status"].Value!="completed")
                //        {
                //            frameDoc.InvokeScript("eval", new string[] { string.Format(@"top.window.frames.code.NavigateItems('{0}')",i) });
                //            break;
                //        }
                //    }
                //}
                //else
                //{
                    
                //}
               

               
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            
        }

        private void GetNextPageUrl()
        {
            try
            {
#if REMOTE
                HtmlDocument frameDoc = this.webBrowser1.Document.Window.Frames["examIframe"].Document;
#else
                HtmlDocument frameDoc = this.webBrowser1.Document;
#endif
                
                foreach (HtmlElement _link in frameDoc.Links)
                {
                    if (_link.InnerText.Trim() == "下一页")
                    {
                        _link.InvokeMember("click");
                    }
                }


                //HtmlAgilityPack.HtmlDocument hapDoc = new HtmlAgilityPack.HtmlDocument();
                //hapDoc.LoadHtml(frameDoc.Body.InnerHtml);

                //HtmlAgilityPack.HtmlNode nodeNextPage = hapDoc.DocumentNode.SelectSingleNode(@"//a[.=""下一页""]");
                //string nextPageUrl= nodeNextPage.GetAttributeValue("href", string.Empty);
                //if (!nextPageUrl.StartsWith("http"))
                //{
                //    nextPageUrl = @"http://exam.chinahrt.com/ucenter/" + nextPageUrl;
                //}
                //return nextPageUrl;
                
            }
            catch (System.Exception e)
            {
                //return string.Empty;
            }
        }


        private void UpdataPaper()
        {
            try
            {
#if REMOTE
                HtmlDocument frameDoc = this.webBrowser1.Document.Window.Frames["examIframe"].Document;
#else
                HtmlDocument frameDoc = this.webBrowser1.Document;
#endif
                
                HtmlAgilityPack.HtmlDocument hapDoc = new HtmlAgilityPack.HtmlDocument();
                hapDoc.LoadHtml(frameDoc.Body.InnerHtml);

                //////////////////////////////////////////////////////////////////////////
                //获取post参数；
                
                string FRecordId = string.Empty;
                FRecordId = hapDoc.DocumentNode.SelectSingleNode(@"//input[@id=""FRecordId""]").GetAttributeValue("value", "Error");
                HttpRequest.HttpRequest webRequest = new HttpRequest.HttpRequest();
                string urlString = string.Empty;
                string refString =System.Web.HttpUtility.UrlEncode(this.webBrowser1.Url.ToString(),Encoding.Default) ;
                string postData = string.Empty;

                string webCookies = frameDoc.Cookie;
                
                //////////////////////////////////////////////////////////////////////////
                //处理Cookie
                foreach (string _s in webCookies.Split(new string[]{";"},StringSplitOptions.RemoveEmptyEntries))
                {
                    try
                    {
                        string _cn=_s.Split(new string[]{"="},StringSplitOptions.None)[0].Trim();
                        string _cv=_s.Split(new string[]{"="},StringSplitOptions.None)[1].Trim();
                        webRequest.craboCookie.Add(new Cookie(_cn, _cv, "/", "exam.chinahrt.com"));
                    }
                    catch (System.Exception)
                    {}
                }




                HtmlAgilityPack.HtmlNodeCollection NodeAnswerDivs = hapDoc.DocumentNode.SelectNodes(@"//div[@class=""answerDiv""]");
                foreach (HtmlAgilityPack.HtmlNode _AnswerDiv in NodeAnswerDivs)
                {
                    try
                    {
                        //处理一个答题块；
                        string FUserpapersId = string.Empty;
                        string FUserAnswer = string.Empty;
                        string FQuestionMark ="1";
                        List<string> lsRightID = new List<string>();


                        HtmlAgilityPack.HtmlNodeCollection NodeInputs = _AnswerDiv.SelectNodes(@".//input[@id][@name]");


#region 生成updata 中的FuserAnswer参数及正确答案的InputＩＤ；
                        //////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////


                        foreach (HtmlAgilityPack.HtmlNode _input in NodeInputs)
                        {
                            try
                            {
                                if (string.IsNullOrEmpty(FUserpapersId))
                                {
                                    FUserpapersId = _input.GetAttributeValue("name", string.Empty).Split(new string[] { "_" }, StringSplitOptions.None)[1];
                                }

                                string inputID = _input.GetAttributeValue("id", "Error");
                                string orgInputID = inputID;
                                //////////////////////////////////////////////////////////////////////////
                                //开始处理判断题
                                //////////////////////////////////////////////////////////////////////////
                                if (inputID.StartsWith(@"judge"))
                                {
                                    //处理判断题
                                    //1、获取题目
                                    string questionBody = _input.SelectSingleNode(@"preceding::div[@class=""questionTitleDiv"" or  @class=""nameQuestionsDiv""][1]/img").GetAttributeValue("src", "ErrorImageSrc");

                                    string srcPattern = @"(?<=fquestionid=)[\s\S]+?(?=&)";
                                    Match _m = Regex.Match(questionBody, srcPattern, RegexOptions.IgnoreCase);
                                    if (_m.Success)
                                    {
                                        questionBody = _m.Value;
                                    }

                                    //questionBody = System.Text.RegularExpressions.Regex.Replace(questionBody, @"\s", string.Empty);
                                    questionBody += inputID.Substring(0, 7);
                                    inputID = WebQQ.Util.CommonClass.GetMD5str(questionBody);
                                }
                                if (hsAnswer.Contains(inputID))
                                {
                                    FUserAnswer += _input.GetAttributeValue("value", string.Empty);
                                    lsRightID.Add(orgInputID);
                                }

                            }
                            catch (System.Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                           
                        }
                        //////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////
#endregion

#region 开始updata 发包；
                        //////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////
                        int tryCount = 1;
                        bool updataSucess = false;
                        while (tryCount<=MAXTRYUPDATACOUNT)
                        {
                            tryCount++;
                            urlString = string.Format(@"http://exam.chinahrt.com/ucenter/ucenterPaper_update_json.do");
                            //refString = string.Empty;
                            postData = string.Format(@"FUserpapersId={0}&FUserAnswer={1}&FQuestionMark={2}&FRecordId={3}", FUserpapersId, FUserAnswer, FQuestionMark, FRecordId);
                            webRequest.OpenRequest(urlString, refString, postData);
                            string htmlString = webRequest.HtmlDocument;
                            if (System.Text.RegularExpressions.Regex.IsMatch(htmlString,@"ret""\s*:\s*0",System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                updataSucess = true;
                                break;
                            }
                        }

                        for (int i = 0; i < lsRightID.Count;i++ )
                        {
                            string inputID = lsRightID[i];
                            HtmlElement elementInput = frameDoc.GetElementById(inputID);
                            if (paramsUpdata_mark)
                            {
                                if (elementInput != null)
                                {
                                    foreach (HtmlElement _elementChildren in elementInput.Parent.Children)
                                    {
                                        if (_elementChildren.GetAttribute("htmlfor") == inputID)
                                        {
                                            // _elementChildren.SetAttribute("style", _elementChildren.Style+ ";color:red");
                                            _elementChildren.Style += ";color:red";
                                        }


                                    }
                                }
                            }


                            if (paramsUpdata_sel && updataSucess)
                            {
                                elementInput.SetAttribute("checked", "checked");
                            }
                        }



                        //////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////
                        
#endregion

                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (System.Exception)
            {}



        }

        
        private void 标识ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            paramsUpdata_mark = this.标识ToolStripMenuItem.Checked;
        }

       

        private void 自动翻页ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            paramsAutoNext = this.自动翻页ToolStripMenuItem.Checked;
        }

        private void 自动选择ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            paramsUpdata_sel = this.自动选择ToolStripMenuItem.Checked;
        }

        private void 入口一ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string entryUrl = @"http://guangxi.chinahrt.com/";
            this.webBrowser1.Navigate(entryUrl);
        }

        private void 入口二ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string entryUrl = @"http://passport.chinahrt.com/login_init.do";
            this.webBrowser1.Navigate(entryUrl);
        }

        private void 快速学习ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            this.study_Quick = this.快速学习ToolStripMenuItem.Checked;
        }

        private void 自动学习ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            this.study_AutoNext = this.自动学习ToolStripMenuItem.Checked;
        }

        private void savePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string _host= this.webBrowser1.Url.Host.ToLower();
                switch (_host)
                {
                    case "passport.chinahrt.com":
                    case "guangxi.chinahrt.com":
                        OLUpdataChinahrt();
                	break;
                    case "www.gxpx365.com":
                        OLUpdataGxpx365();
                    break;
                    default:
                    MessageBox.Show("不支持该站点在线采集题库！！");
                        break;

                }
                SaveHsanswerToFile();
                MessageBox.Show("更新题库成功！");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("更新题库失败！\r\n"+ex.Message);
            }
            
        }

        private void OLUpdataGxpx365()
        {
            string htmlTxt = this.webBrowser1.Document.Body.InnerHtml;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlTxt);
            HtmlAgilityPack.HtmlNodeCollection nodeColCheckInputs = doc.DocumentNode.SelectNodes(@"//input[@checked][@id]");
            foreach (HtmlAgilityPack.HtmlNode _input in nodeColCheckInputs)
            {
                try
                {
                    string inputItem = string.Format(@"gxpx365{0}{1}", _input.GetAttributeValue("name", "error"), _input.GetAttributeValue("value", "error"));
                    inputItem = WebQQ.Util.CommonClass.GetMD5str(inputItem);
                    hsAnswer.Add(inputItem);
                }
                catch (System.Exception)
                { }
            }
        }

        private void OLUpdataChinahrt()
        {
            string htmlTxt = this.webBrowser1.Document.Window.Frames["examIframe"].Document.Body.InnerHtml;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlTxt);
            HtmlAgilityPack.HtmlNodeCollection nodeColCheckInputs = doc.DocumentNode.SelectNodes(@"//input[@checked][@id]");
            foreach (HtmlAgilityPack.HtmlNode _input in nodeColCheckInputs)
            {
                //Console.WriteLine(nodeItem.GetAttributeValue("id", string.Empty));
                //HsCheckInput.Add(nodeItem.GetAttributeValue("id", "error"));
                try
                {
                    string inputID = _input.GetAttributeValue("id", "error");
                    if (inputID.StartsWith(@"judge"))
                    {
                        //处理判断题
                        //1、获取题目

                        string questionBody = _input.SelectSingleNode(@"preceding::div[@class=""questionTitleDiv"" or  @class=""nameQuestionsDiv""][1]/img").GetAttributeValue("src", "ErrorImageSrc");

                        string srcPattern = @"(?<=fquestionid=)[\s\S]+?(?=&)";
                        Match _m = Regex.Match(questionBody, srcPattern, RegexOptions.IgnoreCase);
                        if (_m.Success)
                        {
                            questionBody = _m.Value;
                        }

                        //questionBody = System.Text.RegularExpressions.Regex.Replace(questionBody, @"\s", string.Empty);
                        questionBody += inputID.Substring(0, 7);
                        inputID = WebQQ.Util.CommonClass.GetMD5str(questionBody);
                    }


                    hsAnswer.Add(inputID);
                }
                catch (System.Exception)
                { }


            }
        }

        /// <summary>
        /// need try catch
        /// </summary>
        private void SaveHsanswerToFile()
        {
            if (hsAnswer == null || hsAnswer.Count <= 0)
            {
                throw new Exception("hsanswer is null");
            }
            using (StreamWriter sww = new StreamWriter(dataFileName, false, Encoding.Default))
            {
                foreach (string __id in hsAnswer)
                {
                    sww.WriteLine(string.Format(@"""{0}"",", __id));
                }

                sww.Flush();
                sww.Close();
            }
        }

        private void 公务员题库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string rootPath =this.folderBrowserDialog1.SelectedPath;
            if (string.IsNullOrEmpty(rootPath))
            {
                return;
            }
            
            DirectoryInfo di = new DirectoryInfo(rootPath);
            FileInfo[] fis = di.GetFiles("ExamControl.htm", SearchOption.AllDirectories);
            int curPos = 0;
            foreach (FileInfo fi in fis)
            {
                
                try
                {
                    string htmlTxt = fi.OpenText().ReadToEnd();
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(htmlTxt);
                    HtmlAgilityPack.HtmlNodeCollection nodeColCheckInputs = doc.DocumentNode.SelectNodes(@"//input[@checked][@value]");
                    foreach (HtmlAgilityPack.HtmlNode _input in nodeColCheckInputs)
                    {
                        try
                        {
                            string inputItem = string.Format(@"gxpx365{0}{1}", _input.GetAttributeValue("name", "error"), _input.GetAttributeValue("value", "error"));
                            inputItem = WebQQ.Util.CommonClass.GetMD5str(inputItem);
                            hsAnswer.Add(inputItem);
                        }
                        catch (System.Exception)
                        {}
                    }

                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            try
            {
                SaveHsanswerToFile();
                MessageBox.Show("导入题库成功！");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void 公务员入口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string entryUrl = @"http://www.gxpx365.com//";
            this.webBrowser1.Navigate(entryUrl);
        }

        private void 标识ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private string pfHostUrl = string.Empty;
        private void 普法考试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPfExam pfexam = new FormPfExam();
            pfexam.HostUrl = pfHostUrl;
            pfexam.ShowDialog();
            if (!string.IsNullOrEmpty(pfexam.HostUrl))
            {
                pfHostUrl = pfexam.HostUrl;
            }
        }
    }
}
