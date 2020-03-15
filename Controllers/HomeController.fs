namespace Xpath.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Xpath.Models
open System.Xml.XPath
open System.IO
open System.Xml

type HomeController (logger : ILogger<HomeController>) =
    inherit Controller()

    member this.Index (vm:HomeViewModel) =
        try
            vm.debug <- "";

            let x = XPathDocument(new StringReader(vm.xml))
            let nav = x.CreateNavigator();
            nav.MoveToFollowing(XPathNodeType.Element) |> ignore

            let nsmgr = XmlNamespaceManager(nav.NameTable);

            let namespaces = nav.GetNamespacesInScope(XmlNamespaceScope.All);
            vm.namespaces <- namespaces
            
            for ns in namespaces do
                nsmgr.AddNamespace(ns.Key, ns.Value);
            
            if  vm.xpath.Length>0 then
                vm.result <- nav.Select(vm.xpath, nsmgr).Cast<XPathNavigator>();
        with
            | ex -> vm.debug <- ex.Message;

        this.View(vm)
    
    member this.Error () =
        this.View();
