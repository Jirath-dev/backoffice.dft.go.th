<%@ Control language="vb" CodeBehind="~/admin/Containers/container.vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Containers.Container" %>
<%@ Register TagPrefix="dnn" TagName="ACTIONS" Src="~/Admin/Containers/SolPartActions.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TITLE" Src="~/Admin/Containers/Title.ascx" %>
<%@ Register TagPrefix="dnn" TagName="ACTIONBUTTON" Src="~/Admin/Containers/ActionButton.ascx" %>

<div class="cn_wraper">
  <div class="ctn_edi_tl">
  	<div class="ctn_edi_tr">
    	<div class="ctn_edi_tc"><div class="TitleStyle"> <dnn:ACTIONS runat="server" id="dnnACTIONS" ProviderName="DNNMenuNavigationProvider" ExpandDepth="1" PopulateNodesFromClient="True" />
            &nbsp;<dnn:TITLE runat="server" id="dnnTITLE" CssClass="Head" /></div></div>
    </div>
  </div> 
  <div class="ctn_edi_ml">
  	<div class="ctn_edi_mr">
    		<div class="ctn_edi_mc">   
			<div runat="server" id="ContentPane"  class="Content_Style"></div> 
            <div class="Clear_Both"></div>
			<div >
	<dnn:ACTIONBUTTON runat="server" id="dnnACTIONBUTTON1"  CommandName="AddContent.Action" DisplayIcon="True" DisplayLink="True" />
            	<dnn:ACTIONBUTTON runat="server" id="dnnACTIONBUTTON2"  CommandName="SyndicateModule.Action" DisplayIcon="True" DisplayLink="false" />
            	<dnn:ACTIONBUTTON runat="server" id="dnnACTIONBUTTON3"  CommandName="PrintModule.Action" DisplayIcon="false" DisplayLink="false" />
            	<dnn:ACTIONBUTTON runat="server" id="dnnACTIONBUTTON4"  CommandName="ModuleSettings.Action" DisplayIcon="True" DisplayLink="false" />
			</div>    
		</div>
    </div>
  </div> 
 <div class="ctn_edi_bl">
  	<div class="ctn_edi_br">
    	<div class="ctn_edi_bc"></div>
    </div>
  </div> 
</div>

	

 


