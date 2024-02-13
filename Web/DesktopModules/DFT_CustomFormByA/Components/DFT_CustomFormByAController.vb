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

Namespace YourCompany.Modules.DFT_CustomFormByA

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_CustomFormByA
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_CustomFormByAController
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
        Public Function GetDFT_CustomFormByAs(ByVal ModuleId As Integer) As List(Of DFT_CustomFormByAInfo)

            Return CBO.FillCollection(Of DFT_CustomFormByAInfo)(DataProvider.Instance().GetDFT_CustomFormByAs(ModuleId))

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
        Public Function GetDFT_CustomFormByA(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_CustomFormByAInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_CustomFormByA(ModuleId,ItemId), GetType(DFT_CustomFormByAInfo)), DFT_CustomFormByAInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_CustomFormByA">The DFT_CustomFormByAInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_CustomFormByA(ByVal objDFT_CustomFormByA As DFT_CustomFormByAInfo)

            If objDFT_CustomFormByA.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_CustomFormByA(objDFT_CustomFormByA.ModuleId, objDFT_CustomFormByA.Content, objDFT_CustomFormByA.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_CustomFormByA">The DFT_CustomFormByAInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_CustomFormByA(ByVal objDFT_CustomFormByA As DFT_CustomFormByAInfo)

            If objDFT_CustomFormByA.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_CustomFormByA(objDFT_CustomFormByA.ModuleId, objDFT_CustomFormByA.ItemId, objDFT_CustomFormByA.Content, objDFT_CustomFormByA.CreatedByUser)
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
        Public Sub DeleteDFT_CustomFormByA(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_CustomFormByA(ModuleId, ItemId)

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

            Dim colDFT_CustomFormByAs As List(Of DFT_CustomFormByAInfo) = GetDFT_CustomFormByAs(ModInfo.ModuleID)
            Dim objDFT_CustomFormByA As DFT_CustomFormByAInfo
            For Each objDFT_CustomFormByA In colDFT_CustomFormByAs
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_CustomFormByA.Content, objDFT_CustomFormByA.CreatedByUser, objDFT_CustomFormByA.CreatedDate, ModInfo.ModuleID, objDFT_CustomFormByA.ItemId.ToString, objDFT_CustomFormByA.Content, "ItemId=" & objDFT_CustomFormByA.ItemId.ToString)
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

            Dim colDFT_CustomFormByAs As List(Of DFT_CustomFormByAInfo) = GetDFT_CustomFormByAs(ModuleID)
            If colDFT_CustomFormByAs.Count <> 0 Then
                strXML += "<DFT_CustomFormByAs>"
                Dim objDFT_CustomFormByA As DFT_CustomFormByAInfo
                For Each objDFT_CustomFormByA In colDFT_CustomFormByAs
                    strXML += "<DFT_CustomFormByA>"
                    strXML += "<content>" & XMLEncode(objDFT_CustomFormByA.Content) & "</content>"
                    strXML += "</DFT_CustomFormByA>"
                Next
                strXML += "</DFT_CustomFormByAs>"
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

            Dim xmlDFT_CustomFormByA As XmlNode
            Dim xmlDFT_CustomFormByAs As XmlNode = GetContent(Content, "DFT_CustomFormByAs")
            For Each xmlDFT_CustomFormByA In xmlDFT_CustomFormByAs.SelectNodes("DFT_CustomFormByA")
                Dim objDFT_CustomFormByA As New DFT_CustomFormByAInfo
                objDFT_CustomFormByA.ModuleId = ModuleID
                objDFT_CustomFormByA.Content = xmlDFT_CustomFormByA.SelectSingleNode("content").InnerText
                objDFT_CustomFormByA.CreatedByUser = UserId
                AddDFT_CustomFormByA(objDFT_CustomFormByA)
            Next

        End Sub

#End Region

    End Class
End Namespace
