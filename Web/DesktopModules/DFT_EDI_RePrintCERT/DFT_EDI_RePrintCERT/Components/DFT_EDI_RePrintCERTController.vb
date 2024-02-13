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

Namespace NTi.Modules.DFT_EDI_RePrintCERT

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_EDI_RePrintCERT
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_EDI_RePrintCERTController
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
        Public Function GetDFT_EDI_RePrintCERTs(ByVal ModuleId As Integer) As List(Of DFT_EDI_RePrintCERTInfo)

            Return CBO.FillCollection(Of DFT_EDI_RePrintCERTInfo)(DataProvider.Instance().GetDFT_EDI_RePrintCERTs(ModuleId))

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
        Public Function GetDFT_EDI_RePrintCERT(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_EDI_RePrintCERTInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_EDI_RePrintCERT(ModuleId,ItemId), GetType(DFT_EDI_RePrintCERTInfo)), DFT_EDI_RePrintCERTInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_EDI_RePrintCERT">The DFT_EDI_RePrintCERTInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_EDI_RePrintCERT(ByVal objDFT_EDI_RePrintCERT As DFT_EDI_RePrintCERTInfo)

            If objDFT_EDI_RePrintCERT.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_EDI_RePrintCERT(objDFT_EDI_RePrintCERT.ModuleId, objDFT_EDI_RePrintCERT.Content, objDFT_EDI_RePrintCERT.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_EDI_RePrintCERT">The DFT_EDI_RePrintCERTInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_EDI_RePrintCERT(ByVal objDFT_EDI_RePrintCERT As DFT_EDI_RePrintCERTInfo)

            If objDFT_EDI_RePrintCERT.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_EDI_RePrintCERT(objDFT_EDI_RePrintCERT.ModuleId, objDFT_EDI_RePrintCERT.ItemId, objDFT_EDI_RePrintCERT.Content, objDFT_EDI_RePrintCERT.CreatedByUser)
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
        Public Sub DeleteDFT_EDI_RePrintCERT(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_EDI_RePrintCERT(ModuleId, ItemId)

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

            Dim colDFT_EDI_RePrintCERTs As List(Of DFT_EDI_RePrintCERTInfo) = GetDFT_EDI_RePrintCERTs(ModInfo.ModuleID)
            Dim objDFT_EDI_RePrintCERT As DFT_EDI_RePrintCERTInfo
            For Each objDFT_EDI_RePrintCERT In colDFT_EDI_RePrintCERTs
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_EDI_RePrintCERT.Content, objDFT_EDI_RePrintCERT.CreatedByUser, objDFT_EDI_RePrintCERT.CreatedDate, ModInfo.ModuleID, objDFT_EDI_RePrintCERT.ItemId.ToString, objDFT_EDI_RePrintCERT.Content, "ItemId=" & objDFT_EDI_RePrintCERT.ItemId.ToString)
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

            Dim colDFT_EDI_RePrintCERTs As List(Of DFT_EDI_RePrintCERTInfo) = GetDFT_EDI_RePrintCERTs(ModuleID)
            If colDFT_EDI_RePrintCERTs.Count <> 0 Then
                strXML += "<DFT_EDI_RePrintCERTs>"
                Dim objDFT_EDI_RePrintCERT As DFT_EDI_RePrintCERTInfo
                For Each objDFT_EDI_RePrintCERT In colDFT_EDI_RePrintCERTs
                    strXML += "<DFT_EDI_RePrintCERT>"
                    strXML += "<content>" & XMLEncode(objDFT_EDI_RePrintCERT.Content) & "</content>"
                    strXML += "</DFT_EDI_RePrintCERT>"
                Next
                strXML += "</DFT_EDI_RePrintCERTs>"
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

            Dim xmlDFT_EDI_RePrintCERT As XmlNode
            Dim xmlDFT_EDI_RePrintCERTs As XmlNode = GetContent(Content, "DFT_EDI_RePrintCERTs")
            For Each xmlDFT_EDI_RePrintCERT In xmlDFT_EDI_RePrintCERTs.SelectNodes("DFT_EDI_RePrintCERT")
                Dim objDFT_EDI_RePrintCERT As New DFT_EDI_RePrintCERTInfo
                objDFT_EDI_RePrintCERT.ModuleId = ModuleID
                objDFT_EDI_RePrintCERT.Content = xmlDFT_EDI_RePrintCERT.SelectSingleNode("content").InnerText
                objDFT_EDI_RePrintCERT.CreatedByUser = UserId
                AddDFT_EDI_RePrintCERT(objDFT_EDI_RePrintCERT)
            Next

        End Sub

#End Region

    End Class
End Namespace
