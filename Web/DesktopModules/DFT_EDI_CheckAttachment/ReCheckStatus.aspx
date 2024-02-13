<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReCheckStatus.aspx.vb" Inherits=".ReCheckStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        body, table td { font-size:10pt;}
    </style>
    <script type="text/javascript" language="javascript">
        function CloseMyWin() {
            try { window.parent.opener.parent.opener.refreshGrid(); } catch (e) { }
            try { window.parent.opener.close(); } catch (e) { }
            window.close();
        }
    </script>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    <h3 style="border-bottom:1px solid #cccccc;padding-bottom:3px;">ย้อนสถานะผลการตรวจสอบแบบคำขอและเอกสารแนบ</h3>
        <table cellpadding="3">
            <tr>
                <td style="width:80px;">
                    ชื่อเจ้าหน้าที่:</td>
                <td>
                    <asp:Label ID="lblUserName" runat="server" ForeColor="#0000CC"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="top">
                    เหตุผล:</td>
                <td>
                    <asp:TextBox ID="txtResult" runat="server" Rows="10" TextMode="MultiLine" 
                        Width="397px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtResult" ErrorMessage="* กรุณาใส่เหตุผลให้เรียบร้อย"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="บันทึกข้อมูล" />
&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" OnClientClick="window.close();return false;" CausesValidation="false" UseSubmitBehavior="false" Text="ยกเลิก" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
