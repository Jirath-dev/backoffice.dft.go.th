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

Namespace NTi.Modules.DFT_Tariff_ECO

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_Tariff_ECO
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_Tariff_ECOController
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
        Public Function GetDFT_Tariff_ECOs(ByVal ModuleId As Integer) As List(Of DFT_Tariff_ECOInfo)

            Return CBO.FillCollection(Of DFT_Tariff_ECOInfo)(DataProvider.Instance().GetDFT_Tariff_ECOs(ModuleId))

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
        Public Function GetDFT_Tariff_ECO(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_Tariff_ECOInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_Tariff_ECO(ModuleId,ItemId), GetType(DFT_Tariff_ECOInfo)), DFT_Tariff_ECOInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_Tariff_ECO">The DFT_Tariff_ECOInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_Tariff_ECO(ByVal objDFT_Tariff_ECO As DFT_Tariff_ECOInfo)

            If objDFT_Tariff_ECO.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_Tariff_ECO(objDFT_Tariff_ECO.ModuleId, objDFT_Tariff_ECO.Content, objDFT_Tariff_ECO.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_Tariff_ECO">The DFT_Tariff_ECOInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_Tariff_ECO(ByVal objDFT_Tariff_ECO As DFT_Tariff_ECOInfo)

            If objDFT_Tariff_ECO.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_Tariff_ECO(objDFT_Tariff_ECO.ModuleId, objDFT_Tariff_ECO.ItemId, objDFT_Tariff_ECO.Content, objDFT_Tariff_ECO.CreatedByUser)
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
        Public Sub DeleteDFT_Tariff_ECO(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_Tariff_ECO(ModuleId, ItemId)

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

            Dim colDFT_Tariff_ECOs As List(Of DFT_Tariff_ECOInfo) = GetDFT_Tariff_ECOs(ModInfo.ModuleID)
            Dim objDFT_Tariff_ECO As DFT_Tariff_ECOInfo
            For Each objDFT_Tariff_ECO In colDFT_Tariff_ECOs
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_Tariff_ECO.Content, objDFT_Tariff_ECO.CreatedByUser, objDFT_Tariff_ECO.CreatedDate, ModInfo.ModuleID, objDFT_Tariff_ECO.ItemId.ToString, objDFT_Tariff_ECO.Content, "ItemId=" & objDFT_Tariff_ECO.ItemId.ToString)
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

            Dim colDFT_Tariff_ECOs As List(Of DFT_Tariff_ECOInfo) = GetDFT_Tariff_ECOs(ModuleID)
            If colDFT_Tariff_ECOs.Count <> 0 Then
                strXML += "<DFT_Tariff_ECOs>"
                Dim objDFT_Tariff_ECO As DFT_Tariff_ECOInfo
                For Each objDFT_Tariff_ECO In colDFT_Tariff_ECOs
                    strXML += "<DFT_Tariff_ECO>"
                    strXML += "<content>" & XMLEncode(objDFT_Tariff_ECO.Content) & "</content>"
                    strXML += "</DFT_Tariff_ECO>"
                Next
                strXML += "</DFT_Tariff_ECOs>"
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

            Dim xmlDFT_Tariff_ECO As XmlNode
            Dim xmlDFT_Tariff_ECOs As XmlNode = GetContent(Content, "DFT_Tariff_ECOs")
            For Each xmlDFT_Tariff_ECO In xmlDFT_Tariff_ECOs.SelectNodes("DFT_Tariff_ECO")
                Dim objDFT_Tariff_ECO As New DFT_Tariff_ECOInfo
                objDFT_Tariff_ECO.ModuleId = ModuleID
                objDFT_Tariff_ECO.Content = xmlDFT_Tariff_ECO.SelectSingleNode("content").InnerText
                objDFT_Tariff_ECO.CreatedByUser = UserId
                AddDFT_Tariff_ECO(objDFT_Tariff_ECO)
            Next

        End Sub

#End Region

    End Class
End Namespace
