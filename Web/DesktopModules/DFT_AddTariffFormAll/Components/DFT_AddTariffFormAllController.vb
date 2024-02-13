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

Namespace YourCompany.Modules.DFT_AddTariffFormAll

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_AddTariffFormAll
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_AddTariffFormAllController
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
        Public Function GetDFT_AddTariffFormAlls(ByVal ModuleId As Integer) As List(Of DFT_AddTariffFormAllInfo)

            Return CBO.FillCollection(Of DFT_AddTariffFormAllInfo)(DataProvider.Instance().GetDFT_AddTariffFormAlls(ModuleId))

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
        Public Function GetDFT_AddTariffFormAll(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_AddTariffFormAllInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_AddTariffFormAll(ModuleId,ItemId), GetType(DFT_AddTariffFormAllInfo)), DFT_AddTariffFormAllInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_AddTariffFormAll">The DFT_AddTariffFormAllInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_AddTariffFormAll(ByVal objDFT_AddTariffFormAll As DFT_AddTariffFormAllInfo)

            If objDFT_AddTariffFormAll.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_AddTariffFormAll(objDFT_AddTariffFormAll.ModuleId, objDFT_AddTariffFormAll.Content, objDFT_AddTariffFormAll.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_AddTariffFormAll">The DFT_AddTariffFormAllInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_AddTariffFormAll(ByVal objDFT_AddTariffFormAll As DFT_AddTariffFormAllInfo)

            If objDFT_AddTariffFormAll.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_AddTariffFormAll(objDFT_AddTariffFormAll.ModuleId, objDFT_AddTariffFormAll.ItemId, objDFT_AddTariffFormAll.Content, objDFT_AddTariffFormAll.CreatedByUser)
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
        Public Sub DeleteDFT_AddTariffFormAll(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_AddTariffFormAll(ModuleId, ItemId)

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

            Dim colDFT_AddTariffFormAlls As List(Of DFT_AddTariffFormAllInfo) = GetDFT_AddTariffFormAlls(ModInfo.ModuleID)
            Dim objDFT_AddTariffFormAll As DFT_AddTariffFormAllInfo
            For Each objDFT_AddTariffFormAll In colDFT_AddTariffFormAlls
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_AddTariffFormAll.Content, objDFT_AddTariffFormAll.CreatedByUser, objDFT_AddTariffFormAll.CreatedDate, ModInfo.ModuleID, objDFT_AddTariffFormAll.ItemId.ToString, objDFT_AddTariffFormAll.Content, "ItemId=" & objDFT_AddTariffFormAll.ItemId.ToString)
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

            Dim colDFT_AddTariffFormAlls As List(Of DFT_AddTariffFormAllInfo) = GetDFT_AddTariffFormAlls(ModuleID)
            If colDFT_AddTariffFormAlls.Count <> 0 Then
                strXML += "<DFT_AddTariffFormAlls>"
                Dim objDFT_AddTariffFormAll As DFT_AddTariffFormAllInfo
                For Each objDFT_AddTariffFormAll In colDFT_AddTariffFormAlls
                    strXML += "<DFT_AddTariffFormAll>"
                    strXML += "<content>" & XMLEncode(objDFT_AddTariffFormAll.Content) & "</content>"
                    strXML += "</DFT_AddTariffFormAll>"
                Next
                strXML += "</DFT_AddTariffFormAlls>"
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

            Dim xmlDFT_AddTariffFormAll As XmlNode
            Dim xmlDFT_AddTariffFormAlls As XmlNode = GetContent(Content, "DFT_AddTariffFormAlls")
            For Each xmlDFT_AddTariffFormAll In xmlDFT_AddTariffFormAlls.SelectNodes("DFT_AddTariffFormAll")
                Dim objDFT_AddTariffFormAll As New DFT_AddTariffFormAllInfo
                objDFT_AddTariffFormAll.ModuleId = ModuleID
                objDFT_AddTariffFormAll.Content = xmlDFT_AddTariffFormAll.SelectSingleNode("content").InnerText
                objDFT_AddTariffFormAll.CreatedByUser = UserId
                AddDFT_AddTariffFormAll(objDFT_AddTariffFormAll)
            Next

        End Sub

#End Region

    End Class
End Namespace
