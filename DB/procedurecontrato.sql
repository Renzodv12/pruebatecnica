CREATE OR ALTER PROCEDURE dbo.sp_CalcularPorcentajePagoContrato
    @ContratoId INT
AS
BEGIN
    SELECT
        c.ContratoId,
        c.TotalCuotas,
        COUNT(CASE WHEN q.Estado = 'PAGADA' THEN 1 END) AS CuotasPagadas,
        CAST(
            CASE 
                WHEN c.TotalCuotas = 0 THEN 0
                ELSE (COUNT(CASE WHEN q.Estado = 'PAGADA' THEN 1 END) * 100.0) / c.TotalCuotas
            END
        AS DECIMAL(10,2)) AS PorcentajePagado
    FROM Contrato c
    LEFT JOIN Cuota q ON q.ContratoId = c.ContratoId
    WHERE c.ContratoId = @ContratoId
    GROUP BY c.ContratoId, c.TotalCuotas;
END
GO
