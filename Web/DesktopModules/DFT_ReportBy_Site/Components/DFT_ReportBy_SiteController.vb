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

Namespace NTi.Modules.DFT_ReportBy_Site

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_ReportBy_Site
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_ReportBy_SiteController
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
        Public Function GetDFT_ReportBy_Sites(ByVal ModuleId As Integer) As List(Of DFT_ReportBy_SiteInfo)

            Return CBO.FillCollection(Of DFT_ReportBy_SiteInfo)(DataProvider.Instance().GetDFT_ReportBy_Sites(ModuleId))

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
        Public Function GetDFT_ReportBy_Site(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_ReportBy_SiteInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_ReportBy_Site(ModuleId,ItemId), GetType(DFT_ReportBy_SiteInfo)), DFT_ReportBy_SiteInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_ReportBy_Site">The DFT_ReportBy_SiteInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_ReportBy_Site(ByVal objDFT_ReportBy_Site As DFT_ReportBy_SiteInfo)

            If objDFT_ReportBy_Site.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_ReportBy_Site(objDFT_ReportBy_Site.ModuleId, objDFT_ReportBy_Site.Content, objDFT_ReportBy_Site.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_ReportBy_Site">The DFT_ReportBy_SiteInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_ReportBy_Site(ByVal objDFT_ReportBy_Site As DFT_ReportBy_SiteInfo)

            If objDFT_ReportBy_Site.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_ReportBy_Site(objDFT_ReportBy_Site.ModuleId, objDFT_ReportBy_Site.ItemId, objDFT_ReportBy_Site.Content, objDFT_ReportBy_Site.CreatedByUser)
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
        Public Sub DeleteDFT_ReportBy_Site(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_ReportBy_Site(ModuleId, ItemId)

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

            Dim colDFT_ReportBy_Sites As List(Of DFT_ReportBy_SiteInfo) = GetDFT_ReportBy_Sites(ModInfo.ModuleID)
            Dim objDFT_ReportBy_Site As DFT_ReportBy_SiteInfo
            For Each objDFT_ReportBy_Site In colDFT_ReportBy_Sites
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_ReportBy_Site.Content, objDFT_ReportBy_Site.CreatedByUser, objDFT_ReportBy_Site.CreatedDate, ModInfo.ModuleID, objDFT_ReportBy_Site.ItemId.ToString, objDFT_ReportBy_Site.Content, "ItemId=" & objDFT_ReportBy_Site.ItemId.ToString)
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

            Dim colDFT_ReportBy_Sites As List(Of DFT_ReportBy_SiteInfo) = GetDFT_ReportBy_Sites(ModuleID)
            If colDFT_ReportBy_Sites.Count <> 0 Then
                strXML += "<DFT_ReportBy_Sites>"
                Dim objDFT_ReportBy_Site As DFT_ReportBy_SiteInfo
                For Each objDFT_ReportBy_Site In colDFT_ReportBy_Sites
                    strXML += "<DFT_ReportBy_Site>"
                    strXML += "<content>" & XMLEncode(objDFT_ReportBy_Site.Content) & "</content>"
                    strXML += "</DFT_ReportBy_Site>"
                Next
                strXML += "</DFT_ReportBy_Sites>"
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

            Dim xmlDFT_ReportBy_Site As XmlNode
            Dim xmlDFT_ReportBy_Sites As XmlNode = GetContent(Content, "DFT_ReportBy_Sites")
            For Each xmlDFT_ReportBy_Site In xmlDFT_ReportBy_Sites.SelectNodes("DFT_ReportBy_Site")
                Dim objDFT_ReportBy_Site As New DFT_ReportBy_SiteInfo
                objDFT_ReportBy_Site.ModuleId = ModuleID
                objDFT_ReportBy_Site.Content = xmlDFT_ReportBy_Site.SelectSingleNode("content").InnerText
                objDFT_ReportBy_Site.CreatedByUser = UserId
                AddDFT_ReportBy_Site(objDFT_ReportBy_Site)
            Next

        End Sub

#End Region

    End Class
End Namespace
