USE [JKCRM]
GO

/****** Object:  StoredProcedure [dbo].[Usp_GetFunctionsForUserid16]    Script Date: 10/28/2014 16:35:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Usp_GetFunctionsForUserid16]
	@uid int --”√ªßid
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select p.rID,p.mID,p.fID,f.fName,f.fPicname,f.fFunction,m.mArea,m.mController,m.mUrl from sysPermissList p 
	inner join sysUserInfo_Role ur on (p.rID=ur.rID and ur.uID=@uid)
	inner join sysFunction f on (p.fID=f.fID)
	inner join sysMenus m on (p.mID=m.mID)
END

GO


