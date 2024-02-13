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

Namespace YourCompany.Modules.DFT_ReportEDIByCountryForm

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_ReportEDIByCountryForm
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_ReportEDIByCountryFormController
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
        Public Function GetDFT_ReportEDIByCountryForms(ByVal ModuleId As Integer) As List(Of DFT_ReportEDIByCountryFormInfo)

            Return CBO.FillCollection(Of DFT_ReportEDIByCountryFormInfo)(DataProvider.Instance().GetDFT_ReportEDIByCountryForms(ModuleId))

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
        Public Function GetDFT_ReportEDIByCountryForm(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_ReportEDIByCountryFormInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_ReportEDIByCountryForm(ModuleId,ItemId), GetType(DFT_ReportEDIByCountryFormInfo)), DFT_ReportEDIByCountryFormInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_ReportEDIByCountryForm">The DFT_ReportEDIByCountryFormInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_ReportEDIByCountryForm(ByVal objDFT_ReportEDIByCountryForm As DFT_ReportEDIByCountryFormInfo)

            If objDFT_ReportEDIByCountryForm.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_ReportEDIByCountryForm(objDFT_ReportEDIByCountryForm.ModuleId, objDFT_ReportEDIByCountryForm.Content, objDFT_ReportEDIByCountryForm.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_ReportEDIByCountryForm">The DFT_ReportEDIByCountryFormInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_ReportEDIByCountryForm(ByVal objDFT_ReportEDIByCountryForm As DFT_ReportEDIByCountryFormInfo)

            If objDFT_ReportEDIByCountryForm.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_ReportEDIByCountryForm(objDFT_ReportEDIByCountryForm.ModuleId, objDFT_ReportEDIByCountryForm.ItemId, objDFT_ReportEDIByCountryForm.Content, objDFT_ReportEDIByCountryForm.CreatedByUser)
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
        Public Sub DeleteDFT_ReportEDIByCountryForm(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_ReportEDIByCountryForm(ModuleId, ItemId)

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

            Dim colDFT_ReportEDIByCountryForms As List(Of DFT_ReportEDIByCountryFormInfo) = GetDFT_ReportEDIByCountryForms(ModInfo.ModuleID)
            Dim objDFT_ReportEDIByCountryForm As DFT_ReportEDIByCountryFormInfo
            For Each objDFT_ReportEDIByCountryForm In colDFT_ReportEDIByCountryForms
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_ReportEDIByCountryForm.Content, objDFT_ReportEDIByCountryForm.CreatedByUser, objDFT_ReportEDIByCountryForm.CreatedDate, ModInfo.ModuleID, objDFT_ReportEDIByCountryForm.ItemId.ToString, objDFT_ReportEDIByCountryForm.Content, "ItemId=" & objDFT_ReportEDIByCountryForm.ItemId.ToString)
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

            Dim colDFT_ReportEDIByCountryForms As List(Of DFT_ReportEDIByCountryFormInfo) = GetDFT_ReportEDIByCountryForms(ModuleID)
            If colDFT_ReportEDIByCountryForms.Count <> 0 Then
                strXML += "<DFT_ReportEDIByCountryForms>"
                Dim objDFT_ReportEDIByCountryForm As DFT_ReportEDIByCountryFormInfo
                For Each objDFT_ReportEDIByCountryForm In colDFT_ReportEDIByCountryForms
                    strXML += "<DFT_ReportEDIByCountryForm>"
                    strXML += "<content>" & XMLEncode(objDFT_ReportEDIByCountryForm.Content) & "</content>"
                    strXML += "</DFT_ReportEDIByCountryForm>"
                Next
                strXML += "</DFT_ReportEDIByCountryForms>"
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

            Dim xmlDFT_ReportEDIByCountryForm As XmlNode
            Dim xmlDFT_ReportEDIByCountryForms As XmlNode = GetContent(Content, "DFT_ReportEDIByCountryForms")
            For Each xmlDFT_ReportEDIByCountryForm In xmlDFT_ReportEDIByCountryForms.SelectNodes("DFT_ReportEDIByCountryForm")
                Dim objDFT_ReportEDIByCountryForm As New DFT_ReportEDIByCountryFormInfo
                objDFT_ReportEDIByCountryForm.ModuleId = ModuleID
                objDFT_ReportEDIByCountryForm.Content = xmlDFT_ReportEDIByCountryForm.SelectSingleNode("content").InnerText
                objDFT_ReportEDIByCountryForm.CreatedByUser = UserId
                AddDFT_ReportEDIByCountryForm(objDFT_ReportEDIByCountryForm)
            Next

        End Sub

#End Region

    End Class
End Namespace
