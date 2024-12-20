'
' DotNetNukeŽ - http://www.dotnetnuke.com
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

Namespace NTi.Modules.DFT_EDI2B_Report_01

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_EDI2B_Report_01
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_EDI2B_Report_01Controller
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
        Public Function GetDFT_EDI2B_Report_01s(ByVal ModuleId As Integer) As List(Of DFT_EDI2B_Report_01Info)

            Return CBO.FillCollection(Of DFT_EDI2B_Report_01Info)(DataProvider.Instance().GetDFT_EDI2B_Report_01s(ModuleId))

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
        Public Function GetDFT_EDI2B_Report_01(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_EDI2B_Report_01Info

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_EDI2B_Report_01(ModuleId,ItemId), GetType(DFT_EDI2B_Report_01Info)), DFT_EDI2B_Report_01Info)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_EDI2B_Report_01">The DFT_EDI2B_Report_01Info object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_EDI2B_Report_01(ByVal objDFT_EDI2B_Report_01 As DFT_EDI2B_Report_01Info)

            If objDFT_EDI2B_Report_01.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_EDI2B_Report_01(objDFT_EDI2B_Report_01.ModuleId, objDFT_EDI2B_Report_01.Content, objDFT_EDI2B_Report_01.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_EDI2B_Report_01">The DFT_EDI2B_Report_01Info object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_EDI2B_Report_01(ByVal objDFT_EDI2B_Report_01 As DFT_EDI2B_Report_01Info)

            If objDFT_EDI2B_Report_01.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_EDI2B_Report_01(objDFT_EDI2B_Report_01.ModuleId, objDFT_EDI2B_Report_01.ItemId, objDFT_EDI2B_Report_01.Content, objDFT_EDI2B_Report_01.CreatedByUser)
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
        Public Sub DeleteDFT_EDI2B_Report_01(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_EDI2B_Report_01(ModuleId, ItemId)

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

            Dim colDFT_EDI2B_Report_01s As List(Of DFT_EDI2B_Report_01Info) = GetDFT_EDI2B_Report_01s(ModInfo.ModuleID)
            Dim objDFT_EDI2B_Report_01 As DFT_EDI2B_Report_01Info
            For Each objDFT_EDI2B_Report_01 In colDFT_EDI2B_Report_01s
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_EDI2B_Report_01.Content, objDFT_EDI2B_Report_01.CreatedByUser, objDFT_EDI2B_Report_01.CreatedDate, ModInfo.ModuleID, objDFT_EDI2B_Report_01.ItemId.ToString, objDFT_EDI2B_Report_01.Content, "ItemId=" & objDFT_EDI2B_Report_01.ItemId.ToString)
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

            Dim colDFT_EDI2B_Report_01s As List(Of DFT_EDI2B_Report_01Info) = GetDFT_EDI2B_Report_01s(ModuleID)
            If colDFT_EDI2B_Report_01s.Count <> 0 Then
                strXML += "<DFT_EDI2B_Report_01s>"
                Dim objDFT_EDI2B_Report_01 As DFT_EDI2B_Report_01Info
                For Each objDFT_EDI2B_Report_01 In colDFT_EDI2B_Report_01s
                    strXML += "<DFT_EDI2B_Report_01>"
                    strXML += "<content>" & XMLEncode(objDFT_EDI2B_Report_01.Content) & "</content>"
                    strXML += "</DFT_EDI2B_Report_01>"
                Next
                strXML += "</DFT_EDI2B_Report_01s>"
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

            Dim xmlDFT_EDI2B_Report_01 As XmlNode
            Dim xmlDFT_EDI2B_Report_01s As XmlNode = GetContent(Content, "DFT_EDI2B_Report_01s")
            For Each xmlDFT_EDI2B_Report_01 In xmlDFT_EDI2B_Report_01s.SelectNodes("DFT_EDI2B_Report_01")
                Dim objDFT_EDI2B_Report_01 As New DFT_EDI2B_Report_01Info
                objDFT_EDI2B_Report_01.ModuleId = ModuleID
                objDFT_EDI2B_Report_01.Content = xmlDFT_EDI2B_Report_01.SelectSingleNode("content").InnerText
                objDFT_EDI2B_Report_01.CreatedByUser = UserId
                AddDFT_EDI2B_Report_01(objDFT_EDI2B_Report_01)
            Next

        End Sub

#End Region

    End Class
End Namespace
