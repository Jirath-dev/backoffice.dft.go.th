'
' DotNetNuke® - http://www.dotnetnuke.com
' Copyright (c) 2002-2005
' by Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
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

Namespace Modules.DFT_PrinterSetting

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_PrinterSetting
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_PrinterSettingController
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
        Public Function GetDFT_PrinterSettings(ByVal ModuleId As Integer) As List(Of DFT_PrinterSettingInfo)

            Return CBO.FillCollection(Of DFT_PrinterSettingInfo)(DataProvider.Instance().GetDFT_PrinterSettings(ModuleId))

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
        Public Function GetDFT_PrinterSetting(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_PrinterSettingInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_PrinterSetting(ModuleId,ItemId), GetType(DFT_PrinterSettingInfo)), DFT_PrinterSettingInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_PrinterSetting">The DFT_PrinterSettingInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_PrinterSetting(ByVal objDFT_PrinterSetting As DFT_PrinterSettingInfo)

            If objDFT_PrinterSetting.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_PrinterSetting(objDFT_PrinterSetting.ModuleId, objDFT_PrinterSetting.Content, objDFT_PrinterSetting.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_PrinterSetting">The DFT_PrinterSettingInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_PrinterSetting(ByVal objDFT_PrinterSetting As DFT_PrinterSettingInfo)

            If objDFT_PrinterSetting.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_PrinterSetting(objDFT_PrinterSetting.ModuleId, objDFT_PrinterSetting.ItemId, objDFT_PrinterSetting.Content, objDFT_PrinterSetting.CreatedByUser)
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
        Public Sub DeleteDFT_PrinterSetting(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_PrinterSetting(ModuleId, ItemId)

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

            Dim colDFT_PrinterSettings As List(Of DFT_PrinterSettingInfo) = GetDFT_PrinterSettings(ModInfo.ModuleID)
            Dim objDFT_PrinterSetting As DFT_PrinterSettingInfo
            For Each objDFT_PrinterSetting In colDFT_PrinterSettings
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_PrinterSetting.Content, objDFT_PrinterSetting.CreatedByUser, objDFT_PrinterSetting.CreatedDate, ModInfo.ModuleID, objDFT_PrinterSetting.ItemId.ToString, objDFT_PrinterSetting.Content, "ItemId=" & objDFT_PrinterSetting.ItemId.ToString)
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

            Dim colDFT_PrinterSettings As List(Of DFT_PrinterSettingInfo) = GetDFT_PrinterSettings(ModuleID)
            If colDFT_PrinterSettings.Count <> 0 Then
                strXML += "<DFT_PrinterSettings>"
                Dim objDFT_PrinterSetting As DFT_PrinterSettingInfo
                For Each objDFT_PrinterSetting In colDFT_PrinterSettings
                    strXML += "<DFT_PrinterSetting>"
                    strXML += "<content>" & XMLEncode(objDFT_PrinterSetting.Content) & "</content>"
                    strXML += "</DFT_PrinterSetting>"
                Next
                strXML += "</DFT_PrinterSettings>"
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

            Dim xmlDFT_PrinterSetting As XmlNode
            Dim xmlDFT_PrinterSettings As XmlNode = GetContent(Content, "DFT_PrinterSettings")
            For Each xmlDFT_PrinterSetting In xmlDFT_PrinterSettings.SelectNodes("DFT_PrinterSetting")
                Dim objDFT_PrinterSetting As New DFT_PrinterSettingInfo
                objDFT_PrinterSetting.ModuleId = ModuleID
                objDFT_PrinterSetting.Content = xmlDFT_PrinterSetting.SelectSingleNode("content").InnerText
                objDFT_PrinterSetting.CreatedByUser = UserId
                AddDFT_PrinterSetting(objDFT_PrinterSetting)
            Next

        End Sub

#End Region

    End Class
End Namespace
