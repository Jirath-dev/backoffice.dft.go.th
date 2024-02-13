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

Namespace YourCompany.Modules.DFT_SearchPhone

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_SearchPhone
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_SearchPhoneController
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
        Public Function GetDFT_SearchPhones(ByVal ModuleId As Integer) As List(Of DFT_SearchPhoneInfo)

            Return CBO.FillCollection(Of DFT_SearchPhoneInfo)(DataProvider.Instance().GetDFT_SearchPhones(ModuleId))

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
        Public Function GetDFT_SearchPhone(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_SearchPhoneInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_SearchPhone(ModuleId,ItemId), GetType(DFT_SearchPhoneInfo)), DFT_SearchPhoneInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_SearchPhone">The DFT_SearchPhoneInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_SearchPhone(ByVal objDFT_SearchPhone As DFT_SearchPhoneInfo)

            If objDFT_SearchPhone.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_SearchPhone(objDFT_SearchPhone.ModuleId, objDFT_SearchPhone.Content, objDFT_SearchPhone.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_SearchPhone">The DFT_SearchPhoneInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_SearchPhone(ByVal objDFT_SearchPhone As DFT_SearchPhoneInfo)

            If objDFT_SearchPhone.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_SearchPhone(objDFT_SearchPhone.ModuleId, objDFT_SearchPhone.ItemId, objDFT_SearchPhone.Content, objDFT_SearchPhone.CreatedByUser)
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
        Public Sub DeleteDFT_SearchPhone(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_SearchPhone(ModuleId, ItemId)

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

            Dim colDFT_SearchPhones As List(Of DFT_SearchPhoneInfo) = GetDFT_SearchPhones(ModInfo.ModuleID)
            Dim objDFT_SearchPhone As DFT_SearchPhoneInfo
            For Each objDFT_SearchPhone In colDFT_SearchPhones
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_SearchPhone.Content, objDFT_SearchPhone.CreatedByUser, objDFT_SearchPhone.CreatedDate, ModInfo.ModuleID, objDFT_SearchPhone.ItemId.ToString, objDFT_SearchPhone.Content, "ItemId=" & objDFT_SearchPhone.ItemId.ToString)
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

            Dim colDFT_SearchPhones As List(Of DFT_SearchPhoneInfo) = GetDFT_SearchPhones(ModuleID)
            If colDFT_SearchPhones.Count <> 0 Then
                strXML += "<DFT_SearchPhones>"
                Dim objDFT_SearchPhone As DFT_SearchPhoneInfo
                For Each objDFT_SearchPhone In colDFT_SearchPhones
                    strXML += "<DFT_SearchPhone>"
                    strXML += "<content>" & XMLEncode(objDFT_SearchPhone.Content) & "</content>"
                    strXML += "</DFT_SearchPhone>"
                Next
                strXML += "</DFT_SearchPhones>"
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

            Dim xmlDFT_SearchPhone As XmlNode
            Dim xmlDFT_SearchPhones As XmlNode = GetContent(Content, "DFT_SearchPhones")
            For Each xmlDFT_SearchPhone In xmlDFT_SearchPhones.SelectNodes("DFT_SearchPhone")
                Dim objDFT_SearchPhone As New DFT_SearchPhoneInfo
                objDFT_SearchPhone.ModuleId = ModuleID
                objDFT_SearchPhone.Content = xmlDFT_SearchPhone.SelectSingleNode("content").InnerText
                objDFT_SearchPhone.CreatedByUser = UserId
                AddDFT_SearchPhone(objDFT_SearchPhone)
            Next

        End Sub

#End Region

    End Class
End Namespace
