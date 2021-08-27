CREATE TABLE [dbo].[Cuenta]
(
    [CuentaId] INT NOT NULL PRIMARY KEY IDENTITY,
    [Nombre] VARCHAR(50) NOT NULL,
    [Numero] VARCHAR(50) NOT NULL,
    [ClienteId] INT FOREIGN KEY REFERENCES Cliente(ClienteId),
)
