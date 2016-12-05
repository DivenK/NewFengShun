USE [JKCRM]
GO

/****** Object:  StoredProcedure [dbo].[USP_GetMenusForUserid16]    Script Date: 10/27/2014 15:17:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<ivan>
-- TEL:			<13828459782>
-- Create date: <2014-10-27>
-- Description:	<负责根据用户id获取其权限菜单数据>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetMenusForUserid16]
	@uid int --用户id
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   select distinct m.* from sysMenus m 
inner join sysPermissList p on (m.mID=p.mID)
inner join sysUserInfo_Role ur on(p.rID=ur.rID and ur.uID=@uid )
END

GO


