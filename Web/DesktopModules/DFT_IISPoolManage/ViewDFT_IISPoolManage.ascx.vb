'
' DotNetNukeฎ - http://www.dotnetnuke.com
' Copyright (c) 2002-2012
' by DotNetNuke Corporation
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System
Imports System.Web
Imports System.Management
Imports System.DirectoryServices
Imports System.Web.UI.WebControls

Imports System.ServiceProcess

Imports System.Xml


Imports System.Text
Imports Microsoft.Web.Administration
Namespace Nti.Modules.DFT_IISPoolManage

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_IISPoolManage class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_IISPoolManage
        Inherits Entities.Modules.PortalModuleBase
        'Implements Entities.Modules.IActionable

#Region "Event Handlers"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Page_Load runs when the control is loaded
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------

#End Region

#Region "Optional Interfaces"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Registers the module actions required for interfacing with the portal framework
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        'Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
        '    Get
        '        Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
        '        Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
        '        Return Actions
        '    End Get
        'End Property

#End Region

        Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                DDL_ListPool.Items.Clear()
                For i As Integer = 0 To HentAppPools2.Count - 1
                    DDL_ListPool.Items.Add(HentAppPools2.Item(i).ToString)
                Next
            End If
        End Sub

        Sub listnn()
            Dim list As List(Of String) = New List(Of String)()

            Dim W3SVC As DirectoryEntry = New DirectoryEntry("IIS://ASPIRE-D09/W3SVC/AppPools/TESTPoolMy", "", "")


            Dim children As DirectoryEntries = W3SVC.Children

            For Each child As DirectoryEntry In children
                list.Add(child.Name.Trim())
            Next child

            For i As Integer = 0 To list.Count - 1
                txtStatus.Text += list.Item(i) & vbNewLine
            Next

        End Sub
        Sub ddd()
            Dim AppPoolsList As New XmlDocument
            AppPoolsList.Load(Server.MapPath("DesktopModules\DFT_IISPoolManage\XMLFile1.xml"))
            Dim AppPool As XmlNode
            For Each AppPool In AppPoolsList.SelectNodes("AppPoolsList/AppPool")
                Dim AppPoolFullPath As String = AppPool.InnerText
                ' AppPoolFullPath must be in the form of: "IIS://" + machine + _
                '       "/W3SVC/AppPools/" + appPoolName
                Try
                    Dim w3svc As New DirectoryEntry(AppPoolFullPath)
                    w3svc.Invoke("Recycle", Nothing)
                    'Response.Write(AppPoolFullPath & "<br />")
                Catch
                    Response.Write(AppPoolFullPath & " [error]<br />")
                End Try
            Next

        End Sub
        Protected Sub status()
            Dim appPoolName As String = txtpool.Text '"mysite.edi2.dft.go.th" ' "dev.somesite.com"
            Dim appPoolPath As String = "IIS://" & System.Environment.MachineName & "/W3SVC/AppPools/" & appPoolName
            Dim intStatus As Integer = 0
            Try
                Dim w3svc As DirectoryEntry = New DirectoryEntry(appPoolPath)

                w3svc.Invoke("Stop", Nothing)
                intStatus = CInt(w3svc.InvokeGet("AppPoolState"))

                'ASPIRE-D09
                '<AppPoolsList>
                '  <AppPool>IIS://SERVER00123/W3SVC/AppPools/Northwind</AppPool>
                '  <AppPool>IIS://SERVER00123/W3SVC/AppPools/Acme</AppPool>
                '  <AppPool>IIS://SERVER00125/W3SVC/AppPools/Northwind</AppPool>
                '  <AppPool>IIS://SERVER00125/W3SVC/AppPools/Acme</AppPool>
                '</AppPoolsList
                'For example, in "IIS://SERVER00123/W3SVC/AppPools/Northwind":

                '"SERVER00123" is the web server machine name (as returned by System.Environment.MachineName when run on that machine); 
                '"Northwind" is the AppPool name as it appears on the IIS management console when looking at AppPools hosted on SERVER00123. 

                Select Case intStatus
                    '1=กำลัง start,3=กำลัง stop
                    Case 2
                        txtStatus.Text = "Running"
                    Case 4
                        txtStatus.Text = "Stopped"
                    Case Else
                        txtStatus.Text = "Unknown"
                End Select
            Catch ex As Exception
                Response.Write(ex.ToString())
            End Try
        End Sub
        Protected Sub ListPoolMain()
            Dim machine As String = "localhost"
            Console.WriteLine(machine)
            Using appPoolsEntry As DirectoryEntry = New DirectoryEntry(String.Format("IIS://{0}/W3SVC/AppPools", machine))
                For Each childEntry As DirectoryEntry In appPoolsEntry.Children
                    'Console.WriteLine(childEntry.Name)
                    'Console.WriteLine(childEntry.Path)
                    txtStatus.Text += childEntry.Name & "--" & childEntry.Path & vbNewLine
                Next childEntry
            End Using
            'Thread.Sleep(5000)

        End Sub

        'Public Sub GetappName()
        '    Dim mgr As ServerManager = New ServerManager()
        '    Dim ie As System.Collections.IEnumerator = mgr.ApplicationPools.GetEnumerator()
        '    Do While ie.MoveNext()
        '        DropDownList1.Items.Add((CType(ie.Current, Microsoft.Web.Administration.ApplicationPool)).Name)
        '    Loop
        'End Sub

        Private Sub GetAppPoolNames()
            Dim Root As System.DirectoryServices.DirectoryEntry = Me.GetDirectoryEntry("IIS://localhost/W3SVC/1/Root")
            'Dim Root As System.DirectoryServices.DirectoryEntry = Me.GetDirectoryEntry("IIS://localhost/W3SVC/AppPools")
            'DirectoryEntry Root = new DirectoryEntry("IIS://localhost/W3SVC/1/Root");

            If Root Is Nothing Then
                Response.Write("No Child in AppPool")
            Else
                For Each dir As DirectoryEntry In Root.Children
                    Dim pr As System.DirectoryServices.PropertyCollection = dir.Properties
                    'ApplicationPool pool = new ApplicationPool();
                    'pool.Name = dir.Name;
                    'DropDownList1.Items.Add(pool.Name);
                    DropDownList1.Items.Add(dir.Name)
                Next dir
            End If
        End Sub

        ''iis 7
        'Public Function GetApplicationPoolCollection() As ArrayList
        '    ' Use an ArrayList to transfer objects to the client.
        '    Dim arrayOfApplicationBags As ArrayList = New ArrayList()

        '    Dim serverManager As ServerManager = New ServerManager()
        '    Dim applicationPoolCollection As ApplicationPoolCollection = serverManager.ApplicationPools
        '    For Each applicationPool As ApplicationPool In applicationPoolCollection
        '        Dim applicationPoolBag As PropertyBag = New PropertyBag()
        '        applicationPoolBag(ServerManagerDemoGlobals.ApplicationPoolArray) = applicationPool
        '        arrayOfApplicationBags.Add(applicationPoolBag)
        '        ' If the applicationPool is stopped, restart it.
        '        If applicationPool.State = ObjectState.Stopped Then
        '            applicationPool.Start()
        '        End If

        '    Next applicationPool

        'End Function

        Public Shared Function HentAppPools() As List(Of String)
            Dim list As List(Of String) = New List(Of String)()
            Dim W3SVC As DirectoryEntry = New DirectoryEntry("IIS://LocalHost/w3svc", "", "")
            For Each Site As DirectoryEntry In W3SVC.Children
                If Site.Name = "AppPools" Then
                    For Each child As DirectoryEntry In Site.Children
                        list.Add(child.Name)
                    Next child
                End If
            Next Site
            Return list
        End Function

        Private Function GetDirectoryEntry(ByVal path As String) As DirectoryEntry
            Dim root As DirectoryEntry = Nothing
            Try
                root = New DirectoryEntry(path)
            Catch
                Response.Write("Could not access Node")
                Return Nothing
            End Try
            If root Is Nothing Then
                Response.Write("Could not access Node")
                Return Nothing
            End If
            Return root
        End Function

        'Protected Sub stopAppPool(ByVal sender As Object, ByVal e As EventArgs)
        Protected Sub stopAppPool()
            'Dim btn As System.Web.UI.WebControls.Button = CType(sender, System.Web.UI.WebControls.Button)

            Dim appPoolName As String = txtpool.Text '"mysite.edi2.dft.go.th" 'btn.CommandArgument
            Dim appPoolPath As String = "IIS://" & System.Environment.MachineName & "/W3SVC/AppPools/" & appPoolName
            Try
                Dim w3svc As DirectoryEntry = New DirectoryEntry(appPoolPath)
                w3svc.Invoke("Stop", Nothing)
                status()
            Catch ex As Exception
                Response.Write(ex.ToString())
            End Try
        End Sub
        'Protected Sub startAppPool(ByVal sender As Object, ByVal e As EventArgs)
        Protected Sub startAppPool()
            'Dim btn As System.Web.UI.WebControls.Button = CType(sender, System.Web.UI.WebControls.Button)
            Dim appPoolName As String = txtpool.Text '"mysite.edi2.dft.go.th" 'btn.CommandArgument
            Dim appPoolPath As String = "IIS://" & System.Environment.MachineName & "/W3SVC/AppPools/" & appPoolName
            Try
                Dim w3svc As DirectoryEntry = New DirectoryEntry(appPoolPath)
                w3svc.Invoke("Start", Nothing)
                status()
            Catch ex As Exception
                Response.Write(ex.ToString())
            End Try
        End Sub

        Protected Sub RecycleAppPoolss(ByVal machine As String, ByVal appPoolName As String)
            Dim path As String = "IIS://" & machine & "/W3SVC/AppPools/" & appPoolName

            Dim w3svc As DirectoryEntry = New DirectoryEntry(path)
            w3svc.Invoke("Recycle", Nothing)
            status()
        End Sub

        Private Sub Recycle(ByVal appPool As String)
            Dim appPoolPath As String = "IIS://localhost/W3SVC/AppPools/" & appPool
            Using appPoolEntry As DirectoryEntry = New DirectoryEntry(appPoolPath)
                appPoolEntry.Invoke("Recycle", Nothing)
                appPoolEntry.Close()
            End Using
        End Sub

        Shared Sub StopAppPool(ByVal ConnectionUser As String, ByVal ConnectionPassword As String, _
        ByVal Machine As String, ByVal appPool As String)
            Dim co As ConnectionOptions = New ConnectionOptions()
            'co.Username = ConnectionUser
            'co.Password = ConnectionPassword
            co.Impersonation = ImpersonationLevel.Impersonate
            co.Authentication = AuthenticationLevel.PacketPrivacy
            Dim objPath As String = "IISApplicationPool.Name='" & appPool & "'" ' watch the single quotes!!

            Dim scope As ManagementScope = New ManagementScope("\\" & Machine & "\root\MicrosoftIISV2", co)

            Using mc As ManagementObject = New ManagementObject(objPath)
                mc.Scope = scope
                mc.InvokeMethod("Stop", Nothing, Nothing)
            End Using
        End Sub
        Public Sub RecycleAppPool(ByVal appPoolLocation As String, ByVal appPoolName As String)
            'Execute Code to recycle a user's app pool
            Dim w3svc As DirectoryEntry = New DirectoryEntry(appPoolLocation & appPoolName)
            'w3svc.Invoke("Recycle", Nothing)
            w3svc.Invoke("Stop", Nothing)
        End Sub


        Protected Sub btnCheckStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckStatus.Click
            status()
        End Sub

        Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            txtStatus.Text = HentAppPools.Item(0).Trim
        End Sub

        Protected Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            startAppPool()
        End Sub

        Protected Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
            stopAppPool()
        End Sub

        Protected Sub btnRe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRe.Click
            'RecycleAppPool(System.Environment.MachineName, txtpool.Text)
            'Recycle(txtpool.Text)

            'stopAppPool("admin", "adminPwd", "localhost", "W3SVC/AppPools/mysitevm.edi2.dft.go.th")

            RecycleAppPool("IIS://localhost/W3SVC/AppPools/", "mysitevm.edi2.dft.go.th")
        End Sub

        Protected Sub btnMachineName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMachineName.Click
            txtStatus.Text = System.Environment.MachineName
        End Sub

        Protected Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
            ddd()
        End Sub

        Protected Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
            listnn()
        End Sub

        Sub ccc()
            Response.Write("<HTML><HEAD></HEAD><BODY>")

            Dim AppPoolsList As New XmlDocument
            AppPoolsList.Load(Server.MapPath("\DesktopModules\DFT_IISPoolManage\XMLFile1.xml"))
            Dim AppPool As XmlNode
            For Each AppPool In AppPoolsList.SelectNodes("AppPoolsList/AppPool")
                Dim AppPoolFullPath As String = AppPool.InnerText
                ' AppPoolFullPath must be in the form of: "IIS://" + machine + _
                '       "/W3SVC/AppPools/" + appPoolName
                Try
                    Dim w3svc As New DirectoryEntry(AppPoolFullPath)
                    w3svc.Invoke("Recycle", Nothing)
                    Response.Write(AppPoolFullPath & "<br />")
                Catch
                    Response.Write(AppPoolFullPath & " [error]<br />")
                End Try
            Next

            Response.Write("<p />-- done --")
            Response.Write("</BODY></HTML>")
        End Sub

        Protected Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
            ccc()
        End Sub

        Public Shared Function HentAppPools_v1() As List(Of String)
            Dim list As List(Of String) = New List(Of String)()
            Dim W3SVC As DirectoryEntry = New DirectoryEntry("IIS://LocalHost/w3svc", "", "")
            For Each Site As DirectoryEntry In W3SVC.Children
                If Site.Name = "AppPools" Then
                    For Each child As DirectoryEntry In Site.Children
                        list.Add(child.Name)
                    Next child
                End If
            Next Site
            Return list
        End Function
        Public Shared Sub RecycleAppPool_v1(ByVal IIsApplicationPool As String)
            Dim scope As ManagementScope = New ManagementScope("\\localhost\root\MicrosoftIISv2")
            scope.Connect()
            Dim appPool As ManagementObject = New ManagementObject(scope, New ManagementPath("IIsApplicationPool.Name='W3SVC/AppPools/" & IIsApplicationPool & "'"), Nothing)
            appPool.InvokeMethod("Recycle", Nothing, Nothing)
        End Sub

        Sub MainV2()
            Dim serverManager As ServerManager = New ServerManager
            Dim config As Configuration = serverManager.GetApplicationHostConfiguration
            Dim applicationPoolsSection As ConfigurationSection = config.GetSection("system.applicationHost/applicationPools")
            Dim applicationPoolsCollection As ConfigurationElementCollection = applicationPoolsSection.GetCollection
            Dim addElement As ConfigurationElement = applicationPoolsCollection.CreateElement("add")
            addElement("name") = "Contoso"
            Dim recyclingElement As ConfigurationElement = addElement.GetChildElement("recycling")
            Dim periodicRestartElement As ConfigurationElement = recyclingElement.GetChildElement("periodicRestart")
            Dim scheduleCollection As ConfigurationElementCollection = periodicRestartElement.GetCollection("schedule")
            Dim addElement1 As ConfigurationElement = scheduleCollection.CreateElement("add")
            addElement1("value") = TimeSpan.Parse("03:00:00")
            scheduleCollection.Add(addElement1)
            applicationPoolsCollection.Add(addElement)
            serverManager.CommitChanges()
        End Sub

        Sub Mv3()
            Dim manager As New ServerManager
            '' Get the application recycling property values.
            Console.WriteLine("DisallowOverlappingRotation:" & ChrW(9) & "{0}", manager.ApplicationPools.Item("DefaultAppPool").Recycling.DisallowOverlappingRotation.ToString)
            Console.WriteLine("DisallowRotationOnConfigChange:" & ChrW(9) & "{0}", manager.ApplicationPools.Item("DefaultAppPool").Recycling.DisallowRotationOnConfigChange.ToString)
            Console.WriteLine("LogEventOnRecycle:" & ChrW(9) & "{0}", manager.ApplicationPools.Item("DefaultAppPool").Recycling.LogEventOnRecycle.ToString)
            Console.WriteLine("PeriodicRestart.Time:" & ChrW(9) & "{0}", manager.ApplicationPools.Item("DefaultAppPool").Recycling.PeriodicRestart.Time)
            '' Change the LogEventOnRecycle and PeriodicRestart.Time properties.
            manager.ApplicationPools.Item("DefaultAppPool").Recycling.LogEventOnRecycle = (RecyclingLogEventOnRecycle.Schedule Or RecyclingLogEventOnRecycle.Requests)
            manager.ApplicationPools.Item("DefaultAppPool").Recycling.PeriodicRestart.Time = TimeSpan.FromMinutes(5)
            '' Commit the changes to ApplicationHost.config file.
            manager.CommitChanges()

        End Sub
        Protected Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
            Mv3()
        End Sub

        Protected Sub btnCheckListPoolAgain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckListPoolAgain.Click
            DDL_ListPool.Items.Clear()
            For i As Integer = 0 To HentAppPools2.Count - 1
                DDL_ListPool.Items.Add(HentAppPools2.Item(i).ToString)
            Next
        End Sub

        Protected Sub btnResetPoolStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetPoolStatus.Click
            Select Case DDL_ListPool.Text
                Case ""
                    lblError.Text = "กรุณา เลือก Pool Name ที่ต้องการ"
                Case Else
                    Select Case RDO_ListStatus.SelectedValue
                        Case 0 'กรณี ไม่เลือก เพื่อ

                        Case 1 'กรณี start pool
                            Start_AppPool(DDL_ListPool.Text)
                        Case 2 'กรณี stop pool
                            Stop_AppPool(DDL_ListPool.Text)
                        Case 3 'กรณี recycle pool
                            Recycle_AppPool(DDL_ListPool.Text)
                    End Select
                    lblstatuspool.Text = status(txtTempNamePool.Text)
            End Select

        End Sub

        Protected Sub btnCheckStatusPool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckStatusPool.Click
            lblstatuspool.Text = status(txtTempNamePool.Text)
        End Sub

        Protected Sub DDL_ListPool_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDL_ListPool.SelectedIndexChanged
            txtTempNamePool.Text = DDL_ListPool.Text
        End Sub

#Region "for iis 6"
        ''' <summary>
        ''' Get a list of available Application Pools
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function HentAppPools2() As List(Of String)

            Dim list As List(Of String) = New List(Of String)()
            Dim W3SVC As DirectoryEntry = New DirectoryEntry("IIS://LocalHost/w3svc", "", "")
            For Each Site As DirectoryEntry In W3SVC.Children
                If Site.Name = "APPPOOLS" Then
                    list.Add("--- กรุณาเลือก ---")
                    For Each child As DirectoryEntry In Site.Children
                        list.Add(child.Name)
                    Next child
                End If
            Next Site
            Return list
        End Function

        ''' <summary>
        ''' Recycle an application pool
        ''' </summary>
        ''' <param name="IIsApplicationPool"></param>
        Public Shared Sub Recycle_AppPool(ByVal IIsApplicationPool As String)
            Dim scope As ManagementScope = New ManagementScope("\\localhost\root\MicrosoftIISv2")
            scope.Connect()
            Dim appPool As ManagementObject = New ManagementObject(scope, New ManagementPath("IIsApplicationPool.Name='W3SVC/AppPools/" & IIsApplicationPool & "'"), Nothing)

            appPool.InvokeMethod("Recycle", Nothing, Nothing)
        End Sub
        Public Shared Sub Start_AppPool(ByVal IIsApplicationPool As String)
            Dim scope As ManagementScope = New ManagementScope("\\localhost\root\MicrosoftIISv2")
            scope.Connect()
            Dim appPool As ManagementObject = New ManagementObject(scope, New ManagementPath("IIsApplicationPool.Name='W3SVC/AppPools/" & IIsApplicationPool & "'"), Nothing)

            appPool.InvokeMethod("Start", Nothing, Nothing)
        End Sub
        Public Shared Sub Stop_AppPool(ByVal IIsApplicationPool As String)
            Dim scope As ManagementScope = New ManagementScope("\\localhost\root\MicrosoftIISv2")
            scope.Connect()
            Dim appPool As ManagementObject = New ManagementObject(scope, New ManagementPath("IIsApplicationPool.Name='W3SVC/AppPools/" & IIsApplicationPool & "'"), Nothing)

            appPool.InvokeMethod("Stop", Nothing, Nothing)
        End Sub

        Protected Function status(ByVal By_PoolName As String) As String
            Dim Status_Pool As String = ""

            Dim appPoolName As String = By_PoolName '"mysite.edi2.dft.go.th" ' "dev.somesite.com"
            Dim appPoolPath As String = "IIS://" & System.Environment.MachineName & "/W3SVC/AppPools/" & appPoolName
            Dim intStatus As Integer = 0
            Try
                Dim w3svc As DirectoryEntry = New DirectoryEntry(appPoolPath)

                intStatus = CInt(w3svc.InvokeGet("AppPoolState"))

                Select Case intStatus
                    '1=กำลัง start,3=กำลัง stop
                    Case 2
                        Status_Pool = "Running"
                    Case 4
                        Status_Pool = "Stopped"
                    Case Else
                        Status_Pool = "Unknown"
                End Select

                Return Status_Pool
            Catch ex As Exception
                Response.Write(ex.ToString())
            End Try
        End Function
#End Region
    End Class
End Namespace
