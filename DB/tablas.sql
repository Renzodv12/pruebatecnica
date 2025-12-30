CREATE TABLE dbo.Contrato
(
    ContratoId   INT IDENTITY(1,1) NOT NULL,
    Cliente      NVARCHAR(100) NOT NULL,
    TotalCuotas  INT NOT NULL,

    CONSTRAINT PK_Contrato PRIMARY KEY (ContratoId)
);
GO


CREATE TABLE dbo.Cuota
(
    CuotaId     INT IDENTITY(1,1) NOT NULL,
    ContratoId  INT NOT NULL,
    Monto       DECIMAL(18,2) NOT NULL,
    Estado      NVARCHAR(20) NOT NULL, -- PAGADA / PENDIENTE

    CONSTRAINT PK_Cuota PRIMARY KEY (CuotaId),
    CONSTRAINT FK_Cuota_Contrato 
        FOREIGN KEY (ContratoId) 
        REFERENCES dbo.Contrato (ContratoId)
        ON DELETE CASCADE,

    CONSTRAINT CK_Cuota_Estado 
        CHECK (Estado IN ('PAGADA', 'PENDIENTE'))
);
GO
