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

Namespace YourCompany.Modules.DFT_AddCOTax

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_AddCOTax
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_AddCOTaxController
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
        Public Function GetDFT_AddCOTaxs(ByVal ModuleId As Integer) As List(Of DFT_AddCOTaxInfo)

            Return CBO.FillCollection(Of DFT_AddCOTaxInfo)(DataProvider.Instance().GetDFT_AddCOTaxs(ModuleId))

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
        Public Function GetDFT_AddCOTax(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_AddCOTaxInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_AddCOTax(ModuleId,ItemId), GetType(DFT_AddCOTaxInfo)), DFT_AddCOTaxInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_AddCOTax">The DFT_AddCOTaxInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_AddCOTax(ByVal objDFT_AddCOTax As DFT_AddCOTaxInfo)

            If objDFT_AddCOTax.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_AddCOTax(objDFT_AddCOTax.ModuleId, objDFT_AddCOTax.Content, objDFT_AddCOTax.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_AddCOTax">The DFT_AddCOTaxInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_AddCOTax(ByVal objDFT_AddCOTax As DFT_AddCOTaxInfo)

            If objDFT_AddCOTax.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_AddCOTax(objDFT_AddCOTax.ModuleId, objDFT_AddCOTax.ItemId, objDFT_AddCOTax.Content, objDFT_AddCOTax.CreatedByUser)
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
        Public Sub DeleteDFT_AddCOTax(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_AddCOTax(ModuleId, ItemId)

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

            Dim colDFT_AddCOTaxs As List(Of DFT_AddCOTaxInfo) = GetDFT_AddCOTaxs(ModInfo.ModuleID)
            Dim objDFT_AddCOTax As DFT_AddCOTaxInfo
            For Each objDFT_AddCOTax In colDFT_AddCOTaxs
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_AddCOTax.Content, objDFT_AddCOTax.CreatedByUser, objDFT_AddCOTax.CreatedDate, ModInfo.ModuleID, objDFT_AddCOTax.ItemId.ToString, objDFT_AddCOTax.Content, "ItemId=" & objDFT_AddCOTax.ItemId.ToString)
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

            Dim colDFT_AddCOTaxs As List(Of DFT_AddCOTaxInfo) = GetDFT_AddCOTaxs(ModuleID)
            If colDFT_AddCOTaxs.Count <> 0 Then
                strXML += "<DFT_AddCOTaxs>"
                Dim objDFT_AddCOTax As DFT_AddCOTaxInfo
                For Each objDFT_AddCOTax In colDFT_AddCOTaxs
                    strXML += "<DFT_AddCOTax>"
                    strXML += "<content>" & XMLEncode(objDFT_AddCOTax.Content) & "</content>"
                    strXML += "</DFT_AddCOTax>"
                Next
                strXML += "</DFT_AddCOTaxs>"
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

            Dim xmlDFT_AddCOTax As XmlNode
            Dim xmlDFT_AddCOTaxs As XmlNode = GetContent(Content, "DFT_AddCOTaxs")
            For Each xmlDFT_AddCOTax In xmlDFT_AddCOTaxs.SelectNodes("DFT_AddCOTax")
                Dim objDFT_AddCOTax As New DFT_AddCOTaxInfo
                objDFT_AddCOTax.ModuleId = ModuleID
                objDFT_AddCOTax.Content = xmlDFT_AddCOTax.SelectSingleNode("content").InnerText
                objDFT_AddCOTax.CreatedByUser = UserId
                AddDFT_AddCOTax(objDFT_AddCOTax)
            Next

        End Sub

#End Region

    End Class
End Namespace
