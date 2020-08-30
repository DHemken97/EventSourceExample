<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="EventSourceTesting.TestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="javascript:initialize()">

<div id="targetDiv"></div>  
  
<script>  
      
    function initialize() {  
     
  
        if (window.EventSource == undefined) {  
            // If not supported  
            document.getElementById('targetDiv').innerHTML = "Your browser doesn't support Server Sent Events.";  
            return;  
        } else {  
            var source = new EventSource('./EventSource.aspx');  
  
            source.onopen = function (event) {  
                document.getElementById('targetDiv').innerHTML += event.toString()+ 'Connection Opened.<br>';  
            };  
  
            source.onerror = function (event) {  
                if (event.eventPhase == EventSource.CLOSED) {  
                    document.getElementById('targetDiv').innerHTML += 'Connection Closed.<br>';  
                }  
            };  
  
            source.onmessage = function (event) {  
                document.getElementById('targetDiv').innerHTML += event.data + '<br>';  
            };  
        }  
    }  
</script> 
</body>
</html>
