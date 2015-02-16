using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneNoteCollaborate.Configuration
{
    public static class HTMLConsts
    {
        public const string HTML_WRAP = @"<?xml version=\""1.0\"" encoding=\""utf-8\"" ?><html xmlns=\""http://www.w3.org/1999/xhtml\"" lang=\""en-us\""><head>
            <title>OneNoteCollaborate</title></head><body><div id=""item"">";
        public const string HTML_CLOSE_WRAP = "</div></body></html>";
        public const string PATCH_WRAP = @"[{
              ""target"": ""#item"",
              ""action"": ""append"",
              ""position"": ""after"", 
              ""content"": """;
        public const string PATCH_CLOSE_WRAP = @""" }]";
    }
}