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

Namespace NTi.Modules.DFT_EDI_AUsers

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_EDI_AUsers
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_EDI_AUsersController
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
        Public Function GetDFT_EDI_AUserss(ByVal ModuleId As Integer) As List(Of DFT_EDI_AUsersInfo)

            Return CBO.FillCollection(Of DFT_EDI_AUsersInfo)(DataProvider.Instance().GetDFT_EDI_AUserss(ModuleId))

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
        Public Function GetDFT_EDI_AUsers(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_EDI_AUsersInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_EDI_AUsers(ModuleId,ItemId), GetType(DFT_EDI_AUsersInfo)), DFT_EDI_AUsersInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_EDI_AUsers">The DFT_EDI_AUsersInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_EDI_AUsers(ByVal objDFT_EDI_AUsers As DFT_EDI_AUsersInfo)

            If objDFT_EDI_AUsers.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_EDI_AUsers(objDFT_EDI_AUsers.ModuleId, objDFT_EDI_AUsers.Content, objDFT_EDI_AUsers.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_EDI_AUsers">The DFT_EDI_AUsersInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_EDI_AUsers(ByVal objDFT_EDI_AUsers As DFT_EDI_AUsersInfo)

            If objDFT_EDI_AUsers.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_EDI_AUsers(objDFT_EDI_AUsers.ModuleId, objDFT_EDI_AUsers.ItemId, objDFT_EDI_AUsers.Content, objDFT_EDI_AUsers.CreatedByUser)
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
        Public Sub DeleteDFT_EDI_AUsers(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_EDI_AUsers(ModuleId, ItemId)

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

            Dim colDFT_EDI_AUserss As List(Of DFT_EDI_AUsersInfo) = GetDFT_EDI_AUserss(ModInfo.ModuleID)
            Dim objDFT_EDI_AUsers As DFT_EDI_AUsersInfo
            For Each objDFT_EDI_AUsers In colDFT_EDI_AUserss
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_EDI_AUsers.Content, objDFT_EDI_AUsers.CreatedByUser, objDFT_EDI_AUsers.CreatedDate, ModInfo.ModuleID, objDFT_EDI_AUsers.ItemId.ToString, objDFT_EDI_AUsers.Content, "ItemId=" & objDFT_EDI_AUsers.ItemId.ToString)
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

            Dim colDFT_EDI_AUserss As List(Of DFT_EDI_AUsersInfo) = GetDFT_EDI_AUserss(ModuleID)
            If colDFT_EDI_AUserss.Count <> 0 Then
                strXML += "<DFT_EDI_AUserss>"
                Dim objDFT_EDI_AUsers As DFT_EDI_AUsersInfo
                For Each objDFT_EDI_AUsers In colDFT_EDI_AUserss
                    strXML += "<DFT_EDI_AUsers>"
                    strXML += "<content>" & XMLEncode(objDFT_EDI_AUsers.Content) & "</content>"
                    strXML += "</DFT_EDI_AUsers>"
                Next
                strXML += "</DFT_EDI_AUserss>"
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

            Dim xmlDFT_EDI_AUsers As XmlNode
            Dim xmlDFT_EDI_AUserss As XmlNode = GetContent(Content, "DFT_EDI_AUserss")
            For Each xmlDFT_EDI_AUsers In xmlDFT_EDI_AUserss.SelectNodes("DFT_EDI_AUsers")
                Dim objDFT_EDI_AUsers As New DFT_EDI_AUsersInfo
                objDFT_EDI_AUsers.ModuleId = ModuleID
                objDFT_EDI_AUsers.Content = xmlDFT_EDI_AUsers.SelectSingleNode("content").InnerText
                objDFT_EDI_AUsers.CreatedByUser = UserId
                AddDFT_EDI_AUsers(objDFT_EDI_AUsers)
            Next

        End Sub

#End Region

    End Class
End Namespace
