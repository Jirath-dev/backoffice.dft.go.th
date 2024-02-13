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

Namespace YourCompany.Modules.DFT_ReportEDIAll

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_ReportEDIAll
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_ReportEDIAllController
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
        Public Function GetDFT_ReportEDIAlls(ByVal ModuleId As Integer) As List(Of DFT_ReportEDIAllInfo)

            Return CBO.FillCollection(Of DFT_ReportEDIAllInfo)(DataProvider.Instance().GetDFT_ReportEDIAlls(ModuleId))

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
        Public Function GetDFT_ReportEDIAll(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_ReportEDIAllInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_ReportEDIAll(ModuleId,ItemId), GetType(DFT_ReportEDIAllInfo)), DFT_ReportEDIAllInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_ReportEDIAll">The DFT_ReportEDIAllInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_ReportEDIAll(ByVal objDFT_ReportEDIAll As DFT_ReportEDIAllInfo)

            If objDFT_ReportEDIAll.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_ReportEDIAll(objDFT_ReportEDIAll.ModuleId, objDFT_ReportEDIAll.Content, objDFT_ReportEDIAll.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_ReportEDIAll">The DFT_ReportEDIAllInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_ReportEDIAll(ByVal objDFT_ReportEDIAll As DFT_ReportEDIAllInfo)

            If objDFT_ReportEDIAll.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_ReportEDIAll(objDFT_ReportEDIAll.ModuleId, objDFT_ReportEDIAll.ItemId, objDFT_ReportEDIAll.Content, objDFT_ReportEDIAll.CreatedByUser)
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
        Public Sub DeleteDFT_ReportEDIAll(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_ReportEDIAll(ModuleId, ItemId)

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

            Dim colDFT_ReportEDIAlls As List(Of DFT_ReportEDIAllInfo) = GetDFT_ReportEDIAlls(ModInfo.ModuleID)
            Dim objDFT_ReportEDIAll As DFT_ReportEDIAllInfo
            For Each objDFT_ReportEDIAll In colDFT_ReportEDIAlls
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_ReportEDIAll.Content, objDFT_ReportEDIAll.CreatedByUser, objDFT_ReportEDIAll.CreatedDate, ModInfo.ModuleID, objDFT_ReportEDIAll.ItemId.ToString, objDFT_ReportEDIAll.Content, "ItemId=" & objDFT_ReportEDIAll.ItemId.ToString)
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

            Dim colDFT_ReportEDIAlls As List(Of DFT_ReportEDIAllInfo) = GetDFT_ReportEDIAlls(ModuleID)
            If colDFT_ReportEDIAlls.Count <> 0 Then
                strXML += "<DFT_ReportEDIAlls>"
                Dim objDFT_ReportEDIAll As DFT_ReportEDIAllInfo
                For Each objDFT_ReportEDIAll In colDFT_ReportEDIAlls
                    strXML += "<DFT_ReportEDIAll>"
                    strXML += "<content>" & XMLEncode(objDFT_ReportEDIAll.Content) & "</content>"
                    strXML += "</DFT_ReportEDIAll>"
                Next
                strXML += "</DFT_ReportEDIAlls>"
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

            Dim xmlDFT_ReportEDIAll As XmlNode
            Dim xmlDFT_ReportEDIAlls As XmlNode = GetContent(Content, "DFT_ReportEDIAlls")
            For Each xmlDFT_ReportEDIAll In xmlDFT_ReportEDIAlls.SelectNodes("DFT_ReportEDIAll")
                Dim objDFT_ReportEDIAll As New DFT_ReportEDIAllInfo
                objDFT_ReportEDIAll.ModuleId = ModuleID
                objDFT_ReportEDIAll.Content = xmlDFT_ReportEDIAll.SelectSingleNode("content").InnerText
                objDFT_ReportEDIAll.CreatedByUser = UserId
                AddDFT_ReportEDIAll(objDFT_ReportEDIAll)
            Next

        End Sub

#End Region

    End Class
End Namespace
