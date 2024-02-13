<%@ Control language="vb" CodeBehind="~/admin/Skins/skin.vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="USER" Src="~/Admin/Skins/User.ascx" %>
<%@ Register TagPrefix="dnn" TagName="NAV" Src="~/Admin/Skins/Nav.ascx" %>
<%@ Register TagPrefix="dnn" TagName="COPYRIGHT" Src="~/Admin/Skins/Copyright.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LINKS" Src="~/Admin/Skins/Links.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="BREADCRUMB" Src="~/Admin/Skins/BreadCrumb.ascx" %>

<div class="wrapper_edi">
  <div class="fullwidth_edi">
    <div class="edi_tr">
      <div class="edi_tc">
        <div class="logo_style">
          <div  class="float_right">
            <div class="nav_positon">
              <div class="edi_nav_tr">
                <div class="edi_nav_tl">
                  <div class="edi_nav_tc">
                    <dnn:NAV runat="server" id="dnnNAV" 
                                                                                ProviderName="DNNMenuNavigationProvider" 
                                                                                IndicateChildren="True"
                                                                                IndicateChildImageRoot="[SKINPATH]images/menu_down1.gif"
                                                                    			IndicateChildImageSub="[SKINPATH]images/arrow1.gif"
                                                                                CSSIndicateChildSub="MainMenu_MenuArrow"
                                                                                CSSIndicateChildRoot="MainMenu_RootMenuArrow"
                                                                                CSSNodeRoot="main_dnnmenu_rootitem" 
                                                                                CSSNodeHoverRoot="main_dnnmenu_rootitem_hover" 
                                                                                CSSNodeSelectedRoot="main_dnnmenu_rootitem_selected" 
                                                                                CSSBreadCrumbRoot="main_dnnmenu_rootitem_selected" 
                                                                                CSSContainerSub="main_dnnmenu_submenu" 
                                                                                CSSNodeHoverSub="main_dnnmenu_itemhover" 
                                                                                CSSNodeSelectedSub="main_dnnmenu_itemselected" 
                                                                                CSSContainerRoot="main_dnnmenu_container" 
                                                                                CSSControl="main_dnnmenu_bar" CSSNode="main_dnnmenu_item" CSSBreak="main_dnnmenu_break"  />
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="Clear_Both"></div>
        </div>
      </div>
    </div>
    <div class="edi_mr">
      <div class="edi_ml">
        <div class="edi_mc0">
          <div class="edi_mc">
            <div id="Bread_style"><img src="<%= SkinPath %>images/icon_bread.gif"  />&nbsp;
              <dnn:BREADCRUMB runat="server" id="dnnBREADCRUMB" RootLevel="0" Separator="&nbsp;&raquo;&nbsp;" CssClass="BREADCRUMB"/>
            </div>
            <div class="edi_user_zone">
              <div id="Login_style">
                <dnn:USER runat="server" id="dnnUSER"  CssClass="USER"/>
                <dnn:LOGIN runat="server" id="dnnLOGIN"  CssClass="Login" />
              </div>
              <!--<div class="Link_login">
                                    	<ul>
                                        	<li class="icon_idcard"><a href="#">เข้าสู่ระบบด้วยเลขที่บัตรประจำตัวผู้ส่งออก-นำเข้าสินค้าทั่วไป</a></li>
                                            <li class="icon_u_center"><a href="#">เข้าสู่ระบบด้วย UserName กลางที่ลงทะเบียนไว้กับกรมฯ</a></li>
                                            <li class="icon_tariff"><a href="#">พิกัดอัตราศุลกากร</a></li>
                                        </ul>
                                    </div>-->
              <div class="hilightpane" id="HilightPane" runat="server"  valign="top" visible="false"> </div>
              <div class="Clear_Both"></div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="edi_mr">
      <div class="edi_ml">
        <div class="edi_mc1">
          <table width="100%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td class="toppane" colspan="3" id="TopPane" runat="server" valign="top" visible="false" ></td>
            </tr>
            <tr>
              <td class="leftpane" id="LeftPane" runat="server" valign="top" visible="false" ></td>
              <td class="contentpane" id="ContentPane" runat="server"  valign="top" visible="false" ></td>
              <td class="rightpane" id="RightPane" runat="server" valign="top" visible="false"  ></td>
            </tr>
            <tr>
              <td class="bottompane" colspan="3" id="BottomPane" runat="server" valign="top"  visible="false"></td>
            </tr>
          </table>
        </div>
      </div>
    </div>
    <div class="edi_br">
      <div class="edi_bl">
        <div class="edi_bc">
          <div class="text_center">
            <dnn:COPYRIGHT runat="server" id="dnnCOPYRIGHT" CssClass="CopyRight" />
          </div>
        </div>
      </div>
    </div>
    <div class="footer_zone">
      <dnn:LINKS runat="server" ID="dnnLINKS" CssClass="links" Level="Root" Separator="&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;" />
    </div>
  </div>
  <div></div>
</div>
</div>
