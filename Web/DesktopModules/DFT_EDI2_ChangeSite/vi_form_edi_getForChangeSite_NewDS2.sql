set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

alter PROCEDURE [dbo].[vi_form_edi_getForChangeSite_NewDS2] 
@INVH_RUN_AUTO	varchar(15)

AS
	
select a.invh_run_auto,a.form_type,a.SentBy,a.edi_status_id,a.company_taxno,a.invoice_no1,a.sent_date,a.company_name,a.CheckDoc_By,a.PrintFormDate,b.DESCRIPTION,c.FORM_NAME,(Select site_name From site Where site_id = a.site_id) As site_name, SentType = 
		CASE SentBy
			 WHEN 1 THEN '.FILE'
			 WHEN 2 THEN '.XML'
			 ELSE ''
		  END
from FORM_HEADER_EDI a 
left outer join EDI_STATUS b on a.EDI_STATUS_ID=b.EDI_STATUS_ID 
left outer join FORM_TYPE c on a.FORM_TYPE=c.FORM_TYPE
left outer join rfcard f on a.card_id=f.card_id 
left outer join COMPANY d on a.COMPANY_TAXNO=d.COMPANY_TAXNO and d.company_branchNO=f.company_branchNO
where 
invh_run_auto=@INVH_RUN_AUTO
--and a.EDI_STATUS_ID in('Q','A','D')
and (a.SentBy=1 or a.SentBy=2)

