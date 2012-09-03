﻿/*
Copyright 2012 Stephen Swensen

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Swensen.RestSharpGui {
    public class RequestViewModel {
        public string Url { get; set; }
        public Method? Method { get; set; }
        public string[] Headers { get; set; }
        public string Body { get; set; }

        public void Save(string fileName) {
            var ws = new XmlWriterSettings();
            ws.NewLineHandling = NewLineHandling.Entitize;

            using (var sw = XmlWriter.Create(File.Create(fileName), ws)) {
                var serializer = new XmlSerializer(typeof(RequestViewModel));
                serializer.Serialize(sw, this);
            }
        }

        public static RequestViewModel Open(string fileName) {
            var rs = new XmlReaderSettings();
            rs.IgnoreWhitespace = false;
            using (var file = XmlReader.Create(File.Open(fileName, FileMode.Open), rs)) {
                var serializer = new XmlSerializer(typeof(RequestViewModel));
                return serializer.Deserialize(file) as RequestViewModel;
            }
        }
    }
}
