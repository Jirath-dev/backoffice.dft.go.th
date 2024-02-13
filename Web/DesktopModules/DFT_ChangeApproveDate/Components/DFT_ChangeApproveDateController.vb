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

Namespace YourCompany.Modules.DFT_ChangeApproveDate

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_ChangeApproveDate
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_ChangeApproveDateController
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
        Public Function GetDFT_ChangeApproveDates(ByVal ModuleId As Integer) As List(Of DFT_ChangeApproveDateInfo)

            Return CBO.FillCollection(Of DFT_ChangeApproveDateInfo)(DataProvider.Instance().GetDFT_ChangeApproveDates(ModuleId))

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
        Public Function GetDFT_ChangeApproveDate(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_ChangeApproveDateInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_ChangeApproveDate(ModuleId,ItemId), GetType(DFT_ChangeApproveDateInfo)), DFT_ChangeApproveDateInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_ChangeApproveDate">The DFT_ChangeApproveDateInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_ChangeApproveDate(ByVal objDFT_ChangeApproveDate As DFT_ChangeApproveDateInfo)

            If objDFT_ChangeApproveDate.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_ChangeApproveDate(objDFT_ChangeApproveDate.ModuleId, objDFT_ChangeApproveDate.Content, objDFT_ChangeApproveDate.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_ChangeApproveDate">The DFT_ChangeApproveDateInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_ChangeApproveDate(ByVal objDFT_ChangeApproveDate As DFT_ChangeApproveDateInfo)

            If objDFT_ChangeApproveDate.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_ChangeApproveDate(objDFT_ChangeApproveDate.ModuleId, objDFT_ChangeApproveDate.ItemId, objDFT_ChangeApproveDate.Content, objDFT_ChangeApproveDate.CreatedByUser)
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
        Public Sub DeleteDFT_ChangeApproveDate(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_ChangeApproveDate(ModuleId, ItemId)

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

            Dim colDFT_ChangeApproveDates As List(Of DFT_ChangeApproveDateInfo) = GetDFT_ChangeApproveDates(ModInfo.ModuleID)
            Dim objDFT_ChangeApproveDate As DFT_ChangeApproveDateInfo
            For Each objDFT_ChangeApproveDate In colDFT_ChangeApproveDates
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_ChangeApproveDate.Content, objDFT_ChangeApproveDate.CreatedByUser, objDFT_ChangeApproveDate.CreatedDate, ModInfo.ModuleID, objDFT_ChangeApproveDate.ItemId.ToString, objDFT_ChangeApproveDate.Content, "ItemId=" & objDFT_ChangeApproveDate.ItemId.ToString)
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

            Dim colDFT_ChangeApproveDates As List(Of DFT_ChangeApproveDateInfo) = GetDFT_ChangeApproveDates(ModuleID)
            If colDFT_ChangeApproveDates.Count <> 0 Then
                strXML += "<DFT_ChangeApproveDates>"
                Dim objDFT_ChangeApproveDate As DFT_ChangeApproveDateInfo
                For Each objDFT_ChangeApproveDate In colDFT_ChangeApproveDates
                    strXML += "<DFT_ChangeApproveDate>"
                    strXML += "<content>" & XMLEncode(objDFT_ChangeApproveDate.Content) & "</content>"
                    strXML += "</DFT_ChangeApproveDate>"
                Next
                strXML += "</DFT_ChangeApproveDates>"
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

            Dim xmlDFT_ChangeApproveDate As XmlNode
            Dim xmlDFT_ChangeApproveDates As XmlNode = GetContent(Content, "DFT_ChangeApproveDates")
            For Each xmlDFT_ChangeApproveDate In xmlDFT_ChangeApproveDates.SelectNodes("DFT_ChangeApproveDate")
                Dim objDFT_ChangeApproveDate As New DFT_ChangeApproveDateInfo
                objDFT_ChangeApproveDate.ModuleId = ModuleID
                objDFT_ChangeApproveDate.Content = xmlDFT_ChangeApproveDate.SelectSingleNode("content").InnerText
                objDFT_ChangeApproveDate.CreatedByUser = UserId
                AddDFT_ChangeApproveDate(objDFT_ChangeApproveDate)
            Next

        End Sub

#End Region

    End Class
End Namespace
