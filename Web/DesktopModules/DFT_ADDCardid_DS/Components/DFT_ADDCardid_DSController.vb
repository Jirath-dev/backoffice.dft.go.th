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

Namespace YourCompany.Modules.DFT_ADDCardid_DS

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_ADDCardid_DS
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_ADDCardid_DSController
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
        Public Function GetDFT_ADDCardid_DSs(ByVal ModuleId As Integer) As List(Of DFT_ADDCardid_DSInfo)

            Return CBO.FillCollection(Of DFT_ADDCardid_DSInfo)(DataProvider.Instance().GetDFT_ADDCardid_DSs(ModuleId))

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
        Public Function GetDFT_ADDCardid_DS(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_ADDCardid_DSInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_ADDCardid_DS(ModuleId,ItemId), GetType(DFT_ADDCardid_DSInfo)), DFT_ADDCardid_DSInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_ADDCardid_DS">The DFT_ADDCardid_DSInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_ADDCardid_DS(ByVal objDFT_ADDCardid_DS As DFT_ADDCardid_DSInfo)

            If objDFT_ADDCardid_DS.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_ADDCardid_DS(objDFT_ADDCardid_DS.ModuleId, objDFT_ADDCardid_DS.Content, objDFT_ADDCardid_DS.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_ADDCardid_DS">The DFT_ADDCardid_DSInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_ADDCardid_DS(ByVal objDFT_ADDCardid_DS As DFT_ADDCardid_DSInfo)

            If objDFT_ADDCardid_DS.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_ADDCardid_DS(objDFT_ADDCardid_DS.ModuleId, objDFT_ADDCardid_DS.ItemId, objDFT_ADDCardid_DS.Content, objDFT_ADDCardid_DS.CreatedByUser)
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
        Public Sub DeleteDFT_ADDCardid_DS(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_ADDCardid_DS(ModuleId, ItemId)

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

            Dim colDFT_ADDCardid_DSs As List(Of DFT_ADDCardid_DSInfo) = GetDFT_ADDCardid_DSs(ModInfo.ModuleID)
            Dim objDFT_ADDCardid_DS As DFT_ADDCardid_DSInfo
            For Each objDFT_ADDCardid_DS In colDFT_ADDCardid_DSs
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_ADDCardid_DS.Content, objDFT_ADDCardid_DS.CreatedByUser, objDFT_ADDCardid_DS.CreatedDate, ModInfo.ModuleID, objDFT_ADDCardid_DS.ItemId.ToString, objDFT_ADDCardid_DS.Content, "ItemId=" & objDFT_ADDCardid_DS.ItemId.ToString)
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

            Dim colDFT_ADDCardid_DSs As List(Of DFT_ADDCardid_DSInfo) = GetDFT_ADDCardid_DSs(ModuleID)
            If colDFT_ADDCardid_DSs.Count <> 0 Then
                strXML += "<DFT_ADDCardid_DSs>"
                Dim objDFT_ADDCardid_DS As DFT_ADDCardid_DSInfo
                For Each objDFT_ADDCardid_DS In colDFT_ADDCardid_DSs
                    strXML += "<DFT_ADDCardid_DS>"
                    strXML += "<content>" & XMLEncode(objDFT_ADDCardid_DS.Content) & "</content>"
                    strXML += "</DFT_ADDCardid_DS>"
                Next
                strXML += "</DFT_ADDCardid_DSs>"
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

            Dim xmlDFT_ADDCardid_DS As XmlNode
            Dim xmlDFT_ADDCardid_DSs As XmlNode = GetContent(Content, "DFT_ADDCardid_DSs")
            For Each xmlDFT_ADDCardid_DS In xmlDFT_ADDCardid_DSs.SelectNodes("DFT_ADDCardid_DS")
                Dim objDFT_ADDCardid_DS As New DFT_ADDCardid_DSInfo
                objDFT_ADDCardid_DS.ModuleId = ModuleID
                objDFT_ADDCardid_DS.Content = xmlDFT_ADDCardid_DS.SelectSingleNode("content").InnerText
                objDFT_ADDCardid_DS.CreatedByUser = UserId
                AddDFT_ADDCardid_DS(objDFT_ADDCardid_DS)
            Next

        End Sub

#End Region

    End Class
End Namespace
