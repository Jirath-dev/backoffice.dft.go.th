'
' DotNetNuke® - http://www.dotnetnuke.com
' Copyright (c) 2002-2008
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

Imports System
Imports System.Configuration
Imports System.Data
Imports System.XML
Imports System.Web
Imports System.Collections.Generic

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities.XmlUtils
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Services.Search

Namespace YourCompany.Modules.RadModuleTest

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for RadModuleTest
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class RadModuleTestController
        Implements Entities.Modules.ISearchable
        Implements Entities.Modules.IPortable

#Region "Public Methods"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' gets an object from the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="moduleId">The Id of the module</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Function GetRadModuleTests(ByVal ModuleId As Integer) As List(Of RadModuleTestInfo)

            Return CBO.FillCollection(Of RadModuleTestInfo)(DataProvider.Instance().GetRadModuleTests(ModuleId))

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' gets an object from the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="ModuleId">The Id of the module</param>
        ''' <param name="ItemId">The Id of the item</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Function GetRadModuleTest(ByVal ModuleId As Integer, ByVal ItemId As Integer) As RadModuleTestInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetRadModuleTest(ModuleId,ItemId), GetType(RadModuleTestInfo)), RadModuleTestInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objRadModuleTest">The RadModuleTestInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddRadModuleTest(ByVal objRadModuleTest As RadModuleTestInfo)

            If objRadModuleTest.Content.Trim <> "" Then
		DataProvider.Instance().AddRadModuleTest(objRadModuleTest.ModuleId, objRadModuleTest.Content, objRadModuleTest.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objRadModuleTest">The RadModuleTestInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateRadModuleTest(ByVal objRadModuleTest As RadModuleTestInfo)

            If objRadModuleTest.Content.Trim <> "" Then
                DataProvider.Instance().UpdateRadModuleTest(objRadModuleTest.ModuleId, objRadModuleTest.ItemId, objRadModuleTest.Content, objRadModuleTest.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' deletes an object from the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="ModuleId">The Id of the module</param>
        ''' <param name="ItemId">The Id of the item</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub DeleteRadModuleTest(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteRadModuleTest(ModuleId, ItemId)

        End Sub

#End Region

#Region "Optional Interfaces"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' GetSearchItems implements the ISearchable Interface
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Function GetSearchItems(ByVal ModInfo As Entities.Modules.ModuleInfo) As DotNetNuke.Services.Search.SearchItemInfoCollection Implements DotNetNuke.Entities.Modules.ISearchable.GetSearchItems

            Dim SearchItemCollection As New SearchItemInfoCollection

            Dim colRadModuleTests As List(Of RadModuleTestInfo) = GetRadModuleTests(ModInfo.ModuleID)
            Dim objRadModuleTest As RadModuleTestInfo
            For Each objRadModuleTest In colRadModuleTests
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objRadModuleTest.Content, objRadModuleTest.CreatedByUser, objRadModuleTest.CreatedDate, ModInfo.ModuleID, objRadModuleTest.ItemId.ToString, objRadModuleTest.Content, "ItemId=" & objRadModuleTest.ItemId.ToString)
                SearchItemCollection.Add(SearchItem)
            Next

            Return SearchItemCollection

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' ExportModule implements the IPortable ExportModule Interface
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="ModuleID">The Id of the module to be exported</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Function ExportModule(ByVal ModuleID As Integer) As String Implements DotNetNuke.Entities.Modules.IPortable.ExportModule

            Dim strXML As String = ""

            Dim colRadModuleTests As List(Of RadModuleTestInfo) = GetRadModuleTests(ModuleID)
            If colRadModuleTests.Count <> 0 Then
                strXML += "<RadModuleTests>"
                Dim objRadModuleTest As RadModuleTestInfo
                For Each objRadModuleTest In colRadModuleTests
                    strXML += "<RadModuleTest>"
                    strXML += "<content>" & XMLEncode(objRadModuleTest.Content) & "</content>"
                    strXML += "</RadModuleTest>"
                Next
                strXML += "</RadModuleTests>"
            End If

            Return strXML

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' ImportModule implements the IPortable ImportModule Interface
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="ModuleID">The Id of the module to be imported</param>
        ''' <param name="Content">The content to be imported</param>
        ''' <param name="Version">The version of the module to be imported</param>
        ''' <param name="UserId">The Id of the user performing the import</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserId As Integer) Implements DotNetNuke.Entities.Modules.IPortable.ImportModule

            Dim xmlRadModuleTest As XmlNode
            Dim xmlRadModuleTests As XmlNode = GetContent(Content, "RadModuleTests")
            For Each xmlRadModuleTest In xmlRadModuleTests.SelectNodes("RadModuleTest")
                Dim objRadModuleTest As New RadModuleTestInfo
                objRadModuleTest.ModuleId = ModuleID
                objRadModuleTest.Content = xmlRadModuleTest.SelectSingleNode("content").InnerText
                objRadModuleTest.CreatedByUser = UserId
                AddRadModuleTest(objRadModuleTest)
            Next

        End Sub

#End Region

    End Class
End Namespace
