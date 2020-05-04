USE [MATCHES]
GO
/****** Object:  StoredProcedure [dbo].[GetPositions]    Script Date: 21/04/2020 20:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		DAVID SANTAFE
-- Create date: 2019-07-10
-- Description:	RETORNA LA TABLA DE POSICIONES
-- =============================================
ALTER PROCEDURE [dbo].[GetPositions]
AS

--Posición | ID de Equipo | Nombre de Equipo | Puntos | Partidos Jugados | Goles a Favor | Goles en Contra

SELECT 
ROW_NUMBER() OVER(ORDER BY (SELECT 1) ASC) AS POS,
T.ID		AS ID,
T.NAME		AS NAME,
SUM(T.PTOS)	AS PTOS,
SUM(T.PJ)	AS PJ,
SUM(T.GF)	AS GF,
SUM(T.GC)	AS GC,
SUM(T.PG)	AS PG,
SUM(T.PP)	AS PP,
SUM(T.GF) - SUM(T.GC) AS DG
FROM
(
 SELECT 
	 COUNT(*)					AS PJ,
	 SUM(match.[HostGoals])		AS GF,
	 SUM(match.[GuestGoals])	AS GC,
	 match.[TeamHostId]			AS ID,
	 team.Name					AS NAME,
	 CASE	WHEN match.[HostGoals] > match.[GuestGoals] THEN 3
			WHEN match.[HostGoals] < match.[GuestGoals] THEN 0
			ELSE 1 END AS PTOS,
	 CASE	WHEN match.[HostGoals] > match.[GuestGoals] THEN 1 ELSE 0 END PG,
	 CASE	WHEN match.[HostGoals] < match.[GuestGoals] THEN 1 ELSE 0 END PP
 FROM [dbo].[Matches] match
 JOIN [dbo].[Teams] team ON match.[TeamHostId] = team.[Id]
 GROUP BY 
 match.[TeamHostId],
 team.Name,
 match.[HostGoals],
 match.[GuestGoals]

 UNION ALL

 SELECT 
	 COUNT(*)					AS PJ, 
	 SUM(match.[GuestGoals])	AS GF,
	 SUM(match.[HostGoals])		AS GC,
	 match.[TeamGuestId]		AS ID,
	 team.Name					AS NAME,
	 CASE	WHEN match.[HostGoals] < match.[GuestGoals] THEN 3
			WHEN match.[HostGoals] > match.[GuestGoals] THEN 0
			ELSE 1 END AS PTOS,
	 CASE	WHEN match.[HostGoals] < match.[GuestGoals] THEN 1 ELSE 0 END PG,
	 CASE	WHEN match.[HostGoals] > match.[GuestGoals] THEN 1 ELSE 0 END PP
 FROM [dbo].[Matches] match
 JOIN [dbo].[Teams] team ON match.[TeamGuestId] = team.[Id]
 GROUP BY 
 match.[TeamGuestId],
 team.Name,
  match.[HostGoals],
  match.[GuestGoals]

 UNION ALL

 SELECT 
 0								AS PJ,
 0								AS GF,
 0								AS GC, 
 team.[Id]						AS ID,
 team.name						AS NAME,
 0								AS PTOS,
 0 PG,
 0 PP
 FROM [dbo].[Teams] team WHERE
 team.Id	 NOT IN (SELECT [TeamHostId] AS id_team FROM [dbo].[Matches]
				      UNION ALL
					  SELECT [TeamGuestId] AS id_team FROM [dbo].[Matches])

) T
GROUP BY 
T.ID,
T.NAME
ORDER BY PTOS DESC, ID