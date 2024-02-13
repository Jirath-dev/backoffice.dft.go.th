<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmFormPDF.aspx.vb" Inherits=".frmFormPDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style id="headerscript" runat="server" type="text/css">
        html, body, form, iframe {
            height: 100%;
            margin: 0px;
            padding: 0px;
            overflow: hidden;
        }
         </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-right: 25px; height: 100%;">
            <iframe runat="server" height="100%" width="100%" frameborder="0" id="PDF_FRAME"
                style="margin-left: 3px;"></iframe>
        </div>
    </form>
</body>
</html>
