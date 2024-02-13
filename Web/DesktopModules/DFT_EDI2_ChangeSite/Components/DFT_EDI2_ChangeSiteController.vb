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

Namespace NTI.Modules.DFT_EDI2_ChangeSite

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_EDI2_ChangeSite
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_EDI2_ChangeSiteController
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
        Public Function GetDFT_EDI2_ChangeSites(ByVal ModuleId As Integer) As List(Of DFT_EDI2_ChangeSiteInfo)

            Return CBO.FillCollection(Of DFT_EDI2_ChangeSiteInfo)(DataProvider.Instance().GetDFT_EDI2_ChangeSites(ModuleId))

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
        Public Function GetDFT_EDI2_ChangeSite(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_EDI2_ChangeSiteInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_EDI2_ChangeSite(ModuleId, ItemId), GetType(DFT_EDI2_ChangeSiteInfo)), DFT_EDI2_ChangeSiteInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_EDI2_ChangeSite">The DFT_EDI2_ChangeSiteInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_EDI2_ChangeSite(ByVal objDFT_EDI2_ChangeSite As DFT_EDI2_ChangeSiteInfo)

            If objDFT_EDI2_ChangeSite.Content.Trim <> "" Then
                DataProvider.Instance().AddDFT_EDI2_ChangeSite(objDFT_EDI2_ChangeSite.ModuleId, objDFT_EDI2_ChangeSite.Content, objDFT_EDI2_ChangeSite.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_EDI2_ChangeSite">The DFT_EDI2_ChangeSiteInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_EDI2_ChangeSite(ByVal objDFT_EDI2_ChangeSite As DFT_EDI2_ChangeSiteInfo)

            If objDFT_EDI2_ChangeSite.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_EDI2_ChangeSite(objDFT_EDI2_ChangeSite.ModuleId, objDFT_EDI2_ChangeSite.ItemId, objDFT_EDI2_ChangeSite.Content, objDFT_EDI2_ChangeSite.CreatedByUser)
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
        Public Sub DeleteDFT_EDI2_ChangeSite(ByVal ModuleId As Integer, ByVal ItemId As Integer)

            DataProvider.Instance().DeleteDFT_EDI2_ChangeSite(ModuleId, ItemId)

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

            Dim colDFT_EDI2_ChangeSites As List(Of DFT_EDI2_ChangeSiteInfo) = GetDFT_EDI2_ChangeSites(ModInfo.ModuleID)
            Dim objDFT_EDI2_ChangeSite As DFT_EDI2_ChangeSiteInfo
            For Each objDFT_EDI2_ChangeSite In colDFT_EDI2_ChangeSites
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_EDI2_ChangeSite.Content, objDFT_EDI2_ChangeSite.CreatedByUser, objDFT_EDI2_ChangeSite.CreatedDate, ModInfo.ModuleID, objDFT_EDI2_ChangeSite.ItemId.ToString, objDFT_EDI2_ChangeSite.Content, "ItemId=" & objDFT_EDI2_ChangeSite.ItemId.ToString)
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

            Dim colDFT_EDI2_ChangeSites As List(Of DFT_EDI2_ChangeSiteInfo) = GetDFT_EDI2_ChangeSites(ModuleID)
            If colDFT_EDI2_ChangeSites.Count <> 0 Then
                strXML += "<DFT_EDI2_ChangeSites>"
                Dim objDFT_EDI2_ChangeSite As DFT_EDI2_ChangeSiteInfo
                For Each objDFT_EDI2_ChangeSite In colDFT_EDI2_ChangeSites
                    strXML += "<DFT_EDI2_ChangeSite>"
                    strXML += "<content>" & XMLEncode(objDFT_EDI2_ChangeSite.Content) & "</content>"
                    strXML += "</DFT_EDI2_ChangeSite>"
                Next
                strXML += "</DFT_EDI2_ChangeSites>"
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

            Dim xmlDFT_EDI2_ChangeSite As XmlNode
            Dim xmlDFT_EDI2_ChangeSites As XmlNode = GetContent(Content, "DFT_EDI2_ChangeSites")
            For Each xmlDFT_EDI2_ChangeSite In xmlDFT_EDI2_ChangeSites.SelectNodes("DFT_EDI2_ChangeSite")
                Dim objDFT_EDI2_ChangeSite As New DFT_EDI2_ChangeSiteInfo
                objDFT_EDI2_ChangeSite.ModuleId = ModuleID
                objDFT_EDI2_ChangeSite.Content = xmlDFT_EDI2_ChangeSite.SelectSingleNode("content").InnerText
                objDFT_EDI2_ChangeSite.CreatedByUser = UserId
                AddDFT_EDI2_ChangeSite(objDFT_EDI2_ChangeSite)
            Next

        End Sub

#End Region

    End Class
End Namespace
