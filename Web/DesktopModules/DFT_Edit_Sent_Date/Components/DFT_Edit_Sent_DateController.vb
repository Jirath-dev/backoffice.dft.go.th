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

Namespace YourCompany.Modules.DFT_Edit_Sent_Date

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_Edit_Sent_Date
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_Edit_Sent_DateController
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
        Public Function GetDFT_Edit_Sent_Dates(ByVal ModuleId As Integer) As List(Of DFT_Edit_Sent_DateInfo)

            Return CBO.FillCollection(Of DFT_Edit_Sent_DateInfo)(DataProvider.Instance().GetDFT_Edit_Sent_Dates(ModuleId))

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
        Public Function GetDFT_Edit_Sent_Date(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_Edit_Sent_DateInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_Edit_Sent_Date(ModuleId,ItemId), GetType(DFT_Edit_Sent_DateInfo)), DFT_Edit_Sent_DateInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_Edit_Sent_Date">The DFT_Edit_Sent_DateInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_Edit_Sent_Date(ByVal objDFT_Edit_Sent_Date As DFT_Edit_Sent_DateInfo)

            If objDFT_Edit_Sent_Date.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_Edit_Sent_Date(objDFT_Edit_Sent_Date.ModuleId, objDFT_Edit_Sent_Date.Content, objDFT_Edit_Sent_Date.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_Edit_Sent_Date">The DFT_Edit_Sent_DateInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_Edit_Sent_Date(ByVal objDFT_Edit_Sent_Date As DFT_Edit_Sent_DateInfo)

            If objDFT_Edit_Sent_Date.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_Edit_Sent_Date(objDFT_Edit_Sent_Date.ModuleId, objDFT_Edit_Sent_Date.ItemId, objDFT_Edit_Sent_Date.Content, objDFT_Edit_Sent_Date.CreatedByUser)
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
        Public Sub DeleteDFT_Edit_Sent_Date(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_Edit_Sent_Date(ModuleId, ItemId)

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

            Dim colDFT_Edit_Sent_Dates As List(Of DFT_Edit_Sent_DateInfo) = GetDFT_Edit_Sent_Dates(ModInfo.ModuleID)
            Dim objDFT_Edit_Sent_Date As DFT_Edit_Sent_DateInfo
            For Each objDFT_Edit_Sent_Date In colDFT_Edit_Sent_Dates
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_Edit_Sent_Date.Content, objDFT_Edit_Sent_Date.CreatedByUser, objDFT_Edit_Sent_Date.CreatedDate, ModInfo.ModuleID, objDFT_Edit_Sent_Date.ItemId.ToString, objDFT_Edit_Sent_Date.Content, "ItemId=" & objDFT_Edit_Sent_Date.ItemId.ToString)
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

            Dim colDFT_Edit_Sent_Dates As List(Of DFT_Edit_Sent_DateInfo) = GetDFT_Edit_Sent_Dates(ModuleID)
            If colDFT_Edit_Sent_Dates.Count <> 0 Then
                strXML += "<DFT_Edit_Sent_Dates>"
                Dim objDFT_Edit_Sent_Date As DFT_Edit_Sent_DateInfo
                For Each objDFT_Edit_Sent_Date In colDFT_Edit_Sent_Dates
                    strXML += "<DFT_Edit_Sent_Date>"
                    strXML += "<content>" & XMLEncode(objDFT_Edit_Sent_Date.Content) & "</content>"
                    strXML += "</DFT_Edit_Sent_Date>"
                Next
                strXML += "</DFT_Edit_Sent_Dates>"
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

            Dim xmlDFT_Edit_Sent_Date As XmlNode
            Dim xmlDFT_Edit_Sent_Dates As XmlNode = GetContent(Content, "DFT_Edit_Sent_Dates")
            For Each xmlDFT_Edit_Sent_Date In xmlDFT_Edit_Sent_Dates.SelectNodes("DFT_Edit_Sent_Date")
                Dim objDFT_Edit_Sent_Date As New DFT_Edit_Sent_DateInfo
                objDFT_Edit_Sent_Date.ModuleId = ModuleID
                objDFT_Edit_Sent_Date.Content = xmlDFT_Edit_Sent_Date.SelectSingleNode("content").InnerText
                objDFT_Edit_Sent_Date.CreatedByUser = UserId
                AddDFT_Edit_Sent_Date(objDFT_Edit_Sent_Date)
            Next

        End Sub

#End Region

    End Class
End Namespace
