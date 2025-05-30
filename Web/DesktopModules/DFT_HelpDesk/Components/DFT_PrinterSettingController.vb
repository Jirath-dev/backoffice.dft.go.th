'
' DotNetNukeŽ - http://www.dotnetnuke.com
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

Namespace Modules.DFT_HelpDesk

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_HelpDesk
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_HelpDeskController
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
        Public Function GetDFT_HelpDesks(ByVal ModuleId As Integer) As List(Of DFT_HelpDeskInfo)

            Return CBO.FillCollection(Of DFT_HelpDeskInfo)(DataProvider.Instance().GetDFT_HelpDesks(ModuleId))

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
        Public Function GetDFT_HelpDesk(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_HelpDeskInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_HelpDesk(ModuleId, ItemId), GetType(DFT_HelpDeskInfo)), DFT_HelpDeskInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_HelpDesk">The DFT_HelpDeskInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_HelpDesk(ByVal objDFT_HelpDesk As DFT_HelpDeskInfo)

            If objDFT_HelpDesk.Content.Trim <> "" Then
                DataProvider.Instance().AddDFT_HelpDesk(objDFT_HelpDesk.ModuleId, objDFT_HelpDesk.Content, objDFT_HelpDesk.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_HelpDesk">The DFT_HelpDeskInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_HelpDesk(ByVal objDFT_HelpDesk As DFT_HelpDeskInfo)

            If objDFT_HelpDesk.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_HelpDesk(objDFT_HelpDesk.ModuleId, objDFT_HelpDesk.ItemId, objDFT_HelpDesk.Content, objDFT_HelpDesk.CreatedByUser)
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
        Public Sub DeleteDFT_HelpDesk(ByVal ModuleId As Integer, ByVal ItemId As Integer)

            DataProvider.Instance().DeleteDFT_HelpDesk(ModuleId, ItemId)

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

            Dim colDFT_HelpDesks As List(Of DFT_HelpDeskInfo) = GetDFT_HelpDesks(ModInfo.ModuleID)
            Dim objDFT_HelpDesk As DFT_HelpDeskInfo
            For Each objDFT_HelpDesk In colDFT_HelpDesks
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_HelpDesk.Content, objDFT_HelpDesk.CreatedByUser, objDFT_HelpDesk.CreatedDate, ModInfo.ModuleID, objDFT_HelpDesk.ItemId.ToString, objDFT_HelpDesk.Content, "ItemId=" & objDFT_HelpDesk.ItemId.ToString)
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

            Dim colDFT_HelpDesks As List(Of DFT_HelpDeskInfo) = GetDFT_HelpDesks(ModuleID)
            If colDFT_HelpDesks.Count <> 0 Then
                strXML += "<DFT_HelpDesks>"
                Dim objDFT_HelpDesk As DFT_HelpDeskInfo
                For Each objDFT_HelpDesk In colDFT_HelpDesks
                    strXML += "<DFT_HelpDesk>"
                    strXML += "<content>" & XMLEncode(objDFT_HelpDesk.Content) & "</content>"
                    strXML += "</DFT_HelpDesk>"
                Next
                strXML += "</DFT_HelpDesks>"
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

            Dim xmlDFT_HelpDesk As XmlNode
            Dim xmlDFT_HelpDesks As XmlNode = GetContent(Content, "DFT_HelpDesks")
            For Each xmlDFT_HelpDesk In xmlDFT_HelpDesks.SelectNodes("DFT_HelpDesk")
                Dim objDFT_HelpDesk As New DFT_HelpDeskInfo
                objDFT_HelpDesk.ModuleId = ModuleID
                objDFT_HelpDesk.Content = xmlDFT_HelpDesk.SelectSingleNode("content").InnerText
                objDFT_HelpDesk.CreatedByUser = UserId
                AddDFT_HelpDesk(objDFT_HelpDesk)
            Next

        End Sub

#End Region

    End Class
End Namespace
