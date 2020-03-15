namespace Xpath.Models

open System.Collections.Generic
open System.Xml.XPath

type HomeViewModel() =
    let mutable _xml = """<xml xmlns="default" xmlns:p="dims">
<p:a>hej</p:a>
<p:a>med</p:a>
</xml>"""

    member public this.xml   
        with get() = _xml 
        and set(value) = _xml <- value

    member val public xpath = "" with get, set

    member val public result : IEnumerable<XPathNavigator> = null with get, set

    member val public debug = "" with get, set

    member val public namespaces : IDictionary<string, string> = null with get, set
