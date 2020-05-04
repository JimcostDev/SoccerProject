
CREATE PROCEDURE [dbo].[ObtenerPosiciones]
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
	 SUM(match.par_golesLocal)		AS GF,
	 SUM(match.par_golesVisitante)	AS GC,
	 match.equ_codigo_local			AS ID,
	 Equipo.equ_nombre					AS NAME,
	 CASE	WHEN match.par_golesLocal > match.par_golesVisitante THEN 3
			WHEN match.par_golesLocal < match.par_golesVisitante THEN 0
			ELSE 1 END AS PTOS,
	 CASE	WHEN match.par_golesLocal > match.par_golesVisitante THEN 1 ELSE 0 END PG,
	 CASE	WHEN match.par_golesLocal < match.par_golesVisitante THEN 1 ELSE 0 END PP
 FROM [dbo].Partido match
 JOIN [dbo].Equipo Equipo ON match.equ_codigo_local = Equipo.equ_codigo
 GROUP BY 
 match.equ_codigo_local,
 Equipo.equ_nombre,
 match.par_golesLocal,
 match.par_golesVisitante

 UNION ALL

 SELECT 
	 COUNT(*)					AS PJ, 
	 SUM(match.par_golesVisitante)	AS GF,
	 SUM(match.par_golesLocal)		AS GC,
	 match.equ_codigo_visitante		AS ID,
	 Equipo.equ_nombre					AS NAME,
	 CASE	WHEN match.par_golesLocal < match.par_golesVisitante THEN 3
			WHEN match.par_golesLocal > match.par_golesVisitante THEN 0
			ELSE 1 END AS PTOS,
	 CASE	WHEN match.par_golesLocal < match.par_golesVisitante THEN 1 ELSE 0 END PG,
	 CASE	WHEN match.par_golesLocal > match.par_golesVisitante THEN 1 ELSE 0 END PP
 FROM [dbo].Partido match
 JOIN [dbo].Equipo Equipo ON match.equ_codigo_visitante = Equipo.equ_codigo
 GROUP BY 
 match.equ_codigo_visitante,
 Equipo.equ_nombre,
  match.par_golesLocal,
  match.par_golesVisitante

 UNION ALL

 SELECT 
 0								AS PJ,
 0								AS GF,
 0								AS GC, 
 Equipo.equ_codigo						AS ID,
 Equipo.equ_nombre						AS NAME,
 0								AS PTOS,
 0 PG,
 0 PP
 FROM [dbo].Equipo Equipo WHERE
 Equipo.equ_codigo	 NOT IN (SELECT equ_codigo_local AS id_Equipo FROM [dbo].Partido
				      UNION ALL
					  SELECT equ_codigo_visitante AS id_Equipo FROM [dbo].Partido)

) T
GROUP BY 
T.ID,
T.NAME
ORDER BY PTOS DESC, ID