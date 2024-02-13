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

Namespace NTi.Modules.DFT_FormE20Approve

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_FormE20Approve
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_FormE20ApproveController
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
        Public Function GetDFT_FormE20Approves(ByVal ModuleId As Integer) As List(Of DFT_FormE20ApproveInfo)

            Return CBO.FillCollection(Of DFT_FormE20ApproveInfo)(DataProvider.Instance().GetDFT_FormE20Approves(ModuleId))

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
        Public Function GetDFT_FormE20Approve(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_FormE20ApproveInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_FormE20Approve(ModuleId,ItemId), GetType(DFT_FormE20ApproveInfo)), DFT_FormE20ApproveInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_FormE20Approve">The DFT_FormE20ApproveInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_FormE20Approve(ByVal objDFT_FormE20Approve As DFT_FormE20ApproveInfo)

            If objDFT_FormE20Approve.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_FormE20Approve(objDFT_FormE20Approve.ModuleId, objDFT_FormE20Approve.Content, objDFT_FormE20Approve.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_FormE20Approve">The DFT_FormE20ApproveInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_FormE20Approve(ByVal objDFT_FormE20Approve As DFT_FormE20ApproveInfo)

            If objDFT_FormE20Approve.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_FormE20Approve(objDFT_FormE20Approve.ModuleId, objDFT_FormE20Approve.ItemId, objDFT_FormE20Approve.Content, objDFT_FormE20Approve.CreatedByUser)
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
        Public Sub DeleteDFT_FormE20Approve(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_FormE20Approve(ModuleId, ItemId)

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

            Dim colDFT_FormE20Approves As List(Of DFT_FormE20ApproveInfo) = GetDFT_FormE20Approves(ModInfo.ModuleID)
            Dim objDFT_FormE20Approve As DFT_FormE20ApproveInfo
            For Each objDFT_FormE20Approve In colDFT_FormE20Approves
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_FormE20Approve.Content, objDFT_FormE20Approve.CreatedByUser, objDFT_FormE20Approve.CreatedDate, ModInfo.ModuleID, objDFT_FormE20Approve.ItemId.ToString, objDFT_FormE20Approve.Content, "ItemId=" & objDFT_FormE20Approve.ItemId.ToString)
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

            Dim colDFT_FormE20Approves As List(Of DFT_FormE20ApproveInfo) = GetDFT_FormE20Approves(ModuleID)
            If colDFT_FormE20Approves.Count <> 0 Then
                strXML += "<DFT_FormE20Approves>"
                Dim objDFT_FormE20Approve As DFT_FormE20ApproveInfo
                For Each objDFT_FormE20Approve In colDFT_FormE20Approves
                    strXML += "<DFT_FormE20Approve>"
                    strXML += "<content>" & XMLEncode(objDFT_FormE20Approve.Content) & "</content>"
                    strXML += "</DFT_FormE20Approve>"
                Next
                strXML += "</DFT_FormE20Approves>"
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

            Dim xmlDFT_FormE20Approve As XmlNode
            Dim xmlDFT_FormE20Approves As XmlNode = GetContent(Content, "DFT_FormE20Approves")
            For Each xmlDFT_FormE20Approve In xmlDFT_FormE20Approves.SelectNodes("DFT_FormE20Approve")
                Dim objDFT_FormE20Approve As New DFT_FormE20ApproveInfo
                objDFT_FormE20Approve.ModuleId = ModuleID
                objDFT_FormE20Approve.Content = xmlDFT_FormE20Approve.SelectSingleNode("content").InnerText
                objDFT_FormE20Approve.CreatedByUser = UserId
                AddDFT_FormE20Approve(objDFT_FormE20Approve)
            Next

        End Sub

#End Region

    End Class
End Namespace
