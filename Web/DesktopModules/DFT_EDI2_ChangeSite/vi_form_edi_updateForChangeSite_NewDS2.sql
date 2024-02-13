set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


ALTER PROCEDURE [dbo].[vi_form_edi_updateForChangeSite_NewDS2] 
@INVH_RUN_AUTO	varchar(15),
@SITE_ID	varchar(20)

AS

	
update FORM_HEADER_EDI
set site_id=@SITE_ID,CheckDoc_By=NULL
where invh_run_auto=@INVH_RUN_AUTO
and EDI_STATUS_ID='Q'
and (SentBy=1 or SentBy=2)

return @@ROWCOUNT

