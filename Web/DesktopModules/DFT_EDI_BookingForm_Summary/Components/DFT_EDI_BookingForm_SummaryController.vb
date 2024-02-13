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

Namespace NTi.Modules.DFT_EDI_BookingForm_Summary

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_EDI_BookingForm_Summary
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_EDI_BookingForm_SummaryController
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
        Public Function GetDFT_EDI_BookingForm_Summarys(ByVal ModuleId As Integer) As List(Of DFT_EDI_BookingForm_SummaryInfo)

            Return CBO.FillCollection(Of DFT_EDI_BookingForm_SummaryInfo)(DataProvider.Instance().GetDFT_EDI_BookingForm_Summarys(ModuleId))

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
        Public Function GetDFT_EDI_BookingForm_Summary(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_EDI_BookingForm_SummaryInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_EDI_BookingForm_Summary(ModuleId,ItemId), GetType(DFT_EDI_BookingForm_SummaryInfo)), DFT_EDI_BookingForm_SummaryInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_EDI_BookingForm_Summary">The DFT_EDI_BookingForm_SummaryInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_EDI_BookingForm_Summary(ByVal objDFT_EDI_BookingForm_Summary As DFT_EDI_BookingForm_SummaryInfo)

            If objDFT_EDI_BookingForm_Summary.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_EDI_BookingForm_Summary(objDFT_EDI_BookingForm_Summary.ModuleId, objDFT_EDI_BookingForm_Summary.Content, objDFT_EDI_BookingForm_Summary.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_EDI_BookingForm_Summary">The DFT_EDI_BookingForm_SummaryInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_EDI_BookingForm_Summary(ByVal objDFT_EDI_BookingForm_Summary As DFT_EDI_BookingForm_SummaryInfo)

            If objDFT_EDI_BookingForm_Summary.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_EDI_BookingForm_Summary(objDFT_EDI_BookingForm_Summary.ModuleId, objDFT_EDI_BookingForm_Summary.ItemId, objDFT_EDI_BookingForm_Summary.Content, objDFT_EDI_BookingForm_Summary.CreatedByUser)
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
        Public Sub DeleteDFT_EDI_BookingForm_Summary(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_EDI_BookingForm_Summary(ModuleId, ItemId)

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

            Dim colDFT_EDI_BookingForm_Summarys As List(Of DFT_EDI_BookingForm_SummaryInfo) = GetDFT_EDI_BookingForm_Summarys(ModInfo.ModuleID)
            Dim objDFT_EDI_BookingForm_Summary As DFT_EDI_BookingForm_SummaryInfo
            For Each objDFT_EDI_BookingForm_Summary In colDFT_EDI_BookingForm_Summarys
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_EDI_BookingForm_Summary.Content, objDFT_EDI_BookingForm_Summary.CreatedByUser, objDFT_EDI_BookingForm_Summary.CreatedDate, ModInfo.ModuleID, objDFT_EDI_BookingForm_Summary.ItemId.ToString, objDFT_EDI_BookingForm_Summary.Content, "ItemId=" & objDFT_EDI_BookingForm_Summary.ItemId.ToString)
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

            Dim colDFT_EDI_BookingForm_Summarys As List(Of DFT_EDI_BookingForm_SummaryInfo) = GetDFT_EDI_BookingForm_Summarys(ModuleID)
            If colDFT_EDI_BookingForm_Summarys.Count <> 0 Then
                strXML += "<DFT_EDI_BookingForm_Summarys>"
                Dim objDFT_EDI_BookingForm_Summary As DFT_EDI_BookingForm_SummaryInfo
                For Each objDFT_EDI_BookingForm_Summary In colDFT_EDI_BookingForm_Summarys
                    strXML += "<DFT_EDI_BookingForm_Summary>"
                    strXML += "<content>" & XMLEncode(objDFT_EDI_BookingForm_Summary.Content) & "</content>"
                    strXML += "</DFT_EDI_BookingForm_Summary>"
                Next
                strXML += "</DFT_EDI_BookingForm_Summarys>"
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

            Dim xmlDFT_EDI_BookingForm_Summary As XmlNode
            Dim xmlDFT_EDI_BookingForm_Summarys As XmlNode = GetContent(Content, "DFT_EDI_BookingForm_Summarys")
            For Each xmlDFT_EDI_BookingForm_Summary In xmlDFT_EDI_BookingForm_Summarys.SelectNodes("DFT_EDI_BookingForm_Summary")
                Dim objDFT_EDI_BookingForm_Summary As New DFT_EDI_BookingForm_SummaryInfo
                objDFT_EDI_BookingForm_Summary.ModuleId = ModuleID
                objDFT_EDI_BookingForm_Summary.Content = xmlDFT_EDI_BookingForm_Summary.SelectSingleNode("content").InnerText
                objDFT_EDI_BookingForm_Summary.CreatedByUser = UserId
                AddDFT_EDI_BookingForm_Summary(objDFT_EDI_BookingForm_Summary)
            Next

        End Sub

#End Region

    End Class
End Namespace
