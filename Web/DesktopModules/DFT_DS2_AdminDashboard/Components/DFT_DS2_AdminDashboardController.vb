'
' DotNetNuke® - http://www.dotnetnuke.com
' Copyright (c) 2002-2005
' by Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
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

Namespace NTI.Modules.DFT_DS2_AdminDashboard

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_DS2_AdminDashboard
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_DS2_AdminDashboardController
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
        Public Function GetDFT_DS2_AdminDashboards(ByVal ModuleId As Integer) As List(Of DFT_DS2_AdminDashboardInfo)

            Return CBO.FillCollection(Of DFT_DS2_AdminDashboardInfo)(DataProvider.Instance().GetDFT_DS2_AdminDashboards(ModuleId))

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
        Public Function GetDFT_DS2_AdminDashboard(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_DS2_AdminDashboardInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_DS2_AdminDashboard(ModuleId, ItemId), GetType(DFT_DS2_AdminDashboardInfo)), DFT_DS2_AdminDashboardInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_DS2_AdminDashboard">The DFT_DS2_AdminDashboardInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_DS2_AdminDashboard(ByVal objDFT_DS2_AdminDashboard As DFT_DS2_AdminDashboardInfo)

            If objDFT_DS2_AdminDashboard.Content.Trim <> "" Then
                DataProvider.Instance().AddDFT_DS2_AdminDashboard(objDFT_DS2_AdminDashboard.ModuleId, objDFT_DS2_AdminDashboard.Content, objDFT_DS2_AdminDashboard.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_DS2_AdminDashboard">The DFT_DS2_AdminDashboardInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_DS2_AdminDashboard(ByVal objDFT_DS2_AdminDashboard As DFT_DS2_AdminDashboardInfo)

            If objDFT_DS2_AdminDashboard.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_DS2_AdminDashboard(objDFT_DS2_AdminDashboard.ModuleId, objDFT_DS2_AdminDashboard.ItemId, objDFT_DS2_AdminDashboard.Content, objDFT_DS2_AdminDashboard.CreatedByUser)
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
        Public Sub DeleteDFT_DS2_AdminDashboard(ByVal ModuleId As Integer, ByVal ItemId As Integer)

            DataProvider.Instance().DeleteDFT_DS2_AdminDashboard(ModuleId, ItemId)

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

            Dim colDFT_DS2_AdminDashboards As List(Of DFT_DS2_AdminDashboardInfo) = GetDFT_DS2_AdminDashboards(ModInfo.ModuleID)
            Dim objDFT_DS2_AdminDashboard As DFT_DS2_AdminDashboardInfo
            For Each objDFT_DS2_AdminDashboard In colDFT_DS2_AdminDashboards
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_DS2_AdminDashboard.Content, objDFT_DS2_AdminDashboard.CreatedByUser, objDFT_DS2_AdminDashboard.CreatedDate, ModInfo.ModuleID, objDFT_DS2_AdminDashboard.ItemId.ToString, objDFT_DS2_AdminDashboard.Content, "ItemId=" & objDFT_DS2_AdminDashboard.ItemId.ToString)
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

            Dim colDFT_DS2_AdminDashboards As List(Of DFT_DS2_AdminDashboardInfo) = GetDFT_DS2_AdminDashboards(ModuleID)
            If colDFT_DS2_AdminDashboards.Count <> 0 Then
                strXML += "<DFT_DS2_AdminDashboards>"
                Dim objDFT_DS2_AdminDashboard As DFT_DS2_AdminDashboardInfo
                For Each objDFT_DS2_AdminDashboard In colDFT_DS2_AdminDashboards
                    strXML += "<DFT_DS2_AdminDashboard>"
                    strXML += "<content>" & XMLEncode(objDFT_DS2_AdminDashboard.Content) & "</content>"
                    strXML += "</DFT_DS2_AdminDashboard>"
                Next
                strXML += "</DFT_DS2_AdminDashboards>"
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

            Dim xmlDFT_DS2_AdminDashboard As XmlNode
            Dim xmlDFT_DS2_AdminDashboards As XmlNode = GetContent(Content, "DFT_DS2_AdminDashboards")
            For Each xmlDFT_DS2_AdminDashboard In xmlDFT_DS2_AdminDashboards.SelectNodes("DFT_DS2_AdminDashboard")
                Dim objDFT_DS2_AdminDashboard As New DFT_DS2_AdminDashboardInfo
                objDFT_DS2_AdminDashboard.ModuleId = ModuleID
                objDFT_DS2_AdminDashboard.Content = xmlDFT_DS2_AdminDashboard.SelectSingleNode("content").InnerText
                objDFT_DS2_AdminDashboard.CreatedByUser = UserId
                AddDFT_DS2_AdminDashboard(objDFT_DS2_AdminDashboard)
            Next

        End Sub

#End Region

    End Class
End Namespace
