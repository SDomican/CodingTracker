﻿// See https://aka.ms/new-console-template for more information
using System.Configuration;
using System.Collections.Specialized;
using ConsoleTableExt;

var sAttr = ConfigurationManager.AppSettings.Get("Key0");

var codingString = ConfigurationManager.AppSettings.Get("ConnectionString");

Console.WriteLine(sAttr);

Console.WriteLine(codingString);




//var tableData = new List<List<object>>
//{
//    new List<object>{ "Sakura Yamamoto", "Support Engineer", "London", 46},
//    new List<object>{ "Serge Baldwin", "Data Coordinator", "San Francisco", 28, "something else" },
//    new List<object>{ "Shad Decker", "Regional Director", "Edinburgh"},
//};

//ConsoleTableBuilder
//    .From(tableData)
//    .ExportAndWriteLine();

