using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamAutoSel
{
    class CHelper
    {
        public static CLessonInfoSecItem[]  TimeLessonInfoSec=new CLessonInfoSecItem[]{
            new CLessonInfoSecItem(){lengthHMS="00:06:02",lengthS="362"},
            new CLessonInfoSecItem(){lengthHMS="00:06:11",lengthS="371"},
            new CLessonInfoSecItem(){lengthHMS="00:08:18",lengthS="498"},
            new CLessonInfoSecItem(){lengthHMS="00:04:19",lengthS="259"},
            new CLessonInfoSecItem(){lengthHMS="00:39:29",lengthS="2369"},
            new CLessonInfoSecItem(){lengthHMS="00:10:58",lengthS="658"},
            new CLessonInfoSecItem(){lengthHMS="00:03:37",lengthS="217"},
            new CLessonInfoSecItem(){lengthHMS="00:55:50",lengthS="3350"},
            new CLessonInfoSecItem(){lengthHMS="00:10:02",lengthS="602"},
            new CLessonInfoSecItem(){lengthHMS="00:17:19",lengthS="1039"},
            new CLessonInfoSecItem(){lengthHMS="00:50:17",lengthS="3017"},
            new CLessonInfoSecItem(){lengthHMS="01:00:25",lengthS="3625"}
        };
        public static CLessonInfoSecItem[] TimeLessonDTJJ = new CLessonInfoSecItem[]{
            new CLessonInfoSecItem(){lengthHMS="00:28:28",lengthS="1708"},
            new CLessonInfoSecItem(){lengthHMS="00:24:30",lengthS="1470"},
            new CLessonInfoSecItem(){lengthHMS="00:13:33",lengthS="813"},
            new CLessonInfoSecItem(){lengthHMS="00:22:19",lengthS="1339"},
            new CLessonInfoSecItem(){lengthHMS="00:38:18",lengthS="2298"},
            new CLessonInfoSecItem(){lengthHMS="00:29:39",lengthS="1779"},
            new CLessonInfoSecItem(){lengthHMS="00:17:22",lengthS="1042"},
            new CLessonInfoSecItem(){lengthHMS="00:46:24",lengthS="2784"},
            new CLessonInfoSecItem(){lengthHMS="00:34:26",lengthS="2066"},
            new CLessonInfoSecItem(){lengthHMS="00:43:26",lengthS="2606"}
        };
        //低碳经济与绿色生活
        public static CLessonInfoSecItem[] TimeLessonDTJJ_LSSH = new CLessonInfoSecItem[]{
            new CLessonInfoSecItem(){lengthHMS="00:25:27",lengthS="1527"},
            new CLessonInfoSecItem(){lengthHMS="00:05:27",lengthS="327"},
            new CLessonInfoSecItem(){lengthHMS="00:23:53",lengthS="1433"},
            new CLessonInfoSecItem(){lengthHMS="00:17:15",lengthS="1035"},
            new CLessonInfoSecItem(){lengthHMS="01:11:28",lengthS="4288"},
            new CLessonInfoSecItem(){lengthHMS="00:23:19",lengthS="1399"},
            new CLessonInfoSecItem(){lengthHMS="00:19:22",lengthS="1162"},
            new CLessonInfoSecItem(){lengthHMS="00:25:09",lengthS="1509"},
            new CLessonInfoSecItem(){lengthHMS="00:16:54",lengthS="1014"},
            new CLessonInfoSecItem(){lengthHMS="00:08:28",lengthS="508"}
        };
        //b08fdf58-ccf5-4550-936b-5fe61ee2d3b9
        //广西生态文明与可持续发展
        public static CLessonInfoSecItem[] TimeLessonGXSTWM_KCXFZ = new CLessonInfoSecItem[]{
            new CLessonInfoSecItem(){lengthHMS="00:52:23",lengthS="3143"},
            new CLessonInfoSecItem(){lengthHMS="00:53:23",lengthS="3203"},
            new CLessonInfoSecItem(){lengthHMS="00:49:13",lengthS="2953"},
            new CLessonInfoSecItem(){lengthHMS="00:45:53",lengthS="2753"},
            new CLessonInfoSecItem(){lengthHMS="00:52:37",lengthS="3157"},
            new CLessonInfoSecItem(){lengthHMS="00:44:40",lengthS="2680"},
           
        };
    }

    class CParamUpadeServer
    {
        public string student_id { get; set; }
        public string student_name { get; set; }
        public int currentScoID { get; set; }
        public string cookies { get; set; }
        public string refString { get; set; }
        public string entityID { get; set; }
    }
    class CLessonInfoSecItem
    {
        public string lengthHMS { get; set; }
        public string lengthS { get;set;}
    }


}
