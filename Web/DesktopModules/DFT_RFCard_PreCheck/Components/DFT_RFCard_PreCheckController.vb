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

Namespace NTi.Modules.DFT_RFCard_PreCheck

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for DFT_RFCard_PreCheck
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class DFT_RFCard_PreCheckController
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
        Public Function GetDFT_RFCard_PreChecks(ByVal ModuleId As Integer) As List(Of DFT_RFCard_PreCheckInfo)

            Return CBO.FillCollection(Of DFT_RFCard_PreCheckInfo)(DataProvider.Instance().GetDFT_RFCard_PreChecks(ModuleId))

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
        Public Function GetDFT_RFCard_PreCheck(ByVal ModuleId As Integer, ByVal ItemId As Integer) As DFT_RFCard_PreCheckInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetDFT_RFCard_PreCheck(ModuleId,ItemId), GetType(DFT_RFCard_PreCheckInfo)), DFT_RFCard_PreCheckInfo)

        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' adds an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_RFCard_PreCheck">The DFT_RFCard_PreCheckInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub AddDFT_RFCard_PreCheck(ByVal objDFT_RFCard_PreCheck As DFT_RFCard_PreCheckInfo)

            If objDFT_RFCard_PreCheck.Content.Trim <> "" Then
		DataProvider.Instance().AddDFT_RFCard_PreCheck(objDFT_RFCard_PreCheck.ModuleId, objDFT_RFCard_PreCheck.Content, objDFT_RFCard_PreCheck.CreatedByUser)
            End If

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' saves an object to the database
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="objDFT_RFCard_PreCheck">The DFT_RFCard_PreCheckInfo object</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub UpdateDFT_RFCard_PreCheck(ByVal objDFT_RFCard_PreCheck As DFT_RFCard_PreCheckInfo)

            If objDFT_RFCard_PreCheck.Content.Trim <> "" Then
                DataProvider.Instance().UpdateDFT_RFCard_PreCheck(objDFT_RFCard_PreCheck.ModuleId, objDFT_RFCard_PreCheck.ItemId, objDFT_RFCard_PreCheck.Content, objDFT_RFCard_PreCheck.CreatedByUser)
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
        Public Sub DeleteDFT_RFCard_PreCheck(ByVal ModuleId As Integer, ByVal ItemId As Integer) 

            DataProvider.Instance().DeleteDFT_RFCard_PreCheck(ModuleId, ItemId)

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

            Dim colDFT_RFCard_PreChecks As List(Of DFT_RFCard_PreCheckInfo) = GetDFT_RFCard_PreChecks(ModInfo.ModuleID)
            Dim objDFT_RFCard_PreCheck As DFT_RFCard_PreCheckInfo
            For Each objDFT_RFCard_PreCheck In colDFT_RFCard_PreChecks
                Dim SearchItem As SearchItemInfo = New SearchItemInfo(ModInfo.ModuleTitle, objDFT_RFCard_PreCheck.Content, objDFT_RFCard_PreCheck.CreatedByUser, objDFT_RFCard_PreCheck.CreatedDate, ModInfo.ModuleID, objDFT_RFCard_PreCheck.ItemId.ToString, objDFT_RFCard_PreCheck.Content, "ItemId=" & objDFT_RFCard_PreCheck.ItemId.ToString)
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

            Dim colDFT_RFCard_PreChecks As List(Of DFT_RFCard_PreCheckInfo) = GetDFT_RFCard_PreChecks(ModuleID)
            If colDFT_RFCard_PreChecks.Count <> 0 Then
                strXML += "<DFT_RFCard_PreChecks>"
                Dim objDFT_RFCard_PreCheck As DFT_RFCard_PreCheckInfo
                For Each objDFT_RFCard_PreCheck In colDFT_RFCard_PreChecks
                    strXML += "<DFT_RFCard_PreCheck>"
                    strXML += "<content>" & XMLEncode(objDFT_RFCard_PreCheck.Content) & "</content>"
                    strXML += "</DFT_RFCard_PreCheck>"
                Next
                strXML += "</DFT_RFCard_PreChecks>"
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

            Dim xmlDFT_RFCard_PreCheck As XmlNode
            Dim xmlDFT_RFCard_PreChecks As XmlNode = GetContent(Content, "DFT_RFCard_PreChecks")
            For Each xmlDFT_RFCard_PreCheck In xmlDFT_RFCard_PreChecks.SelectNodes("DFT_RFCard_PreCheck")
                Dim objDFT_RFCard_PreCheck As New DFT_RFCard_PreCheckInfo
                objDFT_RFCard_PreCheck.ModuleId = ModuleID
                objDFT_RFCard_PreCheck.Content = xmlDFT_RFCard_PreCheck.SelectSingleNode("content").InnerText
                objDFT_RFCard_PreCheck.CreatedByUser = UserId
                AddDFT_RFCard_PreCheck(objDFT_RFCard_PreCheck)
            Next

        End Sub

#End Region

    End Class
End Namespace
