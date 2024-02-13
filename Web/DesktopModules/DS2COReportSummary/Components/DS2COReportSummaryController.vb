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

Namespace YourCompany.Modules.DS2COReportSummary

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DS2COReportSummary
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DS2COReportSummaryController
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
        Public Function GetDS2COReportSummarys(ByVal ModuleId As Integer) As List(Of DS2COReportSummaryInfo)

            Return CBO.FillCollection(Of DS2COReportSummaryInfo)(DataProvider.Instance().GetDS2COReportSummarys(ModuleId))

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
        Public Function GetDS2COReportSummary(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DS2COReportSummaryInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDS2COReportSummary(ModuleId,ItemId), GetType(DS2COReportSummaryInfo)), DS2COReportSummaryInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDS2COReportSummary">The DS2COReportSummaryInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDS2COReportSummary(ByVal objDS2COReportSummary As DS2COReportSummaryInfo)

            If objDS2COReportSummary.Content.Trim <> "" Then
		DataProvider.Instance().AddDS2COReportSummary(objDS2COReportSummary.ModuleId, objDS2COReportSummary.Content, objDS2COReportSummary.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDS2COReportSummary">The DS2COReportSummaryInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDS2COReportSummary(ByVal objDS2COReportSummary As DS2COReportSummaryInfo)

            If objDS2COReportSummary.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDS2COReportSummary(objDS2COReportSummary.ModuleId, objDS2COReportSummary.ItemId, objDS2COReportSummary.Content, objDS2COReportSummary.CreatedByUser)
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
        Public Sub DeleteDS2COReportSummary(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDS2COReportSummary(ModuleId, ItemId)

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

            Dim colDS2COReportSummarys As List(Of DS2COReportSummaryInfo) = GetDS2COReportSummarys(ModInfo.ModuleID)
            Dim objDS2COReportSummary As DS2COReportSummaryInfo
            For Each objDS2COReportSummary In colDS2COReportSummarys
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDS2COReportSummary.Content, objDS2COReportSummary.CreatedByUser, objDS2COReportSummary.CreatedDate, ModInfo.ModuleID, objDS2COReportSummary.ItemId.ToString, objDS2COReportSummary.Content, "ItemId=" & objDS2COReportSummary.ItemId.ToString)
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

            Dim colDS2COReportSummarys As List(Of DS2COReportSummaryInfo) = GetDS2COReportSummarys(ModuleID)
            If colDS2COReportSummarys.Count <> 0 Then
                strXML += "<DS2COReportSummarys>"
                Dim objDS2COReportSummary As DS2COReportSummaryInfo
                For Each objDS2COReportSummary In colDS2COReportSummarys
                    strXML += "<DS2COReportSummary>"
                    strXML += "<content>" & XMLEncode(objDS2COReportSummary.Content) & "</content>"
                    strXML += "</DS2COReportSummary>"
                Next
                strXML += "</DS2COReportSummarys>"
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

            Dim xmlDS2COReportSummary As XmlNode
            Dim xmlDS2COReportSummarys As XmlNode = GetContent(Content, "DS2COReportSummarys")
            For Each xmlDS2COReportSummary In xmlDS2COReportSummarys.SelectNodes("DS2COReportSummary")
                Dim objDS2COReportSummary As New DS2COReportSummaryInfo
                objDS2COReportSummary.ModuleId = ModuleID
                objDS2COReportSummary.Content = xmlDS2COReportSummary.SelectSingleNode("content").InnerText
                objDS2COReportSummary.CreatedByUser = UserId
                AddDS2COReportSummary(objDS2COReportSummary)
            Next

        End Sub

#End Region

    End Class
End Namespace
