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

Namespace YourCompany.Modules.DFT_Report_Receipt_ByPro

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_Report_Receipt_ByPro
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_Report_Receipt_ByProController
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
        Public Function GetDFT_Report_Receipt_ByPros(ByVal ModuleId As Integer) As List(Of DFT_Report_Receipt_ByProInfo)

            Return CBO.FillCollection(Of DFT_Report_Receipt_ByProInfo)(DataProvider.Instance().GetDFT_Report_Receipt_ByPros(ModuleId))

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
        Public Function GetDFT_Report_Receipt_ByPro(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_Report_Receipt_ByProInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_Report_Receipt_ByPro(ModuleId,ItemId), GetType(DFT_Report_Receipt_ByProInfo)), DFT_Report_Receipt_ByProInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_Report_Receipt_ByPro">The DFT_Report_Receipt_ByProInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_Report_Receipt_ByPro(ByVal objDFT_Report_Receipt_ByPro As DFT_Report_Receipt_ByProInfo)

            If objDFT_Report_Receipt_ByPro.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_Report_Receipt_ByPro(objDFT_Report_Receipt_ByPro.ModuleId, objDFT_Report_Receipt_ByPro.Content, objDFT_Report_Receipt_ByPro.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_Report_Receipt_ByPro">The DFT_Report_Receipt_ByProInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_Report_Receipt_ByPro(ByVal objDFT_Report_Receipt_ByPro As DFT_Report_Receipt_ByProInfo)

            If objDFT_Report_Receipt_ByPro.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_Report_Receipt_ByPro(objDFT_Report_Receipt_ByPro.ModuleId, objDFT_Report_Receipt_ByPro.ItemId, objDFT_Report_Receipt_ByPro.Content, objDFT_Report_Receipt_ByPro.CreatedByUser)
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
        Public Sub DeleteDFT_Report_Receipt_ByPro(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_Report_Receipt_ByPro(ModuleId, ItemId)

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

            Dim colDFT_Report_Receipt_ByPros As List(Of DFT_Report_Receipt_ByProInfo) = GetDFT_Report_Receipt_ByPros(ModInfo.ModuleID)
            Dim objDFT_Report_Receipt_ByPro As DFT_Report_Receipt_ByProInfo
            For Each objDFT_Report_Receipt_ByPro In colDFT_Report_Receipt_ByPros
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_Report_Receipt_ByPro.Content, objDFT_Report_Receipt_ByPro.CreatedByUser, objDFT_Report_Receipt_ByPro.CreatedDate, ModInfo.ModuleID, objDFT_Report_Receipt_ByPro.ItemId.ToString, objDFT_Report_Receipt_ByPro.Content, "ItemId=" & objDFT_Report_Receipt_ByPro.ItemId.ToString)
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

            Dim colDFT_Report_Receipt_ByPros As List(Of DFT_Report_Receipt_ByProInfo) = GetDFT_Report_Receipt_ByPros(ModuleID)
            If colDFT_Report_Receipt_ByPros.Count <> 0 Then
                strXML += "<DFT_Report_Receipt_ByPros>"
                Dim objDFT_Report_Receipt_ByPro As DFT_Report_Receipt_ByProInfo
                For Each objDFT_Report_Receipt_ByPro In colDFT_Report_Receipt_ByPros
                    strXML += "<DFT_Report_Receipt_ByPro>"
                    strXML += "<content>" & XMLEncode(objDFT_Report_Receipt_ByPro.Content) & "</content>"
                    strXML += "</DFT_Report_Receipt_ByPro>"
                Next
                strXML += "</DFT_Report_Receipt_ByPros>"
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

            Dim xmlDFT_Report_Receipt_ByPro As XmlNode
            Dim xmlDFT_Report_Receipt_ByPros As XmlNode = GetContent(Content, "DFT_Report_Receipt_ByPros")
            For Each xmlDFT_Report_Receipt_ByPro In xmlDFT_Report_Receipt_ByPros.SelectNodes("DFT_Report_Receipt_ByPro")
                Dim objDFT_Report_Receipt_ByPro As New DFT_Report_Receipt_ByProInfo
                objDFT_Report_Receipt_ByPro.ModuleId = ModuleID
                objDFT_Report_Receipt_ByPro.Content = xmlDFT_Report_Receipt_ByPro.SelectSingleNode("content").InnerText
                objDFT_Report_Receipt_ByPro.CreatedByUser = UserId
                AddDFT_Report_Receipt_ByPro(objDFT_Report_Receipt_ByPro)
            Next

        End Sub

#End Region

    End Class
End Namespace
